using JetBrains.Annotations;
using MarketingBox.AffiliateApi.Grpc;
using MyJetWallet.Sdk.Grpc;

namespace MarketingBox.AffiliateApi.Client
{
    [UsedImplicitly]
    public class AffiliateApiClientFactory: MyGrpcClientFactory
    {
        public AffiliateApiClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
