using DriveOfCity.Infra;
using DriveOfCity.IServices.IUsuarioService;
using DriveOfCity.Models.MUsuario;
using DriveOfCity.Services.UsuarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DriveOfCity.Controllers.UsuarioController
{
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        private readonly ContextDataBase _contextData;

        public UsuarioController(IUsuarioService usuarioService, ContextDataBase contextData, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _contextData = contextData;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public IResult Post([FromBody] Usuario entidade)
        {
            var result = new Usuario();
            try
            {
                result = this._usuarioService.Save(entidade);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return Results.Ok();
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public IResult Token([FromBody] LoginRequest entidade)
        {
            var usuario = _contextData.Usuario.Where(x => x.Email == entidade.Email && x.Senha == entidade.Senha).FirstOrDefault();
            if (usuario == null)
                throw new Exception("Usuário não localizado!");

            var key = Encoding.ASCII.GetBytes(_configuration["JwtBearerTokenSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["JwtBearerTokenSettings:Audience"],
                Issuer = _configuration["JwtBearerTokenSettings:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Results.Ok(new
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Token = tokenHandler.WriteToken(token),
            });
        }

        [HttpGet]
        [Authorize]
        [Route("getall")]
        public IResult GetAll()
        {
            IQueryable result;
            try
            {
                result =  _usuarioService.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Results.Ok(result);
        }

        [HttpPut]
        [Authorize]
        public IResult PutUsuario([FromBody] Usuario usuario)
        {
            Usuario result = null;
            try
            {
                result = this._usuarioService.Update(usuario);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return Results.Ok(result);
        }
    }
}
