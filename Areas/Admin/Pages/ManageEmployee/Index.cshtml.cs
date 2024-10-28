using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages.ManageEmployee
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
       
        private ManoContext _context;
        [BindProperty]
        public Employee employee { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context)
        {
            _context = context;
            employee = new Employee();
        }
        public void OnGet()
        {

            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }



        public async Task<JsonResult> OnPostAsync()
        {


            var recordsTotal = _context.Employees.Where(e => e.IsDeleted == false).Count();

            var customersQuery = _context.Employees.Where(e => e.IsDeleted == false).Select(i => new
            {
                EmployeeId = i.EmployeeId,
                EmployeeEmail = i.EmployeeEmail,
                EmployeeName = i.EmployeeName,
                EmployeePassword = i.EmployeePassword,
                EmployeePic = i.EmployeePic,
                EmployeePhoneNumber = i.EmployeePhoneNumber,
                IsActive = i.IsActive,
              

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
        public IActionResult OnGetSingleEmployeeForView(int EmployeeId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var Result = _context.Employees.Where(c => c.EmployeeId == EmployeeId).Select(i => new
            {
                EmployeeName = i.EmployeeName,
                EmployeeEmail = i.EmployeeEmail,
                EmployeePic = i.EmployeePic,
                EmployeePassword = i.EmployeePassword,
                EmployeePhoneNumber = i.EmployeePhoneNumber,
                IsActive = i.IsActive,
            }).FirstOrDefault();
            return new JsonResult(Result);
        }

    }
}
