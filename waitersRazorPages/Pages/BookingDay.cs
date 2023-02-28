namespace waiterApp;

public class BookingDay
{
    public DayOfWeek Day
    {
        get; set;
    }

    public Boolean Booked
    {
        get; set;
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