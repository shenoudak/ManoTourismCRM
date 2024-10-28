using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using NToastNotify;
using System.Linq.Dynamic.Core;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageVisaRequiredDocument
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;

        public string url { get; set; }
        public static int staticVisaId { get; set; }

        [BindProperty]
        public VisaCountryDocument addVisaCountryDocument { get; set; }
        public VisaCountryDocument addVisaCountryDocumentObj { get; set; }
        public List<VisaCountryDocument> visaCountryDocuments = new List<VisaCountryDocument>();
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context,
                                            IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _toastNotification = toastNotification;
            addVisaCountryDocument = new VisaCountryDocument();
            addVisaCountryDocumentObj = new VisaCountryDocument();
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet(int VisaId)
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
                            bool isAuthoried = _context.AssignEmployeeRoles.Any(e => e.EmployeeId == employee.EmployeeId && e.EmployeeRoleId == (int)EmpRoles.VisasManagement);
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
                    var visa = _context.Visas.Where(e => e.VisaId == VisaId).FirstOrDefault();
                    if (visa == null)
                    {
                        return Redirect("/Admin/PageNotFound");

                    }
                    staticVisaId = VisaId;
                    url = $"{this.Request.Scheme}://{this.Request.Host}";
                    visaCountryDocuments = _context.VisaCountryDocuments.Where(e => e.VisaId == VisaId).ToList();

                }
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error");
                return Redirect("/Admin/PageNotFound");
            }
            
            return Page();
        }





        public async Task<IActionResult> OnPostAddVisaCountryDocument()
        {

            try
            {
                addVisaCountryDocument.VisaId = staticVisaId;
                _context.VisaCountryDocuments.Add(addVisaCountryDocument);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Required Document Added Successfully");
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect($"/Admin/ManageVisaRequiredDocument/index?VisaId={staticVisaId}");
        }
        public IActionResult OnGetSingleSubProductStepOneForEdit(int VisaCountryDocumentId)
        {
            addVisaCountryDocument = _context.VisaCountryDocuments.Where(c => c.VisaCountryDocumentId == VisaCountryDocumentId).FirstOrDefault();

            return new JsonResult(addVisaCountryDocument);
        }
        public async Task<IActionResult> OnPostEditVisaCountryDocument(int VisaCountryDocumentId)
        {

            try
            {
                var model = _context.VisaCountryDocuments.Where(c => c.VisaCountryDocumentId == VisaCountryDocumentId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");
                    return Redirect($"/Admin/ManageVisaRequiredDocument/index?VisaId={staticVisaId}");

                }
                model.DocumentTLAR = addVisaCountryDocument.DocumentTLAR;
                model.DocumentTLEn = addVisaCountryDocument.DocumentTLEn;

                var UpdatedVisaCountryDocument = _context.VisaCountryDocuments.Attach(model);

                UpdatedVisaCountryDocument.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Edited Successfully");



            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect($"/Admin/ManageVisaRequiredDocument/index?VisaId={staticVisaId}");


        }

        public IActionResult OnGetSingleVisaCountryDocumentForDelete(int VisaCountryDocumentId)
        {
            var Result = _context.VisaCountryDocuments.Where(c => c.VisaCountryDocumentId == VisaCountryDocumentId).FirstOrDefault();
            return new JsonResult(Result);
        }
        public IActionResult OnGetSingleVisaCountryDocumentForView(int VisaCountryDocumentId)
        {
            var Result = _context.VisaCountryDocuments.Where(c => c.VisaCountryDocumentId == VisaCountryDocumentId).FirstOrDefault();
            return new JsonResult(Result);
        }
        public async Task<IActionResult> OnPostDeleteVisaCountryDocument(int VisaCountryDocumentId)
        {
            try
            {
                var model = _context.VisaCountryDocuments.Where(c => c.VisaCountryDocumentId == VisaCountryDocumentId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");
                    return Redirect($"/Admin/ManageVisaRequiredDocument/index?VisaId={staticVisaId}");

                }
                _context.VisaCountryDocuments.Remove(model);
                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Deleted Successfully");



            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect($"/Admin/ManageVisaRequiredDocument/index?VisaId={staticVisaId}");



        }



    }
}
