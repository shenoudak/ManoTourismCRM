using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class HotelReview
    {
        [Key]
        public int HotelReviewId { get; set; }
        public string ReviewWriterImage { get; set; }
        public string ReviewWriterNameAr { get; set; }
        public string ReviewWriterNameEn { get; set; }
        public string ReviewEn { get; set; }
        public string ReviewAr { get; set; }
        public int Rate { get; set; }
        public bool IsActive { get; set; }
        public DateTime ReviewDate { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
