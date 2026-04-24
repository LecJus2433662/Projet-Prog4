using System.ComponentModel.DataAnnotations;
using Projet4_prog.Models;

namespace Projet4_prog.Models
{
    public class Produit
    {
        public int Id { get; set; }

        [Required]
        public required string Nom { get; set; }

        [Required]
        public double? Prix { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public int NbProduitRestant { get; set; }

        public string image { get; set; } = string.Empty;

        public ICollection<ItemCommande> ItemsCommande { get; set; } = new List<ItemCommande>();
    }
}