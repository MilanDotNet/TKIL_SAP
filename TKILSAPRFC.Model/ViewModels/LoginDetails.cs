using TKILSAPRFC.Model.DataBaseModels;

namespace TKILSAPRFC.Model.ViewModels
{
    public class LoginDetails
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserType { get; set; }
        public string? Phone { get; set; }

    }
}