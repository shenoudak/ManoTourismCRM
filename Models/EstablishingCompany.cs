using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class EstablishingCompany
    {
        [Key]
        public int EstablishingCompanyId { get; set; }
        public string Pic { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AddedDate { get; set; }
         
    }
}
