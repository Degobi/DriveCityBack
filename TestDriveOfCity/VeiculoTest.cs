using DriveOfCity.Infra;
using Microsoft.EntityFrameworkCore;
using GenFu;
using DriveOfCity.Models.MEmpresa;
using DriveOfCity.Services.VeiculoService;
using DriveOfCity.Models.MVeiculo;

namespace TestDriveOfCity
{
    [TestClass]
    public class VeiculoTest
    {
        private ContextDataBase _context;
        private VeiculoService _veiculoService;

        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = "Server=localhost;Database=DriveOfCity;User Id=sa;Password=ads@2020;Integrated Security=SSPI;TrustServerCertificate=True";

            var options = new DbContextOptionsBuilder<ContextDataBase>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new ContextDataBase(options);
            _veiculoService = new VeiculoService(_context);
        }

        [TestMethod]
        public async Task Criar_Veiculo()
        {
            //Arr
            var veiculoExpected = A.New<Veiculo>();

            //Act
            var result = await _veiculoService.Save(veiculoExpected);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, veiculoExpected);

        }

        [TestMethod]
        public async Task Atualizar_Veiculo()
        {
            //Arr
            var empresaExpected = A.New<Veiculo>();

            //Act
            var veiculo = await _veiculoService.Save(empresaExpected);
            veiculo.Marca = "UpdateTeste";
            veiculo.Modelo = "Update";
            var result = await _veiculoService.Update(veiculo);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(veiculo.Marca, result.Marca);
            Assert.AreEqual(veiculo.Modelo, result.Modelo);
            _veiculoService.Delete(result.Id);
        }

        [TestMethod]
        public async Task Listar_Veiculo()
        {
            //Arr
            var veiculoBancoExpected = _context.Veiculo.Where(x => x.Modelo != null).FirstOrDefault();

            //Act
            var result = await _veiculoService.GetId(veiculoBancoExpected.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(veiculoBancoExpected, result);

        }

        [TestMethod]
        public async Task Deletar_Veiculo()
        {
            //Arr
            var veiculo = A.New<Veiculo>();

            //Act
            var veiculoEmpresa = await _veiculoService.Save(veiculo);
            bool deletedVeiculo = _veiculoService.Delete(veiculoEmpresa.Id);

            //Assert
            Assert.IsTrue(deletedVeiculo);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }
    }
}
