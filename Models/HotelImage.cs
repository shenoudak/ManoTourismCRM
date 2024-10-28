using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class HotelImage
    {
        [Key]
        public int HotelImageId { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
