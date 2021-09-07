using MarketingBox.AffiliateApi.Models.Partners;

namespace MarketingBox.AffiliateApi.Models.Campaigns
{
    public class Revenue
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public Plan Plan { get; set; }
    }
}