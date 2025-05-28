namespace TKILSAPRFC.Model.ViewModels
{
    public class UserLoginVM
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class S3Credential
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
