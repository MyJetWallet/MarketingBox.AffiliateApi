using System;
using System.Runtime.Serialization;
using Destructurama.Attributed;

namespace MarketingBox.Reporting.Service.Grpc.Models.Leads
{
    public class LeadGeneralInfo
    {
        [LogMasked(PreserveLength = true, ShowFirst = 2, ShowLast = 2)]
        public string FirstName { get; set; }

        [LogMasked(PreserveLength = true, ShowFirst = 2, ShowLast = 2)]
        public string LastName { get; set; }

        [LogMasked(PreserveLength = true, ShowFirst = 2, ShowLast = 2)]
        public string Email { get; set; }

        [LogMasked(PreserveLength = true, ShowFirst = 2, ShowLast = 2)]
        public string Phone { get; set; }
        
        [LogMasked(PreserveLength = true, ShowFirst = 2, ShowLast = 2)]
        public string Ip { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}