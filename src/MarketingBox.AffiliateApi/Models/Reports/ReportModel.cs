namespace MarketingBox.AffiliateApi.Models.Reports
{
    public class ReportModel
    {
        public long AffiliateId { get; set; }

        public long LeadCount { get; set; }

        public long FtdCount { get; set; }

        public decimal Payout { get; set; }

        public decimal Revenue { get; set; }

        public decimal Ctr { get; set; }
    }
}