using WikipediaSearch.Models;

namespace WikipediaSearch.UI;

/// <summary>
/// Выводит результаты поиска и сообщения программы.
/// </summary>
public class ConsoleOutput
{
    public void PrintGreeting()
    {
        Console.WriteLine("===== Поиск в Википедии =====");
        Console.WriteLine("Выберите статью, чтобы открыть её в браузере.");
        Console.WriteLine();
    }

    public void PrintResults(IReadOnlyList<SearchResult> results)
    {
        Console.WriteLine();

        if (results.Count == 0)
        {
            Console.WriteLine("Ничего не найдено. Попробуйте другой запрос.");
            return;
        }

        Console.WriteLine("Найденные статьи (до 10 результатов):");
        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {results[i].Title}");
        }
    }

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }
}
