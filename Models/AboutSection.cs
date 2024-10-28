using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class AboutSection
    {
        [Key]

        public int AboutSectionId { get; set; }
        public string HeaderTLAR { get; set; }
        public string HeaderTLEn { get; set; }
        public string MissionCaptionAr { get; set; }
        public string MissionCaptionEn { get; set; }
        public string CustomerFocusCaptionAr { get; set; }
        public string CustomerFocusCaptionEn { get; set; }
        
        public string AboutVideo { get; set; }
        public string VideoBackground { get; set; }
        public string VideoUrl { get; set; }
        public string AboutUsImage { get; set; }
    }
}
