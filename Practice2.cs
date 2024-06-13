using System;


namespace Practice2;


public class Practice2
{
    public class TimestampVariable
    {
        public int Percent { get; set; }
        public double MinutesToCheck { get; set; }
    }
    public class B
    {
        public void Method()
        {
            Console.WriteLine("B.Method");
            Method2();
        }

        public virtual void Method2()
        {
            Console.WriteLine("B.Method2");
        }
    }

    public class A : B
    {
        public override void Method2()
        {
            Console.WriteLine("A.Method2");
        }

        public void CallBaseMethod()
        {
            base.Method();  // Call base class method
        }
    }

    public static void Main(string[] args)
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

        var A = new A();
        A.CallBaseMethod(); //B.Method A.Method2
    }
}
