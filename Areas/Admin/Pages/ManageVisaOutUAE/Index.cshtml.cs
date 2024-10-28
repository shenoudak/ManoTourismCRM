using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using NToastNotify;

namespace ManoTourism.Areas.Admin.Pages.ManageVisaOutUAE
{
    public class IndexModel : PageModel
    {
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private ManoContext _context;
        [BindProperty]
        public Visa visa { get; set; }
        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification)
        {
            _context = context;
            visa = new Visa();
            _toastNotification = toastNotification;
            _userManager = userManager;


        }
        public async Task<IActionResult> OnGet()
        {
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
                        return Page();
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
                            bool isAuthoried = _context.AssignEmployeeRoles.Any(e => e.EmployeeId == employee.EmployeeId && e.EmployeeRoleId == (int)EmpRoles.VisasManagement);
                            if (isAuthoried)
                            {
                                return Page();
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
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error");
                return Redirect("/Admin/PageNotFound");
            }
            return Page();
        }

       

        public async Task<JsonResult> OnPostAsync()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();

            var recordsTotal = _context.Visas.Include(e => e.Country).Include(e => e.VisaType).Where(e => e.IsDeleted == false && e.VisaTargetId == 2).Count();

            var customersQuery = _context.Visas.Include(e=>e.Country).Include(e=>e.VisaType).Where(e => e.IsDeleted == false && e.VisaTargetId == 2).Select(i => new
            {
                CountryId = i.CountryId,
                VisaId = i.VisaId,
                VisaTitleAr = i.VisaTitleAr,
                VisaTitleEn = i.VisaTitleEn,
                NewPricePerPerson = i.NewPricePerPerson,
                OldPricePerPerson = i.OldPricePerPerson,
                DescriptionAr = i.DescriptionAr,
                DescriptionEn = i.DescriptionEn,
                IsActive = i.IsActive,
                Pic = i.Pic,
                ValidityInDays = i.ValidityInDays,
                VisaTypeId = i.VisaTypeId,
                VisaTypeTitleAr = i.VisaType.VisaTitleAr,
                VisaTypeTitleEn = i.VisaType.VisaTitleEn,
                CountryTLAR = i.Country.CountryTLAR,
                CountryTLEN = i.Country.CountryTLEN,
                BrowserCulture = BrowserCulture,

            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.VisaTitleEn.ToUpper().Contains(searchText) ||
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
        public IActionResult OnGetSingleVisaForView(int VisaId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var Result = _context.Visas.Include(e=>e.Country).Include(e=>e.VisaType).Where(c => c.VisaId == VisaId).Select(i => new
            {
                VisaTitleAr=i.VisaTitleAr,
                VisaTitleEn=i.VisaTitleEn,
                Pic = i.Pic,
                CountryTLAR=i.Country.CountryTLAR,
                CountryTLEN=i.Country.CountryTLEN,
                VisaTypeTitleAr = i.VisaType.VisaTitleAr,
                VisaTypeTitleEn = i.VisaType.VisaTitleEn,
                OldPricePerPerson=i.OldPricePerPerson,
                NewPricePerPerson=i.NewPricePerPerson,
                DescriptionAr=i.DescriptionAr,
                DescriptionEn=i.DescriptionEn,
                ValidityInDays=i.ValidityInDays,
                IsActive = i.IsActive,
                AddedDate = i.AddedDate.ToString("dddd, dd MMMM yyyy"),
                BrowserCulture = BrowserCulture,
            }).FirstOrDefault();
            return new JsonResult(Result);
        }

    }
}
