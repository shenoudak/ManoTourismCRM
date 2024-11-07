using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages.ManageSales
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        [BindProperty]
        public Sales salesObj { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context)
        {
            _context = context;
            salesObj = new Sales();
        }
        public void OnGet()
        {

            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }



        public async Task<JsonResult> OnPostAsync()
        {


            var recordsTotal = _context.Sales.Where(e => e.IsDeleted == false).Count();

            var customersQuery = _context.Sales.Include(e=>e.Employee).Where(e => e.IsDeleted == false).Select(i => new
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
            var Result = _context.Sales.Include(e=>e.Employee).Where(c => c.SalesId == SalesId).Select(i => new
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
