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
       static string GetConnectionString() 
       {
            var theCN = Environment.GetEnvironmentVariable("PSQLConnectionString");
            if (theCN == "" || theCN == null) 
            {
                theCN = connectionString;
            }
            return theCN;
        }
   IWaiterShift ma = new Shift(GetConnectionString());
    public UnitTest1()
    {
        var sql = File.ReadAllText("./sql/data.sql");
        using(var connection = new NpgsqlConnection(GetConnectionString()))
        {
            connection.Execute(sql);
        }
    }
    [Fact]
    public void AddWaiterWorkingDaysOnTheDB()
    {
        Assert.Equal(ma.GetListOfDays(), ma.ShifDayOfWaiter("Phumza"));    
    }
    [Fact]
    public void ShouldBeAbleToReturnAllTheWaitersInTheDBWithTheirWorkingDays()
    {
        Assert.Equal(ma.GetDictionary(), ma.DisplayDays());
    }
   [Fact]
    public void ShouldBeAbleToUpdateWorkingDayOfWaiter()
    {
        List<DateTime> s = new List<DateTime>()
        {
            DateTime.Parse("2023/03/01"),
            DateTime.Parse("2023/02/21")
        };
        ma.UpdatingShifts("Phumza", s, 0);
        ma.UpdatingShifts("Phumza", s, 7);

        Assert.Equal(ma.GetListOfDays(), ma.ShifDayOfWaiter("Phumza"));
    } 

     [Fact]
    public void ShouldBeAbleToReturnAllWorkingDaysOfWaiter()
    {
        List<DateTime> s = new List<DateTime>()
        {
            DateTime.Parse("2023/02/27"),
            DateTime.Parse("2023/02/28")
        };
        ma.UpdatingShifts("Karabo", s, 0);
        ma.UpdatingShifts("Karabo", s, 7);

        Assert.Equal(ma.GetListOfDays(), ma.ShifDayOfWaiter("Phumza"));
    }

}