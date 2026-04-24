using System.ComponentModel.DataAnnotations;

namespace Projet4_prog.DTO.ItemCommande
{
    public class ItemCommandeCreationDto
    {
        [Required]
        public int ProduitId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être d'au moins 1.")]
        public int Quantite { get; set; }
    }
}
