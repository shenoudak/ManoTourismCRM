using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        public string HotelMainImage { get; set; }
        public string HotelTitleAr { get; set; }
        public string HotelTitleEn { get; set; }
        public string Location { get; set; }
        public string VideoUrl { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public DateTime AddedDate { get; set; }
        public double Review { get; set; }
        public int ReviewCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<HotelReview> HotelReviews { get; set; }
        public virtual ICollection<HotelImage> HotelImages { get; set; }

    }
}
