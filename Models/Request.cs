using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int NationalityId { get; set; }
        public int CountryId { get; set; }
        public string Message { get; set; }
        public string? RejectRequestReason { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? EmployeeRequestUpdateDate { get; set; }
        public DateTime? AssignedDateToEmployee { get; set; }
        public DateTime? HotelArrivedDate { get; set; }
        public DateTime? HotelDepartureDate { get; set; }
        public DateTime? OneWayDate { get; set; }
        public DateTime? BeturnedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public bool OneWay { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPaid { get; set; }
        public int EntityId { get; set; }
        public string EntityTitleAr { get; set; }
        public string EntityTitleEn { get; set; }
        public string? UserId { get; set; }
        public string? AffiliateName { get; set; }
        public string? SharedEmployeeeName { get; set; }
        public int? SharedEmployeeeId { get; set; }
        public int? SellesId { get; set; }
        public int ManoEntityTypeId { get; set; }
        public int CompanyMarktingId { get; set; }
        public int? EmployeeId { get; set; }
        public int VisaRequestStatusId { get; set; }
        public int? RejectReasonId { get; set; }
        public string? Notes { get; set; }
        public string? ActivityType { get; set; }
        public string? TransactionType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual VisaRequestStatus VisaRequestStatus { get; set; }
        public virtual RejectReason RejectReason { get; set; }
        public virtual Country Country { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual CompanyMarkting CompanyMarkting { get; set; }
        public virtual ManoEntityType ManoEntityType { get; set; }
    }
}
