
namespace waiterApp;

public interface IWaiterShift
{
    // public void SelectDay1(string firstname, List<string> days);
    public void SelectDay(string firstname, Dictionary<DateOnly, DayOfWeek> days);
    
    public Dictionary<string, List<string>>DisplayDays();
    public List<string> GetListOfDays();
    public Dictionary<string, List<string>> GetDictionary();
    public List<string> ShifDayOfWaiter(string firstname);
    public void UpdatingShifts(string name, List<string> newDays);
    public string CheckEmployees(string firstname);
    public void ManagerResetData();
}