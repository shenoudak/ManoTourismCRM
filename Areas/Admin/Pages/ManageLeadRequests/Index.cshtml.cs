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

namespace ManoTourism.Areas.Admin.Pages.ManageLeadRequests
{
    [Authorize(Roles = "employee,lead,accountant")]
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

            TotalRequestsCount = _context.Requests.Where(e => e.UserId == user.Id).Count();
            CanceledRequests = _context.Requests.Where(e => e.UserId == user.Id && e.IsDeleted == false && e.VisaRequestStatusId == 5).Count();
            NewRequestsCount = _context.Requests.Where(e => e.UserId == user.Id && e.IsDeleted == false && e.VisaRequestStatusId == 1).Count();
            ProcessingRequestsCount = _context.Requests.Where(e => e.UserId == user.Id && e.IsDeleted == false && e.VisaRequestStatusId == 2).Count();
            RejectedRequestsCount = _context.Requests.Where(e => e.UserId == user.Id && e.IsDeleted == false && e.VisaRequestStatusId == 4).Count();
            FinishedRequestsCount = _context.Requests.Where(e => e.UserId == user.Id && e.IsDeleted == false && e.VisaRequestStatusId == 3).Count();
            VisaCount = _context.Requests.Where(e => e.UserId == user.Id && e.IsDeleted == false && e.ManoEntityTypeId == 1).Count();
            TripCount = _context.Requests.Where(e => e.UserId == user.Id && e.IsDeleted == false && e.ManoEntityTypeId == 2).Count();
            InsuranceCount = _context.Requests.Where(e => e.UserId == user.Id && e.IsDeleted == false && e.ManoEntityTypeId == 3).Count();
            TicketCount = 0;

            UserId = user.Id;
            return Page();
        }



        public async Task<JsonResult> OnPostAsync()
        {


            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var recordsTotal = _context.Requests.Where(e => e.IsDeleted == false && e.UserId == UserId).Count();

            var customersQuery = _context.Requests.Include(e => e.Country).Include(e => e.VisaRequestStatus).Include(e => e.Nationality).Include(e => e.ManoEntityType).Include(e => e.CompanyMarkting).Where(e => e.IsDeleted == false && e.UserId == UserId).Select(i => new
            {
                RequestId = i.RequestId,
                RequestDate = i.RequestDate.ToString("dddd, dd MMMM yyyy"),
                CountryId = i.CountryId,
                EntityId = i.EntityId,
                EntityTitleAr = i.EntityTitleAr,
                EntityTitleEn = i.EntityTitleEn,
                FullName = i.FullName,
                PhoneNumber = i.PhoneNumber,
                Message = i.Message,
                Email = i.Email,
                RequestStatusId = i.VisaRequestStatusId,
                NationalityTLEN = i.Nationality.NationalityTLEN,
                CountryTLEN = i.Country.CountryTLEN,
                CountryTLAR = i.Country.CountryTLAR,
                CompanyMarktingTitleEn = i.CompanyMarkting.TitleEn,
                StatusTitleEn = i.VisaRequestStatus.StatusTitleEn,
                ManoEntityTitleEn = i.ManoEntityType.EntityTitleEn,
                ManoEntityTitleAr = i.ManoEntityType.EntityTitleAr,
                BrowserCulture = BrowserCulture,
                AffiliateName = i.AffiliateName,

            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.EntityTitleEn.ToUpper().Contains(searchText) ||
                    s.CountryTLEN.ToUpper().Contains(searchText)
                );
            }

            var recordsFiltered = customersQuery.Count();

            var sortColumnName = DataTablesRequest.Columns.ElementAt(DataTablesRequest.Order.ElementAt(0).Column).Name;
            var sortDirection = DataTablesRequest.Order.ElementAt(0).Dir.ToLower();

            // using System.Linq.Dynamic.Core
            customersQuery = customersQuery.OrderBy($"{sortColumnName} {sortDirection}");

            var skip = DataTablesRequest.Start;
            var take = DataTablesRequest.Length;
            var data = await customersQuery
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new JsonResult(new
            {
                draw = DataTablesRequest.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data
            });
        }

        public IActionResult OnGetSingleRequestDetailsForView(int RequestId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var Result = _context.Requests.Include(e => e.Country).Include(e => e.Nationality).Include(e => e.CompanyMarkting).Include(e => e.ManoEntityType).Where(c => c.RequestId == RequestId).Select(i => new
            {
                RequestId = i.RequestId,
                RequestDate = i.RequestDate.ToString("dddd, dd MMMM yyyy"),
                CountryId = i.CountryId,
                EntityId = i.EntityId,
                EntityTitleAr = i.EntityTitleAr,
                EntityTitleEn = i.EntityTitleEn,
                FullName = i.FullName,
                PhoneNumber = i.PhoneNumber,
                ManoEntityTypeId = i.ManoEntityTypeId,
                Message = i.Message,
                VisaRequestStatusId = i.VisaRequestStatusId,
                StatusTitleEn = i.VisaRequestStatus.StatusTitleEn,
                StatusTitleAr = i.VisaRequestStatus.StatusTitleAr,
                Email = i.Email,
                NationalityTLAR = i.Nationality.NationalityTLAR,
                NationalityTLEN = i.Nationality.NationalityTLEN,
                CountryTLAR = i.Country.CountryTLAR,
                CountryTLEN = i.Country.CountryTLEN,
                CompanyMarktingTitleEn = i.CompanyMarkting.TitleEn,
                CompanyMarktingTitleAr = i.CompanyMarkting.TitleAr,
                AffiliateName = i.AffiliateName,
                ActivityType = i.ActivityType,
                HotelArrivedDate = i.HotelArrivedDate.Value.ToString("dddd, dd MMMM yyyy"),
                HotelDepartureDate = i.HotelDepartureDate.Value.ToString("dddd, dd MMMM yyyy"),
                OneWayDate = i.OneWayDate.Value.ToString("dddd, dd MMMM yyyy"),
                BeturnedDate = i.BeturnedDate.Value.ToString("dddd, dd MMMM yyyy"),
                OneWay = i.OneWay,
                ManoEntityTitleEn = i.ManoEntityType.EntityTitleEn,
                ManoEntityTitleAr = i.ManoEntityType.EntityTitleAr,
                BrowserCulture = BrowserCulture,
            }).FirstOrDefault();
            return new JsonResult(Result);
        }

    }
}


