using DriveOfCity.Models.MUsuario;

namespace DriveOfCity.IServices.IUsuarioService
{
    public interface IUsuarioService
    {
        Usuario Save(Usuario usuario, bool isTeste = false);
        Usuario Update(Usuario usuario, bool isTeste = false);
        bool Delete(int id);
        Usuario GetId(int id);
        IQueryable GetAll();
    }
}
