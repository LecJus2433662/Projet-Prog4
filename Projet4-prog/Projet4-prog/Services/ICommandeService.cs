using Projet4_prog.DTO.Commande;
using Projet4_prog.Models;

namespace Projet4_prog.Services
{
    public interface ICommandeService
    {
        Task<IEnumerable<CommandeLectureDto>> ObtenirToutesAsync();
        Task<IEnumerable<CommandeLectureDto>> ObtenirParUtilisateurAsync(string utilisateurId);
        Task<CommandeLectureDto?> ObtenirParIdAsync(int id, string utilisateurId);
        Task<CommandeLectureDto> CreerAsync(CommandeCreationDto dto, string utilisateurId);
        Task<bool> SupprimerAsync(int id, string utilisateurId);
        Task MettreAJourStatutAsync(int commandeId, StatutCommande statut);
    }
}