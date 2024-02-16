using System;


namespace Practice2;


public class Practice2
{
    public class TimestampVariable
    {
        public int Percent { get; set; }
        public double MinutesToCheck { get; set; }
    }

    public static void Mainw(string[] args)
    {

        var o2 = new TimestampVariable() { Percent = 10, MinutesToCheck = 10 };
        Console.WriteLine(nameof(o2));

        var dates = new List<DateTime>() { DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2) };
        dates.OrderBy(d => d).ToList().ForEach(d => Console.WriteLine(d));
        Console.WriteLine(dates.First());

        object[] values = new object[1];
        if(values[0] == null)
        {
            Console.WriteLine("null");
        }
        if (values[0] is TimeSpan)
        {
            Console.WriteLine("TimeSpan");
        }
    }
}
