namespace TKILSAPRFC.Model.DataBaseModels
{
    public class UserLoginDetail
    {
        public int PERSON_ID { get; set; }
        public int USERTYPE_ID { get; set; }
        public string? USER_ID { get; set; }
        public string? FULL_NAME { get; set; }
        public string? EMAIL_ADDRESS { get; set; }
    }
    public class UserLoginDetailWithDatabase
    {
        public UserLoginDetail UserLoginDetail { get; set; } = new();
        public string? DatabaseName { get; set; }
    }

    public class UserDetails
    {
        public int UserID { get; set; }
        public string? FirebaseDeviceToken { get; set; }
    }
}
