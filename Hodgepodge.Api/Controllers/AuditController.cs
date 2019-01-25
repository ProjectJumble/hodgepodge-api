using Hodgepodge.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hodgepodge.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class AuditController : Controller
    {
        private readonly FakeboxService _fakeboxService;
        private readonly IConfiguration _configuration;
        private readonly IHtmlParserService _htmlParserService;
        private readonly IMemoryCache _memoryCache;
        private readonly IResourceService _resourceService;

        private readonly double _memoryCacheEntrySlidingExpiration;

        public AuditController(
            FakeboxService fakeboxService,
            IConfiguration configuration,
            IHtmlParserService htmlParserService,
            IMemoryCache memoryCache,
            IResourceService resourceService)
        {
            _fakeboxService = fakeboxService ??
                throw new ArgumentNullException(nameof(fakeboxService));

            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));

            _htmlParserService = htmlParserService ??
                throw new ArgumentNullException(nameof(htmlParserService));

            _memoryCache = memoryCache ??
                throw new ArgumentNullException(nameof(memoryCache));

            _resourceService = resourceService ??
                throw new ArgumentNullException(nameof(resourceService));

            _memoryCacheEntrySlidingExpiration =
                double.Parse(_configuration["MemoryCacheEntryOptions:SlidingExpirationHours"]);
        }

        [HttpPost, ActionName("fakebox")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Contracts.Fakebox.Request), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SerializableError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetFakeboxAsync([FromBody] Contracts.Fakebox.Request request)
        {
            try
            {
                if (request == default(Contracts.Fakebox.Request))
                    return BadRequest(nameof(request));

                //if (request.Content == default(string))
                //    return BadRequest(nameof(request.Content));

                if (request.Title == default(string))
                    return BadRequest(nameof(request.Title));

                if (request.Url == default(string))
                    return BadRequest(nameof(request.Url));

                if (!_memoryCache.TryGetValue<Contracts.Fakebox.Response>(request.Url, out var response))
                {
                    // How to introduce an unnecessary type cast, and decrease
                    // performance where (in)appropriate? Look no further.
                    var fakeboxRequest = JsonConvert
                        .DeserializeObject<Data.Models.Fakebox.Request>
                            (JsonConvert.SerializeObject(request));

                    var fakeboxResponse = await _fakeboxService
                        .GetFakeboxResponseAsync(fakeboxRequest)
                        .ConfigureAwait(true);

                    response = JsonConvert
                        .DeserializeObject<Contracts.Fakebox.Response>
                            (JsonConvert.SerializeObject(fakeboxResponse));

                    var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(_memoryCacheEntrySlidingExpiration));

                    _memoryCache.Set(request.Url, response, memoryCacheEntryOptions);
                }

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
