using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;

namespace ManoTourism.Areas.Admin.Pages.ManageInsurance
{
    public class AddInsuranceModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public List<InsuranceType> insuranceTypes = new List<InsuranceType>();
        [BindProperty]
        public Insurance AddInsurance { get; set; }


        public AddInsuranceModel(ManoContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

            AddInsurance = new Insurance();

        }
        public async Task<IActionResult> OnGet()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            insuranceTypes = _context.InsuranceTypes.Where(e => e.IsDeleted == false).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile file)
        {

            try
            {

                if (file != null)
                {
                    string folder = "Images/Insurance/";
                    AddInsurance.Pic = UploadImage(folder, file);
                }
                AddInsurance.AddedDate = DateTime.Now;
                _context.Insurances.Add(AddInsurance);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Insurances Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/ManageInsurance/Index");
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