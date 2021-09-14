using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace MarketingBox.AffiliateApi.Settings
{
    public class SettingsModel
    {
        [YamlProperty("MarketingBoxAffiliateApi.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("MarketingBoxAffiliateApi.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("MarketingBoxAffiliateApi.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        [YamlProperty("MarketingBoxAffiliateApi.AffiliateServiceUrl")]
        public string AffiliateServiceUrl { get; set; }

        [YamlProperty("MarketingBoxAffiliateApi.JwtAudience")]
        public string JwtAudience { get; set; }

        [YamlProperty("MarketingBoxAffiliateApi.JwtSecret")]
        public string JwtSecret { get; set; }
    }
}
