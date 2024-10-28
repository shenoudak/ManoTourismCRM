using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace ManoTourism.Areas.Admin.Pages.ManageHotelReview
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
       
        public string url { get; set; }
        public static int staticHotelId = 0;
        [BindProperty]
        public int NotStaticHotelId { get; set; }
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context)
        {
            _context = context;
           


        }
        public IActionResult OnGet(int HotelId)
        {
            var Hotel = _context.Hotels.FirstOrDefault(a => a.HotelId == HotelId);
            if (Hotel == null)
            {
                return Redirect("/Admin/PageNotFound");
            }
            staticHotelId = HotelId;
            NotStaticHotelId = HotelId;
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            return Page();
        }

       

        public async Task<JsonResult> OnPostAsync()
        {
           

            var recordsTotal = _context.HotelReviews.Where(e=>e.HotelId== staticHotelId).Count();

            var customersQuery = _context.HotelReviews.Where(e => e.HotelId == staticHotelId).Select(i => new
            {
                HotelReviewId = i.HotelReviewId,
                HotelId = i.HotelId,
                ReviewAr = i.ReviewAr,
                ReviewEn = i.ReviewEn,
                ReviewWriterNameEn = i.ReviewWriterNameEn,
                ReviewWriterNameAr = i.ReviewWriterNameAr,
                Rate = i.Rate,
                ReviewWriterImage = i.ReviewWriterImage,
                IsActive = i.IsActive,
               
                
              
            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.ReviewWriterNameAr.ToUpper().Contains(searchText) ||
                    s.ReviewWriterNameEn.ToUpper().Contains(searchText)
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
