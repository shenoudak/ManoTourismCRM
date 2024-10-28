using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ManoTourism.ViewModels;

namespace ManoTourism.Areas.Admin.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ChangePasswordVM changePasswordVM { get; set; }
        [BindProperty]
        public UserProfile profileDetails { get; set; }
        public IndexModel(ManoContext context, ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _signInManager = signInManager;
            _userManager = userManager;
            profileDetails = new UserProfile();
            _db = db;


        }
        public async Task<IActionResult> OnGet()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/index");

            }
            profileDetails.ProfileImage = user.Pic;
            profileDetails.FullName = user.FullName;
            profileDetails.PhoneNumber = user.PhoneNumber;
            // profileDetails = _context.Stores.Include(e => e.StoreProfileImages).Where(e => e.Email == user.Email).FirstOrDefault();

            //if (storDetails == null)
            //{
            //    return Redirect("/Login");
            //}
            //if (storDetails.CatagoriesTypes != null)
            //{
            //    storeCatagories = storDetails.CatagoriesTypes.Split(",").ToList();


            //}
            //if (storDetails.OtherCatagories != null)
            //{
            //    otherCatagories = storDetails.OtherCatagories.Split(",").ToList();


            //}

            return Page();
        }
        public async Task<IActionResult> OnPostEditProfile()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Redirect("/Identity/Account/Login");

                }



                var roleName = await _userManager.GetRolesAsync(user);
                if (roleName.FirstOrDefault() == "employee")
                {
                    var employee = _context.Employees.Where(e => e.EmployeeEmail == user.Email).FirstOrDefault();
                    if (employee == null)
                    {
                        return Redirect("/Identity/Account/Login");
                    }
                    employee.EmployeeName = profileDetails.FullName;
                    employee.EmployeePhoneNumber = profileDetails.PhoneNumber;

                    var Updatedemployee = _context.Employees.Attach(employee);
                    Updatedemployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                }
                if (roleName.FirstOrDefault() == "lead")
                {
                    var affiliate = _context.Affiliates.Where(e => e.AffiliateEmail == user.Email).FirstOrDefault();
                    if (affiliate == null)
                    {
                        return Redirect("/Identity/Account/Login");
                    }
                    affiliate.AffiliateName = profileDetails.FullName;

                    var UpdatedAff = _context.Affiliates.Attach(affiliate);
                    UpdatedAff.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                user.FullName = profileDetails.FullName;
                user.PhoneNumber = profileDetails.PhoneNumber;

                await _userManager.UpdateAsync(user);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Profile Updated Successfully");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error");
            }
            return Redirect("/Admin/Profile/Index");
        }


        public async Task<IActionResult> OnPostEditProfilePic(IFormFile Editfile)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Redirect("/Identity/Account/Login");

                }

                if (Editfile != null)
                {
                    string folder = "Images/Employee/";
                    profileDetails.ProfileImage = UploadImage(folder, Editfile);
                }

                var roleName = await _userManager.GetRolesAsync(user);
                if (roleName.FirstOrDefault() == "employee")
                {
                    var employee = _context.Employees.Where(e => e.EmployeeEmail == user.Email).FirstOrDefault();
                    if (employee == null)
                    {
                        return Redirect("/Identity/Account/Login");
                    }

                    employee.EmployeePic = profileDetails.ProfileImage;
                    var Updatedemployee = _context.Employees.Attach(employee);
                    Updatedemployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                }
                if (roleName.FirstOrDefault() == "lead")
                {
                    var affiliate = _context.Affiliates.Where(e => e.AffiliateEmail == user.Email).FirstOrDefault();
                    if (affiliate == null)
                    {
                        return Redirect("/Identity/Account/Login");
                    }

                    affiliate.AffiliatePic = profileDetails.ProfileImage;
                    var UpdatedAff = _context.Affiliates.Attach(affiliate);
                    UpdatedAff.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                user.Pic = profileDetails.ProfileImage;
                await _userManager.UpdateAsync(user);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Profile Picture Updated Successfully");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error");
            }
            return Redirect("/Admin/Profile/Index");
        }
        public async Task<IActionResult> OnPost()
        {
            if (changePasswordVM.CurrentPassword == changePasswordVM.NewPassword)
            {
                ModelState.AddModelError("", "New password Must be Diffrent from Current Password..");
                return Page();
            }
            //if (!ModelState.IsValid)
            //    return Page();
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.ChangePasswordAsync(user, changePasswordVM.CurrentPassword, changePasswordVM.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            var roleName = await _userManager.GetRolesAsync(user);
            if (roleName.FirstOrDefault() == "employee")
            {
                var employee = _context.Employees.Where(e => e.EmployeeEmail == user.Email).FirstOrDefault();
                if (employee == null)
                {
                    return Redirect("/Identity/Account/Login");
                }

                employee.EmployeePassword = changePasswordVM.NewPassword;
                var Updatedemployee = _context.Employees.Attach(employee);
                Updatedemployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            }
            if (roleName.FirstOrDefault() == "lead")
            {
                var affiliate = _context.Affiliates.Where(e => e.AffiliateEmail == user.Email).FirstOrDefault();
                if (affiliate == null)
                {
                    return Redirect("/Identity/Account/Login");
                }

                affiliate.AffiliatePassword = changePasswordVM.NewPassword;
                var UpdatedAff = _context.Affiliates.Attach(affiliate);
                UpdatedAff.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            await _signInManager.RefreshSignInAsync(user);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Password Updated Successfully");
            return Redirect("/Admin/Profile/Index");

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