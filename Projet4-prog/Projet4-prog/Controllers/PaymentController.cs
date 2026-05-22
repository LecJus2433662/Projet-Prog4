using Microsoft.AspNetCore.Mvc;
using Projet4_prog.DTO.Commande;
using Projet4_prog.DTO.ItemCommande;
using Projet4_prog.Models;
using Projet4_prog.Services;
using Stripe;
using Stripe.Checkout;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IStripeService _stripeService;
    private readonly ICommandeService _commandeService;  
    private readonly IProduitService _produitService;    

    public CheckoutController(
        IStripeService stripeService,
        IConfiguration configuration,
        ICommandeService commandeService,   
        IProduitService produitService)     
    {
        _stripeService = stripeService;
        _configuration = configuration;
        _commandeService = commandeService; 
        _produitService = produitService;   
    }

    [HttpPost]
    public IActionResult Create([FromBody] CartRequest request)
    {
        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            Mode = "payment",
            SuccessUrl = "http://localhost:3000/succesAchat?session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = "http://localhost:3000",
            Metadata = new Dictionary<string, string>
            {
                { "items", System.Text.Json.JsonSerializer.Serialize(request.Items) },
                { "utilisateurId", request.UtilisateurId ?? "" }
            },
            LineItems = request.Items.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "cad",
                    UnitAmount = (long)(item.Prix * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Nom
                    }
                },
                Quantity = item.Quantite
            }).ToList()
        };

        var service = new SessionService();
        var session = service.Create(options);
        return Ok(new { url = session.Url });
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmerCommande([FromBody] ConfirmRequest request)
    {
        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

        var sessionService = new SessionService();
        Session session;
        try
        {
            session = await sessionService.GetAsync(request.SessionId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Session introuvable: {ex.Message}");
            return NotFound("Session Stripe introuvable.");
        }

        if (session.PaymentStatus != "paid")
            return BadRequest("Paiement non complété.");

        session.Metadata.TryGetValue("items", out var itemsJson);
        Console.WriteLine($"✅ Items JSON: {itemsJson ?? "NULL"}");

        if (string.IsNullOrEmpty(itemsJson))
            return BadRequest("Aucun item trouvé dans la session.");

        var cartItems = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(itemsJson);
        if (cartItems == null || cartItems.Count == 0)
            return BadRequest("Items invalides.");

        // ✅ Diminuer le stock avec les items des metadata Stripe
        foreach (var item in cartItems)
        {
            try
            {
                Console.WriteLine($"🔄 Diminution stock → ProduitId={item.ProduitId}, Quantite={item.Quantite}");
                await _produitService.DiminuerStockAsync(item.ProduitId, item.Quantite);
                Console.WriteLine($"✅ Stock diminué pour ProduitId={item.ProduitId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur stock ProduitId={item.ProduitId}: {ex.Message}");
            }
        }

        return Ok(new { success = true });
    }
}