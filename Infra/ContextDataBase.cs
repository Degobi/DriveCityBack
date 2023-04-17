using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Models.MUsuario;
using DriveOfCity.Models.MVeiculo;
using Microsoft.EntityFrameworkCore;

namespace DriveOfCity.Infra
{
    public class ContextDataBase : DbContext
    {

        #region USUARIO ==============================
        public DbSet<Usuario> Usuario { get; set; }
        #endregion

        #region Empresa ==============================
        public DbSet<Empresa> Empresa { get; set; }
        #endregion

        #region Veiculo ==============================
        public DbSet<Veiculo> Veiculo { get; set; }
        #endregion
        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options){ }
    }
}
