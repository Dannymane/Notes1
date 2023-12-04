using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes1
{
    public class Note3_OrderBy
    {
        public static void Main(string[] args)
        {
            var n1 = new List<decimal?>{ 10, -9, null, 1, 0, null };
            Console.WriteLine(String.Join(' ', n1.OrderBy(n => Math.Abs(n ?? 0))));
        }
    }
}
