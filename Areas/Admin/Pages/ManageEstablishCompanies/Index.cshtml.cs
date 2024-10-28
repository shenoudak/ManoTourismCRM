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

namespace ManoTourism.Areas.Admin.Pages.ManageEstablishCompanies
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        [BindProperty]
        public EstablishingCompany establishingCompany { get; set; }
        public string url { get; set; }
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification)
        {
            _context = context;
            establishingCompany = new EstablishingCompany();
            _toastNotification = toastNotification;
            _userManager = userManager;


        }
        public async Task<IActionResult> OnGet()
        {
            bool aleadyAuthorized = false;
            try
            {
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
                            bool isAuthoried = _context.AssignEmployeeRoles.Any(e => e.EmployeeId == employee.EmployeeId && e.EmployeeRoleId == (int)EmpRoles.EstablishingCompanies);
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
            var recordsTotal = _context.EstablishingCompanies.Where(e => e.IsDeleted == false).Count();

            var customersQuery = _context.EstablishingCompanies.Where(e => e.IsDeleted == false).Select(i => new
            {
                EstablishingCompanyId = i.EstablishingCompanyId,
                TitleAr = i.TitleAr,
                TitleEn = i.TitleEn,
                IsActive = i.IsActive,
                IsDeleted = i.IsDeleted,
                Pic = i.Pic,
                DescriptionAr = i.DescriptionAr,
                DescriptionEn = i.DescriptionEn,
                BrowserCulture = BrowserCulture,
            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.TitleAr.ToUpper().Contains(searchText) ||
                    s.TitleEn.ToUpper().Contains(searchText)
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
        public IActionResult OnGetSingleEstCompanyForView(int EstablishingCompanyId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var Result = _context.EstablishingCompanies.Where(c => c.EstablishingCompanyId == EstablishingCompanyId).Select(i => new
            {
                EstablishingCompanyId = i.EstablishingCompanyId,
                TitleAr = i.TitleAr,
                TitleEn = i.TitleEn,
                IsActive = i.IsActive,
                IsDeleted = i.IsDeleted,
                Pic = i.Pic,
                DescriptionAr = i.DescriptionAr,
                DescriptionEn = i.DescriptionEn,
                AddedDate = i.AddedDate.ToString("dddd, dd MMMM yyyy"),
                
                BrowserCulture = BrowserCulture,
               

            }).FirstOrDefault();
            return new JsonResult(Result);
        }

    }
}
