using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class Affiliate
    {
        [Key]
        public int AffiliateId { get; set; }
        public string AffiliatePic { get; set; }
        public string AffiliateName { get; set; }
        public string AffiliateEmail { get; set; }
        public string AffiliatePassword { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
