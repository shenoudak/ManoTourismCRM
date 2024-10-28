using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageTrip
{
    public class AddTourModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        public string url { get; set; }
        public IRequestCultureFeature locale;
        private readonly UserManager<ApplicationUser> _userManager;
        public string BrowserCulture;
        public List<TripType> tripTypes = new List<TripType>();
        public List<Country> countries = new List<Country>();
        [BindProperty]
        public Trip AddTrip { get; set; }


        public AddTourModel(ManoContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

            AddTrip = new Trip();
            _userManager = userManager;

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
                    tripTypes = _context.TripTypes.Where(e => e.IsDeleted == false).ToList();
                    countries = _context.Countries.Where(e => e.IsAtive == true).ToList();

                }
            }
            catch (Exception ex)
            {

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
                    string folder = "Images/Trip/";
                    AddTrip.TripImage = UploadImage(folder, file);
                }
                List<TripImage> tripImagesList = new List<TripImage>();


                if (MorePhoto.Count != 0)
                {
                    foreach (var item in MorePhoto)
                    {
                        var tripImageObj = new TripImage();
                        string folder = "Images/Trip/";
                        tripImageObj.Image = UploadImage(folder, item);
                        tripImagesList.Add(tripImageObj);


                    }
                    AddTrip.TripImages = tripImagesList;
                }

                AddTrip.TripTargetId = 1;
                AddTrip.AddedDate = DateTime.Now;

                _context.Trips.Add(AddTrip);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Trip Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/ManageTrip/Index");
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
