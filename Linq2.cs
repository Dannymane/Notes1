

using System.ComponentModel.Design;
using System.Security.Cryptography;
using Notes1;
using Practice;

public class Linq2{
	public class Book{
		public string? Author {get; set;}
		public string? Title {get; set;}
		public int? Pages {get; set;}
	}
	public static void Main(string[] args){
		var books = new[] {
			new { Author = "Robert Martin", Title = "Clean Code", Pages = 464 },
			new { Author = "Enrico Buonanno", Title = "Functional Programming in C#", Pages = 425 },
			new { Author = "Martin Fowler", Title = "Patterns of Enterprise Application Architecture", Pages = 533 },
			new { Author = "Bill Wagner", Title = "Effective C#", Pages = 328 },
		};
		var emptyBooks = new List<Book>();
		// books.FirstOrDefault(b => b.Pages == books.Max(b => b.Pages))?.Title //Bad solution - for every book it will iterate over whole books
		var maxPages = books.Max(b => b.Pages);
		Console.WriteLine(books.FirstOrDefault(b => b.Pages ==maxPages)?.Title);//Patterns of Enterprise Application Architecture
		
		Console.WriteLine(books.MaxBy(b => b.Pages)?.Title);//Patterns of Enterprise Application Architecture
		Console.WriteLine(emptyBooks.MaxBy(b => b.Pages)?.Title); // ""
	}
}