using Hodgepodge.Data.Enums.Review;
using Hodgepodge.Data.Models;
using Hodgepodge.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hodgepodge.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository) =>
            _reviewRepository = reviewRepository ??
                throw new ArgumentNullException(nameof(reviewRepository));

        [HttpPost, ActionName("get")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SerializableError), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult Get([FromBody] Contracts.Review contract)
        {
            try
            {
                if (contract == default(Contracts.Review))
                    return BadRequest(nameof(contract));

                if (contract.Token == default(string))
                    return BadRequest(nameof(contract.Token));

                if (contract.Url == default(string))
                    return BadRequest(nameof(contract.Url));

                var review = new Review
                {
                    Token = new Guid(contract.Token),
                    Url = Uri.EscapeDataString(contract.Url)
                };

                // Disallow submitting multiple reviews.
                if (_reviewRepository.Get(review) == default(Review))
                    return NoContent();

                //return Forbid();
                return Conflict();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost, ActionName("post")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SerializableError), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PostAsync([FromBody] Contracts.Review contract)
        {
            try
            {
                if (contract == default(Contracts.Review))
                    return BadRequest(nameof(contract));

                if (contract.IdeologicalConsistency == default(int))
                    return BadRequest(nameof(contract.IdeologicalConsistency));

                if (contract.Rating == default(int))
                    return BadRequest(nameof(contract.Rating));

                if (contract.Token == default(string))
                    return BadRequest(nameof(contract.Token));

                if (contract.Url == default(string))
                    return BadRequest(nameof(contract.Url));

                var review = new Review
                {
                    Token = new Guid(contract.Token),
                    IdeologicalConsistency = (IdeologicalConsistency)contract.IdeologicalConsistency,
                    Url = Uri.EscapeDataString(contract.Url),
                    Rating = (Rating)contract.Rating
                };

                // Disallow submitting multiple reviews.
                if (_reviewRepository.Get(review) == default(Review))
                {
                    await _reviewRepository
                        .UpsertAsync(review)
                        .ConfigureAwait(false);

                    return NoContent();
                }

                //return Forbid();
                return Conflict();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
