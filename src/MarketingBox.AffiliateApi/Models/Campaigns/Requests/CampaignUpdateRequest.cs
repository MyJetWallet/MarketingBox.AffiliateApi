using MarketingBox.AffiliateApi.Models.Partners;

namespace MarketingBox.AffiliateApi.Models.Campaigns.Requests
{
    public class CampaignUpdateRequest
    {
        public string Name { get; set; }

        public long BrandId { get; set; }

        public Payout Payout { get; set; }

        public Revenue Revenue { get; set; }

        public CampaignStatus Status { get; set; }

        public CampaignPrivacy Privacy { get; set; }
        public long Sequence { get; set; }
    }
}