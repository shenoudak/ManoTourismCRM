using System.ComponentModel.DataAnnotations;
namespace ManoTourism.Models
{
    public class TrackRequest
    {
        public int TrackRequestId { get; set; }
        public DateTime EventDate { get; set; }
    }
}
