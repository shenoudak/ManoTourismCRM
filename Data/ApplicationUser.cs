using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int EntityId { get; set; }
        public string Pic { get; set; }
        public string FullName { get; set; }

    }
}
