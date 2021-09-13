using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenTelemetry.Context.Propagation;

namespace MarketingBox.AffiliateApi.Models.Boxes
{
    public class BoxModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long Sequence { get; set; }
    }
}
