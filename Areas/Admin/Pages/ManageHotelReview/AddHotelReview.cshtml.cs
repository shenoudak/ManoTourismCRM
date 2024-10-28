using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;

namespace ManoTourism.Areas.Admin.Pages.ManageHotelReview
{
    public class AddHotelReviewModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public static int staticHotelId = 0;

        [BindProperty]
        public HotelReview AddHotelReview { get; set; }
       


        public AddHotelReviewModel(ManoContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

            AddHotelReview = new HotelReview();

        }
        public async Task<IActionResult> OnGet(int HotelId)
        {
            var Hotel = _context.Hotels.FirstOrDefault(a => a.HotelId == HotelId);
            if (Hotel == null)
            {
                return Redirect("/Admin/PageNotFound");
            }
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            staticHotelId = HotelId;
           
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile file)
        {

            try
            {

                if (file != null)
                {
                    string folder = "Images/Hotel/";
                    AddHotelReview.ReviewWriterImage = UploadImage(folder, file);
                }

                AddHotelReview.HotelId = staticHotelId;
                AddHotelReview.ReviewDate = DateTime.Now;
                _context.HotelReviews.Add(AddHotelReview);
                _context.SaveChanges();



            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
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