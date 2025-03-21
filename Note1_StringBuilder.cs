using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text; //StringBuilder
using System.Text.RegularExpressions; //Regex

namespace Note1;

//What to learn
// exceptions in asynchronous programming

public static class StaticClass{
   public static void Test(){
      Console.WriteLine("Test");
   }
}
//virtual method or property can be overriden 
public class Vehicle{
   public virtual void Drive(){
      Console.WriteLine("Drive");
   }
}
public class Car : Vehicle{
   public override void Drive(){
      base.Drive();                    //call the base class method
      Console.WriteLine("Car Drive");
   }
}
public class Program{
   public static IEnumerable<int> IteratorMethod()
   {
      yield return 0;
      yield return 1;
      yield return 2;
   }//main commit
   public static void Mainw(){
      Console.WriteLine(Guid.NewGuid().ToString()); //7497599a-d2fc-43bc-84b1-ed67156f7672

      //Can not create an instance of the static class 
      // StaticClass sc = new StaticClass();
      // sc.Test();

      foreach(int i in Program.IteratorMethod())
      {
         Console.WriteLine(i);
      }

      var array = Program.IteratorMethod().ToArray();
      Console.WriteLine(array[2]);

      Car c = new Car();
      c.Drive();                      //Drive
                                       // Car Drive
      //StringBuilder

      StringBuilder sb1 = new StringBuilder();
      StringBuilder sb2 = new StringBuilder(50); //capacity, it will automatically doubled once it reaches 50 characters 
      StringBuilder sb3 = new StringBuilder("Hello World!");
      StringBuilder sb4 = new StringBuilder("Hello World!", 50);

      Console.WriteLine(sb4[1]); //e

      string s4 = sb4.ToString();

      sb4.Append("?"); //Hello World!?
      
      StringBuilder sbAmout = new StringBuilder("Your total amount is ");
      sbAmout.AppendFormat("{0:C} ", 25); //Your total amount is $ 25.00
      
      StringBuilder sb5 = new StringBuilder("Hello World!");
      sb5.Insert(5," C#");  //Hello C# World!

      StringBuilder sb6 = new StringBuilder("Hello World!",50);
      sb6.Remove(5, 6);     //Hello!        //(start index, number of characters to remove)

      StringBuilder sb7 = new StringBuilder("Hello World!");
      sb7.Replace("World", "C#"); //Hello C#!


   //Test yourself

      //1. StringBuilder with capacity 40

      //2. StringBuilder "Hello World!"
         Console.WriteLine();
      //3. StringBuilder "Hello World!" with capacity 40
         Console.WriteLine();
      //4. write third letter of "Hello World!"
      Console.WriteLine(); //l









      //5. "Hello World!" -> Hello World!?
      StringBuilder sb15 = new StringBuilder("Hello World!");
      Console.WriteLine(sb15.Append("?"));

      //6."Your total amount is " -> "Your total amount is 25,00"
      StringBuilder sb16 = new StringBuilder("Your total amount is ");
      int twentyFive = 25;
      Console.WriteLine();
      
      //7. Hello World! -> Hello C# World!
      StringBuilder sb17 = new StringBuilder("Hello World!");
      Console.WriteLine();

      //8. "Hello World!" -> "Hello!"
      StringBuilder sb18 = new StringBuilder("Hello World!",50);
      Console.WriteLine();    

      //9. "Hello World!" -> "Hello C#!"
      StringBuilder sb19 = new StringBuilder("Hello World!");
      Console.WriteLine();


//---------------------- String ---------------------------
      string word1 = "Hello";
      string word2 = "hellO";
      
      //Ignore cases
      Console.WriteLine(string.Equals(word1, word2, StringComparison.OrdinalIgnoreCase));

      //Ordinal StringComparison based on comparison of Unicode code values


   }
}