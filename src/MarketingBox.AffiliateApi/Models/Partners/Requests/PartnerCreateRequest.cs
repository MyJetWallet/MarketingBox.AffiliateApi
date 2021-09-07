using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AffiliateApi.Models.Partners.Requests
{
    public class PartnerCreateRequest
    {
        public PartnerGeneralInfo GeneralInfo { get; set; }

        public PartnerCompany Company { get; set; }

        public PartnerBank Bank { get; set; }
    }
}
