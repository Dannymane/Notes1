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
}
//var a = new Derived(); //number not overriden, because Base constructor overrides old base "number" variable
//Console.WriteLine(a.number); //3

// public class Base2 {
// 	public virtual int number = 1; //error - virtual not valid for fields
// 	public Base2(){
// 		number = 2;
// 	}
// }

// public class Derived2 : Base2 {
// 	public override int number = 3; //error - override not valid for fields
// }


public class Base3 {
	public virtual int number {get; set;} = 1; 
	public Base3(){
		number = 2;
	}
}

public class Derived3 : Base3 {
	new public int number {get; set;} = 3;
}
//var a = new Derived2(); //number initialized to 3 and then overriden by base constructor
//Console.WriteLine(a.number); //2