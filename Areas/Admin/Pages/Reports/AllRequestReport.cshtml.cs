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
    public class AllRequestReportModel : PageModel
    {
        public ManoContext _context { get; set; }
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        //private EmpRoles empRoles;
        [BindProperty]
        public FilterModel filterModel { get; set; }

        public AllRequestReportModel(ManoContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;

        }

        public ManoTourism.Report.AllRequestsTransactionRpt Report { get; set; }
        //public AppointmentReportNewt Report { get; set; }

        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public async Task<IActionResult> OnGet()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            List<RequestVM> ds = _context.Requests.Include(e => e.Country).Include(e => e.VisaRequestStatus).Include(e => e.Nationality).Include(e => e.ManoEntityType).Include(e => e.CompanyMarkting).Include(e => e.Employee).Where(e => e.IsDeleted == false).Select(i => new RequestVM
            {


                RequestId = i.RequestId,
                EmployeeId = i.EmployeeId == null ? null : i.EmployeeId.Value,
                FullName = i.FullName,
                StatusId = i.VisaRequestStatusId,
                EntityTitle = BrowserCulture == "en-US" ? i.ManoEntityType.EntityTitleEn : i.ManoEntityType.EntityTitleAr,
                StatusTitle = BrowserCulture == "en-US" ? i.VisaRequestStatus.StatusTitleEn : i.VisaRequestStatus.StatusTitleAr,
                AssignedDateToEmployee = i.AssignedDateToEmployee == null ? null : i.AssignedDateToEmployee.Value,
                AffiliateName = i.AffiliateName,
                UserId = i.UserId,
                EmployeeImg = i.Employee == null ? "" : i.Employee.EmployeePic,
                EmployeeName = i.Employee == null ? " Not Assigned" : i.Employee.EmployeeName,
                RequestDate = i.RequestDate,
                EmployeeRequestUpdateDate = i.FinishedDate == null ? null : i.EmployeeRequestUpdateDate.Value,
            }).ToList();
            Report = new ManoTourism.Report.AllRequestsTransactionRpt(BrowserCulture);
            Report.DataSource = ds;
            return Page();

        }
        public IActionResult OnPost()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            List<RequestVM> ds = _context.Requests.Include(e => e.Country).Include(e => e.VisaRequestStatus).Include(e => e.Nationality).Include(e => e.ManoEntityType).Include(e => e.CompanyMarkting).Include(e => e.Employee).Where(e => e.IsDeleted == false).Select(i => new RequestVM
            {


                RequestId = i.RequestId,
                EmployeeId = i.EmployeeId == null ? null : i.EmployeeId.Value,
                FullName = i.FullName,
                StatusId = i.VisaRequestStatusId,
                EntityTitle = BrowserCulture == "en-US" ? i.ManoEntityType.EntityTitleEn : i.ManoEntityType.EntityTitleAr,
                StatusTitle = BrowserCulture == "en-US" ? i.VisaRequestStatus.StatusTitleEn : i.VisaRequestStatus.StatusTitleAr,
                AssignedDateToEmployee = i.AssignedDateToEmployee == null ? null : i.AssignedDateToEmployee.Value,
                AffiliateName = i.AffiliateName,
                UserId = i.UserId,
                EmployeeImg = i.Employee == null ? "" : i.Employee.EmployeePic,
                EmployeeName = i.Employee == null ? " Not Assigned" : i.Employee.EmployeeName,
                RequestDate = i.RequestDate,
                EmployeeRequestUpdateDate = i.FinishedDate == null ? null : i.EmployeeRequestUpdateDate.Value,

            }).ToList();

            //if (filterModel.EmployeeId != null)
            //{
            //    ds = ds.Where(i => i.EmployeeId == filterModel.EmployeeId).ToList();
            //}
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
                ds = ds.Where(i => i.RequestDate.Date >= filterModel.FromDate.Value.Date && i.RequestDate <= filterModel.ToDate.Value.Date).ToList();
            }
            //if (filterModel.OnDay != null || filterModel.FromDate != null || filterModel.ToDate != null)
            //{
            //    ds = null;
            //}
            Report = new ManoTourism.Report.AllRequestsTransactionRpt(BrowserCulture);
            Report.DataSource = ds;
            //if (filterModel.radioBtn != null)
            //{
            //    if (filterModel.radioBtn == "OnDay")
            //    {
            //        if (filterModel.OnDay != null)
            //        {
            //            ds = ds.Where(i => i.AppointmentCreateDateNotFormat.Date == filterModel.OnDay.Value.Date).ToList();
            //        }
            //        else
            //        {
            //            ds = null;
            //        }
            //    }
            //    else if (filterModel.radioBtn == "Period")
            //    {
            //        if (filterModel.FromDate != null && filterModel.ToDate == null)
            //        {
            //            ds = null;
            //        }
            //        if (filterModel.FromDate == null && filterModel.ToDate != null)
            //        {
            //            ds = null;
            //        }
            //        if (filterModel.FromDate != null && filterModel.ToDate != null)

            //        {
            //            ds = ds.Where(i => i.AppointmentCreateDateNotFormat.Date >= filterModel.FromDate.Value.Date && i.AppointmentCreateDateNotFormat <= filterModel.ToDate.Value.Date).ToList();
            //        }
            //    }
            //}
            //if (filterModel.radioBtn == null && (filterModel.OnDay != null || filterModel.FromDate != null || filterModel.ToDate != null))
            //{
            //    ds = null;
            //}

            //if (filterModel.BarberId == null && filterModel.FromDate == null && filterModel.ToDate == null && filterModel.radioBtn == null)
            //{
            //    ds = null;
            //}

            //Report = new AppointmentReportNewt();
            //Report.DataSource = ds;
            return Page();

        }

    }
}
