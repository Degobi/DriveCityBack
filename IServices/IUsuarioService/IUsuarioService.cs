using DriveOfCity.Models.MUsuario;

namespace DriveOfCity.IServices.IUsuarioService
{
    public interface IUsuarioService
    {
        Usuario Save(Usuario usuario);
        Usuario Update(Usuario usuario);
        IQueryable GetAll();
    }
}
