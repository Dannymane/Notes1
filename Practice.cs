


using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Practice{


public abstract class BaseBook{
   public void SayHi(){
      Console.WriteLine("Hi!");
   }
   // public abstract void SayAbstractHi(); //derived class must inherit this
}

interface IBook{
   int Pages{get;set;}
   string Title{get;set;}
   string Author{get;set;}
   Publisher Publisher{get;set;}
   void PrintInfo(){
      Console.WriteLine("Title: {0}", Title);
      Console.WriteLine("Author: {0}", Author);
      Console.WriteLine("Pages: {0}", Pages);
      Console.WriteLine("Publisher: {0}", Publisher.Name);
   }
}

class Book : BaseBook, IBook{
   public int Pages{get;set;}
   public string Title{get;set;}
   public string Author{get;set;}
   public Publisher Publisher{get;set;}
}
class Publisher{
   public string Name{get;set;}
   
}

public static class StringExt
{
   public static string ToAlternatingCase(this string s)
   {
   return String.Concat(s.Select(ch => char.IsUpper(ch) ? char.ToLower(ch) : char.ToUpper(ch)));
   }
}

public class Program{
   internal static Book ReturnNewBook(Book b){
      Book newBook = new Book();
      newBook.Title = b.Title;
      newBook.Author = b.Author;
      newBook.Pages = b.Pages;
      newBook.Publisher = b.Publisher;
      return newBook;
   }
   
   
   public static void NbDig(int n, int d) 
   {
      

      var squares = Enumerable.Range(0,n+1).Select(i => (i*i).ToString());
      // System.Console.WriteLine("Squares: " + String.Join(", ", squares));
      var digits = squares
                     .Select(sq => sq.ToCharArray())
                     .SelectMany(array => array);
      // var suitableSquares = squares.Where(str => Regex.IsMatch(str, $"{d}")).ToArray();
      // System.Console.WriteLine($"Squares with digit {d} : " + String.Join(", ", suitableSquares));
      System.Console.WriteLine(digits
                                 .Where(str => Regex.IsMatch(str.ToString(), $"{d}"))
                                 .Count());
   }
   public static void Maine(){


      Book b = new Book(){
         Title = "The Hobbit",
         Author = "J.R.R. Tolkien",
         Pages = 310,
         Publisher = new Publisher(){
            Name = "Allen & Unwin"
         }
      };
      b.SayHi(); //Hi! (normal method from base abstract class)
      
      IBook ib = b;
      ib.PrintInfo();
      
      Book b2 = ReturnNewBook(b);
      b.Pages = 500; //value type - does not affect b2
      b.Publisher.Name = "Penguin"; //reference type - affects b2
      ib = b2;
      ib.PrintInfo();

      NbDig(5750,0);
      int[] numbers = new int[]{1,1,2,1};
      System.Console.WriteLine(numbers.Count(i => i == 1));

      string str = "12382";
      System.Console.WriteLine(String.Join("", str.ToCharArray().Select(i => {
         if((byte)i-48 < 5)
            return 0;
         else
         {
            return 1;
         }
      })));
      char ch = '3';
      System.Console.WriteLine( (byte)'3'-48);


      System.Console.WriteLine(String.Concat(str.Select(ch => ch < '5' ? "0" : "1" )));

      string UpperLowerString = "Hello";
      System.Console.WriteLine(UpperLowerString.ToAlternatingCase());
      System.Console.WriteLine(UpperLowerString);
      char[] chars = new char[]{'a','e','i','o','u','A','E','I','O','U'};

      char[] vowels = new char[]{'a','e','i','o','u','A','E','I','O','U'};
      System.Console.WriteLine(UpperLowerString.Select(ch => vowels.Any(c => c.Equals(ch)) ? '!' : ch));
      double de = 23.3423;

      var obj1 = new MyClass(){ Value = 1 };
      var obj2 = new MyClass(){ Value = 1 };
      System.Console.WriteLine(obj1.Equals(obj2)); 
      System.Console.WriteLine(obj1 == obj2);

      string s1 = "test";
      string s2 = "test";
      string s3 = "test1".Substring(0, 4);
      object s4 = s3;  // Notice: set to object variable!

      Console.WriteLine($"{object.ReferenceEquals(s1, s2)} {s1 == s2} {s1.Equals(s2)}");
      Console.WriteLine($"{object.ReferenceEquals(s1, s3)} {s1 == s3} {s1.Equals(s3)}");
      Console.WriteLine($"{object.ReferenceEquals(s1, s4)} {s1 == s4} {s1.Equals(s4)}");

      int df = 1;
      double df2 = 1.0;
      System.Console.WriteLine(df.Equals(df2));
      System.Console.WriteLine(df == df2);
   }
}
public class MyClass{
   public int Value{get;set;}

}

}