using ManoTourism.Models;
using ManoTourism.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace ManoTourism.Areas.Admin.Pages.ManageAboutSection
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;



        public string url { get; set; }


        [BindProperty]
        public AboutSection aboutSection { get; set; }


        public List<AboutSection> aboutSections = new List<AboutSection>();

        public AboutSection aboutSectionObj { get; set; }
        private readonly IToastNotification _toastNotification;

        public IndexModel(ManoContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;

            aboutSection = new AboutSection();
            aboutSectionObj = new AboutSection();
            _toastNotification = toastNotification;
        }
        public void OnGet()
        {
            aboutSections = _context.AboutSections.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }
        public IActionResult OnGetSingleAboutSectionForView(int AboutSectionId)
        {
            var Result = _context.AboutSections.Where(c => c.AboutSectionId == AboutSectionId).FirstOrDefault();
            return new JsonResult(Result);
        }
        public IActionResult OnGetSingleAboutSectionForEdit(int AboutSectionId)
        {
            aboutSection = _context.AboutSections.Where(c => c.AboutSectionId == AboutSectionId).FirstOrDefault();

            return new JsonResult(aboutSection);
        }
        public async Task<IActionResult> OnPostEditAboutSection(int AboutSectionId, IFormFile Editfile)
        {

            try
            {
                var model = _context.AboutSections.Where(c => c.AboutSectionId == AboutSectionId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/Admin/ManageAboutSection/index");
                }


                if (Editfile != null)
                {


                    string folder = "images/PublicSlider/";

                    model.AboutVideo = UploadImage(folder, Editfile);
                }
                else
                {
                    model.AboutVideo = aboutSection.AboutVideo;
                }

                model.HeaderTLAR = aboutSection.HeaderTLAR;
                model.HeaderTLEn = aboutSection.HeaderTLEn;
                model.CustomerFocusCaptionAr = aboutSection.CustomerFocusCaptionAr;
                model.CustomerFocusCaptionEn = aboutSection.CustomerFocusCaptionEn;
                model.MissionCaptionAr = aboutSection.MissionCaptionAr;
                model.MissionCaptionEn = aboutSection.MissionCaptionEn;


                var UpdatedAboutSection = _context.AboutSections.Attach(model);

                UpdatedAboutSection.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Mini SubCategory Edited successfully");

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



