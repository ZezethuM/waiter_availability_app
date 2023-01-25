using waiterApp;
using System.Collections.Generic;
using Xunit;

namespace waiterApp.Tests;

 
public class UnitTest1
{
    Shift n = new Shift();
    List<string> m = new List<string>(){"Monday", "Tuesday", "Friday"};

    [Fact]
    public void Test1()
    {

        // n.SelectDay("zeze", m );
        // n.SelectDay("Phumza", m);
        // Assert.Equal(n.shiftDay, n.Ches());
    }
}