using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages.ManageOffers
{
    [Authorize(Roles = "admin")]
    public class EditOfferModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public Offer EditOffer { get; set; }

        public EditOfferModel(ManoContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
        }


        public IActionResult OnGet(int OfferId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            EditOffer = _context.Offers.FirstOrDefault(a => a.OfferId == OfferId);
            if (EditOffer == null)
            {
                return Redirect("/Admin/ManageOffers");
            }
           
            return Page();
        }
        public async Task<IActionResult> OnPost(int OfferId, IFormFileCollection MorePhoto)
        {
            try
            {

                var DbOffer = _context.Offers.Where(c => c.OfferId == OfferId).FirstOrDefault();

                if (DbOffer == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/Admin/ManageOffers/Index");
                }
               
                DbOffer.OfferTitleEn = EditOffer.OfferTitleEn;
                DbOffer.OfferTitleAr = EditOffer.OfferTitleAr;
                DbOffer.IsActive = EditOffer.IsActive;
                
                var UpdatedOffer = _context.Offers.Attach(DbOffer);
                UpdatedOffer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
               

                if (MorePhoto.Count != 0)
                {
                    List<OfferImage> offerImagesList = new List<OfferImage>();

                    foreach (var item in MorePhoto)
                    {
                        var offerImageObj = new OfferImage();
                        string folder = "Images/Offer/";
                        offerImageObj.OfferId = OfferId;
                        offerImageObj.Image = UploadImage(folder, item);
                        offerImagesList.Add(offerImageObj);


                    }
                    _context.OfferImages.AddRange(offerImagesList);
                }
                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Offer Edited Successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageOffers/Index");
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


