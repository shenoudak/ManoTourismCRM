using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace ManoTourism.Areas.Admin.Pages.ManagePublicSlider
{
    public class EditPublicSliderModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public PublicSlider EditPublicSlider { get; set; }


        public EditPublicSliderModel(ManoContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
        }


        public IActionResult OnGet(int PublicSliderId)
        {
            try
            {
                locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                BrowserCulture = locale.RequestCulture.UICulture.ToString();
                url = $"{this.Request.Scheme}://{this.Request.Host}";
                EditPublicSlider = _context.PublicSliders.Where(e => e.PublicSliderId == PublicSliderId).FirstOrDefault();
                if (EditPublicSlider == null)
                {
                    return Redirect("/Admin/PageNotFound");
                }
            }
            catch(Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
                return Redirect("/Admin/PageNotFound");
            }
           


            return Page();
        }
        public async Task<IActionResult> OnPost(int PublicSliderId, IFormFile file)
        {
            try
            {

                var DbPublicSlider = _context.PublicSliders.Where(c => c.PublicSliderId == PublicSliderId).FirstOrDefault();

                if (DbPublicSlider == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/Admin/ManagePublicSlider/Index");
                }
                if (file != null)
                {


                    string folder = "Images/PublicSlider/";

                    DbPublicSlider.Background = UploadImage(folder, file);
                }
                else
                {
                    DbPublicSlider.Background = EditPublicSlider.Background;
                }
                DbPublicSlider.HeaderAr = EditPublicSlider.HeaderAr;
                DbPublicSlider.HeaderEn = EditPublicSlider.HeaderEn;
                DbPublicSlider.CaptionAr = EditPublicSlider.CaptionAr;
                DbPublicSlider.CaptionEn = EditPublicSlider.CaptionEn;
                DbPublicSlider.IsImage = EditPublicSlider.IsImage;
                var UpdatedpublicSlider = _context.PublicSliders.Attach(DbPublicSlider);
                UpdatedpublicSlider.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Public Slider Edited successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManagePublicSlider/Index");
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
