using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageVisaOutUAE
{
    public class EditVisaModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        private readonly UserManager<ApplicationUser> _userManager;

        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public Visa EditVisa { get; set; }

        public List<VisaType> visaTypes = new List<VisaType>();
        public List<Country> countries = new List<Country>();
        public EditVisaModel(ManoContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
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
                    locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    BrowserCulture = locale.RequestCulture.UICulture.ToString();
                    url = $"{this.Request.Scheme}://{this.Request.Host}";
                    EditVisa = _context.Visas.FirstOrDefault(a => a.VisaId == VisaId);
                    if (EditVisa == null)
                    {
                        return Redirect("/Admin/PageNotFound");
                    }
                    visaTypes = _context.VisaTypes.Where(e => e.IsDeleted == false).ToList();
                    countries = _context.Countries.Where(e => e.IsAtive == true).ToList();

                }
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error");
                return Redirect("/Admin/PageNotFound");
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPost(int VisaId, IFormFile file)
        {
            try
            {

                var DbVisa = _context.Visas.Where(c => c.VisaId == VisaId&&c.IsDeleted==false).FirstOrDefault();

                if (DbVisa == null)
                {
                    _toastNotification.AddErrorToastMessage("Visa Not Found");

                    return Redirect("/Admin/ManageVisaOutUAE/Index");
                }
                if (file != null)
                {


                    string folder = "Images/Visa/";

                    DbVisa.Pic = UploadImage(folder, file);
                }
                else
                {
                    DbVisa.Pic = EditVisa.Pic;
                }



                DbVisa.VisaTitleAr = EditVisa.VisaTitleAr;
                DbVisa.VisaTitleEn = EditVisa.VisaTitleEn;
                DbVisa.DescriptionAr = EditVisa.DescriptionAr;
                DbVisa.DescriptionEn = EditVisa.DescriptionEn;
                DbVisa.OldPricePerPerson = EditVisa.OldPricePerPerson;
                DbVisa.NewPricePerPerson = EditVisa.NewPricePerPerson;
                //DbItem.IsActive = EditItem.IsActive;
                DbVisa.VisaTypeId = EditVisa.VisaTypeId;
                DbVisa.CountryId = EditVisa.CountryId;
                DbVisa.IsActive = EditVisa.IsActive;
                DbVisa.ValidityInDays = EditVisa.ValidityInDays;
       
                var UpdatedVisa = _context.Visas.Attach(DbVisa);
                UpdatedVisa.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Visa Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageVisaOutUAE/Index");
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

