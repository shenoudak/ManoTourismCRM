using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Authentication;


namespace ManoTourism.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ManoContext _manoContext;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IndexModel(ILogger<IndexModel> logger, ManoContext manoContext, IToastNotification toastNotification, SignInManager<ApplicationUser> signInManager,
           
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _manoContext = manoContext;
            _toastNotification = toastNotification;
        }

        public IActionResult OnGet()
        {
            return Redirect("/identity/account/login");

        }
        

    }
}