using ManoTourism.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Localization;
namespace ManoTourism.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatisticsController : Controller
    {
        private ManoContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public StatisticsController(ManoContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _context = context;
            _userManager = userManager;
            _db = db;

        }


        [HttpGet]
        public object GetRequestsPerType()
        {
            var data = _context.Requests.Include(e => e.VisaRequestStatus).Include(e => e.ManoEntityType)
                .GroupBy(c => c.ManoEntityTypeId).

                Select(g => new
                {
                    Lable = BrowserCulture == "en-US" ? g.FirstOrDefault().ManoEntityType.EntityTitleEn : g.FirstOrDefault().ManoEntityType.EntityTitleAr,
                    Count = g.Count(),
                }).OrderByDescending(r => r.Count).ToList();

            return data;


        }
        [HttpGet]
        public async Task<object> GetRequestsPerTypeForLead()
        {
            var user = await _userManager.GetUserAsync(User);
            var data = _context.Requests.Include(e => e.ManoEntityType).Where(e=>e.UserId==user.Id)
                .GroupBy(c => c.ManoEntityTypeId).

                Select(g => new
                {
                    Lable = BrowserCulture == "en-US" ? g.FirstOrDefault().ManoEntityType.EntityTitleEn : g.FirstOrDefault().ManoEntityType.EntityTitleAr,
                    Count = g.Count(),
                }).OrderByDescending(r => r.Count).ToList();

            return data;


        }

        [HttpGet]
        public async Task<object> GetRequestsPerMounth()
        {

            var data = _context.Requests
                .GroupBy(c => c.RequestDate.Date.Month).

                Select(g => new
                {
                    Lable = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                    Count = g.Count(),
                }).ToList();


            return data;


        }
        [HttpGet]
        public List<object> GetDountChart()
        {

            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            List<object> dataDount = new List<object>();
            List<string> labels = new List<string>();
            List<double> Revenue = new List<double>();
            var StatusList = _context.VisaRequestStatuses.ToList();
            foreach (var item in StatusList)
            {
                if (BrowserCulture == "en-US")
                {
                    labels.Add(item.StatusTitleEn);
                }
                else
                {
                    labels.Add(item.StatusTitleAr);
                }
                double TemplatesRevenuePerCatagory = _context.Requests.Where(e => e.VisaRequestStatusId == item.VisaRequestStatusId).Count();
                Revenue.Add(TemplatesRevenuePerCatagory);
            }
            dataDount.Add(labels);
            dataDount.Add(Revenue);
            return dataDount;
        }
        [HttpGet]
        public async Task<IEnumerable<object>> GetDountChartForLead()
        {
            var user =await  _userManager.GetUserAsync(User);
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            List<object> dataDount = new List<object>();
            List<string> labels = new List<string>();
            List<double> Revenue = new List<double>();
            var StatusList = _context.VisaRequestStatuses.ToList();
            foreach (var item in StatusList)
            {
                if (BrowserCulture == "en-US")
                {
                    labels.Add(item.StatusTitleEn);
                }
                else
                {
                    labels.Add(item.StatusTitleAr);
                }
                double TemplatesRevenuePerCatagory = _context.Requests.Where(e => e.VisaRequestStatusId == item.VisaRequestStatusId).Where(e=>e.UserId==user.Id).Count();
                Revenue.Add(TemplatesRevenuePerCatagory);
            }
            dataDount.Add(labels);
            dataDount.Add(Revenue);
            return dataDount;
        }

    }
}
