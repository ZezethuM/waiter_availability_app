using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using waiterApp;

namespace waitersRazorPages.Pages
{
    public class ScheduleShiftModel : PageModel
    {
    private IWaiterShift _waiterShits;
    private readonly ILogger<ScheduleShiftModel> _logger;

    public ScheduleShiftModel(ILogger<ScheduleShiftModel> logger, IWaiterShift waiterShift)
    {
        _logger = logger;
        _waiterShits = waiterShift;
    }

    [BindProperty]
    public List<DateTime> CheckedDays {get; set;}

    public List<DateOnly> userdays = new List<DateOnly>();

    DateTime n = DateTime.Now;
    public Dictionary<DateOnly, DayOfWeek> DayDates = new Dictionary<DateOnly, DayOfWeek>();

     public Dictionary<DateOnly, List<string>> DaysOfWeek { get; set; }

    [BindProperty (SupportsGet =true)]
    public Shifts shifts {get; set;}

    [BindProperty]
    public string? Handler{get; set;}

    [BindProperty]
    public string? Username {get; set;}
    

    public void OnGet()
    {
        Username = HttpContext.Session.GetString("username")!;    
        if(Username != "Admin")
        {
            userdays = _waiterShits.ShifDayOfWaiter(Username);
            DayDates = _waiterShits.DaysOfTheWeek(n, shifts.Week);
            DaysOfWeek= _waiterShits.DisplayDays();
        }
        else if(Username == "Admin")
        {
            Username = HttpContext.Session.GetString("name")!;
            userdays = _waiterShits.ShifDayOfWaiter(Username);
            DayDates = _waiterShits.DaysOfTheWeek(n, shifts.Week);
            DaysOfWeek= _waiterShits.DisplayDays();
        }
    }
    
    public IActionResult OnPostNext()
    {
        DayDates = _waiterShits.DaysOfTheWeek(n, shifts.Week);
        DaysOfWeek = _waiterShits.DisplayDays();
        return Redirect($"ScheduleShift?Week={7}");
    }
    public IActionResult OnPostPrevious()
    {
        DayDates = _waiterShits.DaysOfTheWeek(n, shifts.Week);
        DaysOfWeek = _waiterShits.DisplayDays();
        return Redirect($"ScheduleShift?Week={0}");
    }
    public IActionResult OnPostReset()
    {
        Username = HttpContext.Session.GetString("username")!;    
        if(Username != "Admin")
        {
            if(Username != null && shifts.Week == 0)
            {
                _waiterShits.UpdatingShifts(Username, CheckedDays, 0);
                userdays = _waiterShits.ShifDayOfWaiter(Username);
                DayDates = _waiterShits.DaysOfTheWeek(n, 0);
                DaysOfWeek= _waiterShits.DisplayDays();
                TempData["Message"] = "Days have been successfully updated/Added";
                return Page();
            }
            else if(Username != null && shifts.Week == 7)
            {
                _waiterShits.UpdatingShifts(Username, CheckedDays, 7);
                userdays = _waiterShits.ShifDayOfWaiter(Username);
                DayDates = _waiterShits.DaysOfTheWeek(n, 7);
                DaysOfWeek= _waiterShits.DisplayDays();
                TempData["Message"] = "Days have been successfully updated/Added";
                return Page();
            }
        }
        else if(Username == "Admin")
        {
            Username = HttpContext.Session.GetString("name")!;
            if(Username != null && shifts.Week == 0)
            {
                _waiterShits.UpdatingShifts(Username, CheckedDays, 0);
                userdays = _waiterShits.ShifDayOfWaiter(Username);
                DayDates = _waiterShits.DaysOfTheWeek(n, 0);
                DaysOfWeek= _waiterShits.DisplayDays();
                TempData["Message"] = "Days have been successfully updated/Added";
                return Page();
            }
            else if(Username != null && shifts.Week == 7)
            {
                _waiterShits.UpdatingShifts(Username, CheckedDays, 7);
                userdays = _waiterShits.ShifDayOfWaiter(Username);
                DayDates = _waiterShits.DaysOfTheWeek(n, 7);
                DaysOfWeek= _waiterShits.DisplayDays();
                TempData["Message"] = "Days have been successfully updated/Added";
                return Page();
            }
        }
        TempData["LoginMessage"] = "Please Login first";
        return RedirectToPage("Index");
        
    }
    public IActionResult OnPostLogIn()
    {
        TempData["LoginMessage"] = "Please Login first";
        return RedirectToPage("Index");
    }
    }
}
