using GeraBR.Application.UseCases.ValidateCpf;

namespace GeraBR.Tests.UseCases.ValidateCpf
{
    public class ValidateCpfUseCaseTests
    {
        private readonly ValidateCpfUseCase _useCase = new();

        [Fact]
        public void Should_Return_Valid_When_Cpf_Is_Valid()
        {
            var input = "529.982.247-25";

            var result = _useCase.Execute(input);

            Assert.True(result.IsValid);
            Assert.Equal("52998224725", result.NormalizedCpf);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Should_Return_Invalid_When_Cpf_Has_Wrong_Verification_Digits()
        {
            var input = "529.982.247-99";

            var result = _useCase.Execute(input);

            Assert.False(result.IsValid);
            Assert.Null(result.NormalizedCpf);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public void Should_Return_Invalid_When_Cpf_Is_Repeated_Digits()
        {
            var input = "111.111.111-11";

            var result = _useCase.Execute(input);

            Assert.False(result.IsValid);
            Assert.Null(result.NormalizedCpf);
            Assert.Contains("Repeated digits", result.Error);
        }

        [Fact]
        public void Should_Return_Invalid_When_Cpf_Has_Less_Digits()
        {
            var input = "12345678";

            var result = _useCase.Execute(input);

            Assert.False(result.IsValid);
            Assert.Null(result.NormalizedCpf);
            Assert.Contains("not a valid CPF", result.Error);
        }

        [Fact]
        public void Should_Validate_Cpf_With_Mask()
        {
            var input = "529.982.247-25";

            var result = _useCase.Execute(input);

            Assert.True(result.IsValid);
            Assert.Equal("52998224725", result.NormalizedCpf);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Should_Return_Error_When_Cpf_Is_Empty()
        {
            var result = _useCase.Execute("");

            Assert.False(result.IsValid);
            Assert.Null(result.NormalizedCpf);
            Assert.NotNull(result.Error);
        }
    }
}