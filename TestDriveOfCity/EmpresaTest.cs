using DriveOfCity.Infra;
using Microsoft.EntityFrameworkCore;
using GenFu;
using DriveOfCity.Services.EmpresaService;
using DriveOfCity.Models.MEmpresa;

namespace TestDriveOfCity
{
    [TestClass]
    public class EmpresaTest
    {
        private ContextDataBase _context;
        private EmpresaService _empresaService;

        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = "Server=localhost;Database=DriveOfCity;User Id=sa;Password=ads@2020;Integrated Security=SSPI;TrustServerCertificate=True";

            var options = new DbContextOptionsBuilder<ContextDataBase>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new ContextDataBase(options);
            _empresaService = new EmpresaService(_context);
        }

        [TestMethod]
        public async Task Criar_Empresa()
        {
            //Arr
            var empresaExpected = A.New<Empresa>();

            //Act
            var result = await _empresaService.Save(empresaExpected, true);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Nome, empresaExpected.Nome);

        }

        [TestMethod]
        public async Task Atualizar_Empresa()
        {
            //Arr
            var empresaExpected = A.New<Empresa>();

            //Act
            var empresa = await _empresaService.Save(empresaExpected);
            empresa.Nome = "UpdateTeste";
            empresa.Descricao = "Update";
            var result = await _empresaService.UpdateEmpresa(empresa);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(empresa.Nome, result.Nome);
            Assert.AreEqual(empresa.Descricao, result.Descricao);
            _empresaService.Delete(result.Id);
        }

        [TestMethod]
        public async Task Listar_Empresa()
        {
            //Arr
            var empresaBancoExpected = _context.Empresa.Where(x => x.Nome != null).FirstOrDefault();

            //Act
            var result = await _empresaService.GetId(empresaBancoExpected.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(empresaBancoExpected, result);

        }

        [TestMethod]
        public async Task Deletar_Empresa()
        {
            //Arr
            var empresa = A.New<Empresa>();

            //Act
            var saveEmpresa = await _empresaService.Save(empresa);
            bool deletedEmpresa = _empresaService.Delete(saveEmpresa.Id);

            //Assert
            Assert.IsTrue(deletedEmpresa);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }
    }
}
