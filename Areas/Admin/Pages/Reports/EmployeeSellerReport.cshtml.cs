using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.ViewModels;
using Microsoft.EntityFrameworkCore;
using ManoTourism.Models;
using Microsoft.AspNetCore.Localization;
using ManoTourism.Report;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages.Reports
{
    public class EmployeeSellerReportModel : PageModel
    {
        public ManoContext _context { get; set; }
        [BindProperty]
        public FilterModel filterModel { get; set; }

        public EmployeeSellerReportModel(ManoContext context)
        {
            _context = context;
        }

        public ManoTourism.Report.EmpSellerRptReport Report { get; set; }
        //public AppointmentReportNewt Report { get; set; }

        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            List<RequestVM> ds = _context.Requests.Include(e => e.Country).Include(e => e.VisaRequestStatus).Include(e => e.Nationality).Include(e => e.Employee).Include(e => e.ManoEntityType).Include(e => e.CompanyMarkting).Where(e => e.IsDeleted == false).Select(i => new RequestVM
            {


                RequestId = i.RequestId,
                EmployeeId =i.Employee==null?null: i.EmployeeId.Value,
                FullName = i.FullName,
                StatusId = i.VisaRequestStatusId,
                EntityTitle = BrowserCulture == "en-US" ? i.ManoEntityType.EntityTitleEn : i.ManoEntityType.EntityTitleAr,
                StatusTitle = BrowserCulture == "en-US" ? i.VisaRequestStatus.StatusTitleEn : i.VisaRequestStatus.StatusTitleAr,
                AssignedDateToEmployee = i.AssignedDateToEmployee==null?null: i.AssignedDateToEmployee.Value,
                AffiliateName = i.AffiliateName,
                UserId = i.UserId,
                EmployeeImg = i.Employee == null ? "" : i.Employee.EmployeePic,
                EmployeeName = i.Employee == null ? " Not Assigned" : i.Employee.EmployeeName,
                RequestDate = i.RequestDate,
                EmployeeRequestUpdateDate = i.EmployeeRequestUpdateDate == null ?null: i.EmployeeRequestUpdateDate.Value,
                SharedEmployeeeName = i.SharedEmployeeeName,
                SharedEmployeeId = i.SharedEmployeeeId == null ? null : i.SharedEmployeeeId.Value


            }).ToList();
            //if (filterModel.SharedEmpId != null)
            //{
            //    ds = ds.Where(i => i.SharedEmployeeId == filterModel.SharedEmpId).ToList();
            //}
            if (filterModel.UserId != null)
            {
                ds = ds.Where(i => i.UserId == filterModel.UserId).ToList();
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
                ds = ds.Where(i => i.AssignedDateToEmployee.Value.Date >= filterModel.FromDate.Value.Date && i.AssignedDateToEmployee <= filterModel.ToDate.Value.Date).ToList();
            }

            Report = new ManoTourism.Report.EmpSellerRptReport(BrowserCulture);
            Report.DataSource = ds;
            return Page();

        }
    }
}