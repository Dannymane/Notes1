using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes1
{
    public class Note3_Other
    {
        public static void Mainw(string[] args)
        {
            var strings = new List<string> { "Hello", "World", "!" };
            var uppercase = strings.Select(s => 
            {
                Console.WriteLine(s);
                return s.ToUpper();
            })
            .Where(s => s.Length > 3);
            
            foreach (var s in uppercase)
            {
                Console.WriteLine(s);
            }

        }
    }
}
