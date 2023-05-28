using DriveOfCity.IServices.IPagamentoService;
using DriveOfCity.Models.Pagamento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveOfCity.Controllers.PagamentoController
{
    [Route("api/pagamento")]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoService _service;

        public PagamentoController(IPagamentoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody]PagamentoRequisicao entidade)
        {
            try
            {
                var paymentIntent = await _service.CreatePaymentIntent(entidade);

                return Ok(paymentIntent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [Authorize]
        [HttpPost("reembolso")]
        public async Task<IActionResult> ProcessarReembolso([FromBody]ReembolsoRequisicao entidade)
        {
            try
            {
                bool reembolsoProcessado = await _service.ProcessarReembolso(entidade);

                if (!reembolsoProcessado)
                    return BadRequest("Falha ao processar o reembolso");


                return Ok("Reembolso processado com sucesso");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
