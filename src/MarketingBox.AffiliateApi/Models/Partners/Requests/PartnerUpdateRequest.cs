namespace MarketingBox.AffiliateApi.Models.Partners.Requests
{
    public class PartnerUpdateRequest
    {
        public PartnerGeneralInfo GeneralInfo { get; set; }

        public PartnerCompany Company { get; set; }

        public PartnerBank Bank { get; set; }
        public long Sequence { get; set; }
    }
}