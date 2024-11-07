using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace ManoTourism.Areas.Admin.Pages.ManageSales
{
    [Authorize(Roles = "admin")]
    public class AddSalesModel : PageModel
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

        [BindProperty]
        public Sales AddSalesUser { get; set; }


        public AddSalesModel(ManoContext context, ApplicationDbContext db, IWebHostEnvironment hostEnvironment, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            AddSalesUser = new Sales();
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;

        }
        public async Task<IActionResult> OnGet()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            employees = _context.Employees.Where(e=>e.IsActive==true&&e.IsDeleted==false).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile file)
        {
            try
            {
                if (AddSalesUser.SalesEmail != null)
                {
                    var userExists = await _userManager.FindByEmailAsync(AddSalesUser.SalesEmail);
                    if (userExists != null)
                    {
                        _toastNotification.AddErrorToastMessage("Email is already exist");
                        return Redirect("/Admin/ManageSales/Index");

                    }
                    string SalesPassportImage = null;
                    if (file != null)
                    {
                        string folder = "Images/Sales/";
                        SalesPassportImage = UploadImage(folder, file);
                    }
                    var user = new ApplicationUser { UserName = AddSalesUser.SalesEmail, Email = AddSalesUser.SalesEmail, FullName = AddSalesUser.SalesName, Pic = AddSalesUser.PassportPic, PhoneNumber = AddSalesUser.SalesPhoneNumber,RoleId=5 };
                    var result = await _userManager.CreateAsync(user, AddSalesUser.SalesPassword);
                    //string RolesEmpList = Request.Form["EmpRoleList"];
                    List<AssignEmployeeRoles> assignEmployeeRoles = new List<AssignEmployeeRoles>();
                    //if (RolesEmpList != null)
                    //{
                    //    int roleId = 0;
                    //    var values = RolesEmpList.Split(",");
                    //    for (int i = 0; i < values.Length; i++)
                    //    {
                    //        bool checkRes = int.TryParse(values[i], out roleId);
                    //        if (checkRes)
                    //        {
                    //            var empAssignRole = new AssignEmployeeRoles();
                    //            empAssignRole.EmployeeRoleId = roleId;
                    //            assignEmployeeRoles.Add(empAssignRole);
                    //        }


                    //    }
                    //}


                    if (result.Succeeded)
                    {
                        
                        await _userManager.AddToRoleAsync(user, "selles");

                        AddSalesUser.PassportPic = SalesPassportImage;
                        //AddSalesUser.AssignEmployeeRoles = assignEmployeeRoles;
                        _context.Sales.Add(AddSalesUser);
                        _context.SaveChanges();
                        user.EntityId = AddSalesUser.SalesId;
                        _db.SaveChanges();
                        _toastNotification.AddSuccessToastMessage("Sales User Added Successfully");
                    }
                }
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