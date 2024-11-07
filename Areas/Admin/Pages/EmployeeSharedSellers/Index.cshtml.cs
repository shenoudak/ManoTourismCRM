using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Areas.Admin.Pages.EmployeeSharedSellers
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        [BindProperty]
        public Sales salesObj { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public static int EmployeeId = 0;
        public string url { get; set; }
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public IndexModel(ManoContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _context = context;
            _userManager = userManager;
            _db = db;
            salesObj = new Sales();

        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Admin/PageNotFound");
            }
            if (!await _userManager.IsInRoleAsync(user, "employee"))
            {
                return Redirect("/Admin/PageNotFound");
            }
            var Employee = _context.Employees.Where(e => e.EmployeeEmail == user.Email).FirstOrDefault();
            if (Employee == null)
            {
                return Redirect("/Identity/account/login");
            }
            EmployeeId = Employee.EmployeeId;
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            return Page();
        }



        public async Task<JsonResult> OnPostAsync()
        {


            var recordsTotal = _context.Sales.Where(e => e.IsDeleted == false).Count();

            var customersQuery = _context.Sales.Include(e => e.Employee).Where(e => e.IsDeleted == false&&e.EmployeeId==EmployeeId).Select(i => new
            {
                SalesId = i.SalesId,
                SalesName = i.SalesName,
                SalesEmail = i.SalesEmail,
                SalesPhoneNumber = i.SalesPhoneNumber,
                PassportPic = i.PassportPic,
                IsActive = i.IsActive,
                EmployeeName = i.Employee.EmployeeName,
                SalesPassword = i.SalesPassword,


            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.EmployeeName.ToUpper().Contains(searchText) ||
                    s.EmployeeName.ToUpper().Contains(searchText)
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
        public IActionResult OnGetSingleSalesForView(int SalesId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var Result = _context.Sales.Include(e => e.Employee).Where(c => c.SalesId == SalesId).Select(i => new
            {
                SalesName = i.SalesName,
                SalesEmail = i.SalesEmail,
                EmployeeName = i.Employee.EmployeeName,
                PassportPic = i.PassportPic,
                SalesPassword = i.SalesPassword,
                SalesPhoneNumber = i.SalesPhoneNumber,
                IsActive = i.IsActive,
            }).FirstOrDefault();
            return new JsonResult(Result);
        }

    }
}
