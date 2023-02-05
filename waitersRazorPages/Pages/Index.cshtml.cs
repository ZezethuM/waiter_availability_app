using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using waiterApp;

namespace waitersRazorPages.Pages;

public class IndexModel : PageModel
{
    private IWaiterShift _waiterShits;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IWaiterShift waiterShift)
    {
        _logger = logger;
        _waiterShits = waiterShift;
    }

    [BindProperty]
    public List<String> CheckedDays {get; set;}

    public Dictionary<string, List<string>> DaysOfWeek { get { return _waiterShits.DisplayDays();}}



    [BindProperty (SupportsGet =true)]
    public Shifts shifts {get; set;}

    [BindProperty]
    public string? Handler{get; set;}

    public void OnGet()
    {
        _waiterShits.ShifDayOfWaiter(shifts.FirstName!);

        _waiterShits.DisplayDays();
       
    }
    public void OnPostShift()
    {
        if(Handler == "Shift")
        {
            if(shifts.FirstName != null && CheckedDays.Count != 0)
            {
                 if(ModelState.IsValid)
                 {
                    _waiterShits.SelectDay(shifts.FirstName!, CheckedDays);
                    ModelState.Clear();        
                 }
            }
        
        }
    }

    public void OnPostReset()
    {
        _waiterShits.UpdatingShifts(shifts.FirstName!, CheckedDays);
    }
}
