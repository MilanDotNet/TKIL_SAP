namespace TKILSAPRFC.Model.DataBaseModels
{
    public class BuyerLoginDetail
    {
        public int BuyerID { get; set; }
        public string? BuyerCode { get; set; }
        public string? BuyerName { get; set; }
        public string? Password { get; set; }
        public string? OfficeEmail { get; set; }
        public string? AlternateEmail { get; set; }
        public string? AlternateEmail3 { get; set; }
    }
    public class BuyerLoginDetailWithDatabase
    {
        public BuyerLoginDetail BuyerLoginDetail { get; set; } = new();
        public string? DatabaseName { get; set; }
    }

    public class BuyerDetails
    {
        public int BuyerID { get; set; }
        public string? FirebaseDeviceToken { get; set; }
    }
}
