using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using waiterApp;

namespace waitersRazorPages.Pages
{
    public class ManagerViewModel : PageModel
    {
        private IWaiterShift _shiftDays;
        private readonly ILogger<IndexModel> _logger;

        public ManagerViewModel(ILogger<IndexModel> logger, IWaiterShift shiftDays)
        {
            _logger = logger;
            _shiftDays = shiftDays;
        }
        [BindProperty]
            public string? Handler
            {
                get;
                set;
            }
        [BindProperty (SupportsGet =true)]
        public Shifts shiftClass
        {
            get;
            set;
        }
        public string Name;

        [BindProperty]
        public string UserName {get; set;}
        public Dictionary<string, List<string>> DaysOfWeek { get { return _shiftDays.DisplayDays();}}
        public IActionResult OnGetUpdate()
        {
            HttpContext.Session.GetString("name");
            HttpContext.Session.Remove("username");
            return Page();
        }
        public void OnGet()
        {
            UserName = HttpContext.Session.GetString("username");
        }
        public void OnPostDelete()
        {
            _shiftDays.ManagerResetData();
            TempData["AlertMessage"] = "Data has been cleared";
        }
        public void  OnPostUpdate()
        {
            HttpContext.Session.SetString("name", UserName);
            // return RedirectToPage("ScheduleShift");
        }
        public IActionResult OnPostLogIn()
        {
            TempData["LoginMessage"] = "Please Login first";
            return RedirectToPage("Index");
        }
        
    }
}
