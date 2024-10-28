
using System.ComponentModel.DataAnnotations;
namespace ManoTourism.Models
{
    public class Insurance
    {
        [Key]
        public int InsuranceId { get; set; }
        public string InsuranceTitleAr { get; set; }
        public string InsuranceTitleEn { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string Pic { get; set; }
        public int DurationInMonth { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice{ get; set; }
        public int InsuranceTypeId { get; set; }
        public DateTime AddedDate { get; set; }
        public virtual InsuranceType InsuranceType { get; set; }
    }
}
