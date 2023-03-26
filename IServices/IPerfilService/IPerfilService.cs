using DriveOfCity.Models.MPerfil;

namespace DriveOfCity.IServices.IPerfilService
{
    public interface IPerfilService
    {
        Task<Perfil> Salva(Perfil entidade);
        Task<Perfil> GetId(int id);
        Task<Perfil> Update(int id, Perfil entidade);
    }
}
