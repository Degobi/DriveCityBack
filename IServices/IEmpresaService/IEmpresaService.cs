using DriveOfCity.Models.MEmpresa;

namespace DriveOfCity.IServices.IEmpresaService
{
    public interface IEmpresaService
    {
        Task<Empresa> Save(Empresa entidade);

        Task<IQueryable> GetAll();
    }
}
