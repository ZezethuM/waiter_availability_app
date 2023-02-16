using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using waiterApp;

namespace waitersRazorPages.Pages
{
    public class UserGuideModel : PageModel
    {
        public string Username {get; set;}

         private IWaiterShift _waitersShift;

        private readonly ILogger<UserGuideModel> _logger;

        public UserGuideModel(ILogger<UserGuideModel> logger, IWaiterShift waitersShift)
        {
            _logger = logger;
            _waitersShift = waitersShift;
        }

        [BindProperty]
        public string? Handler{get; set;}
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("username");
        }

        public IActionResult OnPostLog()
        {
            TempData["LoginMessage"] = "Please Login first";
            return RedirectToPage("Index");
        }
    }
}
