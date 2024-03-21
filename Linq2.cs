using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Notes1;
using Practice;

public class Linq2{
	public class Book{
		public string? Author {get; set;}
		public string? Title {get; set;}
		public int? Pages {get; set;}
	}
	
	public static void Main(string[] args){

		//1
		Console.WriteLine("1. Books max pages\n");
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

		//2
		Console.WriteLine("\n2. Concat - own extension method\n");
		IEnumerable<decimal> decimalArray = new[] { 1.00m, 2.5m, 3.48m, 4m, 5.5m };
		Console.WriteLine(decimalArray.Concat(";")); //1,00;2,5;3,48;4;5,5;

		//3 
		Console.WriteLine("\n3. Challenge - animals\n");
		string animals = "Dog,Cat,Rabbit,Dog,Dog,Lizard,Cat,Cat,Dog,Rabbit,Guinea Pig,Dog";
		var countedAnimals = animals.CountAnimals(",");
		foreach(var a in countedAnimals)
			Console.WriteLine(a);
	}
}

public static class MyLinqExtensions{
		public static string Concat<T>(this IEnumerable<T> collection, string separator){
			// StringBuilder result = new StringBuilder();
			// foreach(T elem in collection)
			// 	result.Append((elem?.ToString() ?? " ") + separator); 
			
			// return result.ToString();

			return string.Join(separator, collection);
		}

		public static dynamic CountAnimals(this string animals, string separator){
			var countedAnimals = animals.Split(separator)
				.GroupBy(a => a)
				.Select(g => new {
					Animal = g.Key,
					Quantity = g.Count()
				});

			return countedAnimals;
		}
	}