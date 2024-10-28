using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;

namespace ManoTourism.Areas.Admin.Pages.ManageHotel
{
    public class AddHotelModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        [BindProperty]
        public Hotel AddHotel { get; set; }


        public AddHotelModel(ManoContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

            AddHotel = new Hotel();

        }
        public async Task<IActionResult> OnGet()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
    
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile file, IFormFileCollection MorePhoto)
        {

            try
            {

                if (file != null)
                {
                    string folder = "Images/Hotel/";
                    AddHotel.HotelMainImage = UploadImage(folder, file);
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
                    AddHotel.HotelImages = hotelImagesList;
                }

               
                AddHotel.AddedDate = DateTime.Now;

                _context.Hotels.Add(AddHotel);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Hotel Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
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
