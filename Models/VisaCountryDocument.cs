namespace ManoTourism.Models
{
    public class VisaCountryDocument
    {
        public int VisaCountryDocumentId { get; set; }
        public string DocumentTLAR { get; set; }
        public string DocumentTLEn { get; set; }
        public int VisaId { get; set; }
        public virtual Visa Visa { get; set; }
    }
}
