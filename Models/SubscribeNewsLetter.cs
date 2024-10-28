using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class SubscribeNewsLetter
    {
        [Key]
        public int SubscribeNewLetterId { get; set; }
        public string Email { get; set; }
    }
}
