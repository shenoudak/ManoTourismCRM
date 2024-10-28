using ManoTourism.Models;
using ManoTourism.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageTripProgram
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;



        public string url { get; set; }
        [BindProperty]
        public int BindTripId { get; set; }


        [BindProperty]
        public TripProgram tripProgram { get; set; }


        public List<TripProgram> tripPrograms = new List<TripProgram>();
        public IRequestCultureFeature locale;
        private readonly UserManager<ApplicationUser> _userManager;
        public string BrowserCulture;
        public TripProgram tripProgramObj { get; set; }
        public int TripId { get; set; }

        public IndexModel(ManoContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager
                                           )
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            tripProgram = new TripProgram();
            tripProgramObj = new TripProgram();
        }
        public async Task<IActionResult> OnGet(int TripId)
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
                    var trip = _context.Trips.Where(e => e.TripId == TripId).FirstOrDefault();

                    if (trip == null)
                    {
                        return Redirect("/Admin/PageNotFound");

                    }
                    BindTripId = TripId;

                    tripPrograms = _context.TripPrograms.Where(e => e.TripId == TripId).ToList();
                    url = $"{this.Request.Scheme}://{this.Request.Host}";

                }
            }
            catch (Exception ex)
            {

                return Redirect("/Admin/PageNotFound");
            }
            
            return Page();
        }
        public IActionResult OnGetSingleTripProgramForView(int TripProgramId)
        {
            var Result = _context.TripPrograms.Where(c => c.TripProgramId == TripProgramId).FirstOrDefault();
            return new JsonResult(Result);
        }



    }
}
