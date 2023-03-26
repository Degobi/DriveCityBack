using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Models.MEndereco;
using DriveOfCity.Models.MPerfil;
using DriveOfCity.Models.MUsuario;
using Microsoft.EntityFrameworkCore;

namespace DriveOfCity.Infra
{
    public class ContextDataBase : DbContext
    {

        #region USUARIO ==============================
        public DbSet<Usuario> Usuario { get; set; }
        #endregion

        #region PERFIL ===============================
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        #endregion

        #region Empresa ==============================
        public DbSet<Empresa> Empresa { get; set; }
        #endregion

        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options){ }
    }
}
