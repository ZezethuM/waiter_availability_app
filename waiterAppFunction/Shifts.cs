using Dapper;
using Npgsql;

namespace waiterApp
{
public class Shift
{
    string connectionString = "Server=tiny.db.elephantsql.com;Port=5432;Database=dbwkrzkx;UserId=dbwkrzkx;Password=R8YQefPsLqg0vAXJ7XKRGgg9HKqXjgbm";

    public void SelectDay(string firstname, List<string> days)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        int waiter_id = 0;

        List<int> ids = new List<int>();
        var parameter = new {FirstName = firstname};
        var sql = "SELECT count(*) FROM waiters where firstname = @FirstName;";

        var result = connection.QueryFirst(sql, parameter);

        if(result.count == 1)
        {
           var listOfWaiters =  connection.Query<Weekdays>(@"Select * from waiters where firstname = @FirstName;", parameter);
            foreach (var item in listOfWaiters)
            {
                waiter_id = item.Id;
            }
        }

        foreach (var day in days)
        {
        var parameter2 = new {DayInWeek = day};
        var sql2 = "SELECT count(*) FROM weekdays where Day = @DayInWeek;";

         var result2 = connection.QueryFirst(sql2, parameter2);

        if(result2.count == 1)
        {
        var listOfDays = connection.Query<Weekdays>(@"Select * from weekdays where Day = @DayInWeek;", parameter2);
            foreach (var item in listOfDays)
            {
               ids.Add(item.Id);
            }
        }
        }
        foreach (var dayid in ids)
        {
            // connection.Execute(@"Insert INTO schedule(Waiters_Id, Day_Id) Values(waiter_id, dayid);");
            connection.Execute(@"
            insert into 
                schedule (Waiters_Id, Day_Id)
            values 
                (@Waiters_Id, @Day_Id);",
                new Weekdays()
                {
                    Waiters_Id = waiter_id,
                    Day_Id = dayid
                }
            );  
        }
    }

    // public Dictionary<string, List<string>> Ches()
    // {
    //     foreach (var item in shiftDay)
    //     {
    //         Console.WriteLine(item);
    //     }
    //     foreach (var item in l)
    //     {
    //         Console.Write(item);
    //     }
    //     return shiftDay;
    // }

   public void UpdateDay()
   {

   }

   public void StoreDays()
   {

   }
    public void DisplayDays()
    {

    }

}
}

