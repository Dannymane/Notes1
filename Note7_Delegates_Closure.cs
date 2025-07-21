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

        public static void Mainw(string[] args)
        {
            Log log = delegateMethod1;
            log("method 1 works"); //runs delegateMethod1("method 1 works");

            var o = new Note7_Delegates();
            log = o.delegateMethod2;
            log("method 2 works");


            string myVar = myFunc("something");


        }

        Func<string, string> myFunc = var1 => "some value"; //same as:
        Func<string, string> myFunc = delegate (string var1)
                                {
                                    return "some value";
                                };

        //Closure
        public static Func<int, int> GetAFunc()
        {
            var myVar = 1; //free variable
            Func<int, int> inc = delegate (int var1)
                                    {
                                        myVar = myVar + 1;
                                        return var1 + myVar;
                                    };
            return inc;
        }
        static void Main(string[] args)
        {
            var inc = GetAFunc();
            Console.WriteLine(inc(5)); //7
            Console.WriteLine(inc(6)); //9 (myVar == 3)
        }
    }
    
}
