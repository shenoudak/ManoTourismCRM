using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class VisaRequestStatus
    {
        [Key]
        public int VisaRequestStatusId { get; set; }
        public string StatusTitleAr { get; set; }
        public string StatusTitleEn { get; set; }
    }
}
