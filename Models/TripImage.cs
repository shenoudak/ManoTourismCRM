namespace ManoTourism.Models
{
    public class TripImage
    {
        public int TripImageId { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
