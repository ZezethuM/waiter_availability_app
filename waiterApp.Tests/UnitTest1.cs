using waiterApp;
using System;
using System.Collections.Generic;
using Xunit;
using System.IO;
using Npgsql;
using Dapper;

namespace waiterApp.Tests;
 
public class UnitTest1
{
   static string connectionString = "Server=tiny.db.elephantsql.com;Port=5432;Database=dvskbyna;UserId=dvskbyna;Password=ADBsJezup7e_jmpWR07rzHYUCp5qAwFV";
   IWaiterShift ma = new Shift(connectionString);
    // Shift n = new Shift(connectionString);
    public UnitTest1()
    {
        var sql = File.ReadAllText("./sql/data.sql");

        using(var connection = new NpgsqlConnection(connectionString))
        {
            connection.Execute(sql);
        }
    }
    [Fact]
    public void AddWaiterWorkingDaysOnTheDB()
    {
        List<string> m = new List<string>(){"Monday", "Tuesday", "Friday"};

        ma.SelectDay("Phumza", m);
        ma.ShifDayOfWaiter("Phumza");

        Assert.Equal(ma.GetListOfDays(), ma.ShifDayOfWaiter("Phumza"));    
    }
    [Fact]
    public void ShouldBeAbleToReturnAllTheWaitersInTheDBWithTheirWorkingDays()
    {
        Assert.Equal(ma.GetDictionary(), ma.DisplayDays());
    }

    [Fact]
    public void ShoildBeAbleToUpdateWorkingDayOfWaiter()
    {
        List<string> x = new List<string>(){"Monday", "Tuesday", "Friday"};
        ma.SelectDay("Phumza", x);

        List<string> s = new List<string>(){"Tuesday", "Friday","Saturday", "Sunday"};
        ma.UpdatingShifts("Phumza", s);

        Assert.Equal(ma.GetDictionary(), ma.DisplayDays());
    }
    [Fact]
    public void ShouldAllowManagerToResetTheData()
    {

    }

}