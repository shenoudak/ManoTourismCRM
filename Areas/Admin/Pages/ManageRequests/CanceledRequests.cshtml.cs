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

namespace ManoTourism.Areas.Admin.Pages.ManageRequests
{
    public class CanceledRequestsModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public AssignRequestVM assignRequestVM { get; set; }
        public static int EmployeeId = 0;
        private readonly UserManager<ApplicationUser> _userManager;
        public string url { get; set; }
        public static string UserId { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;

        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public CanceledRequestsModel(ManoContext context, IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            assignRequestVM = new AssignRequestVM();
            _toastNotification = toastNotification;
            _userManager = userManager;

        }
        public async Task<IActionResult> OnGet()
        {

            bool aleadyAuthorized = false;
            try
            {
                url = $"{this.Request.Scheme}://{this.Request.Host}";
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Redirect("/Identity/Account/Login");

                }
                var roleName = await _userManager.GetRolesAsync(user);
                if (roleName.FirstOrDefault() == "admin" || roleName.FirstOrDefault() == "employee")
                {
                    if (roleName.FirstOrDefault() == "admin")
                    {
                        aleadyAuthorized = true;
                    }
                    if (roleName.FirstOrDefault() == "employee")
                    {
                        var employee = _context.Employees.Where(e => e.EmployeeEmail == user.Email).FirstOrDefault();
                        if (employee == null)
                        {
                            return Redirect("/Admin/AccessDenied");
                        }
                        else
                        {
                            bool isAuthoried = _context.AssignEmployeeRoles.Any(e => e.EmployeeId == employee.EmployeeId && e.EmployeeRoleId == (int)EmpRoles.CanceledRequests);
                            if (isAuthoried)
                            {
                                aleadyAuthorized = true;
                            }
                            else
                            {
                                return Redirect("/Admin/AccessDenied");
                            }
                        }

                    }
                }

                else
                {
                    return Redirect("/Admin/AccessDenied");
                }
                if (aleadyAuthorized)
                {
                    url = $"{this.Request.Scheme}://{this.Request.Host}";

                }
            }
            catch (Exception ex)
            {

                return Redirect("/Admin/PageNotFound");
            }
           

            return Page();
        }



        public async Task<JsonResult> OnPostAsync()
        {


            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var recordsTotal = _context.Requests.Where(e => e.IsDeleted == false && e.VisaRequestStatusId == 5).Count();
            //var recordsTotal = _context.VisaRequests.Include(e => e.Country).Include(e => e.Nationality).Include(e=>e.Visa).Include(e => e.CompanyMarkting).Where(e =>e.VisaId==staticVisaId&& e.IsDeleted == false).Count();

            var customersQuery = _context.Requests.Include(e => e.Country).Include(e => e.VisaRequestStatus).Include(e => e.Nationality).Include(e => e.ManoEntityType).Include(e => e.CompanyMarkting).Include(e => e.Employee).Where(e => e.IsDeleted == false && e.VisaRequestStatusId == 5).Select(i => new
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
                NationalityTLEN = i.Nationality.NationalityTLEN,
                CompanyMarktingTitleEn = i.CompanyMarkting.TitleEn,
                StatusTitleEn = i.VisaRequestStatus.StatusTitleEn,
                ManoEntityTitleEn = i.ManoEntityType.EntityTitleEn,
                EmployeeName = i.Employee.EmployeeName,
                AssignedDateToEmployee = i.AssignedDateToEmployee.Value.ToString("dddd, dd MMMM yyyy"),
                RequestStatusId = i.VisaRequestStatusId,
                AffiliateName = i.AffiliateName,
                ManoEntityTitleAr = i.ManoEntityType.EntityTitleAr,
                BrowserCulture = BrowserCulture,



            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.EntityTitleEn.ToUpper().Contains(searchText) ||
                    s.EmployeeName.ToUpper().Contains(searchText)
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
                TransactionType = i.TransactionType,
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
        public async Task<IActionResult> OnPostReturnRequestToNew(int RequestId, string RequestNotes)
        {

            try
            {
                var model = _context.Requests.Where(c => c.RequestId == RequestId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/Admin/ManageRequests/CanceledRequests");
                }




                model.VisaRequestStatusId = 1;
                model.EmployeeId = null;
                model.Notes = RequestNotes;
                


                var UpdatedRequest = _context.Requests.Attach(model);

                UpdatedRequest.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Request Returned As New Request Successfully");



            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageRequests/CanceledRequests");
        }

    }
}