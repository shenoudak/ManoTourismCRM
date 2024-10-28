using System.ComponentModel.DataAnnotations;
namespace ManoTourism.Models
{
    public class Testimonial
    {
        [Key]
        public int TestimonialId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string ReviewImage { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }
    }
}
