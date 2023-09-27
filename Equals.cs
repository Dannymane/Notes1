

using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Equals{
 public class Tst{
      public int Value1{get;set;}
      public int Value2{get;set;}
      public Tst(int value1, int value2){
         Value1 = value1;
         Value2 = value2;
      }
      public void Test(object o){
         Console.WriteLine(GetType().ToString() + " " + o.GetType().ToString());
      }
 }

public class Equals{
   
   public static void Main(){
      Tst t43 = new Tst(1,2);
      Tst t44 = new Tst(1,2);
      t43.Test(t44); //Equals.Tst Equals.Tst

      /*
            == :
         - value types are equal when their value are equal | double 1.0 == int 1 --> true
         - reference types (except string) are equal when they refer to the same storage (Object)
         - strings are equal when they have the same value or both null

            Equals():
         - value types ... same, but with boxing/unboxing and some exceptions
         - reference types (except string) are equal when they refer to the same Object
         - string.Equals(o) 'o' can be an object that refer to string.
         - strings are equal when they have the same value
         
            So, just use:
          == for value types 
          == or Equals for references (better use Equals because maybe somebody implemented custom Equals)
          == for strings if string can be null
          Equals fro strings if string can be as an Object
      */
      double de = 23.3423;

      var obj1 = new MyClass(){ Value1 = 1 };
      var obj2 = new MyClass(){ Value1 = 1 };
      System.Console.WriteLine(obj1.Equals(obj2));                         //F
      System.Console.WriteLine(obj1 == obj2);                    //F

      //"test" is a pointer, the s1 is a pointer too.
      
      string s1 = "test";
      string s2 = "test";
      string s3 = "test1".Substring(0, 4); //creates new "test" in heap
      object s4 = s3;  // Notice: set to object variable!

      MyClassString mcs = new MyClassString(){Value1 = "test"}; 
      System.Console.WriteLine(s1.Equals(mcs.Value1));                     //T
      System.Console.WriteLine(s1.Equals(mcs));                   //F

      Console.WriteLine($"{object.ReferenceEquals(s1, s2)} {s1 == s2} {s1.Equals(s2)}");     // T T T
      Console.WriteLine($"{object.ReferenceEquals(s1, s3)} {s1 == s3} {s1.Equals(s3)}");     // F T T
      Console.WriteLine($"{object.ReferenceEquals(s1, s4)} {s1 == s4} {s1.Equals(s4)}");     // F F T

      byte byte1 = 1;
      int int1 = 1;
      double double1 = 1.0;
      System.Console.WriteLine(int1.Equals(double1));          //F
      System.Console.WriteLine(double1.Equals(int1));                      //T
      System.Console.WriteLine(int1 == double1);               //T
      System.Console.WriteLine(double1 == int1);                           //T

      System.Console.WriteLine(int1.Equals(byte1));            //T
      System.Console.WriteLine(byte1.Equals(int1));                         //F



      int value1 = 12;
      long value2 = 12;

      object object1 = value1;
      object object2 = value2;

      Console.WriteLine(object1.Equals(object2));              // F
      Console.WriteLine(value1.Equals(value2));                            // F

      Console.WriteLine(object2.Equals(object1));              // F
      Console.WriteLine(value2.Equals(value1));                            // T 

      Console.WriteLine(object2.Equals(value1));              // F
      Console.WriteLine(object2.Equals(value2));                           // T
   }
}
public class MyClass{
   public int Value1{get;set;}

}
public class MyClassString{
   public string Value1 {get;set;}

}
}