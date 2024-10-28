using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageTrip
{
    public class EditTourModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public Trip EditTrip { get; set; }

        public List<TripType> tripTypes = new List<TripType>();
        public List<Country> countries = new List<Country>();
        public EditTourModel(ManoContext context, IToastNotification toastNotification, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;
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
                    url = $"{this.Request.Scheme}://{this.Request.Host}";
                    EditTrip = _context.Trips.FirstOrDefault(a => a.TripId == TripId);
                    if (EditTrip == null)
                    {
                        return Redirect("/Admin/PageNotFound");
                    }
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
        public async Task<IActionResult> OnPost(int TripId, IFormFile file, IFormFileCollection MorePhoto)
        {
            try
            {

                var DbTrip = _context.Trips.Where(c => c.TripId == TripId && c.IsDeleted == false).FirstOrDefault();

                if (DbTrip == null)
                {
                    _toastNotification.AddErrorToastMessage("Trip Not Found");

                    return Redirect("/Admin/ManageTrip/Index");
                }
                if (file != null)
                {


                    string folder = "Images/Trip/";

                    DbTrip.TripImage = UploadImage(folder, file);
                }
                else
                {
                    DbTrip.TripImage = EditTrip.TripImage;
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
                    _context.TripImages.AddRange(tripImagesList);
                }
                DbTrip.TripTargetId = 1;
                DbTrip.TripTitleAr = EditTrip.TripTitleAr;
                DbTrip.TripTitleEn = EditTrip.TripTitleEn;
                DbTrip.DescriptionAr = EditTrip.DescriptionAr;
                DbTrip.DescriptionEn = EditTrip.DescriptionEn;
                DbTrip.OldPricePerPerson = EditTrip.OldPricePerPerson;
                DbTrip.NewPricePerPerson = EditTrip.NewPricePerPerson;
                DbTrip.TripTypeId = EditTrip.TripTypeId;
                DbTrip.CountryId = EditTrip.CountryId;
                DbTrip.IsActive = EditTrip.IsActive;
                DbTrip.VideoUrl = EditTrip.VideoUrl;
                DbTrip.DurationInDays = EditTrip.DurationInDays;
                var UpdatedTrip = _context.Trips.Attach(DbTrip);
                UpdatedTrip.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Trip Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

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


