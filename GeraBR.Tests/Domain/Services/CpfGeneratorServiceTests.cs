using GeraBR.Domain.Services;

namespace GeraBR.Tests.Domain.Services
{
    public class CpfGeneratorServiceTests
    {
        private readonly CpfGeneratorService _service = new();

        [Fact]
        public void Should_Generate_Cpf_With_11_Digits()
        {
            var cpf = _service.Generate();
            Assert.Equal(11, cpf.Length);
        }

        [Fact]
        public void Should_Generate_Cpf_With_Only_Numbers()
        {
            var cpf = _service.Generate();
            Assert.Matches("^[0-9]{11}$", cpf);
        }

        [Fact]
        public void Should_Generate_Different_Cpfs_On_Multiple_Calls()
        {
            var cpf1 = _service.Generate();
            var cpf2 = _service.Generate();
            Assert.NotEqual(cpf1, cpf2);
        }
    }
}