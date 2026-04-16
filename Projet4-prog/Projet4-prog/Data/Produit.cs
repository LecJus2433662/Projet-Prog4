namespace Projet4_prog.Data
{
    public class Produit
    {
        public string image { get; set; }
        public int Id { get; set; }
        public required string Nom { get; set; }
        public double? Prix { get; set; }
        public string Description { get; set; }
        public int NbProduitRestant { get; set; }

    }
}
