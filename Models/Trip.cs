namespace ManoTourism.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string TripTitleAr { get; set; }
        public string TripTitleEn { get; set; }
        public string TripImage { get; set; }
        public string VideoUrl { get; set; }
        public int DurationInDays { get; set; }
        public int CountryId { get; set; }
        public int TripTypeId { get; set; }
        public double ReviewRating { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public DateTime AddedDate { get; set; }
        public double OldPricePerPerson { get; set; }
        public double NewPricePerPerson { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int TripTargetId { get; set; }
        public virtual TripTarget TripTarget { get; set; }
        public virtual TripType TripType { get; set; }
        public virtual Country Country { get; set; }
        //public virtual ICollection<TripRequest> TripRequests { get; set; }
        public virtual ICollection<TripImage> TripImages { get; set; }
        public virtual ICollection<TripProgram> TripPrograms { get; set; }
    }
}
