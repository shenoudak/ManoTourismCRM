using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryTLAR { get; set; }
        public string CountryTLEN { get; set; }
        public bool IsAtive { get; set; }
    }
}
