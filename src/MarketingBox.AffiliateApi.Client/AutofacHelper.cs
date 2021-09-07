using Autofac;
using MarketingBox.AffiliateApi.Grpc;

// ReSharper disable UnusedMember.Global

namespace MarketingBox.AffiliateApi.Client
{
    public static class AutofacHelper
    {
        public static void RegisterAssetsDictionaryClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new AffiliateApiClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
