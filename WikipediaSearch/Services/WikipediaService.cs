using WikipediaSearch.Models;

namespace WikipediaSearch.Services;

/// <summary>
/// Выполняет поиск через API русской Википедии.
/// </summary>
public class WikipediaService
{
    private readonly HttpClient _client;
    private readonly WikipediaResponseParser _parser;

    public WikipediaService(HttpClient client, WikipediaResponseParser parser)
    {
        _client = client;
        _parser = parser;
    }

    public async Task<List<SearchResult>> SearchAsync(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            throw new ArgumentException("Поисковый запрос не может быть пустым.", nameof(searchText));
        }

        string encodedText = Uri.EscapeDataString(searchText.Trim());
        string url = "https://ru.wikipedia.org/w/api.php"
            + "?action=query&list=search&utf8=1&format=json&srlimit=10&srprop="
            + "&srsearch=" + encodedText;

        string json = await _client.GetStringAsync(url);
        return _parser.Parse(json);
    }
}
