// Controllers/StripeWebhookController.cs
using Microsoft.AspNetCore.Mvc;
using Projet4_prog.DTO.Commande;
using Projet4_prog.DTO.ItemCommande;
using Projet4_prog.Models;
using Projet4_prog.Services;
using Stripe;
using Stripe.Checkout;
using System.Text.Json;

[ApiController]
[Route("api/stripe/webhook")]
public class StripeWebhookController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ICommandeService _commandeService;
    private readonly IProduitService _produitService;
    private readonly ILogger<StripeWebhookController> _logger;

    public StripeWebhookController(
        IConfiguration config,
        ICommandeService commandeService,
        IProduitService produitService,
        ILogger<StripeWebhookController> logger)
    {
        _config = config;
        _commandeService = commandeService;
        _produitService = produitService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Handle()
    {
        // 1️⃣ Lire le body brut (Stripe exige le body non-modifié pour valider la signature)
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        Event stripeEvent;
        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _config["Stripe:WebhookSecret"]   // clé dans appsettings.json
            );
        }
        catch (StripeException ex)
        {
            _logger.LogWarning("Webhook signature invalide : {msg}", ex.Message);
            return BadRequest("Signature invalide.");
        }

        // 2️⃣ On ne traite que les paiements complétés
        if (stripeEvent.Type == "checkout.session.completed")
        {
            var session = stripeEvent.Data.Object as Session;
            if (session is null) return Ok();

            _logger.LogInformation("Paiement complété — session {id}", session.Id);

            // 3️⃣ Récupérer les metadata
            session.Metadata.TryGetValue("utilisateurId", out var utilisateurId);
            session.Metadata.TryGetValue("items", out var itemsJson);

            if (string.IsNullOrEmpty(itemsJson))
            {
                _logger.LogWarning("Aucun item dans les metadata de la session {id}", session.Id);
                return Ok();
            }

            var cartItems = JsonSerializer.Deserialize<List<CartItemWebhook>>(itemsJson);
            if (cartItems is null || cartItems.Count == 0) return Ok();

            // 4️⃣ Créer la commande en BD
            try
            {
                var commandeDto = new CommandeCreationDto
                {
                    Items = cartItems.Select(i => new ItemCommandeCreationDto
                    {
                        ProduitId = i.ProduitId,
                        Quantite = i.Quantite
                    }).ToList()
                };

                var commande = await _commandeService.CreerAsync(commandeDto, utilisateurId ?? "");

                // 5️⃣ Marquer la commande comme confirmée (paiement reçu)
                await _commandeService.MettreAJourStatutAsync(commande.Id, StatutCommande.Confirmee);

                // 6️⃣ Diminuer le stock pour chaque produit
                foreach (var item in cartItems)
                {
                    await _produitService.DiminuerStockAsync(item.ProduitId, item.Quantite);
                }

                _logger.LogInformation(
                    "Commande {cid} créée et stock mis à jour.", commande.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du traitement du webhook.");
                // ⚠️ On retourne 500 → Stripe réessaiera automatiquement
                return StatusCode(500);
            }
        }

        return Ok(); // Stripe attend un 200 pour confirmer la réception
    }
}

// DTO local pour désérialiser les metadata
public class CartItemWebhook
{
    public int ProduitId { get; set; }
    public string Nom { get; set; } = string.Empty;
    public double Prix { get; set; }
    public int Quantite { get; set; }
}