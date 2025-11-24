using GeraBR.Application.UseCases.GenerateCpf;
using GeraBR.Domain.Services;
using Moq;

namespace GeraBR.Tests.UseCases.GenerateCpf;

public class GenerateCpfUseCaseTests
{
    private readonly Mock<ICpfGeneratorService> _cpfGeneratorMock;
    private readonly GenerateCpfUseCase _useCase;

    public GenerateCpfUseCaseTests()
    {
        _cpfGeneratorMock = new Mock<ICpfGeneratorService>();
        _useCase = new GenerateCpfUseCase(_cpfGeneratorMock.Object);
    }

    [Fact]
    public void Should_Return_ValidCpf_When_Service_Generates_ValidCpf()
    {
        const string validCpf = "52998224725";
        _cpfGeneratorMock.Setup(x => x.Generate()).Returns(validCpf);

        var result = _useCase.Execute();

        Assert.Equal(validCpf, result.Value);
        Assert.Equal("529.982.247-25", result.FormattedValue);
    }

    [Fact]
    public void Should_Call_CpfGeneratorService_Once()
    {
        _cpfGeneratorMock.Setup(x => x.Generate()).Returns("52998224725");

        _useCase.Execute();

        _cpfGeneratorMock.Verify(x => x.Generate(), Times.Once);
    }

    [Fact]
    public void Should_Throw_Exception_When_Service_Generates_Invalid_Cpf()
    {
        _cpfGeneratorMock.Setup(x => x.Generate()).Returns("12345678900");

        Assert.Throws<ArgumentException>(() => _useCase.Execute());
    }

    [Fact]
    public void Should_Return_Formatted_Cpf_Correctly()
    {
        _cpfGeneratorMock.Setup(x => x.Generate()).Returns("52998224725");

        var result = _useCase.Execute();

        Assert.Equal("529.982.247-25", result.FormattedValue);
    }
}