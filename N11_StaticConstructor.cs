using System;

namespace notes1;

public class Note6_StaticConstructor
{
	public static void Mainw(string[] args)
	{
		DerivedClass.Method(); // Trigger initialization: Step 1, 2, Method()
		Console.WriteLine(DerivedClass.BaseStaticField);// Step 3, 4, "0"

		Base b1 = new Base();
		Base b2 = new Base() {number = 5};
		Derived d1 = new Derived();

		Console.WriteLine(b1.number); //2
		Console.WriteLine(b2.number); //5
		Console.WriteLine(d1.number); //3
	}
}

class BaseClass
{
	public static int BaseStaticField = InitializeBaseStaticField();

	static int InitializeBaseStaticField()
	{
		Console.WriteLine("Step 3: BaseClass - Initializing BaseStaticField");
		return 0;
	}

	static BaseClass()
	{
		Console.WriteLine("Step 4: BaseClass - Static Constructor");
	}
}

class DerivedClass : BaseClass
{
	public static int DerivedStaticField = InitializeDerivedStaticField();

	static int InitializeDerivedStaticField()
	{
		Console.WriteLine("Step 1: DerivedClass - Initializing DerivedStaticField");
		return 0;
	}

	static DerivedClass()
	{
		Console.WriteLine("Step 2: DerivedClass - Static Constructor");
	}

	public static void Method()
	{
		Console.WriteLine("Static initialization triggered");
	}
}

public class Base {
	public int number = 1;
	public Base(){
		number = 2;
	}
}

public class Derived : Base {
	new public int number = 3;

	// public Derived(){
	// 	number = 4;
	// }

}