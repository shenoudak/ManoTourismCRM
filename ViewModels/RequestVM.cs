using ManoTourism.Models;
using System.ComponentModel.DataAnnotations;

namespace ManoTourism.ViewModels
{
    public class RequestVM
    {
        public int RequestId { get; set; }
        public int ManoEntityTypeId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime EmployeeRequestUpdateDate { get; set; }
        public DateTime AssignedDateToEmployee { get; set; }
        public int EntityId { get; set; }
        public string EntityTitle { get; set; }
        public string AffiliateName { get; set; }
        public string UserId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeImg { get; set; }
        public int StatusId { get; set; }
        public string StatusTitle { get; set; }
        
       

    }
}
