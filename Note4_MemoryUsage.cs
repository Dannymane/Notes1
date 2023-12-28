using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes1
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; } = string.Empty;
    }
    public class Note4_MemoryUsage
    {
        public static void Mainw(string[] args)
        {
            //(true) forces garbage collection - so don't forget to "use" tested element in furhter code
            long currentMemoryUsage = System.GC.GetTotalMemory(true);

            Book b = new Book() { Title = "Title" };

            Console.WriteLine(b.Author);

            string article = "art. Wgr Daniel";
            string[] UNKNOWN_ARTICLES = { "nieznany artykuł", "art.wgr", "art. wgr", };
            Console.WriteLine(UNKNOWN_ARTICLES.Contains(article.ToLower()));
            Console.WriteLine(UNKNOWN_ARTICLES.Any(keyword => article.ToLower().Contains(keyword)));

        }
    }
}
