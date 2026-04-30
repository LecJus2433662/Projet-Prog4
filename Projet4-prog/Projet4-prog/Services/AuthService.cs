using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Projet4_prog.DTO.Auth;
using Projet4_prog.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projet4_prog.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UtilisateurApplication> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<UtilisateurApplication> userManager, IConfiguration config, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
        }

        public async Task<bool> InscriptionAsync(InscriptionDto dto)
        {
            var utilisateur = new UtilisateurApplication
            {
                UserName = dto.NomUtilisateur,
                Email = dto.Courriel
            };

            var resultat = await _userManager.CreateAsync(utilisateur, dto.MotDePasse);
            if (!resultat.Succeeded)
            {
                _logger.LogWarning("Échec d'inscription pour {NomUtilisateur}.", dto.NomUtilisateur);
                return false;
            }

            await _userManager.AddToRoleAsync(utilisateur, "Utilisateur");
            _logger.LogInformation("Utilisateur {NomUtilisateur} inscrit.", dto.NomUtilisateur);
            return true;
        }

        public async Task<TokenReponseDto?> ConnexionAsync(ConnexionDto dto)
        {
            var utilisateur = await _userManager.FindByNameAsync(dto.NomUtilisateur);
            if (utilisateur == null || !await _userManager.CheckPasswordAsync(utilisateur, dto.MotDePasse))
            {
                _logger.LogWarning("Tentative de connexion échouée pour {NomUtilisateur}.", dto.NomUtilisateur);
                return null;
            }

            var roles = await _userManager.GetRolesAsync(utilisateur);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, utilisateur.Id),
                new Claim(ClaimTypes.Name, utilisateur.UserName ?? string.Empty)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var cle = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Cle"]!));
            var credentials = new SigningCredentials(cle, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Emetteur"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            _logger.LogInformation("Utilisateur {NomUtilisateur} connecté.", dto.NomUtilisateur);
            return new TokenReponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}