using System.ComponentModel.DataAnnotations;

namespace Projet4_prog.Models
{
    public enum StatutCommande
    {
        EnAttente,
        Confirmee,
        Expediee,
        Livree,
        Annulee
    }

    public class Commande
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        public StatutCommande Statut { get; set; } = StatutCommande.EnAttente;

        [Required]
        public string UtilisateurId { get; set; } = string.Empty;

        public UtilisateurApplication? Utilisateur { get; set; }

        public ICollection<ItemCommande> ItemsCommande { get; set; } = new List<ItemCommande>();
    }
}