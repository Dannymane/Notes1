using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes1
{
    internal class Note5_Async
    {
        public static async Task Mainw(string[] args)
        {
            // Lock Object
            LockClass lc = new LockClass();//value = 1
            await Task.Run(() =>
            {
                Parallel.For(1, 20, (i) =>
                {
                    lc.MultiplyValue(2);
                    System.Console.Out.WriteLineAsync(i + " " + Thread.CurrentThread.ManagedThreadId);
                });
            });
            System.Console.WriteLine(lc.Value());

            lc.Value(1);




            await Task.Run(() =>
            {
                Parallel.For(1, 20, (i) =>
                {
                    lc.MultiplyValueLock(2);
                    // System.Console.Out.WriteLineAsync(i + " " + Thread.CurrentThread.ManagedThreadId);
                });
            });
            System.Console.WriteLine(lc.Value());
        }
    }
}
