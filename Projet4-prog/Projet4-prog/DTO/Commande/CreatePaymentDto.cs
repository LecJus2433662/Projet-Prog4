namespace Projet4_prog.DTO.Commande
{
    public class CreatePaymentDto
    {
        public long Amount { get; set; }      
        public string Currency { get; set; } = "cad";
        public string Description { get; set; } = string.Empty;
    }
    public class CartRequest
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public string? UtilisateurId { get; set; }
    }

    public class CartItem
    {
        public int ProduitId { get; set; }
        public string Nom { get; set; } = string.Empty;
        public double Prix { get; set; }
        public int Quantite { get; set; }
    }
    
    public class ConfirmRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = new();
    }
    public class CartItemDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
    }
}
