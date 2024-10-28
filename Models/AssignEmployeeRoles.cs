using System.ComponentModel.DataAnnotations;

namespace ManoTourism.Models
{
    public class AssignEmployeeRoles
    {
        [Key]
        public int AssignEmployeeRolesId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeRoleId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual EmployeeRole EmployeeRole { get; set; }
    }
}
