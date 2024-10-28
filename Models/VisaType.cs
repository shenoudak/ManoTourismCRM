using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class VisaType
    {
        [Key]
        public int VisaTypeId { get; set; }
        public string VisaTitleAr { get; set; }
        public string VisaTitleEn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
