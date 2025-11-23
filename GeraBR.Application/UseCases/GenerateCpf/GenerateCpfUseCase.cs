using GeraBR.Domain.ValueObjects;
using GeraBR.Domain.Services;

namespace GeraBR.Application.UseCases.GenerateCpf;

public class GenerateCpfUseCase
{
    private readonly ICpfGeneratorService _cpfGeneratorService;

    public GenerateCpfUseCase(ICpfGeneratorService cpfGeneratorService)
    {
        _cpfGeneratorService = cpfGeneratorService;
    }

    public GenerateCpfOutput Execute()
    {
        var generatedCpf = _cpfGeneratorService.Generate();
        var validatedCpf = new Cpf(generatedCpf);
        return new GenerateCpfOutput(validatedCpf.Digits, validatedCpf.Format());
    }
}