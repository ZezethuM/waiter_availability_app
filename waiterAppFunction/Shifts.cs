using Dapper;
using Npgsql;

namespace waiterApp
{
public class Shift : IWaiterShift
{
    private NpgsqlConnection connection;
    public Shift(string connectionString)
    {
        connection = new NpgsqlConnection(connectionString);
        connection.Open();
    }
    public Dictionary<string, List<string>> dayWithWiaters = new Dictionary<string, List<string>>();
    public List<int> ids = new List<int>();
    public void SelectDay(string firstname, List<string> days)
    {
        // int waiter_id = 0;
        int weekdaysid = 0;
        var parameter = new {FirstName = firstname};
        var sql = "SELECT count(*) FROM waiters where firstname = @FirstName;";

        var result = connection.QuerySingle<int>(sql, parameter);

        if(result == 0)
        {
            throw new Exception($"Invalid username {firstname}");
        //     foreach (var item in listOfWaiters)
        //     {
        //     }
        }
        var listOfWaiters =  connection.QueryFirst<Waiter>(@"Select * from waiters where firstname = @FirstName;", parameter);
         var waiter_id = listOfWaiters.Id;

        foreach (var day in days)
        {
        var parameter2 = new {DayInWeek = day};
        var sql2 = "SELECT count(*) FROM weekdays where Day = @DayInWeek;";

         var result2 = connection.QueryFirst(sql2, parameter2);

        if(result2.count == 1)
        {
        var listOfDays = connection.Query<Weekday>(@"Select * from weekdays where Day = @DayInWeek;", parameter2);
            foreach (var item in listOfDays)
            {
               ids.Add(item.Id);
            }
        }
        foreach (var dayid in ids)
        {  
            weekdaysid = dayid;
        }
        var parameter3 = new {Waiter_Id = waiter_id, Weekdaysid = weekdaysid};
        connection.Execute(@"insert into schedule values (@Waiter_Id, @Weekdaysid)", parameter3); 
        }
    }
    public Dictionary<string, List<string>> DisplayDays()
    {
        var sql = @"select weekdays.day, waiters.firstname
        from weekdays
        inner join schedule on  schedule.day_id = weekdays.id
        inner join waiters on schedule.waiters_id = waiters.id";

        var waiterDays = connection.Query<Shift_Schedule>(sql);

        List<string> monday = new List<string>();
        List<string> tuesday = new List<string>();
        List<string> wednesday = new List<string>();
        List<string> thursday = new List<string>();
        List<string> friday = new List<string>();
        List<string> saturday = new List<string>();
        List<string> sunday = new List<string>();

        Dictionary<string, List<string>> daysAndWaitersShift = new Dictionary<string, List<string>>()
        {
            {"Monday", monday},
            {"Tuesday", tuesday},
            {"Wednesday", wednesday},
            {"Thursday", thursday},
            {"Friday", friday},
            {"Saturday", saturday},
            {"Sunday", sunday}
        };

        foreach (var item in waiterDays)
        {
            if(item.Day == "Monday")
            {
                monday.Add(item.FirstName!);
                daysAndWaitersShift[item.Day] = monday;
            }
            else if(item.Day == "Tuesday")
            {
                tuesday.Add(item.FirstName!);
                daysAndWaitersShift[item.Day] = tuesday;
            }
            else if(item.Day == "Wednesday")
            {
                wednesday.Add(item.FirstName!);
                daysAndWaitersShift[item.Day] = wednesday;
            }
            else if(item.Day == "Thursday")
            {
                thursday.Add(item.FirstName!);
                daysAndWaitersShift[item.Day] = thursday;
            }
            else if(item.Day == "Friday")
            {
                friday.Add(item.FirstName!);
                daysAndWaitersShift[item.Day] = friday;
            }
            else if(item.Day == "Saturday")
            {
                saturday.Add(item.FirstName!);
                daysAndWaitersShift[item.Day] = saturday;
            }
            else if(item.Day == "Sunday")
            {
                sunday.Add(item.FirstName!);
                daysAndWaitersShift[item.Day] = sunday;
            }
        }

        return daysAndWaitersShift;
    }
    public Dictionary<string, List<string>> GetDictionary()
    {
        return DisplayDays();
    }

    public List<string> shiftDays = new List<string>();
    public List<string> ShifDayOfWaiter(string firstname)
    {
        var sql3 = @"select firstname, day from waiters
        inner join schedule on waiters.id = schedule.waiters_id
        inner join weekdays on schedule.day_id = weekdays.id";

        var dayWithWaiters = connection.Query<Shift_Schedule>(sql3);

        foreach (var day in dayWithWaiters)
        {
            if(day.FirstName == firstname)
            {
                shiftDays.Add(day.Day!);
            }
        }
        return shiftDays;
    }
    public List<string> GetListOfDays()
    {
        return shiftDays;
    }

    public void UpdatingShifts(string name, List<string> newDays)
    {
        int waiter_id2 = 0;
        int weekdaysid1 = 0;

        List<int> DayIds = new List<int>(); 

        var parameter4 = new {NewName = name};
        var sql4 = "SELECT count(*) FROM waiters where firstname = @NewName;";

        var result4 = connection.QueryFirst(sql4, parameter4);

        if(result4.count == 1)
        {
        var listOfWaiters =  connection.Query<Waiter>(@"Select * from waiters where firstname = @NewName;", parameter4);
        foreach (var item in listOfWaiters)
        {
            waiter_id2 = item.Id;
        }
        }

        foreach (var newday in newDays)
        {
        var param = new {Week_day = newday};
        var sqlqry = @"SELECT count(*) FROM weekdays where Day = @Week_day;";

        var result2 = connection.QueryFirst(sqlqry, param);

        if(result2.count == 1)
        {
            var listOfDays1 = connection.Query<Weekday>(@"Select * from weekdays where Day = @Week_day;", param);
            foreach (var item in listOfDays1)
            {
            DayIds.Add(item.Id);
            }
        }
        }
        var parameter5 = new {Waiter_Id2 = waiter_id2};
        var sqlqry1 = @"select count(*) from schedule where waiters_id = @Waiter_Id2";
        var result5 = connection.QueryFirst(sqlqry1, parameter5);

        if(result5.count > 1)
        {
            connection.Execute(@"DELETE FROM Schedule where waiters_id = @Waiter_Id2", parameter5);
            foreach (var shifday1 in DayIds)
            {
                weekdaysid1 = shifday1;
                var parameter6 = new {Waiter_Id2 = waiter_id2, Weekdaysid3 = weekdaysid1};
                connection.Execute(@"insert into schedule values (@Waiter_Id2, @Weekdaysid3)", parameter6); 
            }
            
        }
        else{
            foreach (var shifday in DayIds)
            {
                weekdaysid1 = shifday;
                var param2 = new {Waiter_Id1 = waiter_id2, Weekdaysid1 = weekdaysid1};
                connection.Execute(@"insert into schedule values (@Waiter_Id1, @Weekdaysid1)", param2);
            }
        }
    }
    public void ManagerResetData()
    {
        connection.Execute(@"truncate table schedule");
    }
}
}

