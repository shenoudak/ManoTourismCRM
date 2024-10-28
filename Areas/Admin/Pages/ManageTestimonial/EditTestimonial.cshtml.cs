using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;

namespace ManoTourism.Areas.Admin.Pages.ManageTestimonial
{
    public class EditTestimonialModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public Testimonial EditTestimonial { get; set; }


        public EditTestimonialModel(ManoContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
        }


        public IActionResult OnGet(int TestimonialId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            EditTestimonial = _context.Testimonials.FirstOrDefault(a => a.TestimonialId == TestimonialId);
            if (EditTestimonial == null)
            {
                return Redirect("/Admin/PageNotFound");
            }
           
            return Page();
        }
        public async Task<IActionResult> OnPost(int TestimonialId, IFormFile file)
        {
            try
            {

                var DbTestimonial = _context.Testimonials.Where(c => c.TestimonialId == TestimonialId).FirstOrDefault();

                if (DbTestimonial == null)
                {
                    _toastNotification.AddErrorToastMessage("Review Not Found");

                    return Redirect("/Admin/ManageTestimonial/Index");
                }
                if (file != null)
                {


                    string folder = "Images/Testimonial/";

                    DbTestimonial.ReviewImage = UploadImage(folder, file);
                }
                else
                {
                    DbTestimonial.ReviewImage = EditTestimonial.ReviewImage;
                }



                DbTestimonial.TitleAr = EditTestimonial.TitleAr;
                DbTestimonial.TitleEn = EditTestimonial.TitleEn;
                DbTestimonial.NameAr = EditTestimonial.NameAr;
                DbTestimonial.NameEn = EditTestimonial.NameEn;
                DbTestimonial.Rating = EditTestimonial.Rating;
                DbTestimonial.DescriptionAr = EditTestimonial.DescriptionAr;
                DbTestimonial.DescriptionEn = EditTestimonial.DescriptionEn;
                DbTestimonial.IsActive = EditTestimonial.IsActive;
              
                var UpdatedTestimonial = _context.Testimonials.Attach(DbTestimonial);
                UpdatedTestimonial.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("User Review Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageTestimonial/Index");
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

