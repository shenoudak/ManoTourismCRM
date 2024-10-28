using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using ManoTourism.ViewModels;
using NToastNotify;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        public int VisaCount { get; set; }
        public int TripCount { get; set; }
        public int InsuranceCount { get; set; }
        public int TicketCount { get; set; }
        public int TotalRequestsCount { get; set; }
        public int NewRequestsCount { get; set; }
        public int ProcessingRequestsCount { get; set; }
        public int RejectedRequestsCount { get; set; }
        public int FinishedRequestsCount { get; set; }
        public int CanceledRequests { get; set; }
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public UpdateRequestStatusVM updateRequestStatusVM { get; set; }
        public static int EmployeeId = 0;
        private readonly UserManager<ApplicationUser> _userManager;
        public string url { get; set; }
        public static string UserId { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context, IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            updateRequestStatusVM = new UpdateRequestStatusVM();
            _toastNotification = toastNotification;
            _userManager = userManager;

        }
        public async Task<IActionResult> OnGet()
        {

            url = $"{this.Request.Scheme}://{this.Request.Host}";
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            TotalRequestsCount = _context.Requests.Where(e => e.IsDeleted == false).Count();
            CanceledRequests = _context.Requests.Where(e => e.IsDeleted ==false &&  e.VisaRequestStatusId ==5).Count();
            NewRequestsCount = _context.Requests.Where(e => e.IsDeleted == false && e.VisaRequestStatusId == 1).Count();
            ProcessingRequestsCount = _context.Requests.Where(e => e.IsDeleted == false && e.VisaRequestStatusId == 2).Count();
            RejectedRequestsCount = _context.Requests.Where(e => e.IsDeleted == false && e.VisaRequestStatusId == 4).Count();
            FinishedRequestsCount = _context.Requests.Where(e => e.IsDeleted == false && e.VisaRequestStatusId == 3).Count();
            VisaCount = _context.Requests.Where(e => e.IsDeleted == false && e.ManoEntityTypeId == 1).Count();
            TripCount = _context.Requests.Where(e => e.IsDeleted == false && e.ManoEntityTypeId == 2).Count();
            InsuranceCount = _context.Requests.Where(e => e.IsDeleted == false && e.ManoEntityTypeId == 3).Count();
            TicketCount = 0;


            //UserId = user.Id;
            return Page();
        }
    }
}