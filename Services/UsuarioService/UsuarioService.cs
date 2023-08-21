using DriveOfCity.Infra;
using DriveOfCity.IServices.IUsuarioService;
using DriveOfCity.Models.MUsuario;
using DriveOfCity.Models.MVeiculo;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DriveOfCity.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private IRepositorioBase<Usuario> _repositorio;
        private IRepositorioBase<Veiculo> _repositorioVeiculo;
        private readonly ContextDataBase _context;

        public UsuarioService(ContextDataBase context)
        {
            _repositorio = new RepositorioBase<Usuario>(context);
            _repositorioVeiculo = new RepositorioBase<Veiculo>(context);
            _context = context;
        }

        public Usuario Save(Usuario entidade, bool isTeste = false)
        {
            var usuarioExistente = _context.Usuario.Where(x => x.Email == entidade.Email).FirstOrDefault();
            if (usuarioExistente != null)
                throw new Exception("Conta existente com esse Email!");

            var novoUsuario = new Usuario();
            try
            {
                GeneralHelper.CopiarObjeto(entidade, ref novoUsuario);

                if (!isTeste)
                {
                    _context.Usuario.Add(novoUsuario);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return novoUsuario;
        }

        public Usuario Update(Usuario usuario, bool isTeste = false)
        {
            if (usuario == null)
                throw new ArgumentNullException("Dados inválidos!");

            var usuarioBanco = _repositorio.Get().Where(x => x.Id == usuario.Id).FirstOrDefault();
            if (usuarioBanco == null)
                throw new ArgumentNullException("Não foi possível localizar o usuário!");

            try
            {
                GeneralHelper.CopiarObjeto(usuario, ref usuarioBanco);

                if (!isTeste)
                {
                    _context.Update(usuarioBanco);
                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {

                throw;
            }

            return usuarioBanco;
        }

        public bool Delete(int id) 
        {
           
            var usuario = _repositorio.Get().Where(x => x.Id == id).FirstOrDefault();

            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");
            

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();

            return true;
        }

        public Usuario GetId(int id)
        {
            var usuario = _repositorio.Get().Where(x => x.Id == id).FirstOrDefault();
            
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            var veiculos = _repositorioVeiculo.Get().Where(x => x.UsuarioId == usuario.Id).ToList();

            usuario.Veiculo = veiculos;
            usuario.Senha = null;

            return usuario;
        }

        public IQueryable GetAll()
        {
            var result = _repositorio.Get();

            return result;
        }
    }
}
