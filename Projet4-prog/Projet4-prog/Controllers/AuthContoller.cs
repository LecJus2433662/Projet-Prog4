using Microsoft.AspNetCore.Mvc;
using Projet4_prog.DTO.Auth;
using Projet4_prog.Services;

namespace Projet4_prog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("inscription")]
        public async Task<IActionResult> Inscription(InscriptionDto dto)
        {
            var succes = await _authService.InscriptionAsync(dto);
            if (!succes)
                return BadRequest("Échec de l'inscription. Vérifiez les informations fournies.");

            return Ok("Inscription réussie.");
        }

        [HttpPost("connexion")]
        public async Task<IActionResult> Connexion(ConnexionDto dto)
        {
            var token = await _authService.ConnexionAsync(dto);
            if (token == null)
                return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");

            return Ok(token);
        }
    }
}