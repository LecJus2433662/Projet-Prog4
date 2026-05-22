using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projet4_prog.Data;
using Projet4_prog.DTO.Commande;
using Projet4_prog.Models;

namespace Projet4_prog.Services
{
    public class CommandeService : ICommandeService
    {
        private readonly Projet4_progContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CommandeService> _logger;

        public CommandeService(Projet4_progContext context, IMapper mapper, ILogger<CommandeService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CommandeLectureDto>> ObtenirToutesAsync()
        {
            var commandes = await _context.Commandes
                .Include(c => c.ItemsCommande)
                    .ThenInclude(i => i.Produit)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CommandeLectureDto>>(commandes);
        }

        public async Task<IEnumerable<CommandeLectureDto>> ObtenirParUtilisateurAsync(string utilisateurId)
        {
            var commandes = await _context.Commandes
                .Include(c => c.ItemsCommande)
                    .ThenInclude(i => i.Produit)
                .Where(c => c.UtilisateurId == utilisateurId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CommandeLectureDto>>(commandes);
        }

        public async Task<CommandeLectureDto?> ObtenirParIdAsync(int id, string utilisateurId)
        {
            var commande = await _context.Commandes
                .Include(c => c.ItemsCommande)
                    .ThenInclude(i => i.Produit)
                .FirstOrDefaultAsync(c => c.Id == id && c.UtilisateurId == utilisateurId);

            if (commande == null)
            {
                _logger.LogWarning("Commande {Id} introuvable pour l'utilisateur {UtilisateurId}.", id, utilisateurId);
                return null;
            }
            return _mapper.Map<CommandeLectureDto>(commande);
        }

        public async Task<CommandeLectureDto> CreerAsync(CommandeCreationDto dto, string utilisateurId)
        {
            var commande = new Commande
            {
                UtilisateurId = utilisateurId,
                DateCreation = DateTime.UtcNow,
                Statut = StatutCommande.EnAttente
            };

            foreach (var itemDto in dto.ItemsCommande)
            {
                var produit = await _context.Produits.FindAsync(itemDto.ProduitId)
                    ?? throw new KeyNotFoundException($"Produit {itemDto.ProduitId} introuvable.");

                if (produit.NbProduitRestant < itemDto.Quantite)
                    throw new InvalidOperationException($"Stock insuffisant pour le produit {produit.Nom}.");

                produit.NbProduitRestant -= itemDto.Quantite;

                commande.ItemsCommande.Add(new ItemCommande
                {
                    ProduitId = itemDto.ProduitId,
                    Quantite = itemDto.Quantite,
                    PrixUnitaire = (produit.Prix ?? 0)
                });
            }

            _context.Commandes.Add(commande);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Commande {Id} créée pour l'utilisateur {UtilisateurId}.", commande.Id, utilisateurId);

            return await ObtenirParIdAsync(commande.Id, utilisateurId)
                ?? throw new Exception("Erreur lors de la récupération de la commande créée.");
        }

        public async Task<bool> SupprimerAsync(int id, string utilisateurId)
        {
            var commande = await _context.Commandes
                .FirstOrDefaultAsync(c => c.Id == id && c.UtilisateurId == utilisateurId);

            if (commande == null)
            {
                _logger.LogWarning("Commande {Id} introuvable pour suppression.", id);
                return false;
            }

            _context.Commandes.Remove(commande);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Commande {Id} supprimée.", id);
            return true;
        }

        public async Task MettreAJourStatutAsync(int commandeId, StatutCommande statut)
        {
            var commande = await _context.Commandes.FindAsync(commandeId)
                ?? throw new KeyNotFoundException($"Commande {commandeId} introuvable.");

            commande.Statut = statut;
            await _context.SaveChangesAsync();
        }
    }
}