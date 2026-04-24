using System.ComponentModel.DataAnnotations;

namespace Projet4_prog.DTO.Produit
{
    public class ProduitEcritureDto
    {
        [Required]
        public string Nom { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        public double? Prix { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "La quantité ne peut pas être négative.")]
        public int NbProduitRestant { get; set; }

        public string Image { get; set; } = string.Empty;
    }
}