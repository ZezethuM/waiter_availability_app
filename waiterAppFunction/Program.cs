using Dapper;
using Npgsql;
using waiterApp;


Shift waiterShift = new Shift("Server=tiny.db.elephantsql.com;Port=5432;Database=dbwkrzkx;UserId=dbwkrzkx;Password=R8YQefPsLqg0vAXJ7XKRGgg9HKqXjgbm");

// List<string> n = new List<string>(){
//     "Monday",
//     "Sunday",
//     "Wednesday",
// };
// waiterShift.SelectDay("Amos", n);
// List<string> m = new List<string>(){
//     "Monday",
//     "Wednesday",
//     "Friday"
// };
// waiterShift.SelectDay("Karabo", m);
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
