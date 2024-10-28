using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class OfferImage
    {
        [Key]
        public int OfferImageId { get; set; }
        public string Image { get; set; }
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
