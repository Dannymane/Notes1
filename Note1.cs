using System;
using System.Security.Cryptography.X509Certificates;

namespace Note1;

public static class StaticClass{
   public static void Test(){
      Console.WriteLine("Test");
   }
}
public class Program{
   public static void Maine(){
      Console.WriteLine(Guid.NewGuid().ToString()); //7497599a-d2fc-43bc-84b1-ed67156f7672

      //Can not create an instance of the static class 
      // StaticClass sc = new StaticClass();
      // sc.Test();
   }
}