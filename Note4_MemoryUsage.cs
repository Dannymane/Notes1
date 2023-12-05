using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes1
{
    public class Note4_MemoryUsage
    {
        public static void Main(string[] args)
        {
            //(true) forces garbage collection - so don't forget to "use" tested element in furhter code
            long currentMemoryUsage = System.GC.GetTotalMemory(true);
        }
    }
}
