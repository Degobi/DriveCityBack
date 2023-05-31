using DriveOfCity.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using DriveOfCity.Models.MUsuario;
using GenFu;
using DriveOfCity.IServices.IUsuarioService;
using DriveOfCity.Services.UsuarioService;

namespace TestDriveOfCity
{
    [TestClass]
    public class UsuarioTest
    {
        private ContextDataBase _context;
        private UsuarioService _usuarioService;

        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = "Server=localhost;Database=DriveOfCity;User Id=sa;Password=ads@2020;Integrated Security=SSPI;TrustServerCertificate=True";

            var options = new DbContextOptionsBuilder<ContextDataBase>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new ContextDataBase(options);
            _usuarioService = new UsuarioService(_context);
        }

        [TestMethod]
        public void Criar_Usuario()
        {
            //Arr
            var usuarioExpected = A.New<Usuario>();

            //Act
            var result = _usuarioService.Save(usuarioExpected, true);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Nome, usuarioExpected.Nome);
            Assert.AreEqual(result.Email, usuarioExpected.Email);

        }

        [TestMethod]
        public void Atualizar_Usuario()
        {
            //Arr
            var usuarioExpected = A.New<Usuario>();

            //Act
            var usuario = _usuarioService.Save(usuarioExpected);
            usuario.Email = "update@gmail.com";
            usuario.Nome  = "Update";
            usuario.Senha = "updateteste";
            var result = _usuarioService.Update(usuario);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(usuario.Email, result.Email);
            Assert.AreEqual(usuario.Nome, result.Nome);
            Assert.AreEqual(usuario.Senha, result.Senha);
            bool deleteUsuario = _usuarioService.Delete(result.Id);
            Assert.IsTrue(deleteUsuario);
        }

        [TestMethod]
        public void Listar_Usuario()
        {
            //Arr
            var usuarioBancoExpected = _context.Usuario.Where(x => x.Email != null).FirstOrDefault();

            //Act
            var result = _usuarioService.GetId(usuarioBancoExpected.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(usuarioBancoExpected, result);
        }

        [TestMethod]
        public void Deletar_Usuario()
        {
            //Arr
            var usuarioExpected = A.New<Usuario>();

            //Act
            var result = _usuarioService.Save(usuarioExpected);
            bool deleteUser = _usuarioService.Delete(result.Id);

            //Assert
            Assert.IsTrue(deleteUser);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }
    }
}
