using System.ComponentModel.DataAnnotations;
namespace ManoTourism.Models
{
    public class Sales
    {
        [Key]
        public int SalesId { get; set; }
        public string SalesName { get; set; }
        public string SalesEmail { get; set; }
        public string SalesPhoneNumber { get; set; }
        public string PassportPic { get; set; }
        public string SalesPassword { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
