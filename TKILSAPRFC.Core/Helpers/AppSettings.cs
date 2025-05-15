namespace TKILSAPRFC.Core.Helpers
{
    public class AppSettings
    {
        private static AppSettings? current;

        public AppSettings() => Current = this;

        public static AppSettings? Current { get => current; set => current = value; }
        public JwtConfiguration JwtConfiguration { get; set; }
        public CompanyDetails CompanyDetails { get; set; }
        public string MailSiteURL { get; set; }
        public string MailLogoURL { get; set; }
        public int IsUAT { get; set; }
        public string MailCompanySubject { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TKILSAPRFCClientAPIUrl { get; set; }
        public string SAPExePath { get; set; }
        public int SendMail { get; set; }
        public string DocumentPath { get; set; } = string.Empty;
        public string SAPServiceURL { get; set; } = string.Empty;
        public List<DataBaseConnections> DataBaseConnections { get; set; }
    }

    public class JwtConfiguration
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public double AccessTokenExpiryInMins { get; set; }
        public string? SigningKey { get; set; }
        public double RefreshTokenExpiryInMins { get; set; }
    }
    public class ConnectionStrings
    {
        private static ConnectionStrings? current;
        public ConnectionStrings() => Current = this;
        public static ConnectionStrings? Current { get => current; set => current = value; }
        public string? DefaultConnection { get; set; }
        public string? ConnectionTimeOut { get; set; }
    }
    public class DataBaseConnections
    {
        public string? DefaultConnection { get; set; }
        public string? DataBase { get; set; }
    }
    public class EmailSettings
    {

    }

    public class CompanyDetails
    {
        public string RegisterCompany { get; set; }
        public string RegisterAddress { get; set; }
    }
}
