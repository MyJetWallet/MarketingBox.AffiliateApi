using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MarketingBox.Affiliate.Service.Grpc;
using MarketingBox.Affiliate.Service.Grpc.Models.Campaigns.Requests;
using MarketingBox.AffiliateApi.Extensions;
using MarketingBox.AffiliateApi.Models.Campaigns;
using MarketingBox.AffiliateApi.Models.Campaigns.Requests;
using MarketingBox.AffiliateApi.Models.Partners;
using Microsoft.AspNetCore.Authorization;
using CampaignCreateRequest = MarketingBox.AffiliateApi.Models.Campaigns.Requests.CampaignCreateRequest;
using CampaignUpdateRequest = MarketingBox.AffiliateApi.Models.Campaigns.Requests.CampaignUpdateRequest;

namespace MarketingBox.AffiliateApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/campaigns")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
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

            var tenantId = this.GetTenantId();
            var status = request.Status?.MapEnum<MarketingBox.Affiliate.Service.Domain.Models.Campaigns.CampaignStatus>();

            var response = await _campaignService.SearchAsync(new CampaignSearchRequest()
            {
                Asc = request.Order == PaginationOrder.Asc,
                Cursor = request.Cursor,
                CampaignId = request.Id,
                BrandId = request.BrandId,
                Name = request.Name,
                Status = status,
                Take = request.Limit,
                TenantId = tenantId
            });

            return Ok(
                response.Campaigns.Select(Map)
                    .ToArray()
                    .Paginate(request, Url, x => x.Id));
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet("{campaignId}")]
        [ProducesResponseType(typeof(CampaignModel), StatusCodes.Status200OK)]

        public async Task<ActionResult<CampaignModel>> GetAsync(
            [FromRoute, Required] long campaignId)
        {
            var tenantId = this.GetTenantId();
            var response =await _campaignService.GetAsync(new CampaignGetRequest() {CampaignId = campaignId});

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(CampaignModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<CampaignModel>> CreateAsync(
            [FromBody] CampaignCreateRequest request)
        {
            var tenantId = this.GetTenantId();
            var response = await _campaignService.CreateAsync(new Affiliate.Service.Grpc.Models.Campaigns.Requests.CampaignCreateRequest()
            {
                BrandId = request.BrandId,
                Name = request.Name,
                TenantId = tenantId,
                Payout = new Affiliate.Service.Grpc.Models.Campaigns.Payout()
                {
                    Currency = request.Payout.Currency.MapEnum<MarketingBox.Affiliate.Service.Domain.Models.Common.Currency>(),
                    Amount = request.Payout.Amount,
                    Plan = request.Payout.Plan.MapEnum< MarketingBox.Affiliate.Service.Domain.Models.Campaigns.Plan>()
                },
                Status = request.Status.MapEnum<  MarketingBox.Affiliate.Service.Domain.Models.Campaigns.CampaignStatus>(),
                Privacy = request.Privacy.MapEnum< MarketingBox.Affiliate.Service.Domain.Models.Campaigns.CampaignPrivacy >(),
                Revenue = new Affiliate.Service.Grpc.Models.Campaigns.Revenue()
                {
                    Currency = request.Revenue.Currency.MapEnum< MarketingBox.Affiliate.Service.Domain.Models.Common.Currency>(),
                    Amount = request.Revenue.Amount,
                    Plan = request.Revenue.Plan.MapEnum<MarketingBox.Affiliate.Service.Domain.Models.Campaigns.Plan>()
                }
            });

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPut("{campaignId}")]
        [ProducesResponseType(typeof(CampaignModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<CampaignModel>> UpdateAsync(
            [Required, FromRoute] long campaignId,
            [FromBody] CampaignUpdateRequest request)
        {
            var tenantId = this.GetTenantId();
            var response = await _campaignService.UpdateAsync(new Affiliate.Service.Grpc.Models.Campaigns.Requests.CampaignUpdateRequest()
            {
                Id = campaignId,
                Sequence = request.Sequence,
                BrandId = request.BrandId,
                Name = request.Name,
                TenantId = tenantId,
                Payout = new Affiliate.Service.Grpc.Models.Campaigns.Payout()
                {
                    Currency = request.Payout.Currency.MapEnum<MarketingBox.Affiliate.Service.Domain.Models.Common.Currency>(),
                    Amount = request.Payout.Amount,
                    Plan = request.Payout.Plan.MapEnum<MarketingBox.Affiliate.Service.Domain.Models.Campaigns.Plan>()
                },
                Status = request.Status.MapEnum<MarketingBox.Affiliate.Service.Domain.Models.Campaigns.CampaignStatus>(),
                Privacy = request.Privacy.MapEnum<MarketingBox.Affiliate.Service.Domain.Models.Campaigns.CampaignPrivacy>(),
                Revenue = new Affiliate.Service.Grpc.Models.Campaigns.Revenue()
                {
                    Currency = request.Revenue.Currency.MapEnum<MarketingBox.Affiliate.Service.Domain.Models.Common.Currency>(),
                    Amount = request.Revenue.Amount,
                    Plan = request.Revenue.Plan.MapEnum<MarketingBox.Affiliate.Service.Domain.Models.Campaigns.Plan>()
                }
            });

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{campaignId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteAsync(
            [Required, FromRoute] long campaignId)
        {
            var tenantId = this.GetTenantId();
            var response = await _campaignService.DeleteAsync(new Affiliate.Service.Grpc.Models.Campaigns.Requests.CampaignDeleteRequest()
            {
                CampaignId = campaignId
            });

            return MapToResponseEmpty(response);
        }

        private ActionResult MapToResponse(Affiliate.Service.Grpc.Models.Campaigns.CampaignResponse response)
        {
            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            if (response.Campaign == null)
                return NotFound();

            return Ok(Map(response.Campaign));
        }

        private static CampaignModel Map(Affiliate.Service.Grpc.Models.Campaigns.Campaign campaign)
        {
            return new CampaignModel()
            {
                Name = campaign.Name,
                BrandId = campaign.BrandId,
                Id = campaign.Id,
                Payout = new Payout()
                {
                    Currency = campaign.Payout.Currency.MapEnum<Currency>(),
                    Amount = campaign.Payout.Amount,
                    Plan = campaign.Payout.Plan.MapEnum<Plan>()
                },
                Privacy = campaign.Privacy.MapEnum<CampaignPrivacy>(),
                Revenue = new Revenue()
                {
                    Currency = campaign.Revenue.Currency.MapEnum<Currency>(),
                    Amount = campaign.Revenue.Amount,
                    Plan = campaign.Revenue.Plan.MapEnum<Plan>()
                },
                Status = campaign.Status.MapEnum<CampaignStatus>(),
                Sequence = campaign.Sequence
            };
        }

        private ActionResult MapToResponseEmpty(Affiliate.Service.Grpc.Models.Campaigns.CampaignResponse response)
        {
            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}