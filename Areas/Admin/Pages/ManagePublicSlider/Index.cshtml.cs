using ManoTourism.Models;
using ManoTourism.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManoTourism.Areas.Admin.Pages.ManagePublicSlider
{
    public class IndexModel : PageModel
    {
        private ManoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;



        public string url { get; set; }


        [BindProperty]
        public PublicSlider publicSlider { get; set; }


        public List<PublicSlider> publicSliders = new List<PublicSlider>();

        public PublicSlider publicSliderObj { get; set; }

        public IndexModel(ManoContext context, IWebHostEnvironment hostEnvironment
                                           )
        {
            _context = context;
            _hostEnvironment = hostEnvironment;

            publicSlider = new PublicSlider();
            publicSliderObj = new PublicSlider();
        }
        public IActionResult OnGet()
        {
            try
            {
                publicSliders = _context.PublicSliders.ToList();
                url = $"{this.Request.Scheme}://{this.Request.Host}";
            }
            catch(Exception ex)
            {
               
                return Redirect("/Admin/PageNotFound");
            }
            return Page();
           
        }
        public IActionResult OnGetSinglePublicSliderForView(int PublicSliderId)
        {
            var Result = _context.PublicSliders.Where(c => c.PublicSliderId == PublicSliderId).FirstOrDefault();
            return new JsonResult(Result);
        }



    }
}
