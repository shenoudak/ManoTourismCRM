using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManoTourism.Data;
using ManoTourism.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ListsController : Controller
    {
        
        private ManoContext _context;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly UserManager<ApplicationUser> _userManager;
        public ListsController(ManoContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
           
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserMessage>>> GetMessages()
        {
            var data = await _context.UserMessages.Select(i => new
            {
                FullName = i.FullName,
                TransDate = i.SendingDate.ToShortDateString(),
                Email = i.Email,
                UserMessageId = i.UserMessageId,
                Msg = i.Message

            }).ToListAsync();


            return Ok(new { data });
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscribeNewsLetter>>> GetSubscriptionNewsLetter()
        {
            var data = await _context.SubscribeNewsLetters.Select(i => new
            {
                Email = i.Email,
                SubscribeNewLetterId = i.SubscribeNewLetterId,
                

            }).ToListAsync();


            return Ok(new { data });
        }
        [HttpGet]
        public object GetImagesForTrip([FromQuery] int id)
        {
            var TripImages = _context.TripImages.Where(p => p.TripId == id)
                                .Select(i => new
                                {
                                    i.TripImageId,
                                    i.Image,
                                    i.TripId
                                });

            return TripImages;
        }
        [HttpPost]
        public async Task<int> RemoveImageById([FromQuery] int id)
        {
            var tripPic = await _context.TripImages.FirstOrDefaultAsync(p => p.TripImageId == id);
            var tripListCount =  _context.TripImages.Where(p => p.TripImageId == id).Count();
            if (tripListCount > 3)
            {
                _context.TripImages.Remove(tripPic);
                _context.SaveChanges();
            }
           
            
            return id;
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeUserLookup(DataSourceLoadOptions loadOptions)
        {

            var lookup = from i in _userManager.Users.Where(e => e.RoleId == (int)UserRole.Employee)
                         orderby i.FullName
                         select new
                         {
                             Value = i.Id,
                             Text = i.FullName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        [HttpGet]
        
        public async Task<IActionResult> EmployeeLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Employees.Where(e => e.IsActive == true)
                         orderby i.EmployeeName
                         select new
                         {
                             Value = i.EmployeeId,
                             Text = i.EmployeeName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> StatusLookup(DataSourceLoadOptions loadOptions)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var lookup = from i in _context.VisaRequestStatuses.Where(e => e.VisaRequestStatusId != 1)
                         orderby i.VisaRequestStatusId
                         select new
                         {
                             Value = i.VisaRequestStatusId,
                             Text = BrowserCulture == "en-US"?i.StatusTitleEn:i.StatusTitleAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        //[HttpGet]
        //public async Task<IActionResult> AllStatusLookup(DataSourceLoadOptions loadOptions)
        //{
        //    locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
        //    BrowserCulture = locale.RequestCulture.UICulture.ToString();
        //    var lookup = from i in _context.VisaRequestStatuses.Where(e => e.VisaRequestStatusId != 1)
        //                 orderby i.VisaRequestStatusId
        //                 select new
        //                 {
        //                     Value = i.VisaRequestStatusId,
        //                     Text = BrowserCulture == "en-US" ? i.StatusTitleEn : i.StatusTitleAr
        //                 };
        //    return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        //}
        //[HttpGet]
        public async Task<IActionResult> AllStatusLookup(DataSourceLoadOptions loadOptions)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var lookup = from i in _context.VisaRequestStatuses
                         orderby i.VisaRequestStatusId
                         select new
                         {
                             Value = i.VisaRequestStatusId,
                             Text = BrowserCulture == "en-US" ? i.StatusTitleEn : i.StatusTitleAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        
        [HttpGet]
        public async Task<IActionResult> EntityTypeLookup(DataSourceLoadOptions loadOptions)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var lookup = from i in _context.ManoEntityTypes
                         orderby i.ManoEntityTypeId
                         select new
                         {
                             Value = i.ManoEntityTypeId,
                             Text = BrowserCulture == "en-US" ? i.EntityTitleEn : i.EntityTitleAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        
        [HttpGet]
        public async Task<IActionResult> UserLookup(DataSourceLoadOptions loadOptions)
        {
            
            var lookup = from i in  _userManager.Users.Where(e=>e.EntityId==0)
                         orderby i.FullName
                         select new
                         {
                             Value = i.Id,
                             Text = i.FullName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        
            [HttpGet]
        public async Task<IActionResult> SellerEmployeeLookup(DataSourceLoadOptions loadOptions)
        {
            List<int> ListSeller = new List<int>();
            var user = await _userManager.GetUserAsync(User);
            ListSeller = _context.Sales.Where(e => e.Employee.EmployeeEmail == user.Email).Select(e => e.SalesId).ToList();

            var lookup = from i in _userManager.Users.Where(e => ListSeller.Contains(e.EntityId))
                         orderby i.FullName
                         select new
                         {
                             Value = i.Id,
                             Text = i.FullName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> SellerLookup(DataSourceLoadOptions loadOptions)
        {
           

            var lookup = from i in _userManager.Users.Where(e => e.RoleId == (int)UserRole.Seller)
                         orderby i.FullName
                         select new
                         {
                             Value = i.Id,
                             Text = i.FullName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        

              [HttpGet]
        public object GetImagesForHotel([FromQuery] int id)
        {
            var OfferImages = _context.HotelImages.Where(p => p.HotelId == id)
                                .Select(i => new
                                {
                                    i.HotelImageId,
                                    i.Image,
                                    i.HotelId
                                });

            return OfferImages;
        }
        [HttpGet]
        public object GetImagesForOffers([FromQuery] int id)
        {
            var OfferImages = _context.OfferImages.Where(p => p.OfferId == id)
                                .Select(i => new
                                {
                                    i.OfferImageId,
                                    i.Image,
                                    i.OfferId
                                });

            return OfferImages;
        }
        [HttpPost]
        public async Task<int> RemoveOfferImageById([FromQuery] int id)
        {
            var OfferPic = await _context.OfferImages.FirstOrDefaultAsync(p => p.OfferImageId == id);
            
                _context.OfferImages.Remove(OfferPic);
                _context.SaveChanges();
          


            return id;
        }
        [HttpPost]
        public async Task<int> RemoveHotelImageById([FromQuery] int id)
        {
            var HotelPic = await _context.HotelImages.FirstOrDefaultAsync(p => p.HotelImageId == id);

            _context.HotelImages.Remove(HotelPic);
            _context.SaveChanges();



            return id;
        }
        
    }
}