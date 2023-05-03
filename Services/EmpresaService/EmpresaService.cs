using DriveOfCity.Infra;
using DriveOfCity.IServices.IEmpresaService;
using DriveOfCity.Models.MEmpresa;
using Microsoft.EntityFrameworkCore;

namespace DriveOfCity.Services.EmpresaService
{
    public class EmpresaService : IEmpresaService
    {
        private IRepositorioBase<Empresa> _repositorioBase;
        private readonly ContextDataBase _context;

        public EmpresaService(ContextDataBase context)
        {
            _repositorioBase = new RepositorioBase<Empresa>(context);
            _context = context;
        }

        public async Task<Empresa> Save(Empresa entidade, bool isTeste = false)
        {
            try
            {
                //if (file == null || file.Length == 0)
                //    throw new Exception("Nenhum arquivo para imagem foi selecionado");

                //var imageData = new byte[file.Length];
                //using (var stream = new MemoryStream())
                //{
                //    await file.CopyToAsync(stream);
                //    imageData = stream.ToArray();
                //}

                var empresa = new Empresa()
                {
                    Nome = entidade.Nome,
                    Descricao = entidade.Descricao,
                    //ImagemEmpresa = entidade.ImagemEmpresa,
                    Lat = entidade.Lat,
                    Lng = entidade.Lng,
                    TabelaPrecos = entidade.TabelaPrecos != null ? entidade.TabelaPrecos : null,
                };

                if (!isTeste )
                {
                    await _context.AddAsync(empresa);
                    await _context.SaveChangesAsync();
                }

                return empresa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IQueryable> GetAll()
        {
            var result = _repositorioBase.Get().Include("TabelaPrecos");

            return result;
        }

        public async Task<Empresa> GetId(int id)
        {
            var result = await _repositorioBase.Get()
                .Include("TabelaPrecos").Where(x => x.Id == id).FirstOrDefaultAsync();

            if (result == null)
                throw new InvalidOperationException("Empresa não encontrada.");

            return result;

        }

        public async Task<Empresa> UpdateEmpresa(Empresa entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException("Erro ao realizar atualização!");

            var empresaBanco = await _repositorioBase.Get().Where(x => x.Id == entidade.Id).FirstOrDefaultAsync();
            if (empresaBanco == null)
                throw new ArgumentNullException("Nenhuma empresa foi localizada!");

            try
            {
                GeneralHelper.CopiarObjeto(entidade, ref empresaBanco);

                _context.Update(empresaBanco);
                _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return empresaBanco;
        }

        public bool Delete(int id)
        {
            var empresaBanco = _repositorioBase.Get().Include("TabelaPrecos").Where(x => x.Id == id).FirstOrDefault();

            _context.Remove(empresaBanco);
            _context.SaveChanges();

            return true;
        }
    }
}
