using System.Collections;


Console.WriteLine("Инициализация стека");
var stack = new SmartStack<int>(2);

Console.WriteLine("Добавление элементов по одному");
stack.Push(1);
stack.Push(2);
stack.Push(3);

Console.WriteLine($"Вершина стека = {stack[0]}");

Console.WriteLine("Элементы стека:");
foreach (var item in stack) Console.WriteLine(item);

Console.WriteLine($"Результат Pop = {stack.Pop()}");
Console.WriteLine($"Результат Peek = {stack.Peek()}");

Console.WriteLine($"Текущий размер = {stack.Size}");
Console.WriteLine($"Текущая ёмкость = {stack.Capacity}");

Console.WriteLine("Добавление коллекции");
stack.PushRange([3, 4, 5]);

Console.WriteLine("Элементы стека:");
foreach (var item in stack) Console.WriteLine(item);

Console.WriteLine($"Содержит ли стек 5 = {stack.Contains(5)}");
Console.WriteLine($"Содержит ли стек 10 = {stack.Contains(10)}");

Console.WriteLine("Создание стека на основании колекции");
stack = new SmartStack<int>([10, 11, 12]);
Console.WriteLine("Элементы стека:");
foreach (var item in stack) Console.WriteLine(item);


/// <summary>
/// Класс "Умный стек" на основе массивов.
/// </summary>
class SmartStack<T> : IEnumerable<T>
{
    /// <summary>
    /// Размер стека (число хранящихся элементов).
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Ёмкость стека (фактический размер массива).
    /// </summary>
    public int Capacity { get; private set; }

    /// <summary>
    /// Массив для хранения элементов стека.
    /// </summary>
    private T[] _items;

    /// <summary>
    /// Создаёт стек с начальной ёмкостью = 4.
    /// </summary>
    public SmartStack()
    {
        Capacity = 4;
        Size = 0;
        _items = new T[Capacity];
    }

    /// <summary>
    /// Создаёт стек с указанной начальной ёмкостью.
    /// </summary>
    /// 
    /// <param name="capacity">Начальная ёмкость.</param>
    /// 
    /// <exception cref="ArgumentOutOfRangeException">
    /// Случается, когда указанная ёмкость меньше или равна 0.
    /// </exception>
    public SmartStack(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(capacity),
                "Capacity of stack must be positive"
            );
        }

        Capacity = capacity;
        Size = 0;
        _items = new T[Capacity];
    }

    /// <summary>
    /// Создаёт стек и заполняет его указанными значениями.
    /// </summary>
    /// 
    /// <param name="values">Коллекция для заполнения.</param>
    public SmartStack(IEnumerable<T> values)
        : this(values.Count())
    {
        foreach (var item in values) Push(item);
    }

    /// <summary>
    /// Помещает элемент на вершину стека.
    /// </summary>
    /// 
    /// <param name="item">Элемент для добавления.</param>
    public void Push(T item)
    {
        while (Size >= Capacity)
        {
            Capacity *= 2;
            Array.Resize(ref _items, Capacity);
        }

        _items[Size++] = item;
    }

    /// <summary>
    /// Помещает элементы коллекции на вершину стека.
    /// </summary>
    /// 
    /// <param name="range">Элементы коллекции.</param>
    public void PushRange(IEnumerable<T> range)
    {
        foreach (var item in range) Push(item);
    }

    /// <summary>
    /// Удаляет и возвращает элемент из вершины стека.
    /// </summary>
    /// 
    /// <returns>Элемент, извлечённый из вершины стека.</returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Случается, когда стек пуст.
    /// </exception>
    public T Pop()
    {
        if (Size == 0)
            throw new InvalidOperationException("Stack underflow");

        return _items[--Size];
    }

    /// <summary>
    /// Возвращает элемент из вершины стека, не удаляя его.
    /// </summary>
    /// 
    /// <returns>Элемент на вершине стека.</returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Случается, когда стек пуст.
    /// </exception>
    public T Peek()
    {
        if (Size == 0)
            throw new InvalidOperationException("Stack underflow");

        return _items[Size - 1];
    }

    /// <summary>
    /// Проверяет, содержит ли стек указанный элемент.
    /// </summary>
    /// 
    /// <param name="item">Проверяемый элемент.</param>
    /// 
    /// <returns>Результат проверки (true - содержит).</returns>
    public bool Contains(T item)
    {
        for (int i = 0; i < Size; i++)
        {
            if (_items[i]?.Equals(item) ?? item is null)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Метод обхода по стеку (начиная с вершины).
    /// </summary>
    /// 
    /// <returns>Объект для обхода стека.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = Size - 1; i >= 0; i--)
            yield return _items[i];
    }

    /// <summary>Реализация интерфейса IEnumerable.</summary>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Индексатор для доступа к стеку через оператор [].
    /// Возвращает/устанавливает элемент стека по индексу.
    /// </summary>
    /// 
    /// <param name="index">
    /// Индекс элемента (индекс 0 - вершина стека).
    /// </param>
    /// 
    /// <returns>Элемент стека по индексу.</returns>
    /// 
    /// <exception cref="ArgumentOutOfRangeException">
    /// Случается, когда индекс находится за пределами стека.
    /// </exception>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Size)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index)
                );
            }

            return _items[Size - index - 1];
        }
        set
        {
            if (index < 0 || index >= Size)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index)
                );
            }

            _items[Size - index - 1] = value;
        }
    }
}