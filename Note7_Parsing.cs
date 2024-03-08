

namespace notes7_parsing;

public class notes7_parsing{
	public static void Mainw(string[] args){
		var number = "0-1.23";
		// Console.WriteLine(float.Parse(number)); not working
		string n2 = "00123";
		string n3 = "00120";
		string n4 = "00000";
		Console.WriteLine(float.Parse(n2)/100);
		Console.WriteLine(float.Parse(n3)/100);
		Console.WriteLine(float.Parse(n4)/100);
	}
}