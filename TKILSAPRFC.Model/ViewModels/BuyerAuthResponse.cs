using TKILSAPRFC.Model.DataBaseModels;

namespace TKILSAPRFC.Model.ViewModels
{
    public class AuthResponseVm 
    {
        public string? Access_token { get; set; }
        public string? Refresh_token { get; set; }
        public UserLoginDetail? UserLoginDetail  { get; set; }
    }
}
