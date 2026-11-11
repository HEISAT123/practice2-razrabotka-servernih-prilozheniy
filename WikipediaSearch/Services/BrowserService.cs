using System.ComponentModel;
using System.Diagnostics;
using WikipediaSearch.Models;

namespace WikipediaSearch.Services;

/// <summary>
/// Открывает выбранную статью в браузере по умолчанию.
/// </summary>
public class BrowserService
{
    public string GetArticleUrl(SearchResult article)
    {
        return $"https://ru.wikipedia.org/w/index.php?curid={article.PageId}";
    }

    public void OpenArticle(SearchResult article)
    {
        string url = GetArticleUrl(article);

        try
        {
            using Process? process = Process.Start(new ProcessStartInfo(url)
            {
                UseShellExecute = true
            });
        }
        catch (Exception ex) when (ex is Win32Exception or InvalidOperationException or NotSupportedException)
        {
            throw new InvalidOperationException(
                $"Не удалось открыть браузер. Откройте ссылку вручную: {url}", ex);
        }
    }
}
