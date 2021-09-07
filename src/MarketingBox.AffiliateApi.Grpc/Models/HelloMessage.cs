using System.Runtime.Serialization;
using MarketingBox.AffiliateApi.Domain.Models;

namespace MarketingBox.AffiliateApi.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}
