namespace TKILSAPRFC.Model.DataBaseModels
{
    public class TermsAndConditionsDB
    {
        public int EventId { get; set; }
        public int ClauseId { get; set; }
        public string? ClauseName { get; set; }
        public int UserDeviationId { get; set; }
        public bool? IsAccept { get; set; }
        public bool? IsDeviate { get; set; }
        public bool? IsUpdated { get; set; }
        public int UserId { get; set; }
    }

    public class TermsAndConditionsAcceptedDB
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int UserDeviationId { get; set; }
        public int ClauseId { get; set; }
        public bool? IsAccept { get; set; }     
    }
    public class TermsAndConditionsDeviateDB
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int UserDeviationId { get; set; }
        public int ClauseId { get; set; }
        public bool? IsDeviate { get; set; }
        public string? Remark { get; set; }
    }
    public class DeviationRemarksDB
    {
        public int UserDeviationTransactionID { get; set; }
        public int UserDeviationMasterId { get; set; }
        public string? Remarks { get; set; }
        public string? UserType { get; set; }
        public string? UserName { get; set; }
        public bool? IsUpdatedTerm { get; set; }
        public bool? IsUpdated { get; set; }
        public string ActionDate { get; set; }
    }
    public class DeviationRemarksUpdate
    {
        public int UserDeviationTransactionID { get; set; }
        public string? Remarks { get; set; }
    }
}
