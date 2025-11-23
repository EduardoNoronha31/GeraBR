namespace GeraBR.Domain.ValueObjects;

public class Cpf
{
    public string Digits { get; }
    private const int MaxCpfLength = 11;

    public Cpf(string value)
    {
        string normalized = Normalize(value);
        ValidateLength(normalized);
        ValidateNotRepeated(normalized);
        ValidateCpf(normalized);
        Digits = normalized;
    }

    public string Format()
    {
        return $"{Digits[..3]}.{Digits[3..6]}.{Digits[6..9]}-{Digits[9..]}";
    }

    private static int ToDigit(char c) => c - '0';

    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CPF cannot be empty");

        return new string(value.Where(char.IsDigit).ToArray());
    }

    private static void ValidateLength(string digits)
    {
        if (digits.Length != MaxCpfLength)
        {
            throw new ArgumentException($"{digits} is not a valid CPF");
        }
    }

    private static void ValidateNotRepeated(string digits)
    {
        if (digits.All(d => d == digits[0]))
            throw new ArgumentException("Invalid CPF. Repeated digits are not allowed");
    }

    private static int CalculateDigit(string digits, int length, int initialWeight)
    {
        var sum = 0;
        var weight = initialWeight;

        for (var i = 0; i < length; i++)
        {
            sum += ToDigit(digits[i]) * weight;
            weight--;
        }

        var result = (sum * 10) % 11;
        return result == 10 ? 0 : result;
    }

    private static bool ValidateFirstVerifierDigit(string digits)
        => CalculateDigit(digits, length: 9, initialWeight: 10) == ToDigit(digits[9]);

    private static bool ValidateSecondVerifierDigit(string digits)
        => CalculateDigit(digits, length: 10, initialWeight: 11) == ToDigit(digits[10]);

    private static void ValidateCpf(string digits)
    {
        if (!ValidateFirstVerifierDigit(digits) ||
            !ValidateSecondVerifierDigit(digits))
        {
            throw new ArgumentException($"{digits} is not a valid CPF");
        }
    }
}