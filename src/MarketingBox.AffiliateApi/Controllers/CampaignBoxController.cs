using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MarketingBox.AffiliateApi.Models.CampaignBoxes;
using MarketingBox.AffiliateApi.Models.CampaignBoxes.Requests;
using MarketingBox.AffiliateApi.Models.Campaigns;
using MarketingBox.AffiliateApi.Models.Campaigns.Requests;

namespace MarketingBox.AffiliateApi.Controllers
{
    [ApiController]
    [Route("/api/campaign-boxes")]
    public class CampaignBoxController : ControllerBase
    {
        public CampaignBoxController()
        {
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Paginated<CampaignBoxModel, long>), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<CampaignBoxModel, long>>> SearchAsync(
            [FromQuery] CampaignBoxesSearchRequest request)
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
        [ProducesResponseType(typeof(CampaignBoxModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<CampaignBoxModel>> CreateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [FromBody] CampaignBoxCreateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPut("{campaignBoxId}")]
        [ProducesResponseType(typeof(CampaignBoxModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<CampaignBoxModel>> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long campaignBoxId,
            [FromBody] CampaignBoxUpdateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{campaignBoxId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long campaignBoxId)
        {
            return Ok();
        }
    }
}