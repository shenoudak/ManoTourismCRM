using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace ManoTourism.Areas.Admin.Pages.ManageTestimonial
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        [BindProperty]
        public Testimonial testimonial { get; set; }
        public string url { get; set; }
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context)
        {
            _context = context;
            testimonial = new Testimonial();


        }
        public void OnGet()
        {
           
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

       

        public async Task<JsonResult> OnPostAsync()
        {
           

            var recordsTotal = _context.Testimonials.Count();

            var customersQuery = _context.Testimonials.Select(i => new
            {
                TestimonialId = i.TestimonialId,
                TitleAr = i.TitleAr,
                TitleEn = i.TitleEn,
                NameAr = i.NameAr,
                NameEn = i.NameEn,
                DescriptionAr = i.DescriptionAr,
                DescriptionEn = i.DescriptionEn,
                IsActive = i.IsActive,
                Rating = i.Rating,
                Pic = i.ReviewImage,
                
              
            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.NameAr.ToUpper().Contains(searchText) ||
                    s.NameEn.ToUpper().Contains(searchText)
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
        public IActionResult OnGetSingleTestimonialForView(int TestimonialId)
        {
            var Result = _context.Testimonials.Where(e=>e.TestimonialId== TestimonialId).Select(i => new
            {
                TestimonialId = i.TestimonialId,
                TitleAr = i.TitleAr,
                TitleEn = i.TitleEn,
                NameAr = i.NameAr,
                NameEn = i.NameEn,
                DescriptionAr = i.DescriptionAr,
                DescriptionEn = i.DescriptionEn,
                IsActive = i.IsActive,
                Rating = i.Rating,
                Pic = i.ReviewImage,

            }).FirstOrDefault();
            return new JsonResult(Result);
        }

    }
}
