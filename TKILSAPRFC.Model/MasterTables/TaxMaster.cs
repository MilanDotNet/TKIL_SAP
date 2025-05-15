namespace TKILSAPRFC.Model.MasterTables
{
    public class TaxMaster
    {
        public int TaxId { get; set; }
        public string TaxName { get; set; }
        public double TaxPer { get; set; }

    }
    public class NewTaxMaster
    {
        public int TaxId { get; set; }
        public string TaxName { get; set; }
        public double TaxPercentage { get; set; }

    }
}
