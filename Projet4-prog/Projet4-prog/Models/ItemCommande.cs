using System.ComponentModel.DataAnnotations;


namespace Projet4_prog.Models
{
    public class ItemCommande
    {
        public int Id { get; set; }

        [Required]
        public int CommandeId { get; set; }

        public Commande? Commande { get; set; }

        [Required]
        public int ProduitId { get; set; }

        public Produit? Produit { get; set; }

        [Required]
        public int Quantite { get; set; }

        [Required]
        public double PrixUnitaire { get; set; }
    }
}