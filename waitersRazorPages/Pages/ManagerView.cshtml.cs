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
        public Dictionary<DateOnly, List<string>> DaysOfWeek {get; set;}
        public Dictionary<DateOnly, DayOfWeek> DayDates = new Dictionary<DateOnly, DayOfWeek>();

        public IActionResult OnGetUpdate()
        {
            HttpContext.Session.GetString("name");
            HttpContext.Session.Remove("username");
            return Page();
        }
        public void OnGet()
        {
            UserName = HttpContext.Session.GetString("username");
            DayDates = _shiftDays.DaysOfTheWeek(datenow, shiftClass.Week);
            DaysOfWeek = _shiftDays.DisplayDays();
        }
        public void OnPostDelete()
        {
            _shiftDays.ManagerResetData();
            TempData["AlertMessage"] = "Data has been cleared";
        }
        DateTime datenow = DateTime.Now;
        public int week = 7;
        public IActionResult OnPostNext()
        {
            HttpContext.Session.GetString("name");
            DayDates = _shiftDays.DaysOfTheWeek(datenow, shiftClass.Week);
            DaysOfWeek = _shiftDays.DisplayDays();
            return Redirect($"ManagerView?Week={week++}");
        }
        public IActionResult OnPostPrevious()
        {
            HttpContext.Session.GetString("name");
            DayDates = _shiftDays.DaysOfTheWeek(datenow, shiftClass.Week);
            DaysOfWeek = _shiftDays.DisplayDays();
            return Redirect($"ManagerView?Week={0}");
        }
        public IActionResult  OnPostUpdate()
        {
            HttpContext.Session.SetString("name", UserName);
            // DayDates = _shiftDays.DaysOfTheWeek(datenow, shiftClass.Week);
            // DaysOfWeek = _shiftDays.DisplayDays();
            return Redirect("ScheduleShift");
        }
        public IActionResult OnPostLogIn()
        {
            TempData["LoginMessage"] = "Please Login first";
            return RedirectToPage("Index");
        }
        
    }
}
