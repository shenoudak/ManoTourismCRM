using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
    }
}
