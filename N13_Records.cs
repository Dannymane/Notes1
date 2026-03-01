namespace Records;

public class EmptyMain
{

	public record Person(string Name, int Age);

	public static void Main(string[] args)
	{
		var p1 = new Person("Alice", 25);
		var p2 = new Person("Alice", 25);

		Console.WriteLine(p1 == p2); 										// True


		var a = new Person("Jonny", 12);
		Console.WriteLine(a.ToString());
	}
}
