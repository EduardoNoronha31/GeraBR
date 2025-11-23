namespace GeraBR.Application.UseCases.GenerateCpf;

public record GenerateCpfOutput(
    string Value,
    string FormattedValue
);