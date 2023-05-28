﻿using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Models.Pagamento;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace DriveOfCity.IServices.IPagamentoService
{
    public interface IPagamentoService
    {
        Task<PaymentIntent> CreatePaymentIntent(PagamentoRequisicao entidade);
        bool ProcessarReembolso(ReembolsoRequisicao entidade);
    }
}
