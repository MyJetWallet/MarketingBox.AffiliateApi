using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AffiliateApi.Models.CampaignBoxes.Requests
{
    public class CampaignBoxesSearchRequest : PaginationRequest<long?>
    {
        [FromQuery(Name = "id")]
        public long? Id { get; set; }
    }
}
