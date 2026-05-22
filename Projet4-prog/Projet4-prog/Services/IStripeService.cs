using Projet4_prog.DTO.Commande;
using Stripe;

namespace Projet4_prog.Services
{
    public interface IStripeService
    {
        Task<PaymentIntent> CreatePaymentIntentAsync(CreatePaymentDto dto);
    }

    public class StripeService : IStripeService
    {
        public async Task<PaymentIntent> CreatePaymentIntentAsync(CreatePaymentDto dto)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = dto.Amount,
                Currency = dto.Currency,
                Description = dto.Description,
            };

            var service = new PaymentIntentService();
            return await service.CreateAsync(options);
        }
    }
}
