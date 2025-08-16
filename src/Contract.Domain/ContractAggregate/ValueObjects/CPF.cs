using Abp.Domain.Values;

namespace Contract.Domain.Contract.ValueObjects;

public class CPF(string number) : ValueObject
{
    public const int Length = 11;

    public string Value => number;
    public bool IsValid => Validate(number);

    public static CPF Create(string input)
    {
        if (input is null) throw new ArgumentNullException(nameof(input));

        var digits = OnlyDigits(input);

        if (!Validate(digits))
            throw new ArgumentException("CPF inválido.", nameof(input));

        return new CPF(digits);
    }

    public override string ToString() =>
        $"{number[..3]}.{number.Substring(3, 3)}.{number.Substring(6, 3)}-{number.Substring(9, 2)}";

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return number;
    }

    private static string OnlyDigits(string value) =>
        new(value.Where(char.IsDigit).ToArray());

    private static bool Validate(string digits)
    {
        if (string.IsNullOrWhiteSpace(digits)) return false;
        if (digits.Length != Length) return false;
        if (new string(digits[0], digits.Length) == digits) return false;

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (digits[i] - '0') * (10 - i);
        int r = sum % 11;
        int d1 = r < 2 ? 0 : 11 - r;
        if (d1 != digits[9] - '0') return false;

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += (digits[i] - '0') * (11 - i);
        r = sum % 11;
        int d2 = r < 2 ? 0 : 11 - r;
        if (d2 != digits[10] - '0') return false;

        return true;
    }
}