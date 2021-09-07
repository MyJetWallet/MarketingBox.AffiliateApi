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
    }
}
