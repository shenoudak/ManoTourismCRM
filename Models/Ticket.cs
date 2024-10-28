using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public string TicketTitleAr { get; set; }
        public string TicketTitleEn { get; set; }
        public string TicketDescEn { get; set; }
        public string TicketDescAr { get; set; }
        public string Pic { get; set; }
    }
}
