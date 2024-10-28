using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;


namespace ManoTourism.Areas.Admin.Pages.ManageVisa
{
    public class AddVisaModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public List<VisaType> visaTypes = new List<VisaType>();
        public List<Country> countries = new List<Country>();
        [BindProperty]
        public Visa AddVisa { get; set; }
        public AddVisaModel(ManoContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _userManager = userManager;
            AddVisa = new Visa();

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
        public async Task<IActionResult> OnPost(IFormFile file, IFormFileCollection MorePhoto)
        {

            
            try
            {

                if (file != null)
                {
                    string folder = "Images/Visa/";
                    AddVisa.Pic = UploadImage(folder, file);
                }
               
                AddVisa.VisaTargetId = 1;
                AddVisa.AddedDate = DateTime.Now;
                _context.Visas.Add(AddVisa);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/ManageVisa/Index");
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
