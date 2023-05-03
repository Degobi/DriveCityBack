using DriveOfCity.Models.MEmpresa;

namespace DriveOfCity.IServices.IEmpresaService
{
    public interface IEmpresaService
    {
        Task<Empresa> Save(Empresa entidade, bool isTeste = false);
        Task<Empresa> UpdateEmpresa(Empresa entidade);
        Task<IQueryable> GetAll();
        Task<Empresa> GetId(int id);
        bool Delete(int id);
    }
}
