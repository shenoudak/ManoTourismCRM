using ManoTourism.Data;
using ManoTourism.DataTables;
using ManoTourism.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Identity;
namespace ManoTourism.Areas.Admin.Pages.ManageTrip
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
 
        public string url { get; set; }
        public IRequestCultureFeature locale;
        private readonly UserManager<ApplicationUser> _userManager;
        public string BrowserCulture;
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;


        }
        public async Task<IActionResult> OnGet()
        {
            bool aleadyAuthorized = false;
            try
            {
                url = $"{this.Request.Scheme}://{this.Request.Host}";
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Redirect("/Identity/Account/Login");

                }
                var roleName = await _userManager.GetRolesAsync(user);
                if (roleName.FirstOrDefault() == "admin" || roleName.FirstOrDefault() == "employee")
                {
                    if (roleName.FirstOrDefault() == "admin")
                    {
                        aleadyAuthorized = true;
                    }
                    if (roleName.FirstOrDefault() == "employee")
                    {
                        var employee = _context.Employees.Where(e => e.EmployeeEmail == user.Email).FirstOrDefault();
                        if (employee == null)
                        {
                            return Redirect("/Admin/AccessDenied");
                        }
                        else
                        {
                            bool isAuthoried = _context.AssignEmployeeRoles.Any(e => e.EmployeeId == employee.EmployeeId && e.EmployeeRoleId == (int)EmpRoles.TripsManagement);
                            if (isAuthoried)
                            {
                                aleadyAuthorized = true;
                            }
                            else
                            {
                                return Redirect("/Admin/AccessDenied");
                            }
                        }

                    }
                }

                else
                {
                    return Redirect("/Admin/AccessDenied");
                }
                if (aleadyAuthorized)
                {
                    url = $"{this.Request.Scheme}://{this.Request.Host}";

                }
            }
            catch (Exception ex)
            {
               
                return Redirect("/Admin/PageNotFound");
            }

            return Page();

           
        }



        public async Task<JsonResult> OnPostAsync()
        {

            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var recordsTotal = _context.Trips.Where(e => e.IsDeleted == false && e.TripTargetId == 1).Count();

            var customersQuery = _context.Trips.Include(e => e.Country).Include(e => e.TripType).Where(e => e.IsDeleted == false && e.TripTargetId == 1).Select(i => new
            {
                CountryId = i.CountryId,
                TripId = i.TripId,
                TripTitleAr = i.TripTitleAr,
                TripTitleEn = i.TripTitleEn,
                NewPricePerPerson = i.NewPricePerPerson,
                OldPricePerPerson = i.OldPricePerPerson,
                DescriptionAr = i.DescriptionAr,
                DescriptionEn = i.DescriptionEn,
                IsActive = i.IsActive,
                Pic = i.TripImage,
                DurationInDays = i.DurationInDays,
                TripTypeId = i.TripTypeId,
                TripTypeTitleAr = i.TripType.TripTypeTitleAr,
                TripTypeTitleEn = i.TripType.TripTypeTitleEn,
                CountryTLAR = i.Country.CountryTLAR,
                CountryTLEN = i.Country.CountryTLEN,
                BrowserCulture = BrowserCulture,

            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.TripTitleEn.ToUpper().Contains(searchText) ||
                    s.CountryTLEN.ToUpper().Contains(searchText)
                );
            }

            var recordsFiltered = customersQuery.Count();

            var sortColumnName = DataTablesRequest.Columns.ElementAt(DataTablesRequest.Order.ElementAt(0).Column).Name;
            var sortDirection = DataTablesRequest.Order.ElementAt(0).Dir.ToLower();

            // using System.Linq.Dynamic.Core
            customersQuery = customersQuery.OrderBy($"{sortColumnName} {sortDirection}");

            var skip = DataTablesRequest.Start;
            var take = DataTablesRequest.Length;
            var data = await customersQuery
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new JsonResult(new
            {
                draw = DataTablesRequest.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data
            });
        }
       

    }
}
