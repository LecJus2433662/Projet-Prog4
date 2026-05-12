using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet4_prog.DTO.Produit;
using Projet4_prog.Services;

namespace Projet4_prog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly IProduitService _produitService;
        private readonly ILogger<ProduitsController> _logger;

        public ProduitsController(IProduitService produitService, ILogger<ProduitsController> logger)
        {
            _produitService = produitService;
            _logger = logger;
        }

        // GET api/produits — public
        [HttpGet]
        public async Task<IActionResult> ObtenirTous()
        {
            var produits = await _produitService.ObtenirTousAsync();
            return Ok(produits);
        }

        // GET api/produits/5 — public
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenirParId(int id)
        {
            var produit = await _produitService.ObtenirParIdAsync(id);
            if (produit == null)
                return NotFound($"Produit {id} introuvable.");

            return Ok(produit);
        }

        // POST api/produits — admin seulement
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Creer(ProduitEcritureDto dto)
        {
            var produit = await _produitService.CreerAsync(dto);
            return CreatedAtAction(nameof(ObtenirParId), new { id = produit.Id }, produit);
        }

        // PUT api/produits/5 — admin seulement
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Modifier(int id, ProduitEcritureDto dto)
        {
            var produit = await _produitService.ModifierAsync(id, dto);
            if (produit == null)
                return NotFound($"Produit {id} introuvable.");

            return Ok(produit);
        }

        // DELETE api/produits/5 — admin seulement
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Supprimer(int id)
        {
            var succes = await _produitService.SupprimerAsync(id);
            if (!succes)
                return NotFound($"Produit {id} introuvable.");

            return NoContent();
        }
    }
}