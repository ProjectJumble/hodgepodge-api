using Hodgepodge.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hodgepodge.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SurveyController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyController(ISurveyRepository surveyRepository) =>
            _surveyRepository = surveyRepository ??
                throw new ArgumentNullException(nameof(surveyRepository));

        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
