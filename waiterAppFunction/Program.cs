using Dapper;
using Npgsql;
using waiterApp;


Shift waiterShift = new Shift();
List<string> n = new List<string>(){
    "Monday",
    "Sunday",
    "Wednesday",
    "Friday",
    "Saturday"
};
waiterShift.SelectDay("Amos", n);


