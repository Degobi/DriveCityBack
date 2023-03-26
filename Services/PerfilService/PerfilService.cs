using DriveOfCity.Infra;
using DriveOfCity.IServices.IPerfilService;
using DriveOfCity.Models.MPerfil;
using Microsoft.EntityFrameworkCore;

namespace DriveOfCity.Services.PerfilService
{
    public class PerfilService : IPerfilService
    {
        private IRepositorioBase<Perfil> _repositorioBase;
        private readonly ContextDataBase _context;

        public PerfilService(ContextDataBase context)
        {
            _context = context;
            _repositorioBase = new RepositorioBase<Perfil>(context);
        }

        public async Task<Perfil> Salva(Perfil entidade)
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

                var perfil = new Perfil()
                {
                    DataNascimento = entidade.DataNascimento,
                    Endereco = entidade.Endereco,
                    Nome = entidade.Nome,
                    Sobrenome = entidade.Sobrenome,
                    //Imagem = entidade.Imagem,
                };

                _context.Perfil.Add(perfil);
                _context.SaveChangesAsync();

                return perfil;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Perfil> GetId(int id)
        {
            try
            {
                var perfil = await _repositorioBase.Get().Where(x => x.Id == id).FirstOrDefaultAsync();

                return perfil;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Perfil> Update(int id, Perfil entidade)
        {
            try
            {
                var perfilBanco = await _repositorioBase.Get().Where(x => x.Id == id).FirstOrDefaultAsync();
                if (perfilBanco == null) throw new Exception("Perfil não localizado no banco de dados");

                GenerateHelper.ReferenceEquals(perfilBanco, entidade);

                _context.Perfil.Add(perfilBanco);
                _context.SaveChangesAsync();

                return perfilBanco;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
