using DriveOfCity.Infra;
using DriveOfCity.IServices.IEmpresaService;
using DriveOfCity.Models.MEmpresa;

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

        public async Task<Empresa> Save(Empresa entidade)
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
                    Endereco = entidade.Endereco,
                    //ImagemEmpresa = entidade.ImagemEmpresa,
                    Lat = entidade.Lat,
                    Lng = entidade.Lng,
                };

                _context.Empresa.Add(empresa);
                _context.SaveChangesAsync();

                return empresa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
