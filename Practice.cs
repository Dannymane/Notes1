using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Practice
{


    public abstract class BaseBook
    {
        public virtual void SayHi() //virtual method can be overriden
        {
            Console.WriteLine("Hi!");
        }

        // public abstract void SayAbstractHi(); //derived class must inherit this, abstract method can't have body
    }

    interface IBook
    {
        int Pages { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        Publisher Publisher { get; set; }
        void PrintInfo()
        {
            Console.WriteLine("Title: {0}", Title);
            Console.WriteLine("Author: {0}", Author);
            Console.WriteLine("Pages: {0}", Pages);
            Console.WriteLine("Publisher: {0}", Publisher.Name);
        }
    }

    class Book : BaseBook, IBook
    {
        public int Pages { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Publisher Publisher { get; set; }
        // public void PrintInfo(){      //this will replace the IBook.PrintInfo() implementation
        //    Console.WriteLine("no");   //you can't override IBook.PrintInfo() 
        // }
    }
    public class Publisher
    {
        public string Name { get; set; }

    }

    public static class StringExt
    {
        public static string ToAlternatingCase(this string s)
        {
            return String.Concat(s.Select(ch => char.IsUpper(ch) ? char.ToLower(ch) : char.ToUpper(ch)));
        }
    }

    public class Program
    {
        internal static Book ReturnNewBook(Book b)
        {
            Book newBook = new Book();
            newBook.Title = b.Title;
            newBook.Author = b.Author;
            newBook.Pages = b.Pages;
            newBook.Publisher = b.Publisher;
            return newBook;
        }

        public static void NbDig(int n, int d)
        {
            var squares = Enumerable.Range(0, n + 1).Select(i => (i * i).ToString());
            System.Console.WriteLine("Squares: " + String.Join(", ", squares));
            var digits = squares.Select(sq => sq.ToCharArray())
                                .SelectMany(array => array);

            // var suitableSquares = squares.Where(str => Regex.IsMatch(str, $"{d}")).ToArray();
            // System.Console.WriteLine($"Squares with digit {d} : " + String.Join(", ", suitableSquares));
            System.Console.WriteLine(digits.Where(str => Regex.IsMatch(str.ToString(), $"{d}"))
                                           .Count());
        }
        public static void Main()
        {


            Book b = new Book()
            {
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Pages = 310,
                Publisher = new Publisher()
                {
                    Name = "Allen & Unwin"
                }
            };
            b.SayHi(); //Hi! (normal method from base abstract class)

            //b.PrintInfo(); //Not implemented method - not working

            IBook ib = b;
            ib.PrintInfo();

            Book b2 = ReturnNewBook(b);
            b.Pages = 500; //value type - does not affect b2
            b.Publisher.Name = "Penguin"; //reference type - affects b2
            ib = b2;
            ib.PrintInfo();

            NbDig(15, 1);

            int[] numbers = new int[] { 1, 1, 2, 1 };
            System.Console.WriteLine(numbers.Count(i => i == 1));

            //(byte)'0' = 48 (ASCII). So, if you subtract 48 from the ASCII value of any digit, you get the actual value of that digit.
            string str = "12382";
            System.Console.WriteLine(String.Join("", str.ToCharArray().Select(i =>
            {
                if ((byte)i - 48 < 5)
                    return 0;
                else
                {
                    return 1;
                }
            })));//00010



            System.Console.WriteLine(String.Concat(str.Select(ch => ch < '5' ? "0" : "1")));//00010

            string UpperLowerString = "Hello";
            System.Console.WriteLine(UpperLowerString.ToAlternatingCase());//hELLO
            System.Console.WriteLine(UpperLowerString);//Hello
            char[] chars = new char[] { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
            System.Console.WriteLine(UpperLowerString.Select(ch => vowels.Any(c => c.Equals(ch)) ? '!' : ch));
        }
    }
}