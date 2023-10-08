

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

 public class Age{
   public int Value {get;set;}
   public Age(int value){
      Value = value;
   }
   public void IncreaseAge(int value){
      Value += value;
   }
}
public class Person{
   public string Name{get;set;}
   public Age Age {get;set;}
   public Person(string name, Age age){
      this.Name = name;
      this.Age = age;
   }
   public Person(Person p){
      this.Name = p.Name;
      this.Age = p.Age;
   }
}

public class Equals{
   
   public static void Mainw(){
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
   

      Person p1 = new Person("John", new Age(20));
      Person p2 = new Person(p1);
      p1.Name = "Mary"; //From now, p1.Name points at "Mary", but p2.Name still points at "John"
      p1.Age.IncreaseAge(10);
      Console.WriteLine(p1.Name); //Mary 
      Console.WriteLine(p1.Age.Value); //30
      Console.WriteLine(p2.Name); //John
      Console.WriteLine(p2.Age.Value); //30 

      p1.Age = new Age(40);
      Console.WriteLine(p2.Age.Value); //30, because now p2.Age points at the other object than p1.Age

      Age age1 = new Age(10);
      Age age2 = age1;

      age2.Value = 20;
      Console.WriteLine(age1.Value); //20

      Object o1 = (Object)10;
      Object o2 = o1;
      o1 = (Object)20; //boxing, pointing to another object
      Console.WriteLine(o2); //10

      var an1 = new {Value = 10, Name = "John"};
      var an2 = an1;//an2 point to the same object as an1 - John
      an1 = new {Value = 20, Name = "Dan"}; //an1 do not point to John anymore
      Console.WriteLine(an2.Value); //10

      
   }
}
public class MyClass{
   public int Value1{get;set;}

}
public class MyClassString{
   public string Value1 {get;set;}

}
}