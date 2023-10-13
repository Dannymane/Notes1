using System;
using System.Text;

namespace Practice2;


public class Practice2{

public static void Main(string[] args){
   
   var sb = new StringBuilder("Hello New York", 20);
   sb.Replace("Hello ", "");
   Console.WriteLine(sb);
}
}
