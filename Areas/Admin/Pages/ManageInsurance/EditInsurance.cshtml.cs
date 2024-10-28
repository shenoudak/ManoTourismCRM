using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
namespace ManoTourism.Areas.Admin.Pages.ManageInsurance
{
    public class EditInsuranceModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public List<InsuranceType> insuranceTypes = new List<InsuranceType>();
        [BindProperty]
        public Insurance EInsurance { get; set; }


        public EditInsuranceModel(ManoContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

            EInsurance = new Insurance();

        }
        public async Task<IActionResult> OnGet(int InsuranceId)
        {
            EInsurance = _context.Insurances.FirstOrDefault(a => a.InsuranceId == InsuranceId);
            if (EInsurance == null)
            {
                return Redirect("/Admin/PageNotFound");
            }
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            insuranceTypes = _context.InsuranceTypes.Where(e => e.IsDeleted == false).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile file,int InsuranceId)
        {
            try
            {

                var DbInsurance = _context.Insurances.Where(c => c.InsuranceId == InsuranceId && c.IsDeleted == false).FirstOrDefault();

                if (DbInsurance == null)
                {
                    _toastNotification.AddErrorToastMessage("Insurance Not Found");

                    return Redirect("/Admin/ManageInsurance/Index");
                }
                if (file != null)
                {


                    string folder = "Images/Insurance/";

                    DbInsurance.Pic = UploadImage(folder, file);
                }
                else
                {
                    DbInsurance.Pic = EInsurance.Pic;
                }



                DbInsurance.InsuranceTitleAr = EInsurance.InsuranceTitleAr;
                DbInsurance.InsuranceTitleEn = EInsurance.InsuranceTitleEn;
                DbInsurance.DurationInMonth = EInsurance.DurationInMonth;
                DbInsurance.OldPrice = EInsurance.OldPrice;
                DbInsurance.NewPrice = EInsurance.NewPrice;
                DbInsurance.DescriptionAr = EInsurance.DescriptionAr;
                DbInsurance.DescriptionEn = EInsurance.DescriptionEn;
                DbInsurance.IsActive = EInsurance.IsActive;
                DbInsurance.InsuranceTypeId = EInsurance.InsuranceTypeId;
               
                var UpdatedInsurance = _context.Insurances.Attach(DbInsurance);
                UpdatedInsurance.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Insurance Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

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