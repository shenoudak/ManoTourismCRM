namespace ManoTourism.Models
{
    public class TripProgram
    {
        public int TripProgramId { get; set; }
        public int DayNumber { get; set; }
        public string HeaderAr { get; set; }
        public string HeaderEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
