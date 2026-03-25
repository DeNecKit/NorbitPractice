double x, y, width, height;
try
{
    Console.WriteLine("Ввод прямоугольника:");

    Console.WriteLine("  Координаты левого верхнего угла:");
    Console.Write("    Введите x-координату: ");
    x = double.Parse(Console.ReadLine() ?? "");
    Console.Write("    Введите y-координату: ");
    y = double.Parse(Console.ReadLine() ?? "");

    Console.WriteLine("  Размеры прямоугольника:");
    Console.Write("    Введите ширину: ");
    width = double.Parse(Console.ReadLine() ?? "");
    Console.Write("    Введите высоту: ");
    height = double.Parse(Console.ReadLine() ?? "");
}
catch (FormatException ex)
{
    Console.WriteLine(ex.Message);
    return;
}

try
{
    var rect = new Rectangle(x, y, width, height);

    Console.Write("Прямоугольик с координтатами левого верхнего угла ");
    Console.Write($"({rect.Left}; {rect.Top})");
    Console.WriteLine($" и размером {rect.Width}x{rect.Height}");

    Console.WriteLine($"Периметр = {rect.Perimeter}");
    Console.WriteLine($"Площадь = {rect.Area}");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.Write(ex.Message);
    return;
}


class Rectangle
{
    public double Left { get; private set; }
    public double Top { get; private set; }
    public double Width { get; private set; }
    public double Height { get; private set; }

    public double Perimeter => Width * 2 + Height * 2;
    public double Area => Width * Height;

    public Rectangle(double left, double top,
                     double width, double height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(width),
                $"{nameof(width)} must be positive"
            );
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(height),
                $"{nameof(height)} must be positive"
            );
        }

        Left = left;
        Top = top;
        Width = width;
        Height = height;
    }
}