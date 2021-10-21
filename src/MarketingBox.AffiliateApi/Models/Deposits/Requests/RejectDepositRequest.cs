namespace MarketingBox.AffiliateApi.Models.Deposits.Requests
{
    public class RejectDepositRequest
    {
        public long AffiliateId { get; set; }

        public long LeadId { get; set; }
    }
}