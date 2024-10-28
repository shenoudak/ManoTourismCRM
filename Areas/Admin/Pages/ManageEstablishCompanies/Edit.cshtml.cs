using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageEstablishCompanies
{
    public class EditModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        [BindProperty]
        public EstablishingCompany EditEstCompany { get; set; }

       
        public EditModel(ManoContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }


        public async Task<IActionResult> OnGet(int EstablishingCompanyId)
        {
            bool aleadyAuthorized = false;
            try
            {
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
                            bool isAuthoried = _context.AssignEmployeeRoles.Any(e => e.EmployeeId == employee.EmployeeId && e.EmployeeRoleId == (int)EmpRoles.EstablishingCompanies);
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
                    locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    BrowserCulture = locale.RequestCulture.UICulture.ToString();
                    url = $"{this.Request.Scheme}://{this.Request.Host}";
                    EditEstCompany = _context.EstablishingCompanies.FirstOrDefault(a => a.EstablishingCompanyId == EstablishingCompanyId);
                    if (EditEstCompany == null)
                    {
                        return Redirect("/Admin/PageNotFound");
                    }

                }
            }
            catch (Exception ex)
            {

                return Redirect("/Admin/PageNotFound");
            }

           
           
            return Page();
        }
        public async Task<IActionResult> OnPost(int EstablishingCompanyId, IFormFile file)
        {
            try
            {

                var DbEstComp = _context.EstablishingCompanies.Where(c => c.EstablishingCompanyId == EstablishingCompanyId && c.IsDeleted == false).FirstOrDefault();

                if (DbEstComp == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/Admin/ManageEstablishCompanies/Index");
                }
                if (file != null)
                {


                    string folder = "Images/EsCompany/";

                    DbEstComp.Pic = UploadImage(folder, file);
                }
                else
                {
                    DbEstComp.Pic = EditEstCompany.Pic;
                }



                DbEstComp.TitleAr = EditEstCompany.TitleAr;
                DbEstComp.TitleEn = EditEstCompany.TitleEn;
                DbEstComp.IsActive = EditEstCompany.IsActive;
                DbEstComp.DescriptionAr = EditEstCompany.DescriptionAr;
                DbEstComp.DescriptionEn = EditEstCompany.DescriptionEn;
            

                var UpdatedEstCompany = _context.EstablishingCompanies.Attach(DbEstComp);
                UpdatedEstCompany.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageEstablishCompanies/Index");
        }



        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }
    }
}

