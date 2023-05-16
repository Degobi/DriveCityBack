using DriveOfCity.Infra;
using DriveOfCity.IServices.IVeiculoService;
using DriveOfCity.Models.MVeiculo;
using Microsoft.EntityFrameworkCore;

namespace DriveOfCity.Services.VeiculoService
{
    public class VeiculoService : IVeiculoService
    {
        private IRepositorioBase<Veiculo> _repositorioBase;
        private readonly ContextDataBase _context;

        public VeiculoService(ContextDataBase context)
        {
            _repositorioBase = new RepositorioBase<Veiculo>(context);
            _context = context;
        }

        public async Task<Veiculo> Save(Veiculo entidade, bool isTeste = false)
        {
            throw new NotImplementedException();
        }

        public async Task<Veiculo> Update(Veiculo entidade, bool isTeste = false)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Veiculo> GetId(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
