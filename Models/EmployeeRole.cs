using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class EmployeeRole
    {
        [Key]
        public int EmployeeRoleId { get; set; }
        public string RoleTitleAr { get; set; }
        public string RoleTitleEn { get; set; }
        public virtual ICollection<AssignEmployeeRoles> AssignEmployeeRoles { get; set; }
    }
}
