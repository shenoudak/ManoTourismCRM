
using System;

namespace ManoTourism.ViewModels

{
    public class FilterModel
    {
        public DateTime? FromDate { set; get; }
        public DateTime? ToDate { set; get; }
        public DateTime? OnDay { set; get; }
        public int? EmployeeId { get; set; }
        public int? SharedEmpId { get; set; }
        public int? ManoEntityTypeId { get; set; }
        public int? StatusId { get; set; }
        public string? radioBtn { get; set; }
        public string? UserId { get; set; }
    }
}
