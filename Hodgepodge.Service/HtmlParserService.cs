using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hodgepodge.Service
{
    public class HtmlParserService : IHtmlParserService
    {
        private readonly HttpClient _httpClient;

        public HtmlParserService() =>
            _httpClient = new HttpClient();

        public async Task<string> ParseAsync(string url)
        {
            using (var response = await _httpClient
                .GetAsync(url)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                using (var content = response.Content)
                {
                    var html = await content
                        .ReadAsStringAsync()
                        .ConfigureAwait(false);

                    var document = new HtmlDocument();
                    document.LoadHtml(html);

                    return document
                        .DocumentNode
                        .SelectSingleNode("//body")
                        .InnerHtml;
                }
            }
        }
    }
}
