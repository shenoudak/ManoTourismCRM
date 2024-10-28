using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageTripProgram
{
    public class EditProgramDayModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly UserManager<ApplicationUser> _userManager;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public TripProgram EditTripProgram { get; set; }


        public EditProgramDayModel(ManoContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }


        public async Task<IActionResult> OnGet(int TripProgramId)
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
                            bool isAuthoried = _context.AssignEmployeeRoles.Any(e => e.EmployeeId == employee.EmployeeId && e.EmployeeRoleId == (int)EmpRoles.TripsManagement);
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
                    EditTripProgram = _context.TripPrograms.FirstOrDefault(a => a.TripProgramId == TripProgramId);
                    if (EditTripProgram == null)
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
        public async Task<IActionResult> OnPost(int TripProgramId, IFormFile file)
        {
            try
            {

                var DbTripProg = _context.TripPrograms.Where(c => c.TripProgramId == TripProgramId).FirstOrDefault();

                if (DbTripProg == null)
                {
                    _toastNotification.AddErrorToastMessage("Trip Day Program Not Found");

                    return Redirect("/Admin/ManageTripProgram/Index");
                }




   
                DbTripProg.HeaderAr = EditTripProgram.HeaderAr;
                DbTripProg.HeaderEn = EditTripProgram.HeaderEn;
                DbTripProg.DayNumber = EditTripProgram.DayNumber;
                DbTripProg.DescriptionAr = EditTripProgram.DescriptionAr;
                DbTripProg.DescriptionEn = EditTripProgram.DescriptionEn;
               
               

                var UpdatedTripProgram = _context.TripPrograms.Attach(DbTripProg);
                UpdatedTripProgram.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Trip Program Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageTripProgram/Index");
        }



    
    }
}

