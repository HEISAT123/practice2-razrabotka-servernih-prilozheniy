namespace WikipediaSearch.Models;

/// <summary>
/// Найденная статья Википедии.
/// </summary>
public class SearchResult
{
    public int PageId { get; }
    public string Title { get; }

    public SearchResult(int pageId, string title)
    {
        if (pageId <= 0)
        {
            throw new ArgumentException("Id статьи должен быть положительным числом.", nameof(pageId));
        }
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Название статьи не может быть пустым.", nameof(title));
        }

        PageId = pageId;
        Title = title.Trim();
    }
}
