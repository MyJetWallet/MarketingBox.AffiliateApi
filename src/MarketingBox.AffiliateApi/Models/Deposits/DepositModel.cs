using System;

namespace MarketingBox.AffiliateApi.Models.Deposits
{
    public class DepositModel
    {
        public string UniqueId { get; set; }
        public string CustomerId { get; set; }
        public string Country { get; set; }
        public long LeadId { get; set; }
        public string Email { get; set; }
        public long AffiliateId { get; set; }
        public long CampaignId { get; set; }
        public long BoxId { get; set; }
        public long BrandId { get; set; }
        public ApprovedType Type { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ConversionDate { get; set; }
        public long Sequence { get; set; }
        public string BrandStatus { get; set; }

    }
}
