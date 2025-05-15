using TKILSAPRFC.Model.DataBaseModels;

namespace TKILSAPRFC.Model.ViewModels
{
    public class BuyerAuthResponse
    {
        public string? Access_token { get; set; }
        public string? Refresh_token { get; set; }
        public BuyerLoginDetail? BuyerLoginDetail { get; set; }
    }

    public class ResponseMsg
    {
        public string? msg { get; set; }
        public int code { get; set; }
    }
}
