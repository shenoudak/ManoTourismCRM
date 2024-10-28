using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ManoTourism.Models
{
    public class VisaRequest
    {
        [Key]
        public int VisaRequestId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int NationalityId { get; set; }
        public int CountryId { get; set; }
        public string Message { get; set; }
        public string? RejectRequestReason { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime EmployeeRequestUpdateDate { get; set; }
        public DateTime AssignedDateToEmployee { get; set; }
        public bool IsDeleted { get; set; }
        public int VisaId { get; set; }
        public int CompanyMarktingId { get; set; }
        public int? EmployeeId { get; set; }
        public int VisaRequestStatusId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Visa Visa { get; set; }
        public virtual VisaRequestStatus VisaRequestStatus { get; set; }
        public virtual Country Country { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual CompanyMarkting CompanyMarkting { get; set; }
    }
}
