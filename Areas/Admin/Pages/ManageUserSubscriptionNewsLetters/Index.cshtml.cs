using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
namespace ManoTourism.Areas.Admin.Pages.ManageUserSubscriptionNewsLetters
{

    public class IndexModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public SubscribeNewsLetter newsLetter { get; set; }
        public string url { get; set; }
        public IndexModel(ManoContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            newsLetter = new SubscribeNewsLetter();

        }
        public void OnGet()
        {
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }
        


        public IActionResult OnGetSingleUserSubscripedForDelete(int SubscribeNewLetterId)
        {
            var UserMsg = _context.SubscribeNewsLetters.Where(c => c.SubscribeNewLetterId == SubscribeNewLetterId).FirstOrDefault();
            return new JsonResult(UserMsg);
        }

        public async Task<IActionResult> OnPostDeleteSubscripedUser(int SubscribeNewLetterId)
        {
            try
            {
                SubscribeNewsLetter subscribeNewsLetter = _context.SubscribeNewsLetters.Where(e => e.SubscribeNewLetterId == SubscribeNewLetterId).FirstOrDefault();


                if (subscribeNewsLetter != null)
                {


                    _context.SubscribeNewsLetters.Remove(subscribeNewsLetter);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("User Subscribed To News Letter Deleted successfully");


                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Something went wrong Try Again");
                }
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return RedirectToPage("/ManageUserSubscriptionNewsLetters/Index");
        }




    }
}
