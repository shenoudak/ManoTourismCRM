using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.ViewModels;
//using ManoTourism.Report;
using Microsoft.EntityFrameworkCore;
using ManoTourism.Models;
using Microsoft.AspNetCore.Localization;
using ManoTourism.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NToastNotify;

namespace ManoTourism.Areas.Admin.Pages.Reports
{
    public class RequestsReportModel : PageModel
    {

            public ManoContext _context { get; set; }
            private readonly IToastNotification _toastNotification;
            private readonly UserManager<ApplicationUser> _userManager;
           
            [BindProperty]
            public FilterModel filterModel { get; set; }

            public RequestsReportModel(ManoContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification)
            {
                _context = context;
                _toastNotification = toastNotification;
                _userManager = userManager;

            }

            public ManoTourism.Report.RequestReport Report { get; set; }
           

            public IRequestCultureFeature locale;
            public string BrowserCulture;
            public async Task<IActionResult> OnGet()
            {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            return Page();

            }
            public IActionResult OnPost()
            {
                locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                BrowserCulture = locale.RequestCulture.UICulture.ToString();
                List<RequestVM> ds = _context.Requests.Include(e => e.Country).Include(e => e.VisaRequestStatus).Include(e => e.Nationality).Include(e => e.ManoEntityType).Include(e => e.CompanyMarkting).Include(e => e.Employee).Where(e => e.IsDeleted == false && e.VisaRequestStatusId != 1).Select(i => new RequestVM
                {


                    RequestId = i.RequestId,
                    ManoEntityTypeId = i.ManoEntityTypeId,
                    EmployeeId = i.EmployeeId.Value,
                    FullName = i.FullName,
                    StatusId = i.VisaRequestStatusId,
                    EntityTitle = BrowserCulture == "en-US" ? i.ManoEntityType.EntityTitleEn : i.ManoEntityType.EntityTitleAr,
                    StatusTitle = BrowserCulture == "en-US" ? i.VisaRequestStatus.StatusTitleEn : i.VisaRequestStatus.StatusTitleAr,
                    AssignedDateToEmployee = i.AssignedDateToEmployee,
                    AffiliateName = i.AffiliateName,
                    UserId = i.UserId,
                    EmployeeImg = i.Employee == null ? "" : i.Employee.EmployeePic,
                    EmployeeName = i.Employee == null ? " Not Assigned" : i.Employee.EmployeeName,
                    RequestDate = i.RequestDate,
                    EmployeeRequestUpdateDate = i.EmployeeRequestUpdateDate,

                }).ToList();

                if (filterModel.ManoEntityTypeId != null)
                {
                    ds = ds.Where(i => i.ManoEntityTypeId == filterModel.ManoEntityTypeId).ToList();
                }
                if (filterModel.StatusId != null)
                {
                    ds = ds.Where(i => i.StatusId == filterModel.StatusId).ToList();
                }
                if (filterModel.FromDate != null && filterModel.ToDate == null)
                {
                    ds = null;
                }
                if (filterModel.FromDate == null && filterModel.ToDate != null)
                {
                    ds = null;
                }
                if (filterModel.FromDate != null && filterModel.ToDate != null)

                {
                    ds = ds.Where(i => i.AssignedDateToEmployee.Date >= filterModel.FromDate.Value.Date && i.AssignedDateToEmployee <= filterModel.ToDate.Value.Date).ToList();
                }
               
                Report = new ManoTourism.Report.RequestReport(BrowserCulture);
                Report.DataSource = ds;
              
                return Page();

            }

        }
    }
