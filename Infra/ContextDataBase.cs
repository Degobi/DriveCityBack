using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Models.MUsuario;
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

        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options){ }
    }
}
