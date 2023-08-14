using DriveOfCity.IServices.IEmpresaService;
using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Models.MTabelaPreco;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace DriveOfCity.Controllers.EmpresaController
{
    [Route("api/empresa")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;

        public EmpresaController(IEmpresaService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] Empresa entidade)
        {
            try
            {
                var result = await _service.Save(entidade);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("tabela-preco/{intEmpresaId}")]
        public async Task<IActionResult> Put(int intEmpresaId, [FromBody] List<TabelaPreco> entidade)
        {
            try
            {
                var result = await _service.UpdateTabelaPreco(intEmpresaId, entidade);

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpPut]
        [AllowAnonymous]
        [Route("tabela-preco/{id}")]
        public async Task<IActionResult> PutId(int id, [FromBody] TabelaPreco entidade)
        {
            try
            {
                var result = await _service.UpdateTabelaPrecoId(id, entidade);

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetEmpresas()
        {
            IQueryable result;
            try
            {
                result = await _service.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateEmpresa([FromBody] Empresa entidade)
        {
            try
            {
                var result = await _service.UpdateEmpresa(entidade);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Empresa result = null;

            try
            {
                result = await _service.GetId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public IResult DeleteEmpresa(int id)
        {
            try
            {
               _service.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Results.Ok();
        }
    }
}
