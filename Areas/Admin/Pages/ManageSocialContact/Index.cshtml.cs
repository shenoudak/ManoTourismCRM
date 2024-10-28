using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace ManoTourism.Areas.Admin.Pages.ManageSocialContact
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public ContactSocial contactSocial { get; set; }


        public List<ContactSocial> contactSocialList = new List<ContactSocial>();

        public ContactSocial contactSocialObj { get; set; }

        public IndexModel(ManoContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            contactSocial = new ContactSocial();
            contactSocialObj = new ContactSocial();
        }
        public void OnGet()
        {
            contactSocialList = _context.ContactSocials.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }
        public IActionResult OnGetSingleContactSocialForView(int ContactSocialId)
        {
            var Result = _context.ContactSocials.Where(c => c.ContactSocialId == ContactSocialId).FirstOrDefault();
            return new JsonResult(Result);
        }
        


    }
}
