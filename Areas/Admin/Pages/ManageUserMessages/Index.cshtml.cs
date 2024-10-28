using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace ManoTourism.Areas.Admin.Pages.ManageUserMessages
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public UserMessage userMessage { get; set; }
        public string url { get; set; }
        public IndexModel(ManoContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            userMessage = new UserMessage();

        }
        public void OnGet()
        {
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }
        public IActionResult OnGetSingleMessageForView(int UserMessageId)
        {
            var Result = _context.UserMessages.Where(c => c.UserMessageId == UserMessageId).FirstOrDefault();

            return new JsonResult(Result);
        }


        public IActionResult OnGetSingleMessageForDelete(int UserMessageId)
        {
            var UserMsg = _context.UserMessages.Where(c => c.UserMessageId == UserMessageId).FirstOrDefault();
            return new JsonResult(UserMsg);
        }

        public async Task<IActionResult> OnPostDeleteMessage(int UserMessageId)
        {
            try
            {
                UserMessage MessageObj = _context.UserMessages.Where(e => e.UserMessageId == UserMessageId).FirstOrDefault();


                if (MessageObj != null)
                {


                    _context.UserMessages.Remove(MessageObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("Message Deleted successfully");


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

            return RedirectToPage("/ManageUserMessages/Index");
        }




    }
}
