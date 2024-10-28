using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class Visa
    {
        [Key]
        public int VisaId { get; set; }
        public string VisaTitleAr { get; set; }
        public string VisaTitleEn { get; set; }
        public string Pic { get; set; }
        public int CountryId { get; set; }
        public int VisaTypeId { get; set; }
        public int ValidityInDays { get; set; }
        public double OldPricePerPerson { get; set; }
        public double NewPricePerPerson { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public DateTime AddedDate { get; set; }
        public int VisaTargetId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual VisaType VisaType { get; set; }
        public virtual Country Country { get; set; }
        public virtual VisaTarget VisaTarget { get; set; }
        //public virtual ICollection<VisaRequest> VisaRequests { get; set; }
        
        public virtual ICollection<VisaCountryDocument>VisaCountryDocuments { get; set; }


    }
}
