using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class CompanyMarkting
    {
        [Key]
        public int CompanyMarktingId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
    }
}
