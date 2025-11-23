using GeraBR.Domain.ValueObjects;

namespace GeraBR.Application.UseCases.ValidateCpf;

public class ValidateCpfUseCase
{
    public ValidateCpfOutput Execute(string rawCpf)
    {
        try
        {
            var cpf = new Cpf(rawCpf);
            return new ValidateCpfOutput(true, cpf.Digits, null);
        }
        catch (Exception ex)
        {
            return new ValidateCpfOutput(false, null, ex.Message);
        }
    }
}