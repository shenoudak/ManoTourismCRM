using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }
        public string OfferTitleAr { get; set; }
        public string OfferTitleEn { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<OfferImage> OfferImages { get; set; }
    }
}
