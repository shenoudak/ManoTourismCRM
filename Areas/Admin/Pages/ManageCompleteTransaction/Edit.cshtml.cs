using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageCompleteTransaction
{
    public class EditModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly UserManager<ApplicationUser> _userManager;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public CompleteTransaction EditComTransactions { get; set; }
        public EditModel(ManoContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }


        public async Task<IActionResult> OnGet(int CompleteTransactionId)
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
                            bool isAuthoried = _context.AssignEmployeeRoles.Any(e => e.EmployeeId == employee.EmployeeId && e.EmployeeRoleId == (int)EmpRoles.CompleteTransactionsManagement);
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
                    EditComTransactions = _context.CompleteTransactions.FirstOrDefault(a => a.CompleteTransactionId == CompleteTransactionId);
                    if (EditComTransactions == null)
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
        public async Task<IActionResult> OnPost(int CompleteTransactionId, IFormFile file)
        {
            try
            {

                var DbCompleteTransaction = _context.CompleteTransactions.Where(c => c.CompleteTransactionId == CompleteTransactionId).FirstOrDefault();

                if (DbCompleteTransaction == null)
                {
                    _toastNotification.AddErrorToastMessage("Hotel Not Found");

                    return Redirect("/Admin/ManageCompleteTransaction/Index");
                }
                if (file != null)
                {


                    string folder = "Images/Ticket/";

                    DbCompleteTransaction.Pic = UploadImage(folder, file);
                }
                else
                {
                    DbCompleteTransaction.Pic = EditComTransactions.Pic;
                }






                DbCompleteTransaction.TransactionTitleAr = EditComTransactions.TransactionTitleAr;
                DbCompleteTransaction.TransactionTitleEn = EditComTransactions.TransactionTitleEn;
                DbCompleteTransaction.TransactionDescEn = EditComTransactions.TransactionDescEn;
                DbCompleteTransaction.TransactionDescAr = EditComTransactions.TransactionDescAr;
                

                var UpdatedCompleteTransaction = _context.CompleteTransactions.Attach(DbCompleteTransaction);
                UpdatedCompleteTransaction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageCompleteTransaction/Index");
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
