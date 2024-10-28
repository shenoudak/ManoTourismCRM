using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using ManoTourism.Data;
namespace ManoTourism
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]

    [ApiController]
    public class SettingController : Controller
    {
   

        public SettingController()
        {
            
        }
        [HttpGet]
        public IActionResult ChangeLanguage(string culture, string url)

        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) }
                );
            return Redirect("~" + url);
        }
       
    }
}
