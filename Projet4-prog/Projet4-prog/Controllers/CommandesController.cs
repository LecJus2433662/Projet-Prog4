using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet4_prog.DTO.Commande;
using Projet4_prog.Services;
using System.Security.Claims;

namespace Projet4_prog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommandesController : ControllerBase
    {
        private readonly ICommandeService _commandeService;
        private readonly ILogger<CommandesController> _logger;

        public CommandesController(ICommandeService commandeService, ILogger<CommandesController> logger)
        {
            _commandeService = commandeService;
            _logger = logger;
        }

        // GET api/commandes — Admin voit tout, utilisateur voit les siennes
        [HttpGet]
        public async Task<IActionResult> ObtenirCommandes()
        {
            var utilisateurId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var estAdmin = User.IsInRole("Admin");

            var commandes = estAdmin
                ? await _commandeService.ObtenirToutesAsync()
                : await _commandeService.ObtenirParUtilisateurAsync(utilisateurId);

            return Ok(commandes);
        }

        // GET api/commandes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenirParId(int id)
        {
            var utilisateurId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var estAdmin = User.IsInRole("Admin");

            var commande = estAdmin
                ? await _commandeService.ObtenirParIdAsync(id, string.Empty)
                : await _commandeService.ObtenirParIdAsync(id, utilisateurId);

            if (commande == null)
                return NotFound($"Commande {id} introuvable.");

            return Ok(commande);
        }

        // POST api/commandes
        [HttpPost]
        public async Task<IActionResult> Creer(CommandeCreationDto dto)
        {
            var utilisateurId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var commande = await _commandeService.CreerAsync(dto, utilisateurId);
                return CreatedAtAction(nameof(ObtenirParId), new { id = commande.Id }, commande);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/commandes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Supprimer(int id)
        {
            var utilisateurId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var succes = await _commandeService.SupprimerAsync(id, utilisateurId);

            if (!succes)
                return NotFound($"Commande {id} introuvable.");

            return NoContent();
        }
    }
}