using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManoTourism.Models
{
    public class PublicSlider
    {
        [Key]
        public int PublicSliderId { get; set; }
        public string HeaderAr { get; set; }
        public string HeaderEn { get; set; }
        public string CaptionAr { get; set; }
        public string CaptionEn { get; set; }
        public string Background { get; set; }
        public bool IsImage { get; set; }
    }
}
