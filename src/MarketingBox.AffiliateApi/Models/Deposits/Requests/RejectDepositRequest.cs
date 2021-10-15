namespace MarketingBox.AffiliateApi.Models.Reports.Requests
{
    public class RejectDepositRequest
    {
        public long AffiliateId { get; set; }

        public long LeadId { get; set; }
    }
}