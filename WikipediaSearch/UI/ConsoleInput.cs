namespace WikipediaSearch.UI;

/// <summary>
/// Читает и проверяет ввод пользователя.
/// </summary>
public class ConsoleInput
{
    public string? ReadSearchText()
    {
        while (true)
        {
            Console.Write("Введите запрос (0 — выход): ");
            string? input = Console.ReadLine();

            if (input == null || input.Trim() == "0")
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Поисковый запрос не может быть пустым.");
        }
    }

    /// <summary>
    /// Возвращает номер статьи, 0 для нового поиска или null при завершении ввода.
    /// </summary>
    public int? ReadArticleNumber(int count)
    {
        while (true)
        {
            Console.Write("Номер статьи (0 — новый поиск): ");
            string? input = Console.ReadLine();

            if (input == null)
            {
                return null;
            }
            if (int.TryParse(input, out int number) && number >= 0 && number <= count)
            {
                return number;
            }

            Console.WriteLine($"Введите целое число от 0 до {count}.");
        }
    }
}
