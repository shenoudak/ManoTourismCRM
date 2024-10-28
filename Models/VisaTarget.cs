using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class VisaTarget
    {
        [Key]
        public int VisaTargetId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
    }
}
