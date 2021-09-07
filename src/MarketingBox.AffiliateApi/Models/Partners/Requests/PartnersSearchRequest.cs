using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AffiliateApi.Models.Partners.Requests
{
    public class PartnersSearchRequest : PaginationRequest<long?>
    {
        [FromQuery(Name = "id")]
        public long? Id { get; set; }
    }
}
