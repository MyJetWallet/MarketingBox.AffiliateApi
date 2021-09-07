using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingBox.AffiliateApi.Models.Partners
{
    public class PartnerModel
    {
        public long AffiliateId { get; set; }

        public PartnerGeneralInfo GeneralInfo { get; set; }

        public PartnerCompany Company { get; set; }

        public PartnerBank Bank { get; set; }

    }
}
