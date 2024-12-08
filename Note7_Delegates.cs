using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes1
{
    delegate void Log(string message);

    internal class Note7_Delegates
    {
        List<string> Logs = new List<string>();
        public static void delegateMethod1(string message)
        {
            Console.WriteLine(message);
        }
        public void delegateMethod2(string message)
        {
            Logs.Add(message);
            Console.WriteLine("\"" + message + "\" has wirtten to logs.");
        }

        public static void Main(string[] args)
        {
            Log log = delegateMethod1;
            log("method 1 works");

            var o = new Note7_Delegates();
            log = o.delegateMethod2;
            log("method 2 works");


        }
    }
}
