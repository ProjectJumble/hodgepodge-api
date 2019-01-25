using Hodgepodge.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Hodgepodge.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ServiceController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IDbInitializer _dbInitializer;

        private readonly string _token;

        public ServiceController(
            IConfiguration configuration,
            IDbInitializer dbInitializer)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));

            _dbInitializer = dbInitializer ??
                throw new ArgumentNullException(nameof(dbInitializer));

            _token = _configuration["Service:Token"];
        }

        [HttpPost, ActionName("initialize")]
        public async Task<IActionResult> InitializeAsync([FromBody] Contracts.Service service)
        {
            try
            {
                if (service == default(Contracts.Service))
                    return BadRequest(nameof(service));

                if (service.Token == default(string))
                    return BadRequest(nameof(service.Token));

                if (service.Token != _token)
                    return Forbid();

                await _dbInitializer.InitializeAsync().ConfigureAwait(false);

                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost, ActionName("seed")]
        public async Task<IActionResult> SeedAsync([FromBody] Contracts.Service service)
        {
            try
            {
                if (service == default(Contracts.Service))
                    return BadRequest(nameof(service));

                if (service.Token == default(string))
                    return BadRequest(nameof(service.Token));

                if (service.Token != _token)
                    return Forbid();

                if (service.SeedTimeStamp == default(string))
                {
                    await _dbInitializer.SeedAsync().ConfigureAwait(false);
                }
                else
                {
                    await _dbInitializer
                        .SeedAsync(DateTime.Parse(service.SeedTimeStamp))
                        .ConfigureAwait(false);
                }

                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
