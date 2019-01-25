using Hodgepodge.Data.Models.Fakebox;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hodgepodge.Service
{
    public class FakeboxService
    {
        public HttpClient HttpClient { get; }

        public FakeboxService(HttpClient httpClient) =>
            HttpClient = httpClient;

        public async Task<Response> GetFakeboxResponseAsync(Request request)
        {
            using (var response = await HttpClient
                .PostAsJsonAsync("/fakebox/check", request)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                using (var content = response.Content)
                {
                    return await content
                        .ReadAsAsync<Response>()
                        .ConfigureAwait(false);
                }
            }
        }
    }
}
