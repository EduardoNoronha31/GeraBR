namespace GeraBR.Domain.Services;

public class CpfGeneratorService : ICpfGeneratorService
{
    private static readonly Random Random = new();

    public string Generate()
    {
        var digits = new int[11];

        for (var i = 0; i < 9; i++)
        {
            digits[i] = Random.Next(0, 10);
        }

        digits[9] = CalculateVerifierDigit(digits, 9, 10);
        digits[10] = CalculateVerifierDigit(digits, 10, 11);

        return string.Concat(digits.Select(x => x.ToString()));
    }

    private static int CalculateVerifierDigit(int[] digits, int length, int weight)
    {
        var sum = 0;

        for (var i = 0; i < length; i++)
        {
            sum += digits[i] * weight;
            weight--;
        }

        var result = (sum * 10) % 11;
        return result == 10 ? 0 : result;
    }
}