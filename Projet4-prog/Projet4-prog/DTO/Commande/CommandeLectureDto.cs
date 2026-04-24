using Projet4_prog.DTO.ItemCommande;

namespace Projet4_prog.DTO.Commande
{
    public class CommandeLectureDto
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public string Statut { get; set; } = string.Empty;
        public string UtilisateurId { get; set; } = string.Empty;
        public List<ItemCommandeLectureDto> ItemsCommande { get; set; } = new();
    }
}
