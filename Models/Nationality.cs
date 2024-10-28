using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class Nationality
    {
        [Key]
        public int NationalityId { get; set; }
        public string NationalityTLAR { get; set; }
        public string NationalityTLEN { get; set; }
        public bool IsAtive { get; set; }
    }
}
