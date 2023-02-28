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
        public void SelectDay(string firstname, List<DateTime> days)
        {
            var parameter = new { FirstName = firstname };
            var sql = "SELECT count(*) FROM waiters where firstname = @FirstName;";

            var result = connection.QuerySingle<int>(sql, parameter);

            if (result == 0)
            {
                throw new Exception($"Invalid username {firstname}");
            }
            var listOfWaiters = connection.QueryFirst<Waiter>(@"Select * from waiters where firstname = @FirstName;", parameter);
            var waiter_id = listOfWaiters.Id;


            foreach (var item in days)
            {
                var parameter3 = new
                {

                    Waiter_Id = waiter_id,
                    Weekdays = item.DayOfWeek.ToString(),
                    Weekdaydates = item
                };
                connection.Execute(@"insert into ShiftSchedule(weekday, weekdaydate, waiter_id) values (@Weekdays, @Weekdaydates, @Waiter_Id)", parameter3);
            }
        }
        public Dictionary<DateOnly, List<string>> DisplayDays()
        {
            
            var sql = @"Select firstname, weekdaydate from waiters
                    inner join shiftschedule
                    on waiters.id = shiftschedule.waiter_id;;";

            var waiterDays = connection.Query<Shift_Schedule>(sql);

            Dictionary<DateOnly, List<string>> daysAndWaitersShift = new Dictionary<DateOnly, List<string>>();

            var newList = waiterDays.ToList().GroupBy(x => x.WeekdayDate);

            foreach (var item in newList)
            {
                // foreach (var item2 in DaysOfTheWeek(currDate))
                // {
                //     if(!daysAndWaitersShift.ContainsKey(item2.Key))
                //     {
                        daysAndWaitersShift.Add(DateOnly.FromDateTime(item.Key), new List<string>());    
                    // }
                // }
                foreach (var item1 in item)
                {
                   
                    daysAndWaitersShift[DateOnly.FromDateTime(item1.WeekdayDate)].Add(item1.FirstName!);

                }
            }

            return daysAndWaitersShift;
        }
        // public Dictionary<DateOnly, List<string>> GetDictionary()
        // {
        //     return DisplayDays();
        // }

        public List<string> shiftDays = new List<string>();
        public List<string> ShifDayOfWaiter(string firstname)
        {
            shiftDays.Clear();
            var sql3 = @"select firstname, day from waiters
        inner join schedule on waiters.id = schedule.waiters_id
        inner join weekdays on schedule.day_id = weekdays.id";

            var dayWithWaiters = connection.Query<Shift_Schedule>(sql3);

            foreach (var day in dayWithWaiters)
            {
                if (day.FirstName == firstname)
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

        public void UpdatingShifts(string name, List<DateTime> newDays)
        {
            int waiter_id2 = 0;


            var parameter4 = new { NewName = name };
            var sql4 = "SELECT count(*) FROM waiters where firstname = @NewName;";

            var result4 = connection.QueryFirst(sql4, parameter4);

            if (result4.count == 1)
            {
                var listOfWaiters = connection.Query<Waiter>(@"Select * from waiters where firstname = @NewName;", parameter4);
                foreach (var item in listOfWaiters)
                {
                    waiter_id2 = item.Id;
                }
            }

            var param = new { WaiterId = waiter_id2 };
            var sqlqry = @"SELECT count(*) FROM Shiftschedule where waiter_id = @WaiterId;";

            var result2 = connection.QueryFirst(sqlqry, param);

            if (result2.count > 1)
            {
                connection.Execute(@"DELETE FROM Shiftschedule where waiter_id = @WaiterId", param);
                foreach (var newday in newDays)
                {
                    var parameter6 = new
                    {
                        weekday1 = newday.DayOfWeek.ToString(),
                        Weekday_date1 = newday,
                        Waiter_Id2 = waiter_id2
                    };
                    connection.Execute(@"insert into shiftschedule(weekday, weekdaydate, waiter_id) values (@weekday1, @Weekday_date1, @Waiter_Id2)", parameter6);
                }
            }
            else
            {
                foreach (var newday in newDays)
                {
                    var param2 = new
                    {
                        Weekday2 = newday.DayOfWeek.ToString(),
                        weekday_date2 = newday,
                        Waiter_Id1 = waiter_id2
                    };
                    connection.Execute(@"insert into shiftschedule(weekday, weekdaydate, waiter_id) values (@Weekday2, @weekday_date2, @Waiter_Id1)", param2);
                }
            }
        }
        public string CheckEmployees(string firstname)
        {
            var param3 = new { Name = firstname };
            var sqlquery = "SELECT count(*) FROM waiters where firstname = @Name";

            var results = connection.QueryFirst(sqlquery, param3);

            if (results.count == 1)
            {
                return firstname;
            }
            else
            {
                return "Invalid user please enter a correct firstname";
            }

        }
        public void ManagerResetData()
        {
            connection.Execute(@"truncate table shiftschedule");
        }

        DateTime currDate = DateTime.Now;
        public Dictionary<DateOnly, DayOfWeek> DaysOfTheWeek(DateTime currDateAndTime, int week)
        {
            Dictionary<DateOnly, DayOfWeek> dates = new Dictionary<DateOnly, DayOfWeek>();

            currDateAndTime = currDate;
            var mon = DayOfWeek.Monday - currDateAndTime.DayOfWeek + week;

            var day = currDate.AddDays(mon);
            for (var i = 0; i < 7; i++)
            {
                dates.Add(DateOnly.FromDateTime(day.AddDays(i)), day.AddDays(i).DayOfWeek);
            }
            return dates;
        }
    }
}

