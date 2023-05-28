using DriveOfCity.Infra;
using DriveOfCity.IServices.IPagamentoService;
using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Models.Pagamento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace DriveOfCity.Services.EmpresaService
{
    public class PagamentoService : IPagamentoService
    {
        private readonly string _publishableKey;
        private readonly string _secretKey;

        public PagamentoService(IConfiguration configuration)
        {
            _publishableKey = configuration["StripeOptions:PublishableKey"];
            _secretKey = configuration["StripeOptions:SecretKey"];

            StripeConfiguration.ApiKey = _secretKey;
        }

        public async Task<PaymentIntent> CreatePaymentIntent(PagamentoRequisicao entidade)
        {
            if (entidade == null)
                throw new Exception("erro");

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(entidade.Amount * 100),
                Currency = entidade.Currency,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return paymentIntent;
        }

        public bool ProcessarReembolso(ReembolsoRequisicao entidade)
        {

            var refundOptions = new RefundCreateOptions
            {
                PaymentIntent = entidade.PaymentId,
                Amount = (long)(entidade.Amount * 100), // O valor deve ser fornecido em centavos
            };

            var refundService = new RefundService();

            try
            {
                var refund = refundService.Create(refundOptions);

                return true;
            }
            catch (StripeException ex)
            {
                var stripeError = ex.StripeError;
                var errorMessage = stripeError?.Message;

                return false;
            }
        }
    }
}
