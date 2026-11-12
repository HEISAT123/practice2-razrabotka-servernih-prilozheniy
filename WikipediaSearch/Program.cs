using WikipediaSearch.Services;
using WikipediaSearch.UI;

using HttpClient client = new HttpClient();
client.Timeout = TimeSpan.FromSeconds(15);
client.DefaultRequestHeaders.UserAgent.ParseAdd("WikipediaSearch/1.0");

WikipediaResponseParser parser = new WikipediaResponseParser();
WikipediaService service = new WikipediaService(client, parser);
BrowserService browser = new BrowserService();
ConsoleInput input = new ConsoleInput();
ConsoleOutput output = new ConsoleOutput();
ConsoleMenu menu = new ConsoleMenu(service, browser, input, output);

await menu.RunAsync();