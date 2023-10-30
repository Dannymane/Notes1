using System;
using System.Security.Cryptography.X509Certificates;
using System.Text; //StringBuilder
using System.Text.RegularExpressions; //Regex

namespace Note1;

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
   }
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

      Console.WriteLine(sb4[7]); //W

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




   }
}