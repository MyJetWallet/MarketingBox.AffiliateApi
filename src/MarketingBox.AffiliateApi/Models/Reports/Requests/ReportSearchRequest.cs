using System;
using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AffiliateApi.Models.Reports.Requests
{
    public class ReportSearchRequest : PaginationRequest<long?>
    {
        [FromQuery(Name = "fromDate")]
        public DateTime FromDate { get; set; }

        [FromQuery(Name = "toDate")]
        public DateTime ToDate { get; set; }
    }
}
