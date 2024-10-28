using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class PageContent
    {
        [Key]
        public int PageContentId { get; set; }

        [Required]
        public string PageTitleAr { get; set; }
        [Required]
        public string ContentAr { get; set; }
        [Required]
        public string PageTitleEn { get; set; }
        [Required]
        public string ContentEn { get; set; }
    }
}