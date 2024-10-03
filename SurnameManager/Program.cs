public class InvalidInputException : Exception
{
    public InvalidInputException(string message) : base(message) { }
}

class Program
{
    // Делегат для события сортировки
    public delegate void SortEventHandler(List<string> surnames);

    // Класс для управления списком фамилий и событием сортировки
    public class SurnameManager
    {
        public List<string> Surnames { get; set; } = new List<string>();

        public event SortEventHandler SortEvent;

        // Метод для сортировки списка
        public void Sort(int sortOption)
        {
            if (sortOption == 1)
            {
                Surnames.Sort();
            }
            else if (sortOption == 2)
            {
                Surnames.Sort((x, y) => y.CompareTo(x));
            }
            else
            {
                throw new InvalidInputException("Неверный ввод. Введите 1 или 2.");
            }

            // Вызов события сортировки
            SortEvent?.Invoke(Surnames);
        }
    }

    static void Main(string[] args)
    {
        // Инициализация списка фамилий
        SurnameManager surnameManager = new SurnameManager();
        surnameManager.Surnames.Add("Иванов");
        surnameManager.Surnames.Add("Петров");
        surnameManager.Surnames.Add("Сидоров");
        surnameManager.Surnames.Add("Кузнецов");
        surnameManager.Surnames.Add("Смирнов");

        // Подписка на событие сортировки
        surnameManager.SortEvent += OnSort;

        // Ввод данных от пользователя
        while (true)
        {
            Console.WriteLine("Введите 1 для сортировки А-Я, 2 для сортировки Я-А, или 0 для выхода:");
            try
            {
                int sortOption = int.Parse(Console.ReadLine());
                if (sortOption == 0)
                {
                    break;
                }

                // Вызов метода сортировки
                surnameManager.Sort(sortOption);
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный формат ввода. Введите число.");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Обработка ввода завершена.");
            }
        }

        Console.ReadKey();
    }

    // Обработчик события сортировки
    private static void OnSort(List<string> surnames)
    {
        Console.WriteLine("\nОтсортированный список фамилий:");
        foreach (string surname in surnames)
        {
            Console.WriteLine(surname);
        }
    }
}