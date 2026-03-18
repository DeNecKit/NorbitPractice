using System.Text;

string CalculateDeposit(double initialDeposit, int years, double interestRate)
{
    if (initialDeposit <= 0)
        throw new ArgumentException("initialDeposit must be positive");
    if (years <= 0)
        throw new ArgumentException("years must be positive");
    if (interestRate <= 0)
        throw new ArgumentException("interestRate must be positive");

    StringBuilder result = new();
    double sum = initialDeposit;

    for (int year = 1; year <= years; year++)
    {
        sum += sum * (interestRate / 100.0);
        result.AppendLine($"Год {year}: {sum:0.00} руб.");
    }

    return result.ToString();
}

Console.WriteLine(CalculateDeposit(1000.0, 3, 10.0));