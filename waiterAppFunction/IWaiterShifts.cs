namespace waiterApp;

public interface IWaiterShift
{
    // public void SelectDay1(string firstname, List<string> days);
    public void SelectDay(string firstname, List<DateTime> days);
    public Dictionary<DateOnly, DayOfWeek> DaysOfTheWeek(DateTime currDateAndTime, int week);
    public Dictionary<DateOnly, List<string>>DisplayDays();
    public List<DateOnly> GetListOfDays();
    public Dictionary<DateOnly, List<string>> GetDictionary();
    public List<DateOnly> ShifDayOfWaiter(string firstname);
    public void UpdatingShifts(string name, List<DateTime> newDays, int week);
    public string CheckEmployees(string firstname);
    public void ManagerResetData();
}