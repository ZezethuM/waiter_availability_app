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
        public Dictionary<string, List<string>> DaysOfWeek { get { return _shiftDays.DisplayDays();}}

        public void OnPostDelete()
        {
            _shiftDays.ManagerResetData();
        }
        
    }
}