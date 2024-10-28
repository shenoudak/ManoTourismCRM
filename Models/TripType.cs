using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class TripType
    {
        [Key]
        public int TripTypeId { get; set; }
        public string TripTypeTitleAr { get; set; }
        public string TripTypeTitleEn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
