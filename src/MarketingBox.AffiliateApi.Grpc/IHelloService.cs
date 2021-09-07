using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.AffiliateApi.Grpc.Models;

namespace MarketingBox.AffiliateApi.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}
