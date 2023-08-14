using DriveOfCity.Infra;
using DriveOfCity.IServices.IVeiculoService;
using DriveOfCity.Models.MUsuario;
using DriveOfCity.Models.MVeiculo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveOfCity.Controllers.VeiculoController
{
    [Route("api/veiculo")]
    public class VeiculoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IVeiculoService _veiculoService;
        private readonly ContextDataBase _contextData;

        public VeiculoController(IVeiculoService veiculoService, ContextDataBase contextData, IConfiguration configuration)
        {
            _veiculoService = veiculoService;
            _contextData = contextData;
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<IResult> Post([FromBody] Veiculo entidade)
        {
            try
            {
                Veiculo result = await _veiculoService.Save(entidade);

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getall/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {
                var result = await _veiculoService.GetAll(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPut]
        [Authorize]
        public async Task<IResult> PutVeiculo([FromBody] Veiculo entidade)
        {
            try
            {
                Veiculo result = await this._veiculoService.Update(entidade);

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                Veiculo result = await _veiculoService.GetId(id);

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
