using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using ManoTourism.DataTables;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages.ManageEmployee
{
    [Authorize(Roles = "admin")]
    public class AddEmployeeModel : PageModel
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
        public List<EmployeeRole> employeeRoles = new List<EmployeeRole>();
        //[BindProperty]
        //public List<EmployeeRole> Roles = new List<EmployeeRole>();
        //public string MyProperty { get; set; }
        [BindProperty]
        public Employee AddEmployee { get; set; }


        public AddEmployeeModel(ManoContext context, ApplicationDbContext db, IWebHostEnvironment hostEnvironment, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            AddEmployee = new Employee();
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;

        }
        public async Task<IActionResult> OnGet()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            employeeRoles = _context.EmployeeRoles.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile file)
        {
            try
            {
                if (AddEmployee.EmployeeEmail != null)
                {
                    var userExists = await _userManager.FindByEmailAsync(AddEmployee.EmployeeEmail);
                    if (userExists != null)
                    {
                        _toastNotification.AddErrorToastMessage("Email is already exist");
                        return Redirect("/Admin/ManageEmployee/Index");

                    }
                    string EmployeeImage = null;
                    if (file != null)
                    {
                        string folder = "Images/Employee/";
                        EmployeeImage = UploadImage(folder, file);
                    }
                    var user = new ApplicationUser { UserName = AddEmployee.EmployeeEmail, Email = AddEmployee.EmployeeEmail, FullName = AddEmployee.EmployeeName, Pic = EmployeeImage, PhoneNumber = AddEmployee.EmployeePhoneNumber,RoleId=2 };
                    var result = await _userManager.CreateAsync(user, AddEmployee.EmployeePassword);
                    string RolesEmpList = Request.Form["EmpRoleList"];
                    List<AssignEmployeeRoles> assignEmployeeRoles = new List<AssignEmployeeRoles>();
                    if (RolesEmpList != null)
                    {
                        int roleId = 0;
                        var values = RolesEmpList.Split(",");
                        for (int i = 0; i < values.Length; i++)
                        {
                            bool checkRes = int.TryParse(values[i], out roleId);
                            if (checkRes)
                            {
                                var empAssignRole = new AssignEmployeeRoles();
                                empAssignRole.EmployeeRoleId = roleId;
                                assignEmployeeRoles.Add(empAssignRole);
                            }


                        }
                    }

                        //foreach (var item in Roles)
                        //{
                        //    var empAssignRole = new AssignEmployeeRoles();
                        //    empAssignRole.EmployeeRoleId = item;
                        //    assignEmployeeRoles.Add(empAssignRole);
                        //}
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "employee");

                            AddEmployee.EmployeePic = EmployeeImage;
                            AddEmployee.AssignEmployeeRoles = assignEmployeeRoles;
                            _context.Employees.Add(AddEmployee);
                            _context.SaveChanges();
                            _toastNotification.AddSuccessToastMessage("Employee Added Successfully");
                        }
                    }
                }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/ManageEmployee/Index");
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
