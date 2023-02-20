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
    public List<String> CheckedDays {get; set;}

    public List<string> userdays = new List<string>();
    DateTime n = DateTime.Now;
    public Dictionary<DayOfWeek, DateOnly> DayDates = new Dictionary<DayOfWeek, DateOnly>();


    public Dictionary<string, List<string>> DaysOfWeek { get { return _waiterShits.DisplayDays();}}

    [BindProperty (SupportsGet =true)]
    public Shifts shifts {get; set;}

    [BindProperty]
    public string? Handler{get; set;}

    [BindProperty]
    public string? Username {get; set;}
   
    public void OnGet()
    {    
        Username = HttpContext.Session.GetString("username")!;
        if(Username == null)
        {
            RedirectToPage("Index");
        }
        DayDates = _waiterShits.DaysOfTheWeek(n);
        userdays = _waiterShits.ShifDayOfWaiter(Username);
        _waiterShits.DisplayDays();
    }
    public void OnGetManagerView()
    {
        Username = HttpContext.Session.GetString("name")!;
        userdays = _waiterShits.ShifDayOfWaiter(Username);
        _waiterShits.DisplayDays();
    }
    public IActionResult OnPostReset()
    {
        if(Username != null)
        {
        _waiterShits.UpdatingShifts(Username, CheckedDays);
        userdays = _waiterShits.ShifDayOfWaiter(Username);
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

    // public void OnPostShift()
    // {

    //     Username = HttpContext.Session.GetString("username")!;
    //     Console.WriteLine(Username);
    //     if(Handler == "Shift")
    //     {
    //         if(Username != null && CheckedDays.Count != 0)
    //         {
    //              if(ModelState.IsValid)
    //              {
    //                 _waiterShits.SelectDay(Username, CheckedDays);
    //                 ModelState.Clear();        
    //              }
    //         }
    //     } 
    // }