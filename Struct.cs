using System;
using System.Runtime.CompilerServices;

struct Struct {
   public int Value1{get;set;}
   public int Value2{get;set;}
   public Struct(int value1, int value2){
      Value1 = value1;
      Value2 = value2;
   }
   public void Test(){
      Console.WriteLine("Test");
   }
}

public abstract class AbstractClass{
   public abstract void Test(); //can't have a body
}
public class MyClass : AbstractClass{
   public override void Test(){ //obligatory
      Console.WriteLine("Test");
   }
}
public class Program{
   public static void Maine(){
      Struct s = new Struct(1,2);
      s.Test();
      Console.WriteLine(s.Value1); //1
   }
}
