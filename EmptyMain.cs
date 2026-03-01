namespace Notes1;

public class EmptyMain
{
	public static void wMain(string[] args)
	{
		var range = Enumerable.Range(1 , 3);

		foreach(var a in range)
			Console.WriteLine(a);
	}
}
