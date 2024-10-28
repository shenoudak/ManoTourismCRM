using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class TripTarget
    {
        [Key]
        public int TripTargetId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
    }
}
