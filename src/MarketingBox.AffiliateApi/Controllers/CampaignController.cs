using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MarketingBox.Affiliate.Service.Grpc;
using MarketingBox.Affiliate.Service.Grpc.Models.Campaigns.Requests;
using MarketingBox.AffiliateApi.Extensions;
using MarketingBox.AffiliateApi.Models.Campaigns;
using MarketingBox.AffiliateApi.Models.Campaigns.Requests;
using MarketingBox.AffiliateApi.Models.Partners;
using CampaignCreateRequest = MarketingBox.AffiliateApi.Models.Campaigns.Requests.CampaignCreateRequest;
using CampaignUpdateRequest = MarketingBox.AffiliateApi.Models.Campaigns.Requests.CampaignUpdateRequest;

namespace MarketingBox.AffiliateApi.Controllers
{
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
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
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
                    Currency = request.Payout.Currency.MapEnum<Affiliate.Service.Grpc.Models.Common.Currency>(),
                    Amount = request.Payout.Amount,
                    Plan = request.Payout.Plan.MapEnum<Affiliate.Service.Grpc.Models.Campaigns.Plan>()
                },
                Status = request.Status.MapEnum<Affiliate.Service.Grpc.Models.Campaigns.CampaignStatus>(),
                Privacy = request.Privacy.MapEnum<Affiliate.Service.Grpc.Models.Campaigns.CampaignPrivacy>(),
                Revenue = new Affiliate.Service.Grpc.Models.Campaigns.Revenue()
                {
                    Currency = request.Revenue.Currency.MapEnum<Affiliate.Service.Grpc.Models.Common.Currency>(),
                    Amount = request.Revenue.Amount,
                    Plan = request.Revenue.Plan.MapEnum<Affiliate.Service.Grpc.Models.Campaigns.Plan>()
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
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
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
                    Currency = request.Payout.Currency.MapEnum<Affiliate.Service.Grpc.Models.Common.Currency>(),
                    Amount = request.Payout.Amount,
                    Plan = request.Payout.Plan.MapEnum<Affiliate.Service.Grpc.Models.Campaigns.Plan>()
                },
                Status = request.Status.MapEnum<Affiliate.Service.Grpc.Models.Campaigns.CampaignStatus>(),
                Privacy = request.Privacy.MapEnum<Affiliate.Service.Grpc.Models.Campaigns.CampaignPrivacy>(),
                Revenue = new Affiliate.Service.Grpc.Models.Campaigns.Revenue()
                {
                    Currency = request.Revenue.Currency.MapEnum<Affiliate.Service.Grpc.Models.Common.Currency>(),
                    Amount = request.Revenue.Amount,
                    Plan = request.Revenue.Plan.MapEnum<Affiliate.Service.Grpc.Models.Campaigns.Plan>()
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
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long campaignId)
        {
            var tenantId = this.GetTenantId();
            var response = await _campaignService.DeleteAsync(new Affiliate.Service.Grpc.Models.Campaigns.Requests.CampaignDeleteRequest()
            {
                CampaignId = campaignId
            });

            return MapToResponseEmpty(response);
        }

        public ActionResult MapToResponse(Affiliate.Service.Grpc.Models.Campaigns.CampaignResponse response)
        {
            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            return Ok(new CampaignModel()
            {
               Name = response.Campaign.Name,
               BrandId = response.Campaign.BrandId,
               Id = response.Campaign.Id,
               Payout = new Payout()
               {
                   Currency = response.Campaign.Payout.Currency.MapEnum<Currency>(),
                   Amount = response.Campaign.Payout.Amount,
                   Plan = response.Campaign.Payout.Plan.MapEnum<Plan>()
               },
               Privacy = response.Campaign.Privacy.MapEnum<CampaignPrivacy>(),
               Revenue = new Revenue()
               {
                   Currency = response.Campaign.Revenue.Currency.MapEnum<Currency>(),
                   Amount = response.Campaign.Revenue.Amount,
                   Plan = response.Campaign.Revenue.Plan.MapEnum<Plan>()
               },
               Status = response.Campaign.Status.MapEnum<CampaignStatus>(),
               Sequence = response.Campaign.Sequence
            });
        }

        public ActionResult MapToResponseEmpty(Affiliate.Service.Grpc.Models.Campaigns.CampaignResponse response)
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