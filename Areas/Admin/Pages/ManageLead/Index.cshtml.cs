using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages.ManageLead
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        
        private ManoContext _context;
        [BindProperty]
        public Employee employee { get; set; }
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


            var recordsTotal = _context.Affiliates.Where(e => e.IsDeleted == false).Count();

            var customersQuery = _context.Affiliates.Where(e => e.IsDeleted == false).Select(i => new
            {
                AffiliateId = i.AffiliateId,
                AffiliateEmail = i.AffiliateEmail,
                AffiliateName = i.AffiliateName,
                AffiliatePassword = i.AffiliatePassword,
                AffiliatePic = i.AffiliatePic,
               
                IsActive = i.IsActive,
              

            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.AffiliateName.ToUpper().Contains(searchText) ||
                    s.AffiliateName.ToUpper().Contains(searchText)
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
