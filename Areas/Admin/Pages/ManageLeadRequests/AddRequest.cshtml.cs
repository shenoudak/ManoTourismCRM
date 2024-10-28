using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.ManageLeadRequests
{
    public class AddRequestModel : PageModel
    {

        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public List<VisaType> visaTypes = new List<VisaType>();
        public List<Country> countries = new List<Country>();
        public List<Nationality> nationalities = new List<Nationality>();
        public List<ManoEntityType> ManoEntities = new List<ManoEntityType>();
        public List<CompanyMarkting> CompanyMarktings = new List<CompanyMarkting>();
        [BindProperty]
        public Request AddNewRequest { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;

        public AddRequestModel(ManoContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _userManager = userManager;
            AddNewRequest = new Request();

        }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Redirect("/index");

                }
                locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                BrowserCulture = locale.RequestCulture.UICulture.ToString();
                url = $"{this.Request.Scheme}://{this.Request.Host}";
                visaTypes = _context.VisaTypes.Where(e => e.IsDeleted == false).ToList();
                countries = _context.Countries.Where(e => e.IsAtive == true).ToList();
                nationalities = _context.Nationalities.Where(e => e.IsAtive == true).ToList();
                ManoEntities = _context.ManoEntityTypes.ToList();
                CompanyMarktings = _context.CompanyMarktings.ToList();
            }catch(Exception ex)
            {
                return Redirect("/Admin/PageNotFound");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(string OneWayCheck)
        {

            try
            {

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Redirect("/index");

                }
                AddNewRequest.RequestDate = DateTime.Now;
                AddNewRequest.VisaRequestStatusId = 1;
                if (AddNewRequest.ManoEntityTypeId == 1)
                {
                    var visa = _context.Visas.Where(e => e.VisaId == AddNewRequest.EntityId).FirstOrDefault();
                    if (visa != null)
                    {
                        AddNewRequest.EntityTitleAr = visa.VisaTitleAr;
                        AddNewRequest.EntityTitleEn = visa.VisaTitleEn;
                        AddNewRequest.ActivityType = null;
                        AddNewRequest.HotelArrivedDate = null;
                        AddNewRequest.HotelDepartureDate = null;
                        AddNewRequest.TransactionType = null;
                    }
                 
                }
                if (AddNewRequest.ManoEntityTypeId == 2)
                {
                    var trip = _context.Trips.Where(e => e.TripId == AddNewRequest.EntityId).FirstOrDefault();
                    if (trip != null)
                    {
                        AddNewRequest.EntityTitleAr = trip.TripTitleAr;
                        AddNewRequest.EntityTitleEn = trip.TripTitleEn;
                        AddNewRequest.ActivityType = null;
                        AddNewRequest.HotelArrivedDate = null;
                        AddNewRequest.HotelDepartureDate = null;
                        AddNewRequest.TransactionType = null;
                    }
                    
                }
                if (AddNewRequest.ManoEntityTypeId == 3)
                {
                    var insurance = _context.Insurances.Where(e => e.InsuranceId == AddNewRequest.EntityId).FirstOrDefault();
                    if (insurance != null)
                    {
                        AddNewRequest.EntityTitleAr = insurance.InsuranceTitleAr;
                        AddNewRequest.EntityTitleEn = insurance.InsuranceTitleEn;
                        AddNewRequest.ActivityType = null;
                        AddNewRequest.HotelArrivedDate = null;
                        AddNewRequest.HotelDepartureDate = null;
                        AddNewRequest.TransactionType = null;
                    }

                }
                if (AddNewRequest.ManoEntityTypeId == 4)
                {
                    var esCompany = _context.EstablishingCompanies.Where(e => e.EstablishingCompanyId == AddNewRequest.EntityId).FirstOrDefault();
                    if (esCompany != null)
                    {
                        AddNewRequest.EntityTitleAr = esCompany.TitleAr;
                        AddNewRequest.EntityTitleEn = esCompany.TitleEn;
                        AddNewRequest.HotelArrivedDate = null;
                        AddNewRequest.HotelDepartureDate = null;
                        AddNewRequest.TransactionType = null;
                    }

                }
                if (AddNewRequest.ManoEntityTypeId == 5)
                {
                    var hotel = _context.Hotels.Where(e => e.HotelId == AddNewRequest.EntityId).FirstOrDefault();
                    if (hotel != null)
                    {
                        AddNewRequest.EntityTitleAr = hotel.HotelTitleAr;
                        AddNewRequest.EntityTitleEn = hotel.HotelTitleEn;
                        AddNewRequest.ActivityType = null;
                        AddNewRequest.TransactionType = null;
                    }

                }
                if (AddNewRequest.ManoEntityTypeId == 6)
                {
                    var ticket = _context.Tickets.Where(e => e.TicketId == AddNewRequest.EntityId).FirstOrDefault();
                    if (ticket != null)
                    {
                        AddNewRequest.EntityTitleAr = ticket.TicketTitleAr;
                        AddNewRequest.EntityTitleEn = ticket.TicketTitleEn;
                        AddNewRequest.HotelArrivedDate = null;
                        AddNewRequest.HotelDepartureDate = null;
                        AddNewRequest.TransactionType = null;
                        if (OneWayCheck == "1")
                        {
                            AddNewRequest.BeturnedDate = null;
                            AddNewRequest.OneWay = true;
                        }
                        if (OneWayCheck == "2")
                        {
                            AddNewRequest.OneWay = false;
                        }
                    }

                }
                if (AddNewRequest.ManoEntityTypeId == 7)
                {
                    var ComTransactions = _context.CompleteTransactions.Where(e => e.CompleteTransactionId == AddNewRequest.EntityId).FirstOrDefault();
                    if (ComTransactions != null)
                    {
                        AddNewRequest.EntityTitleAr = ComTransactions.TransactionTitleAr;
                        AddNewRequest.EntityTitleEn = ComTransactions.TransactionTitleEn;
                        AddNewRequest.HotelArrivedDate = null;
                        AddNewRequest.HotelDepartureDate = null;
                        AddNewRequest.ActivityType = null;
                    }

                }
                AddNewRequest.AffiliateName = user.FullName;
                AddNewRequest.UserId = user.Id;
               
                _context.Requests.Add(AddNewRequest);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Your Request Sended Successfully ,Person From Customer Service Will Call With You");



            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/ManageLeadRequests/Index");
        }
        public IActionResult OnGetSingleAllEntities(int ManoEntityTypeId)
        {
            
               object Entities = null;
            if (ManoEntityTypeId==1)
            {
                 Entities = _context.Visas.Where(e => e.IsActive == true && e.IsDeleted == false).Select(e => new
                {
                    EntityId = e.VisaId,
                    EntityNameAr = e.VisaTitleAr,
                    EntityNameEn = e.VisaTitleEn,


                }).ToList();
            }
            else if(ManoEntityTypeId==2)
            {
                 Entities = _context.Trips.Where(e => e.IsActive == true && e.IsDeleted == false).Select(e => new
                {
                    EntityId = e.TripId,
                    EntityNameAr = e.TripTitleAr,
                    EntityNameEn = e.TripTitleEn,


                }).ToList();
                
            }
            else if (ManoEntityTypeId == 3)
            {
                Entities = _context.Insurances.Where(e => e.IsActive == true && e.IsDeleted == false).Select(e => new
                {
                    EntityId = e.InsuranceId,
                    EntityNameAr = e.InsuranceTitleAr,
                    EntityNameEn = e.InsuranceTitleEn,


                }).ToList();

            }
            else if (ManoEntityTypeId == 4)
            {
                Entities = _context.EstablishingCompanies.Where(e => e.IsActive == true && e.IsDeleted == false).Select(e => new
                {
                    EntityId = e.EstablishingCompanyId,
                    EntityNameAr = e.TitleAr,
                    EntityNameEn = e.TitleEn,


                }).ToList();

            }
            else if (ManoEntityTypeId ==5)
            {
                Entities = _context.Hotels.Where(e => e.IsActive == true && e.IsDeleted == false).Select(e => new
                {
                    EntityId = e.HotelId,
                    EntityNameAr = e.HotelTitleAr,
                    EntityNameEn = e.HotelTitleEn,


                }).ToList();

            }
            else if (ManoEntityTypeId == 6)
            {
                Entities = _context.Tickets.Select(e => new
                {
                    EntityId = e.TicketId,
                    EntityNameAr = e.TicketTitleAr,
                    EntityNameEn = e.TicketTitleEn,


                }).ToList();

            }
            else if (ManoEntityTypeId == 7)
            {
                Entities = _context.CompleteTransactions.Select(e => new
                {
                    EntityId = e.CompleteTransactionId,
                    EntityNameAr = e.TransactionTitleAr,
                    EntityNameEn = e.TransactionTitleEn,


                }).ToList();

            }
            return new JsonResult(Entities);
        }
       
    }
}
