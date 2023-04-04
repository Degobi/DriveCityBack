using DriveOfCity.IServices.IEmpresaService;
using DriveOfCity.Models.MEmpresa;
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
        [Authorize]
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
    }
}
