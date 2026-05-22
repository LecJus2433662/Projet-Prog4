using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projet4_prog.Data;
using Projet4_prog.DTO.Produit;
using Projet4_prog.Models;

namespace Projet4_prog.Services
{
    public class ProduitService : IProduitService
    {
        private readonly Projet4_progContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProduitService> _logger;

        public ProduitService(Projet4_progContext context, IMapper mapper, ILogger<ProduitService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProduitLectureDto>> ObtenirTousAsync()
        {
            var produits = await _context.Produits.ToListAsync();
            return _mapper.Map<IEnumerable<ProduitLectureDto>>(produits);
        }

        public async Task<ProduitLectureDto?> ObtenirParIdAsync(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
            {
                _logger.LogWarning("Produit {Id} introuvable.", id);
                return null;
            }
            return _mapper.Map<ProduitLectureDto>(produit);
        }

        public async Task<ProduitLectureDto> CreerAsync(ProduitEcritureDto dto)
        {
            var produit = _mapper.Map<Produit>(dto);
            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Produit {Nom} créé avec l'id {Id}.", produit.Nom, produit.Id);
            return _mapper.Map<ProduitLectureDto>(produit);
        }

        public async Task<ProduitLectureDto?> ModifierAsync(int id, ProduitEcritureDto dto)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
            {
                _logger.LogWarning("Produit {Id} introuvable pour modification.", id);
                return null;
            }
            _mapper.Map(dto, produit);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Produit {Id} modifié.", id);
            return _mapper.Map<ProduitLectureDto>(produit);
        }

        public async Task<bool> SupprimerAsync(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
            {
                _logger.LogWarning("Produit {Id} introuvable pour suppression.", id);
                return false;
            }
            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Produit {Id} supprimé.", id);
            return true;
        }

        public async Task DiminuerStockAsync(int produitId, int quantite)
        {
            var produit = await _context.Produits.FindAsync(produitId)
                ?? throw new KeyNotFoundException($"Produit {produitId} introuvable.");

            if (produit.NbProduitRestant < quantite)
                throw new InvalidOperationException(
                    $"Stock insuffisant pour le produit {produit.Nom}.");

            produit.NbProduitRestant -= quantite;
            await _context.SaveChangesAsync();
        }
    }
}