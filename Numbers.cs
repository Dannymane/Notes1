

namespace notes7_parsing;

public class notes7_parsing{
	public static void Mainw(string[] args){

		//suffix
		var d = 21.22m;
		Console.WriteLine(d.GetType());//System.Decimal

		//int always rounds down (it takes the integer - целое число)
		int int1 = 7/4; //1
		double dbl1 = 9.9;
		int1 = (int)dbl1; // 9

		//casting order:
		//1. casting
		//2 math operation
		// int1 = (int)dbl1/3.3; //error, it converts 9.9 to 9 and 9/3.3
		// int1 = dbl1/3.3; //error, the result 3.0 (double not int)
		int1 = (int)(dbl1/3.3); // 3
 
		// int1 = (int)"123"; //error

		int result;
		// Console.WriteLine(result); //error
		bool success = int.TryParse(null, out result);     // false
		Console.WriteLine(result); //0
		success = int.TryParse(string.Empty, out result); // false


		var number = "0-1.23";
		// Console.WriteLine(float.Parse(number)); not working
		string n2 = "001,23";
		string n3 = "00,120";
		string n4 = "000,00";
		string n5 = "   12,3    ";
		Console.WriteLine(float.Parse(n2)); // 1,23
		Console.WriteLine(float.Parse(n3)); // 0,12
		Console.WriteLine(float.Parse(n4)); // 0
		Console.WriteLine(float.Parse(n5)); // 12,3


		float.TryParse( "   12.333   ", 
			System.Globalization.NumberStyles.Float, 
			System.Globalization.CultureInfo.InvariantCulture, 
			out float fl1);
		
		Console.WriteLine(fl1); // 12,333

		//common tricks
		int r3 = int.TryParse(n2, out int temp) ? temp : 11;
		if (int.TryParse(n2, out int age) && age >= 0 && age <= 120) {}

	}
}