using ManoTourism.Data;
using ManoTourism.DataTables;
using ManoTourism.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ManoTourism.Areas.Admin.Pages.ManageHotel
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;

        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context)
        {
            _context = context;



        }
        public void OnGet()
        {

            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }



        public async Task<JsonResult> OnPostAsync()
        {

            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var recordsTotal = _context.Hotels.Where(e => e.IsDeleted == false).Count();

            var customersQuery = _context.Hotels.Where(e => e.IsDeleted == false).Select(i => new
            {
                HotelId = i.HotelId,
                HotelTitleAr = i.HotelTitleAr,
                HotelTitleEn = i.HotelTitleEn,
                IsActive = i.IsActive,
                Location = i.Location,
                Review = i.Review,
                HotelMainImage = i.HotelMainImage,
                ReviewCount = i.ReviewCount,
                BrowserCulture = BrowserCulture,

            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.HotelTitleEn.ToUpper().Contains(searchText) ||
                    s.HotelTitleAr.ToUpper().Contains(searchText)
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