using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages.ManageSocialContact
{
    [Authorize(Roles = "admin")]
    public class EditSocialContactModel : PageModel
    {
        private ManoContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public ContactSocial EditContactSocial { get; set; }


        public EditSocialContactModel(ManoContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
        }


        public IActionResult OnGet(int ContactSocialId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            EditContactSocial = _context.ContactSocials.Where(e=>e.ContactSocialId== ContactSocialId).FirstOrDefault();
            if (EditContactSocial == null)
            {
                return Redirect("/Admin/PageNotFound");
            }


            return Page();
        }
        public async Task<IActionResult> OnPost(int ContactSocialId, IFormFile file)
        {
            try
            {

                var DbContact = _context.ContactSocials.Where(c => c.ContactSocialId == EditContactSocial.ContactSocialId).FirstOrDefault();

                if (DbContact == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/Admin/ManageSocialContact/Index");
                }
                if (file != null)
                {


                    string folder = "Images/ContactImages/";

                    DbContact.ContactUsImage = UploadImage(folder, file);
                }
                else
                {
                    DbContact.ContactUsImage = EditContactSocial.ContactUsImage;
                }


                DbContact.FirstPhoneNumber = EditContactSocial.FirstPhoneNumber;
                DbContact.SecondPhoneNumber = EditContactSocial.SecondPhoneNumber;
                DbContact.FirstEmail = EditContactSocial.FirstEmail;
                DbContact.SecondEmail = EditContactSocial.SecondEmail;
                DbContact.LocationTitleAr = EditContactSocial.LocationTitleAr;
                DbContact.LocationTitleEn = EditContactSocial.LocationTitleEn;
                DbContact.OpentingTimeAr = EditContactSocial.OpentingTimeAr;
                DbContact.OpentingTimeEn = EditContactSocial.OpentingTimeEn;
                DbContact.Facebook = EditContactSocial.Facebook;
                DbContact.Instagram = EditContactSocial.Instagram;
                DbContact.WhatsApp = EditContactSocial.WhatsApp;
                DbContact.Youtube = EditContactSocial.Youtube;
                DbContact.Telegram = EditContactSocial.Telegram;
                DbContact.Twiter = EditContactSocial.Twiter;
                var UpdatedContact = _context.ContactSocials.Attach(DbContact);
                UpdatedContact.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Contacts Edited successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageSocialContact/Index");
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
