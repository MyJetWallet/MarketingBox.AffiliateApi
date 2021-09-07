using MarketingBox.AffiliateApi.Models.Partners;
using MarketingBox.AffiliateApi.Models.Partners.Requests;
using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MarketingBox.AffiliateApi.Controllers
{
    [ApiController]
    [Route("/api/partners")]
    public class PartnerController : ControllerBase
    {
        public PartnerController()
        {
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Paginated<PartnerModel, long>), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<PartnerModel, long>>> SearchAsync(
            [FromQuery] PartnersSearchRequest request)
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
        [ProducesResponseType(typeof(PartnerModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<PartnerModel>> CreateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [FromBody] PartnerCreateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPut("{partnerId}")]
        [ProducesResponseType(typeof(PartnerModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<PartnerModel>> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long partnerId,
            [FromBody] PartnerUpdateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{partnerId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long partnerId)
        {
            return Ok();
        }
    }
}