using DriveOfCity.Infra;
using DriveOfCity.IServices.IUsuarioService;
using DriveOfCity.Models.MUsuario;

namespace DriveOfCity.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private IRepositorioBase<Usuario> _repositorio;
        private readonly ContextDataBase _context;

        public UsuarioService(ContextDataBase context)
        {
            _repositorio = new RepositorioBase<Usuario>(context);
            _context = context;
        }

        public Usuario Save(Usuario entidade)
        {
            var usuarioExistente = _context.Usuario.Where(x => x.Email == entidade.Email).FirstOrDefault();
            if (usuarioExistente != null)
                throw new Exception("Conta existente com esse Email!");

            var novoUsuario = new Usuario();
            try
            {
                novoUsuario.Nome = entidade.Nome;
                novoUsuario.DataCriacao = DateTime.Now;
                novoUsuario.DataAtualizacao = DateTime.Now;
                novoUsuario.Senha = entidade.Senha;
                novoUsuario.Email = entidade.Email;

                _context.Usuario.Add(novoUsuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return novoUsuario;
        }

        public Usuario Update(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public IQueryable GetAll()
        {
            var result = _repositorio.Get();

            return result;
        }
    }
}
