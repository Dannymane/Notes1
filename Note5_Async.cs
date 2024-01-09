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
                    System.Console.Out.WriteLineAsync(i + " " + Thread.CurrentThread.ManagedThreadId + " " + lc.Value());
                });
            });
            System.Console.WriteLine("2^20 = " + lc.Value());
            Console.WriteLine();
            lc.Value(1);




            await Task.Run(() =>
            {
                Parallel.For(1, 20, (i) =>
                {
                    lc.MultiplyValueLock(2);
                    System.Console.Out.WriteLineAsync(i + " " + Thread.CurrentThread.ManagedThreadId + " " + lc.Value());
                });
            });
            System.Console.WriteLine(lc.Value());

            Console.WriteLine("So the fina result is good even without lock (lockObject), " +
                "but without lock ther is a mess during executing");
        }
    }
    public class LockClass
    {
        private Object lockObject;
        private int _value;
        public void Value(int v)
        {
            _value = v;
        }

        public void MultiplyValue(int v)
        {
            _value = _value * v;
        }
        public void MultiplyValueLock(int v)
        {
            lock (lockObject)
            {
                _value = _value * v;
            }
        }
        public int Value()
        {
            return _value;
        }

        public LockClass()
        {
            lockObject = new Object();
            _value = 1;
        }
    }
}
