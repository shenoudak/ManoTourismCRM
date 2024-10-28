using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;

namespace ManoTourism.Areas.Admin.Pages.ManageInsurance
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        [BindProperty]
        public Insurance insurance { get; set; }
        public string url { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context)
        {
            _context = context;
            insurance = new Insurance();


        }
        public void OnGet()
        {

            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }



        public async Task<JsonResult> OnPostAsync()
        {

            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var recordsTotal = _context.Insurances.Where(e => e.IsDeleted == false).Count();

            var customersQuery = _context.Insurances.Include(e => e.InsuranceType).Where(e => e.IsDeleted == false).Select(i => new
            {
                InsuranceId = i.InsuranceId,
                InsuranceTitleAr = i.InsuranceTitleAr,
                InsuranceTitleEn = i.InsuranceTitleEn,
                OldPrice = i.OldPrice,
                NewPrice = i.NewPrice,
                DescriptionAr = i.DescriptionAr,
                DescriptionEn = i.DescriptionEn,
                IsActive = i.IsActive,
                Pic = i.Pic,
                DurationInMonth = i.DurationInMonth,
                InsuranceTypeId = i.InsuranceTypeId,
                InsuranceTypeTitleAr = i.InsuranceType.InsuranceTypeTitleAr,
                InsuranceTypeTitleEn = i.InsuranceType.InsuranceTypeTitleEn,
                BrowserCulture = BrowserCulture,
            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.InsuranceTitleEn.ToUpper().Contains(searchText) ||
                    s.InsuranceTypeTitleAr.ToUpper().Contains(searchText)
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
        public IActionResult OnGetSingleInsuranceForView(int InsuranceId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var Result = _context.Insurances.Include(e => e.InsuranceType).Where(c => c.InsuranceId == InsuranceId).Select(i => new
            {
                InsuranceId = i.InsuranceId,
                InsuranceTitleAr = i.InsuranceTitleAr,
                InsuranceTitleEn = i.InsuranceTitleEn,
                OldPrice = i.OldPrice,
                NewPrice = i.NewPrice,
                DescriptionAr = i.DescriptionAr,
                DescriptionEn = i.DescriptionEn,
                IsActive = i.IsActive,
                Pic = i.Pic,
                DurationInMonth = i.DurationInMonth,
                InsuranceTypeId = i.InsuranceTypeId,
                InsuranceTypeTitleAr = i.InsuranceType.InsuranceTypeTitleAr,
                InsuranceTypeTitleEn = i.InsuranceType.InsuranceTypeTitleEn,
                BrowserCulture = BrowserCulture,
                AddedDate = i.AddedDate.ToString("dddd, dd MMMM yyyy"),

            }).FirstOrDefault();
            return new JsonResult(Result);
        }

    }
}
