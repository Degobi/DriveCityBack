using DriveOfCity.Models.MVeiculo;

namespace DriveOfCity.IServices.IVeiculoService
{
    public interface IVeiculoService
    {
        Task<Veiculo> Save(Veiculo entidade, bool isTeste = false);
        Task<Veiculo> Update(Veiculo entidade, bool isTeste = false);
        bool Delete(int id);
        Task<Veiculo> GetId(int id);
        Task<List<Veiculo>> GetAll(int usuarioId);
    }
}
