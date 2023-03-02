using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using waiterApp;

namespace waitersRazorPages.Pages;
public class IndexModel : PageModel
{
    private IWaiterShift _waiterShift;
     private readonly ILogger<IndexModel> _logger;
    public IndexModel(ILogger<IndexModel> logger, IWaiterShift waiterShift)
    {
        _logger = logger;
        _waiterShift = waiterShift;
    }
    public string Firstname;

    [BindProperty]
    public string UserName {get; set;}

    public string Msg;

    public void OnGet()
    {

    }
    public IActionResult OnGetLogout()
    {
        HttpContext.Session.Remove("username");
        HttpContext.Session.Remove("name");
        return Page();
    }

    public IActionResult OnPost()
    {
        Firstname = _waiterShift.CheckEmployees(UserName);
        if(UserName.Equals(Firstname))
        {
            HttpContext.Session.SetString("username", UserName);
            return RedirectToPage("ScheduleShift");
        }
        else if(UserName.Equals("Admin"))
        {
            HttpContext.Session.SetString("username", "Admin");
            return RedirectToPage("ManagerView");
        }
        else
        {
            TempData["UserMessage"] = _waiterShift.CheckEmployees(Firstname);
            Msg = _waiterShift.CheckEmployees(Firstname);
            UserName = "";
            ModelState.Clear();
            return Page();
        }
    }
}