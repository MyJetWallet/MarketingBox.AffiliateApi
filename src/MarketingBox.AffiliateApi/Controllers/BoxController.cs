using MarketingBox.AffiliateApi.Models.Partners;
using MarketingBox.AffiliateApi.Models.Partners.Requests;
using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MarketingBox.AffiliateApi.Models.Boxes;
using MarketingBox.AffiliateApi.Models.Boxes.Requests;

namespace MarketingBox.AffiliateApi.Controllers
{
    [ApiController]
    [Route("/api/boxes")]
    public class BoxController : ControllerBase
    {
        public BoxController()
        {
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Paginated<BoxModel, long>), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<BoxModel, long>>> SearchAsync(
            [FromQuery] BoxesSearchRequest request)
        {
            if (request.Limit < 1 || request.Limit > 1000)
            {
                ModelState.AddModelError($"{nameof(request.Limit)}", "Should not be in the range 1..1000");

                return BadRequest();
            }

            //return Ok(
            //    many.Select(MapToResponse)
            //        .ToArray()
            //        .Paginate(request, Url, x => x.Id));

            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(BoxModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BoxModel>> CreateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [FromBody] BoxCreateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPut("{boxId}")]
        [ProducesResponseType(typeof(BoxModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BoxModel>> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long boxId,
            [FromBody] BoxUpdateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{boxId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long boxId)
        {
            return Ok();
        }
    }
}