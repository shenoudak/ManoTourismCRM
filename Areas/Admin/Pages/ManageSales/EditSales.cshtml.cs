using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages.ManageSales
{
    [Authorize(Roles = "admin")]
    public class EditSalesModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        public List<Employee> employees = new List<Employee>();
        public List<AssignEmployeeRoles> assignEmployeesRole = new List<AssignEmployeeRoles>();
        [BindProperty]
        public Sales EditSelles { get; set; }
        public EditSalesModel(ManoContext context, ApplicationDbContext db, IWebHostEnvironment hostEnvironment, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            EditSelles = new Sales();
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;

        }
        public async Task<IActionResult> OnGet(int SalesId)
        {
            EditSelles = _context.Sales.Where(e => e.SalesId == SalesId).FirstOrDefault();
            if (EditSelles == null)
            {
                return Redirect("/Admin/PageNotFound");
            }

            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            employees = _context.Employees.Where(e=>e.IsActive==true&& e.IsDeleted == false).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile file, int SalesId)
        {
            try
            {
                var sallesExixt = _context.Sales.Where(e => e.SalesId == SalesId).FirstOrDefault();
                if (sallesExixt == null)
                {
                    return Redirect("/Admin/ManageSales/Index");
                }
                var user = _userManager.Users.Where(e => e.Email == EditSelles.SalesEmail).FirstOrDefault();
                if (user == null)
                {
                    _toastNotification.AddErrorToastMessage("User Not Found");
                    return Redirect("/Admin/ManageSales/Index");

                }
               

                if (file != null)
                {
                    string folder = "Images/Sales/";

                    sallesExixt.PassportPic = UploadImage(folder, file);
                }
                else
                {
                    sallesExixt.PassportPic = EditSelles.PassportPic;
                }
                sallesExixt.SalesName = EditSelles.SalesName;
                sallesExixt.SalesEmail = EditSelles.SalesEmail;
                sallesExixt.SalesPassword = EditSelles.SalesPassword;
                sallesExixt.SalesPhoneNumber = EditSelles.SalesPhoneNumber;
                sallesExixt.IsActive = EditSelles.IsActive;
                sallesExixt.EmployeeId = EditSelles.EmployeeId;

                var UpdatedSell = _context.Sales.Attach(sallesExixt);
                UpdatedSell.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                user.FullName = EditSelles.SalesName;
                user.PhoneNumber = EditSelles.SalesPhoneNumber;
                _context.SaveChanges();
                await _userManager.UpdateAsync(user);
                _toastNotification.AddSuccessToastMessage("Sales Edited Successfully");


                
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/ManageSales/Index");
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
