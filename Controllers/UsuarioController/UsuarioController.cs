using DriveOfCity.Infra;
using DriveOfCity.IServices.IUsuarioService;
using DriveOfCity.Models.MUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DriveOfCity.Controllers.UsuarioController
{
    [Route("api/usuario")]
    public class UsuarioController : Controller
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
        public IResult Post(Usuario entidade)
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

        [HttpGet]
        [Route("token")]
        [AllowAnonymous]
        public IResult Token(Usuario entidade)
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
                token = tokenHandler.WriteToken(token),
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
    }
}
