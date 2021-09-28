using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AffiliateApi.Models.Campaigns.Requests
{
    public class CampaignsSearchRequest : PaginationRequest<long?>
    {
        [FromQuery(Name = "id")]
        public long? Id { get; set; }

        [FromQuery(Name = "name")]
        public string Name { get; set; }

        [FromQuery(Name = "brandId")]
        public long? BrandId { get; set; }

        [FromQuery(Name = "status")]
        public CampaignStatus? Status { get; set; }
    }
}
