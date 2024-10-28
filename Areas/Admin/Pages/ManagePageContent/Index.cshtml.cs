using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;


namespace ManoTourism.Areas.Admin.Pages.ManagePageContent
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {

        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        [BindProperty]
        public PageContent PageContent { get; set; }
        public List<PageContent> pageContents { set; get; }


        public IndexModel(ManoContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }
        public ActionResult OnGet()
        {
            pageContents = _context.PageContents.ToList();

            return Page();

        }

    }
}
