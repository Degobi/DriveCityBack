using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Models.MTabelaPreco;

namespace DriveOfCity.IServices.IEmpresaService
{
    public interface IEmpresaService
    {
        Task<Empresa> Save(Empresa entidade, bool isTeste = false);
        Task<Empresa> UpdateEmpresa(Empresa entidade);
        Task<List<TabelaPreco>> UpdateTabelaPreco(int intEmpresaId, List<TabelaPreco> entidade);
        Task<TabelaPreco> UpdateTabelaPrecoId(int id, TabelaPreco entidade);
        Task<IQueryable> GetAll();
        Task<Empresa> GetId(int id);
        bool Delete(int id);
    }
}
