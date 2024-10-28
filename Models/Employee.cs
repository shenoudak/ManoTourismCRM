using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeePic { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public string EmployeePassword { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        //public virtual ICollection<VisaRequest> VisaRequests { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<AssignEmployeeRoles> AssignEmployeeRoles { get; set; }
    }
}
