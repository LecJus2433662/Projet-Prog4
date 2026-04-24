namespace Projet4_prog.DTO.ItemCommande
{
    public class ItemCommandeLectureDto
    {
        public int Id { get; set; }
        public int ProduitId { get; set; }
        public string NomProduit { get; set; } = string.Empty;
        public int Quantite { get; set; }
        public decimal PrixUnitaire { get; set; }
    }
}

