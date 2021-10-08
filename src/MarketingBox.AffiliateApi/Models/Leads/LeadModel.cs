using MarketingBox.Reporting.Service.Domain.Models.Lead;
using System.Runtime.Serialization;

namespace MarketingBox.Reporting.Service.Grpc.Models.Leads
{
    public class LeadModel
    {
        public string TenantId { get; set; }

        public long LeadId { get; set; }

        public string UniqueId { get; set; }
        
        public long Sequence { get; set; }

        public LeadGeneralInfo GeneralInfo { get; set; }

        public LeadRouteInfo RouteInfo { get; set; }

        public LeadAdditionalInfo AdditionalInfo { get; set; }

        public LeadType Type  { get; set; }

        public LeadStatus CallStatus{ get; set; }

    }
}
