using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
namespace ManoTourism.Areas.Admin.Pages.ManageHotelReview
{
    public class EditHotelReviewModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        public static int staticHotelId = 0;
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public HotelReview EditHotelReview { get; set; }


        public EditHotelReviewModel(ManoContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
        }


        public IActionResult OnGet(int HotelReviewId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            EditHotelReview = _context.HotelReviews.FirstOrDefault(a => a.HotelReviewId == HotelReviewId);
            if (EditHotelReview == null)
            {
                return Redirect("/Admin/PageNotFound");
            }
            staticHotelId = EditHotelReview.HotelId;
            return Page();
        }
        public async Task<IActionResult> OnPost(int HotelReviewId, IFormFile file)
        {
            try
            {

                var DbHotelReview = _context.HotelReviews.Where(c => c.HotelReviewId == HotelReviewId).FirstOrDefault();

                if (DbHotelReview == null)
                {
                    _toastNotification.AddErrorToastMessage("Review Not Found");

                    return Redirect($"/Admin/ManageHotelReview/Index?HotelId={staticHotelId}");

                }
                if (file != null)
                {


                    string folder = "Images/Hotel/";

                    DbHotelReview.ReviewWriterImage = UploadImage(folder, file);
                }
                else
                {
                    DbHotelReview.ReviewWriterImage = EditHotelReview.ReviewWriterImage;
                }



                DbHotelReview.ReviewWriterNameAr = EditHotelReview.ReviewWriterNameAr;
                DbHotelReview.ReviewWriterNameEn = EditHotelReview.ReviewWriterNameEn;
                DbHotelReview.ReviewAr = EditHotelReview.ReviewAr;
                DbHotelReview.ReviewEn = EditHotelReview.ReviewEn;
                DbHotelReview.HotelId = EditHotelReview.HotelId;
                DbHotelReview.IsActive = EditHotelReview.IsActive;
                DbHotelReview.Rate = EditHotelReview.Rate;
               

                var UpdatedHotelReview = _context.HotelReviews.Attach(DbHotelReview);
                UpdatedHotelReview.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("User Review Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect($"/Admin/ManageHotelReview/Index?HotelId={staticHotelId}");
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
