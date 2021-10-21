using MarketingBox.Reporting.Service.Domain.Models.Lead;

namespace MarketingBox.AffiliateApi.Models.Leads
{
    public class LeadModel
    {
        public long LeadId { get; set; }

        public string UniqueId { get; set; }
        
        public long Sequence { get; set; }

        public LeadGeneralInfo GeneralInfo { get; set; }

        public LeadRouteInfo RouteInfo { get; set; }

        public LeadAdditionalInfo AdditionalInfo { get; set; }

        //public LeadType Type  { get; set; }

        public LeadStatus Status{ get; set; }

    }
}
