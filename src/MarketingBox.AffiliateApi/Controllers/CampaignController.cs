using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MarketingBox.AffiliateApi.Models.Campaigns;
using MarketingBox.AffiliateApi.Models.Campaigns.Requests;

namespace MarketingBox.AffiliateApi.Controllers
{
    [ApiController]
    [Route("/api/campaigns")]
    public class CampaignController : ControllerBase
    {
        public CampaignController()
        {
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Paginated<CampaignModel, long>), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<CampaignModel, long>>> SearchAsync(
            [FromQuery] CampaignsSearchRequest request)
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
        [ProducesResponseType(typeof(CampaignModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<CampaignModel>> CreateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [FromBody] CampaignCreateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPut("{campaignId}")]
        [ProducesResponseType(typeof(CampaignModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<CampaignModel>> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long campaignId,
            [FromBody] CampaignUpdateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{campaignId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long campaignId)
        {
            return Ok();
        }
    }
}