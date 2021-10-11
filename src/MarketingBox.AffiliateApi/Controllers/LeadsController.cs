using MarketingBox.AffiliateApi.Extensions;
using MarketingBox.AffiliateApi.Pagination;
using MarketingBox.Reporting.Service.Grpc;
using MarketingBox.Reporting.Service.Grpc.Models.Leads;
using MarketingBox.Reporting.Service.Grpc.Models.Reports.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingBox.AffiliateApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/leads")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _leadService;

        public LeadsController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Paginated<LeadModel, long>), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<LeadModel, long>>> SearchAsync(
            [FromQuery] Models.Reports.Requests.LeadSearchRequest request)
        {
            if (request.Limit < 1 || request.Limit > 1000)
            {
                ModelState.AddModelError($"{nameof(request.Limit)}", "Should be in the range 1..1000");

                return BadRequest();
            }

            var tenantId = this.GetTenantId();
            var response = await _leadService.SearchAsync(new LeadSearchRequest()
            {
                Asc = request.Order == PaginationOrder.Asc,
                Cursor = request.Cursor,
                Take = request.Limit,
                TenantId = tenantId,
                AffiliateId = request.AffiliateId
            });

            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            return Ok(
                response.Leads.Select(x => new LeadModel()
                    {
                        AdditionalInfo = new LeadAdditionalInfo()
                        {
                            So = x.AdditionalInfo.So,
                            Sub = x.AdditionalInfo.Sub,
                            Sub1 = x.AdditionalInfo.Sub1,
                            Sub10 = x.AdditionalInfo.Sub10,
                            Sub2 = x.AdditionalInfo.Sub2,
                            Sub3 = x.AdditionalInfo.Sub3,
                            Sub4 = x.AdditionalInfo.Sub4,
                            Sub5 = x.AdditionalInfo.Sub5,
                            Sub6 = x.AdditionalInfo.Sub6,
                            Sub7 = x.AdditionalInfo.Sub7,
                            Sub8 = x.AdditionalInfo.Sub8,
                            Sub9 = x.AdditionalInfo.Sub9
                        },
                        CallStatus = x.CallStatus,
                        GeneralInfo = new LeadGeneralInfo()
                        {
                            Email = x.GeneralInfo.Email,
                            CreatedAt = x.GeneralInfo.CreatedAt,
                            FirstName = x.GeneralInfo.FirstName,
                            Ip = x.GeneralInfo.Ip,
                            LastName = x.GeneralInfo.LastName,
                            Phone = x.GeneralInfo.Phone
                        },
                        LeadId = x.LeadId,
                        RouteInfo = new LeadRouteInfo()
                        {
                            AffiliateId = x.RouteInfo.AffiliateId,
                            BoxId = x.RouteInfo.BoxId,
                            BrandId = x.RouteInfo.BrandId,
                            CampaignId = x.RouteInfo.CampaignId
                        },
                        Sequence = x.Sequence,
                        TenantId = x.TenantId,
                        Type = x.Type,
                        UniqueId = x.UniqueId
                    })
                    .ToArray()
                    .Paginate(request, Url, x => x.LeadId));
        }
    }
}