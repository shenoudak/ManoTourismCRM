using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageTickets
{
    public class EditModel : PageModel
    {
        private ManoContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public Ticket EditTicket { get; set; }
        public EditModel(ManoContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }


        public async Task<IActionResult> OnGet(int TicketId)
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
                            bool isAuthoried = _context.AssignEmployeeRoles.Any(e => e.EmployeeId == employee.EmployeeId && e.EmployeeRoleId == (int)EmpRoles.TicketsManagement);
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
                    EditTicket = _context.Tickets.FirstOrDefault(a => a.TicketId == TicketId);
                    if (EditTicket == null)
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
        public async Task<IActionResult> OnPost(int TicketId, IFormFile file, IFormFileCollection MorePhoto)
        {
            try
            {

                var DbTicket = _context.Tickets.Where(c => c.TicketId == TicketId).FirstOrDefault();

                if (DbTicket == null)
                {
                    _toastNotification.AddErrorToastMessage("Hotel Not Found");

                    return Redirect("/Admin/ManageTickets/Index");
                }
                if (file != null)
                {


                    string folder = "Images/Ticket/";

                    DbTicket.Pic = UploadImage(folder, file);
                }
                else
                {
                    DbTicket.Pic = EditTicket.Pic;
                }






                DbTicket.TicketTitleAr = EditTicket.TicketTitleAr;
                DbTicket.TicketTitleEn = EditTicket.TicketTitleEn;
                DbTicket.TicketDescEn = EditTicket.TicketDescEn;
                DbTicket.TicketDescAr = EditTicket.TicketDescAr;
                

                var UpdatedTicket = _context.Tickets.Attach(DbTicket);
                UpdatedTicket.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageTickets/Index");
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
