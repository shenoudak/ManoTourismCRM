using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;

namespace ManoTourism.Areas.Admin.Pages.ManageAboutSection
{
    public class EditAboutSectionModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public AboutSection EAboutSection { get; set; }

        public static int staticAboutSectionId = 0;
        public EditAboutSectionModel(ManoContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
        }


        public IActionResult OnGet()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            EAboutSection = _context.AboutSections.FirstOrDefault();
            if (EAboutSection == null)
            {
                return Redirect("/Admin/PageNotFound");
            }
            staticAboutSectionId = EAboutSection.AboutSectionId;

            return Page();
        }
        public async Task<IActionResult> OnPost(int AboutSectionId, IFormFile file, IFormFile Editfile)
        {
            try
            {
                var model = _context.AboutSections.Where(c => c.AboutSectionId == staticAboutSectionId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/Admin/ManageAboutSection/index");
                }


                if (file != null)
                {


                    string folder = "images/PublicSlider/";

                    model.AboutUsImage = UploadImage(folder, file);
                }
                else
                {
                    model.AboutUsImage = EAboutSection.AboutUsImage;
                }
                if (Editfile != null)
                {


                    string folder = "images/PublicSlider/";

                    model.VideoBackground = UploadImage(folder, Editfile);
                }
                else
                {
                    model.VideoBackground = EAboutSection.VideoBackground;
                }

                model.HeaderTLAR = EAboutSection.HeaderTLAR;
                model.HeaderTLEn = EAboutSection.HeaderTLEn;
                model.CustomerFocusCaptionAr = EAboutSection.CustomerFocusCaptionAr;
                model.CustomerFocusCaptionEn = EAboutSection.CustomerFocusCaptionEn;
                model.MissionCaptionAr = EAboutSection.MissionCaptionAr;
                model.MissionCaptionEn = EAboutSection.MissionCaptionEn;
                model.VideoUrl = EAboutSection.VideoUrl;
               


                var UpdatedAboutSection = _context.AboutSections.Attach(model);

                UpdatedAboutSection.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("About Section Edited successfully");

                return Redirect("/Admin/ManageAboutSection/index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageAboutSection/index");
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

