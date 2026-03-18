/// <summary>
/// Выводит на экран ромб из символов X с диагоналями длиной n
/// </summary>
/// <param name="n">Положительная нечётная длина диагоналей</param>
void PrintDiamond(int n)
{
    if ((n <= 0) || (n % 2 == 0))
        throw new ArgumentException("n must be a positive odd number");

    int space = n / 2 - 1;
    int x = 1;

    for (int i = 0; i < n; i++)
    {
        if ((i == 0) || (i == n - 1))
        {
            for (int j = 0; j < n / 2; j++) Console.Write(" ");
            Console.Write("X");
            for (int j = 0; j < n / 2; j++) Console.Write(" ");
        }
        else
        {
            for (int j = 0; j < space; j++) Console.Write(" ");
            Console.Write("X");
            for (int j = 0; j < x; j++) Console.Write(" ");
            Console.Write("X");
            for (int j = 0; j < space; j++) Console.Write(" ");

            if (i < n / 2)
            {
                space--;
                x += 2;
            }
            else
            {
                space++;
                x -= 2;
            }
        }

        Console.WriteLine();
    }
}

try
{
    PrintDiamond(5);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}