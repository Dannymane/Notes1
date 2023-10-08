using System;
using System.Security.Cryptography.X509Certificates;

namespace Note1;

public static class StaticClass{
   public static void Test(){
      Console.WriteLine("Test");
   }
}

public class Program{
   public static IEnumerable<int> IteratiorMethod()
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


      foreach(int i in Program.IteratiorMethod())
      {
            Console.WriteLine(i);
      }

      var array = Program.IteratiorMethod().ToArray();
      Console.WriteLine(array[2]);




        //StringBuilder
        //Regex
    }
}