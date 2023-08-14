using DriveOfCity.Infra;
using DriveOfCity.IServices.IEmpresaService;
using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Models.MTabelaPreco;
using Microsoft.EntityFrameworkCore;

namespace DriveOfCity.Services.EmpresaService
{
    public class EmpresaService : IEmpresaService
    {
        private IRepositorioBase<Empresa> _repositorioBase;
        private IRepositorioBase<TabelaPreco> _repositorioTabelaPreco;
        private readonly ContextDataBase _context;

        public EmpresaService(ContextDataBase context)
        {
            _repositorioBase = new RepositorioBase<Empresa>(context);
            _repositorioTabelaPreco = new RepositorioBase<TabelaPreco>(context);
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

        public async Task<List<TabelaPreco>> UpdateTabelaPreco(int intEmpresaId, List<TabelaPreco> entidades)
        {
            if (entidades == null)
                throw new ArgumentNullException("Erro ao realizar a atualização!");

            var tabelaPrecos = await _repositorioTabelaPreco.Get().Where(x => x.EmpresaId == intEmpresaId).ToListAsync();

            if (tabelaPrecos == null || tabelaPrecos.Count == 0)
                throw new ArgumentNullException("Nenhuma Tabela de Preço foi localizada!");

            TabelaPreco tabelaPreco = null;

            try
            {
                foreach (var item in entidades)
                {
                    tabelaPreco = tabelaPrecos.FirstOrDefault(tp => tp.Id == item.Id);

                    if (tabelaPreco != null)
                    {
                        GeneralHelper.CopiarObjeto(item, ref tabelaPreco);
                    }
                }
                _context.TabelaPreco.Update(tabelaPreco);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return tabelaPrecos;
        }

        public async Task<TabelaPreco> UpdateTabelaPrecoId(int id, TabelaPreco entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException("Erro ao realizar a atualização!");

            var tabelaPreco = await _repositorioTabelaPreco.Get().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (tabelaPreco == null)
                throw new ArgumentNullException("Nenhuma Tabela de Preço foi localizada!");

            try
            {
               GeneralHelper.CopiarObjeto(entidade, ref tabelaPreco);

               _context.TabelaPreco.Update(tabelaPreco);
               await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return tabelaPreco;
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
