namespace ManoTourism.Models
{
    public class TripRequest
    {
        public int TripRequestId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int NationalityId { get; set; }
        public int CountryId { get; set; }
        public string Message { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CompanyMarktingId { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Country Country { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual CompanyMarkting CompanyMarkting { get; set; }
    }
}
