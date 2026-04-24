namespace Projet4_prog.DTO.Produit
{
    public class ProduitLectureDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double? Prix { get; set; }
        public int NbProduitRestant { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}