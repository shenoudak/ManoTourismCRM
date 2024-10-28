using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;


namespace ManoTourism.Areas.Admin.Pages.ManageHotel
{
    public class EditHotelModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public Hotel EditHotel { get; set; }
        public EditHotelModel(ManoContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
        }


        public IActionResult OnGet(int HotelId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            EditHotel = _context.Hotels.FirstOrDefault(a => a.HotelId == HotelId);
            if (EditHotel == null)
            {
                return Redirect("/Admin/PageNotFound");
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPost(int HotelId, IFormFile file, IFormFileCollection MorePhoto)
        {
            try
            {

                var DbHotel = _context.Hotels.Where(c => c.HotelId == HotelId && c.IsDeleted == false).FirstOrDefault();

                if (DbHotel == null)
                {
                    _toastNotification.AddErrorToastMessage("Hotel Not Found");

                    return Redirect("/Admin/ManageHotel/Index");
                }
                if (file != null)
                {


                    string folder = "Images/Hotel/";

                    DbHotel.HotelMainImage = UploadImage(folder, file);
                }
                else
                {
                    DbHotel.HotelMainImage = EditHotel.HotelMainImage;
                }




                List<HotelImage> hotelImagesList = new List<HotelImage>();


                if (MorePhoto.Count != 0)
                {
                    foreach (var item in MorePhoto)
                    {
                        var hotelImageObj = new HotelImage();
                        string folder = "Images/Hotel/";
                        hotelImageObj.Image = UploadImage(folder, item);
                        hotelImagesList.Add(hotelImageObj);


                    }
                    _context.HotelImages.AddRange(hotelImagesList);
                }
               
                DbHotel.HotelTitleAr = EditHotel.HotelTitleAr;
                DbHotel.HotelTitleEn = EditHotel.HotelTitleEn;
                DbHotel.DescriptionAr = EditHotel.DescriptionAr;
                DbHotel.DescriptionEn = EditHotel.DescriptionEn;
                DbHotel.Location = EditHotel.Location;
                DbHotel.IsActive = EditHotel.IsActive;
                DbHotel.VideoUrl = EditHotel.VideoUrl;
                DbHotel.Review = EditHotel.Review;
                DbHotel.ReviewCount = EditHotel.ReviewCount;
                
                var UpdatedHotel = _context.Hotels.Attach(DbHotel);
                UpdatedHotel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Hotel Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageHotel/Index");
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

