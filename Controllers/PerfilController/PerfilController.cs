using DriveOfCity.Infra;
using DriveOfCity.IServices.IPerfilService;
using DriveOfCity.Models.MPerfil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriveOfCity.Controllers.PerfilController
{
    [Route("api/perfil")]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _service;

        public PerfilController(IPerfilService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _service.GetId(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
 
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] Perfil entidade)
        {
            try
            {
                var result = await _service.Salva(entidade);

                return Ok(entidade);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] Perfil entidade)
        {
            try
            {
                var newPerfil = await _service.Update(id, entidade);

                return Ok(newPerfil);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
