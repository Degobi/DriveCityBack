using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Models.Pagamento;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace DriveOfCity.IServices.IPagamentoService
{
    public interface IPagamentoService
    {
        Task<PaymentIntent> CreatePaymentIntent(PagamentoRequisicao entidade);
        Task<bool> ProcessarReembolso(ReembolsoRequisicao entidade);
        Task<PaymentLinkService> GerarLinkPagamento(LinkRequisicao entidade);
    }
}
