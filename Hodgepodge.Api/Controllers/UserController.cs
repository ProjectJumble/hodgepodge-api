using Hodgepodge.Data.Models;
using Hodgepodge.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hodgepodge.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public UserController(
            IHttpContextAccessor httpContextAccessor,
            //ILogger logger,
            IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor ??
                throw new ArgumentNullException(nameof(httpContextAccessor));

            //_logger = logger ??
            //    throw new ArgumentNullException(nameof(logger));

            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet, ActionName("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Contracts.User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SerializableError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                var token = Guid.NewGuid();

                var remoteIpAddress = _httpContextAccessor.HttpContext.RemoteIpAddress();

                await _userRepository
                    .UpsertAsync(new User
                    {
                        Token = token,
                        RemoteIpAddress = remoteIpAddress
                    })
                    .ConfigureAwait(false);

                return Ok(new Contracts.User { Token = token.ToString() });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost, ActionName("update")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Contracts.User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(SerializableError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromBody] Contracts.User user)
        {
            try
            {
                var userDocument = _userRepository.Get(new Guid(user.Token));

                if (userDocument == default(User))
                    return NotFound();

                if (userDocument.Banned)
                    return Forbid();

                //userDocument.SetPropertyValue("lastSeenTimeStamp", DateTime.UtcNow);

                //await _userRepository
                //    .UpsertAsync(userDocument)
                //    .ConfigureAwait(false);

                return Ok(userDocument);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
