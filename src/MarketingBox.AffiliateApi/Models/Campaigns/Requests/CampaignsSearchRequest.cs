using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AffiliateApi.Models.Campaigns.Requests
{
    public class CampaignsSearchRequest : PaginationRequest<long?>
    {
        [FromQuery(Name = "id")]
        public long? Id { get; set; }
    }
}
