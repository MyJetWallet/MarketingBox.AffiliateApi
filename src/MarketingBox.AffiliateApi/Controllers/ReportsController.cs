using MarketingBox.AffiliateApi.Extensions;
using MarketingBox.AffiliateApi.Models.Reports;
using MarketingBox.AffiliateApi.Pagination;
using MarketingBox.Reporting.Service.Grpc;
using MarketingBox.Reporting.Service.Grpc.Models.Reports.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingBox.AffiliateApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Paginated<ReportModel, long>), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<ReportModel, long>>> SearchAsync(
            [FromQuery] Models.Reports.Requests.ReportSearchRequest request)
        {
            if (request.Limit < 1 || request.Limit > 1000)
            {
                ModelState.AddModelError($"{nameof(request.Limit)}", "Should be in the range 1..1000");

                return BadRequest();
            }

            var tenantId = this.GetTenantId();
            var response = await _reportService.SearchAsync(new ReportSearchRequest()
            {
                Asc = request.Order == PaginationOrder.Asc,
                Cursor = request.Cursor,
                FromDate = DateTime.SpecifyKind(request.FromDate, DateTimeKind.Utc),
                ToDate = DateTime.SpecifyKind(request.ToDate, DateTimeKind.Utc),
                Take = request.Limit,
                TenantId = tenantId
            });

            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            return Ok(
                response.Reports.Select(x => new ReportModel()
                    {
                        AffiliateId = x.AffiliateId,
                        Ctr = x.Ctr,
                        FtdCount = x.FtdCount ,
                        LeadCount = x.LeadCount,
                        Payout = x.Payout,
                        Revenue = x.Revenue,
                    })
                    .ToArray()
                    .Paginate(request, Url, x => x.AffiliateId));
        }
    }
}