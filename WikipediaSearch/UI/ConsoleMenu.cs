using System.Text.Json;
using WikipediaSearch.Models;
using WikipediaSearch.Services;

namespace WikipediaSearch.UI;

/// <summary>
/// Управляет поиском, выбором статьи и её открытием.
/// </summary>
public class ConsoleMenu
{
    private readonly WikipediaService _service;
    private readonly BrowserService _browser;
    private readonly ConsoleInput _input;
    private readonly ConsoleOutput _output;

    public ConsoleMenu(WikipediaService service, BrowserService browser,
        ConsoleInput input, ConsoleOutput output)
    {
        _service = service;
        _browser = browser;
        _input = input;
        _output = output;
    }

    public async Task RunAsync()
    {
        _output.PrintGreeting();

        while (true)
        {
            string? searchText = _input.ReadSearchText();
            if (searchText == null)
            {
                break;
            }

            try
            {
                _output.PrintMessage("Поиск статей...");
                List<SearchResult> results = await _service.SearchAsync(searchText);
                _output.PrintResults(results);

                if (results.Count == 0)
                {
                    continue;
                }

                int? number = _input.ReadArticleNumber(results.Count);
                if (number == null)
                {
                    break;
                }
                if (number == 0)
                {
                    continue;
                }

                SearchResult article = results[number.Value - 1];
                _browser.OpenArticle(article);
                _output.PrintMessage($"Статья открывается в браузере: {article.Title}");
            }
            catch (HttpRequestException)
            {
                _output.PrintMessage("Не удалось связаться с Википедией. Проверьте подключение к интернету.");
            }
            catch (OperationCanceledException)
            {
                _output.PrintMessage("Время ожидания ответа истекло. Попробуйте ещё раз.");
            }
            catch (JsonException)
            {
                _output.PrintMessage("Википедия вернула некорректный ответ. Попробуйте позже.");
            }
            catch (ArgumentException ex)
            {
                _output.PrintMessage($"Некорректные данные: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                _output.PrintMessage(ex.Message);
            }
            catch (IOException)
            {
                _output.PrintMessage("Ошибка чтения ответа сервера. Попробуйте ещё раз.");
            }

            _output.PrintMessage("");
        }

        _output.PrintMessage("Работа завершена.");
    }
}
