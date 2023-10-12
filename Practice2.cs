using System;
using System.Text;

namespace Practice2;


public class Practice2{

public static void Main(string[] args){
   
   var sb = new StringBuilder("Hello World", 40);
   sb.Append("!");

   sb.Insert(5, " C#");
   Console.WriteLine(sb); // Hello C# World!

   string str = sb.ToString();

   sb.Remove(8, sb.Length - 8);
   Console.WriteLine(sb);

   var list = sb.ToString().Split(" ").ToList();
   list.ForEach(str => Console.WriteLine(str));

   var sb2 = new StringBuilder("Hello beautiful World!");
   Console.WriteLine(sb2.Remove(6,10));

   Console.WriteLine(sb2.Replace("Hello", "Goodbye"));

   
}
}
