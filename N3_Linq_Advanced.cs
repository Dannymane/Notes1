using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Notes1;
using Practice;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Reflection.Metadata.Ecma335;

public class Linq2{
	public class Book{
		public string? Author {get; set;}
		public string? Title {get; set;}
		public int? Pages {get; set;}
	}

	//collection.Select(UpperCase)
	//same as (str => str.ToUpper), but you can write big code blocks
	public static string UpperCase(string word){
			return word.ToUpper();
		}
	
	public static void Mainw(string[] args){

		//IEnumerable on objects
		var strings = new List<string> { "Hello", "World", "!" };
		var uppercase = strings.Select(s => 
		{
			Console.WriteLine(s);
			return s.ToUpper();
		})
		.Where(s => s.Length > 3);
		
		foreach (var s in uppercase)//every elements goes through pipeline .Select.Where
		{
			Console.WriteLine(s);
		}

		/* Output:
			Hello
			HELLO
			World
			WORLD
			!
		*/

		//1
		Console.WriteLine("1. Books max pages\n");
		var books = new[] {
			new { Author = "Robert Martin", Title = "Clean Code", Pages = 464 },
			new { Author = "Enrico Buonanno", Title = "Functional Programming in C#", Pages = 425 },
			new { Author = "Martin Fowler", Title = "Patterns of Enterprise Application Architecture", Pages = 533 },
			new { Author = "Bill Wagner", Title = "Effective C#", Pages = 328 },
		};
		var emptyBooks = new List<Book>();
		// books.FirstOrDefault(b => b.Pages == books.Max(b => b.Pages))?.Title //Bad solution - for every book it will iterate over all books
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
		var countedAnimals = animals.CountDogsCatsAndOthers(",");
		foreach(var a in countedAnimals)
			Console.WriteLine(a);

		//4 Linq Zip method
		Console.WriteLine("\n4. Challenge - Swim Length Times\n");
		string timesString = "00:45,01:32,02:18,03:01,03:44,04:31,05:19,06:01,06:47,07:35";
		//convert it into the sequence:
		//Length 1: Start = 0:00 End = 0:45
		//Length 2: Start = 0:45 End = 1:32
		//...

		var timesStart = String.Concat("00:00,", timesString);
		timesStart.Split(",")
			.Zip(timesString.Split(","),
			(start, end) => new {
				Start = start,
				End = end
			})
			.ToList()
			.ForEach(a => Console.WriteLine(a));

		//5 Chunk method. Split a collection of strings into some groups (every line = new group).
        System.Console.WriteLine("\n//5. Chunk method\n");

        string[] cities5 =
        {
         "ROME","LONDON","NAIROBI",
         "CALIFORNIA", "ZURICH","NEW DELHI",
         "AMSTERDAM", "ABU DHABI", "PARIS",
         "NEW YORK"
   		};
		cities5.Chunk(3)
		.ToList()
		.ForEach(g => Console.WriteLine(string.Join(",", g)));
		//output
		/*	ROME,LONDON,NAIROBI
			CALIFORNIA,ZURICH,NEW DELHI
			AMSTERDAM,ABU DHABI,PARIS
			NEW YORK
		*/

		//6 .
        System.Console.WriteLine("\n//6. \n");
		var sales = new int[] {0, 2, 1, 0, 4, 5, 2, 1, 0, 0, 2, 0, 4, 2, 1};

		var max = 0;
		sales.Aggregate((count, s) => {
			if(s > 0) 
			{ 
				count = count + 1; 
				if(max < count) 
				{
					max = count;
				}
			}else{
				count = 0;
			} 
			return count; //returned result is passed into count after every iteration
		});
		Console.WriteLine(max);

		//7
        Console.WriteLine("\n//7. \n");
		
		var words = new List<string> {"hi", "ok", "love"};
		words.Select(UpperCase).ToList().ForEach(w => Console.WriteLine(w));

		
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

		public static dynamic CountDogsCatsAndOthers(this string animals, string separator){
			var countedAnimals = animals.Split(separator)
				.GroupBy(a => (a != "Dog" && a != "Cat") ? "Other" : a)
				//or GroupBy(a => a switch {"Dog" or "Cat" => a, _ => "Other"})
				.Select(g => new {
					Animal = g.Key,
					Quantity = g.Count()
				});

			return countedAnimals;
		}

		
	}