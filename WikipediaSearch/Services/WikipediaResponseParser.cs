using System.Text.Json;
using WikipediaSearch.Models;

namespace WikipediaSearch.Services;

/// <summary>
/// Преобразует ответ API в список найденных статей.
/// </summary>
public class WikipediaResponseParser
{
    public List<SearchResult> Parse(string json)
    {
        using JsonDocument document = JsonDocument.Parse(json);
        JsonElement root = document.RootElement;

        if (root.ValueKind != JsonValueKind.Object)
        {
            throw new JsonException("Ожидался объект с результатами поиска.");
        }
        if (root.TryGetProperty("error", out _))
        {
            throw new InvalidOperationException("Википедия не смогла выполнить поиск. Попробуйте другой запрос.");
        }
        if (!root.TryGetProperty("query", out JsonElement query)
            || query.ValueKind != JsonValueKind.Object
            || !query.TryGetProperty("search", out JsonElement search)
            || search.ValueKind != JsonValueKind.Array)
        {
            throw new JsonException("В ответе отсутствует список статей.");
        }

        List<SearchResult> results = new();

        foreach (JsonElement item in search.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Object
                || !item.TryGetProperty("pageid", out JsonElement pageId)
                || pageId.ValueKind != JsonValueKind.Number
                || !pageId.TryGetInt32(out int id)
                || id <= 0
                || !item.TryGetProperty("title", out JsonElement title)
                || title.ValueKind != JsonValueKind.String
                || string.IsNullOrWhiteSpace(title.GetString()))
            {
                throw new JsonException("Получены некорректные данные статьи.");
            }

            results.Add(new SearchResult(id, title.GetString()!));
        }

        return results;
    }
}
