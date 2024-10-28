
using System.ComponentModel.DataAnnotations;
namespace ManoTourism.Models
{
    public class InsuranceType
    {
        [Key]
        public int InsuranceTypeId { get; set; }
        public string InsuranceTypeTitleAr { get; set; }
        public string InsuranceTypeTitleEn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
