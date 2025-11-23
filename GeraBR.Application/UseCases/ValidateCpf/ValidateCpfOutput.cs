namespace GeraBR.Application.UseCases.ValidateCpf;

public record ValidateCpfOutput(
    bool IsValid,
    string? NormalizedCpf,
    string? Error
);