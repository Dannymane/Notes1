
using System.Security.Cryptography.X509Certificates;

namespace memorizing;

public class Program{
public static void Main(string[] args){
	//--1 Write extension method CountDogsCatsAndOthers(",") 
	/* Result:
	{ Animal = Dog, Quantity = 5 }
	{ Animal = Cat, Quantity = 3 }   
	{ Animal = Other, Quantity = 4 }	*/

	string animals = "Dog,Cat,Rabbit,Dog,Dog,Lizard,Cat,Cat,Dog,Rabbit,Guinea Pig,Dog";
	var countedAnimals = animals.CountDogsCatsAndOthers(",");
	foreach (var animal in countedAnimals)
		Console.WriteLine(animal);

		string timesString = "00:45,01:32,02:18,03:01,03:44,04:31,05:19,06:01,06:47,07:35";
		//convert it into the sequence:
		//Length 1: Start = 0:00 End = 0:45
		//Length 2: Start = 0:45 End = 1:32
		//...
		var timestStart = String.Concat("00:00,", timesString);
		var lengths = timestStart.Split(",")
			.Zip(timesString.Split(","), (start, end) =>
			new {
				Start = start,
				End = end
			});
		
		int index = 1;
		foreach(var length in lengths){
			Console.WriteLine($"Length {index}: Start = {length.Start} End = {length.End}");
			index++;
		}
}
}

public static class ExtensionClass{
	public static dynamic CountDogsCatsAndOthers(this string animals, string separator){
		return animals.Split(separator)
			.GroupBy(a => a != "Dog" && a != "Cat" ? "Other" : a)
			.Select(g => new {
				Animal = g.Key,
				Quantity = g.Count()
			});
	}
}