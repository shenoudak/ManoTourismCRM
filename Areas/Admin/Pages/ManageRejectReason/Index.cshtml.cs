using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using NToastNotify;
using Microsoft.AspNetCore.Authorization;

namespace ManoTourism.Areas.Admin.Pages.ManageRejectReason
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        [BindProperty]
        public RejectReason rejectReason { get; set; }
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly IToastNotification _toastNotification;
        public string url { get; set; }
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(ManoContext context, IToastNotification toastNotification)
        {
            _context = context;
            rejectReason = new RejectReason();
            _toastNotification = toastNotification;
        }
        public void OnGet()
        {

            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }



        public async Task<JsonResult> OnPostAsync()
        {


            var recordsTotal = _context.RejectReasons.Count();

            var customersQuery = _context.RejectReasons.Select(i => new
            {
                RejectReasonId = i.RejectReasonId,
                RejectReasonTitleAr = i.RejectReasonTitleAr,
                RejectReasonTitleEn = i.RejectReasonTitleEn,
                IsActive = i.IsActive,


            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.RejectReasonTitleAr.ToUpper().Contains(searchText) ||
                    s.RejectReasonTitleEn.ToUpper().Contains(searchText)
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
        public async Task<IActionResult> OnPostAddRejectReason()
        {

            try
            {
                _context.RejectReasons.Add(rejectReason);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Reject Reason Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/ManageRejectReason");
        }
        public async Task<IActionResult> OnPostEditRejectReason(int RejectReasonId)
        {

            try
            {
                var model = _context.RejectReasons.Where(c => c.RejectReasonId == RejectReasonId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/Admin/ManageRejectReason");
                }


               
                model.RejectReasonTitleAr = rejectReason.RejectReasonTitleAr;
                model.RejectReasonTitleEn = rejectReason.RejectReasonTitleEn;
                model.IsActive = rejectReason.IsActive;
                var UpdatedRejectReason = _context.RejectReasons.Attach(model);

                UpdatedRejectReason.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Reject Reason Edited successfully");

               

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/ManageRejectReason");
        }


        public IActionResult OnGetSingleRejectReasonForEdit(int RejectReasonId)
        {
           
            var Result = _context.RejectReasons.Where(c => c.RejectReasonId == RejectReasonId).Select(i => new
            {
                RejectReasonId = i.RejectReasonId,
                RejectReasonTitleAr = i.RejectReasonTitleAr,
                RejectReasonTitleEn = i.RejectReasonTitleEn,
                IsActive = i.IsActive,
            }).FirstOrDefault();
            return new JsonResult(Result);
        }

    }
}
