using DriveOfCity.IServices.IEmailService;
using DriveOfCity.IServices.IPagamentoService;
using DriveOfCity.Models.Pagamento;
using DriveOfCity.Services.EmailService;
using Stripe;

namespace DriveOfCity.Services.PagamentoService
{
    public class PagamentoService : IPagamentoService
    {
        private readonly string _publishableKey;
        private readonly string _secretKey;
        private readonly IEmailService _emailService;


        public PagamentoService(IConfiguration configuration, IEmailService sendEmailService)
        {
            _publishableKey = configuration["StripeOptions:PublishableKey"];
            _secretKey = configuration["StripeOptions:SecretKey"];
            _emailService = sendEmailService;

            StripeConfiguration.ApiKey = _secretKey;
        }

        public async Task<PaymentIntent> CreatePaymentIntent(PagamentoRequisicao entidade)
        {
            if (entidade == null)
                throw new Exception("erro");

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(entidade.Amount * 100), // O valor deve ser fornecido em centavos
                Currency = entidade.Currency,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            string destinatario = "erika.correia@hotmail.com";

            await _emailService.EnviarEmail(destinatario, "PAGAMENTO");

            return paymentIntent;
        }

        public async Task<bool> ProcessarReembolso(ReembolsoRequisicao entidade)
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

                string destinatario = "erika.correia@hotmail.com";

                await _emailService.EnviarEmail(destinatario, "REEMBOLSO");

                return true;
            }
            catch (StripeException ex)
            {
                var stripeError = ex.StripeError;
                var errorMessage = stripeError?.Message;

                return false;
            }
        }

        public async Task<PaymentLinkService> GerarLinkPagamento(LinkRequisicao entidade)
        {
            if (entidade == null)
                throw new Exception("erro");

            var options = new PaymentLinkCreateOptions
            {
                LineItems = new List<PaymentLinkLineItemOptions>
                {
                    new PaymentLinkLineItemOptions { Price = "price_1NFkXPDh1EmmTUUN1q4ecyaj", Quantity = 1 },
                },
                AllowPromotionCodes = true,

            };

            var service = new PaymentLinkService();
            await service.CreateAsync(options);

            return service;
        }
    }
}
