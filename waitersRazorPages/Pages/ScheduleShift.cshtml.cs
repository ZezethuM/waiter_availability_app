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

    public List<string> userdays = new List<string>();

    DateTime n = DateTime.Now;
    public Dictionary<DateOnly, DayOfWeek> DayDates = new Dictionary<DateOnly, DayOfWeek>();

    // public Dictionary<DateOnly, List<string>> DaysOfWeek { get {return _waiterShits.DisplayDays();} }
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
    public int week = 7;
    public IActionResult OnPostNext()
    {
        DayDates = _waiterShits.DaysOfTheWeek(n, shifts.Week);
        DaysOfWeek = _waiterShits.DisplayDays();
        return Redirect($"ScheduleShift?Week={week++}");
    }
    public IActionResult OnPostPrevious()
    {
        DayDates = _waiterShits.DaysOfTheWeek(n, shifts.Week);
        DaysOfWeek = _waiterShits.DisplayDays();
        return Redirect($"ScheduleShift?Week={0}");
    }
    public IActionResult OnPostReset()
    {
        if(Username != null)
        {
        _waiterShits.UpdatingShifts(Username, CheckedDays);
        userdays = _waiterShits.ShifDayOfWaiter(Username);
        DayDates = _waiterShits.DaysOfTheWeek(n, shifts.Week);
        DaysOfWeek= _waiterShits.DisplayDays();
        TempData["Message"] = "Days have been successfully updated/Added";
        return Page();
        }
        else
        {
            TempData["LoginMessage"] = "Please Login first";
           return RedirectToPage("Index");
        }
    }
    public IActionResult OnPostLogIn()
    {
        TempData["LoginMessage"] = "Please Login first";
        return RedirectToPage("Index");
    }
    }
}
