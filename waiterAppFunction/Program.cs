using Dapper;
using Npgsql;
using waiterApp;


Shift waiterShift = new Shift("Server=tiny.db.elephantsql.com;Port=5432;Database=dbwkrzkx;UserId=dbwkrzkx;Password=R8YQefPsLqg0vAXJ7XKRGgg9HKqXjgbm");

//  waiterShift.DisplayDays();
// foreach (var item in waiterShift.DisplayDays())
// {
// Console.WriteLine(item.Key);
// waiterShift.ShifDayOfWaiter("Karabo");
// foreach (var item2 in waiterShift.GetListOfDays())
// {
//     // Console.WriteLine(waiterShift.ShifDayOfWaiter("phumza"));
//     Console.WriteLine(item2);
// }
    
// }
// DateTime cur = DateTime.Now;

// foreach (var item in waiterShift.DisplayDays())
// {
//     Console.WriteLine(item.Key);    
// }




// List<string> n = new List<string>(){
//     "Monday",
//     "Sunday",
//     "Wednesday",
// };
// waiterShift.SelectDay("Amos", n);

// List<DateTime> daysOfW = new List<DateTime>(){
// DateTime.Parse("2023/02/20"),
// DateTime.Parse("2023/02/21")
// };

// waiterShift.SelectDay("Karabo", daysOfW);




// List<string> m = new List<string>(){
//     "Monday",
//     "Wednesday",
//     "Friday"
// };
// List<string> o = new List<string>(){
//     "Sunday",
//     "Wednesday"
// };
// waiterShift.SelectDay("Phumza", o);

// waiterShift.DisplayDays();

// foreach (var item in waiterShift.DisplayDays())
// {
//     Console.WriteLine(item.Key + " " + item.Value.Count());

//     foreach (var name in item.Value)
//     {
//         Console.WriteLine(name);
//     }
// }
