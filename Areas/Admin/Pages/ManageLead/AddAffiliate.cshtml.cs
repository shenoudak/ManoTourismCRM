using ManoTourism.Data;
using ManoTourism.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace ManoTourism.Areas.Admin.Pages.ManageLead
{
    [Authorize(Roles = "admin")]
    public class AddAffiliateModel : PageModel
    {
       
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Affiliate AddLead { get; set; }


        public AddAffiliateModel(ManoContext context, ApplicationDbContext db, IWebHostEnvironment hostEnvironment, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            AddLead = new Affiliate();
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;

        }
        public async Task<IActionResult> OnGet()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";

            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile file)
        {
            try
            {
                if (AddLead.AffiliateEmail != null)
                {
                    var userExists = await _userManager.FindByEmailAsync(AddLead.AffiliateEmail);
                    if (userExists != null)
                    {
                        _toastNotification.AddErrorToastMessage("Email is already exist");
                        return Redirect("/Admin/ManageLead/Index");

                    }
                    string AffiliateImage = null;
                    if (file != null)
                    {
                        string folder = "Images/Employee/";
                        AffiliateImage = UploadImage(folder, file);
                    }
                    var user = new ApplicationUser { UserName = AddLead.AffiliateEmail, Email = AddLead.AffiliateEmail, FullName = AddLead.AffiliateName, Pic = AffiliateImage,RoleId=4 };
                    var result = await _userManager.CreateAsync(user, AddLead.AffiliatePassword);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "lead");

                        AddLead.AffiliatePic = AffiliateImage;
                        _context.Affiliates.Add(AddLead);
                        _context.SaveChanges();
                        _toastNotification.AddSuccessToastMessage("Affiliate Added Successfully");
                    }
                }
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/ManageLead/Index");
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
