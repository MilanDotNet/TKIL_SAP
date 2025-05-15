namespace TKILSAPRFC.Model.DataBaseModels
{
    public class UserMaster
    {
        public int PERSON_ID { get; set; }
        public int? USERTYPE_ID { get; set; }
        public string? USER_ID { get; set; }
        public string? USERPASSWORD { get; set; }
        public string? FULL_NAME { get; set; }
        public string? EMAIL_ADDRESS { get; set; }
        public string? TIMEZONE { get; set; }
        public int? CURRENCYID { get; set; }
        public string? STATUS { get; set; }
        public int? REPORTINGTO { get; set; }
        public int? DEPARTMENTHEAD { get; set; }
        public int? AddUserId { get; set; }
        public DateTime? AddDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public int? PriceBidDetails { get; set; }
        public int? TechnicalParametersDetails { get; set; }
        public int? TCDetails { get; set; }
        public int? IsIT { get; set; }
        public string? MobileNumber { get; set; }
        public int? IsTechnicalHOD { get; set; }
        public int? IsPlantHead { get; set; }
        public int? SHApprovalDeactive { get; set; }
        public int? ISQCSApproval { get; set; }
        public int? IsTechnicalApproval { get; set; }
        public int? PlantId { get; set; }
    }
}
