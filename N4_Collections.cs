using System;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.VisualBasic;
using System.Collections;
using System.Text;
using System.Xml.Serialization;


public class Collections{
   public interface ITest
   {
      void Test2(){
         System.Console.WriteLine("Test");
      }
   }
   public class Testd : ITest
   {

   }
   public static void Mainw(){
      //dotnet new {mvc | console | classlib | web  |...} -n SchoolRegister.Web -au Individual -f net6.0 -uld

      Testd t = new Testd();
      ITest it = t;
      it.Test2(); //Test

      //-------------------- List And Array -----------------
      var integers = new List<int>(){10,30,40};
      integers.Insert(1, 20);
      integers.ForEach(i => System.Console.WriteLine(i));//10 ,20, 30, 40

      int[] integersArray = new int[4];
      integers.CopyTo(integersArray);
      integersArray[0] = 1;
      Array.ForEach(integersArray, i => System.Console.WriteLine(i));//1 ,20, 30, 40

      //use FindAll() instead of Where() when you want to get List<int> instead of IEnumerable<int>
      integers.FindAll(i => i > 25).ForEach(i => System.Console.WriteLine(i));//30 40
      integers.Where(i => i > 25).ToList().ForEach(i => System.Console.WriteLine(i));//30 40

      int[] a1 = { 1, 2, 3, 4, 5 };
      int[] a3 = new int[5]; // Creates an array with 5 elements, all initialized to 0
      int[] a4 = new int[] { 1, 2, 3, 4, 5 }; // Creates an array with 5 elements, initialized to 1, 2, 3, 4, 5
      bool[] flags = new bool[3]; // { false, false, false }
      string[] names = new string[2]; // { null, null }
      int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } }; // 2D array
      

      int[] a5 = {1, 2, 3, 4, 5, 6};
      var b = a5[..2];  // 1, 2
      var c = a5[0..2]; // 1, 2
      var d = a5[2..];  // 3, 4, 5, 6
      
      //-------------------- Dictionary -----------------
      //.ToDictionary( gr => gr.Key, gr => gr.Value);
      
      Dictionary<string, string> d1 = new Dictionary<string, string>();
      d1.Add("France", "Paris");
      d1.Add("Japan", "Tokyo");
      d1.Add("USA", "Washington");
      d1.Add("USA2", "Washington");
      d1.Add("USA3", "Washington");
      d1.Add("USA4", "Washington");
      d1.Add("USA5", "Washington");
      d1.Add("USA6", "Washington");
      d1.Add("USA7", "Washington");

      foreach(KeyValuePair<string, string> r in d1)
         System.Console.WriteLine($"Key: {r.Key} Value: {r.Value}");

      System.Console.WriteLine(d1["France"]);

      string result;
      string result2;

      Dictionary<string, DateTime> dDateTime = new Dictionary<string, DateTime>();
      
      //var value = d1["key"]; //KeyNotFoundException
      if(d1.TryGetValue("Japan", out result))
         Console.WriteLine(result); //Tokyo


      if(d1.TryGetValue("qwerty", out result)) //false, after that result is null (not Tokyo)
         Console.WriteLine(result); 

      d1.TryGetValue("qwerty", out result2);//after that result2 is null!!! (default value)
      dDateTime.TryGetValue("qwerty", out var date);//after that `date` will be initialized and = 01/01/0001 00:00:00 (default value)

      foreach(var el in d1)
      {
         Console.WriteLine("Key: {0}, Value: {1}", el.Key, el.Value);
      }

      //--------------------------------------------------
      var lis = new List<KeyValuePair<string, string>>();
      foreach(var el in d1)
            lis.Add(el);

      lis.ForEach(el => Console.WriteLine(el.Key + " " + el.Value)); // Ordered, because modern .NET keeps order, but not Framework
      //You can rely on order only in .NET 5+ and .NET Core 3.0
      /*
      France Paris
      Japan Tokyo
      USA Washington
      USA2 Washington
      USA3 Washington
      USA4 Washington
      USA5 Washington
      USA6 Washington
      USA7 Washington
      */

      //-------------------- StringDictionary -----------------
      //StringDictionary - old, use Dictionary instead 
      StringDictionary strD = new StringDictionary(){
         {"first", "value1"},
         {"second", "value2"}
      };
      System.Console.WriteLine(strD["first"]);

      //-------------------- LinkedList -----------------

      LinkedList<int> linkedList = new LinkedList<int>();
      // Add elements
      linkedList.AddLast(1);
      linkedList.AddLast(2);
      linkedList.AddLast(3);

      // Insert an element in the middle
      LinkedListNode<int> node = linkedList.Find(2);  
      linkedList.AddAfter(node, 4);

      // Remove an element
      linkedList.Remove(1);

      // Traverse the list
      foreach (var item in linkedList)
         Console.WriteLine(item);

      //-------------------- HashSet -----------------
      HashSet<int> hashSet = new HashSet<int>();
      
      hashSet.Add(1);
      hashSet.Add(1); //ignored
      hashSet.Add(2);
      hashSet.Add(3);
      hashSet.Add(4);
      hashSet.Add(5);
      foreach (var e in hashSet)
      {
         System.Console.WriteLine(e);
      }
      System.Console.WriteLine(); 

      HashSet<object> hashSetObject = new HashSet<object>(); // Object = object, String = string
      object o1 = 3;
      Object o2 = 3;
      System.Console.WriteLine(o1 == o2); 
      System.Console.WriteLine(o1.GetHashCode()); 
      System.Console.WriteLine(o2.GetHashCode()); 
      hashSetObject.Add(o1);
      hashSetObject.Add(o2);
      foreach(var o in hashSetObject)
         System.Console.WriteLine(o); //
      


      //-------------------- Queue -----------------
      Queue<string> numbers = new Queue<string>();
      numbers.Enqueue("one");
      numbers.Enqueue("two");
      numbers.Enqueue("three");
      numbers.Enqueue("four");
      numbers.Enqueue("five");

      // A queue can be enumerated without disturbing its contents.
      foreach( string number in numbers )
      {
         Console.WriteLine(number);
      }


      Console.WriteLine("\nDequeuing '{0}'", numbers.Dequeue());
      Console.WriteLine("Peek at next item to dequeue: {0}",
         numbers.Peek());
      Console.WriteLine("Dequeuing '{0}'", numbers.Dequeue());

      // Create a copy of the queue, using the ToArray method and the
      // constructor that accepts an IEnumerable<T>.
      Queue<string> queueCopy = new Queue<string>(numbers.ToArray());

      Console.WriteLine("\nContents of the first copy:");
      foreach( string number in queueCopy )
      {
         Console.WriteLine(number);
      }

      Console.WriteLine("\nqueueCopy.Contains(\"four\") = {0}",
         queueCopy.Contains("four"));

      Console.WriteLine("\nqueueCopy.Clear()");
      queueCopy.Clear();
      Console.WriteLine("\nqueueCopy.Count = {0}", queueCopy.Count);

      //-------------------- Stack -----------------
      System.Console.WriteLine("-------------- Stack ------------");
      Stack<string> stack = new Stack<string>();
      stack.Push("one");
      stack.Push("two");
      stack.Push("three");
      stack.Push("four");
      stack.Push("five");

      // A stack can be enumerated without disturbing its contents.
      foreach( string number in stack )
      {
         Console.WriteLine(number);
      }

      Console.WriteLine("\nPopping '{0}'", stack.Pop());
      Console.WriteLine("Peek at next item to destack: {0}", stack.Peek());
      Console.WriteLine("Popping '{0}'", stack.Pop());

      Console.WriteLine("\nstack.Contains(\"four\") = {0}", stack.Contains("four"));
      Console.WriteLine("\nstack.Contains(\"two\") = {0}", stack.Contains("two"));

      Console.WriteLine("\nstack.Clear()");
      stack.Clear();
      Console.WriteLine("\nstack.Count = {0}", stack.Count);

      //-------------------- SortedList -----------------
      // SortedList faster then SortedDictionary only if you add already sorted data
      var mySL = new SortedList<double, string>();
      mySL.Add( 1.3, "fox" );
      mySL.Add( 1.4, "jumps" );
      mySL.Add( 1.5, "over" );
      mySL.Add( 1.2, "brown" );
      mySL.Add( 1.1, "quick" );
      mySL.Add( 1.0, "The" );
      mySL.Add( 1.6, "the" );
      mySL.Add( 1.8, "dog" );
      mySL.Add( 1.7, "lazy" );

      Console.WriteLine(mySL[1.7]);//lazy     //O(1) access by key 

      // Gets the key and the value based on the index.
      int myIndex=3;
      Console.WriteLine( "The key   at index {0} is {1}.", myIndex, mySL.Keys[myIndex]); //3 1.3
      Console.WriteLine( "The value at index {0} is {1}.", myIndex, mySL.Values[myIndex] );// 3 fox

      // Gets the list of keys and the list of values.
      IList<double> myKeyList = mySL.Keys;
      IList<string> myValueList = mySL.Values;

      // Prints the keys in the first column and the values in the second column.
      Console.WriteLine( "\t-KEY-\t-VALUE-" );
      for ( int i = 0; i < mySL.Count; i++ )
         Console.WriteLine( "\t{0}\t{1}", myKeyList[i], myValueList[i] );

      //-------------------------------------



      //Write number from 1 to 100
      string s1 = String.Join(" ",Enumerable.Range(1,100)); //intuitive version


      StringBuilder sb = new StringBuilder();
      for(int i = 1;i<=100;i++){
         sb.Append(i);
      } // a little bit more efficient

      System.Console.WriteLine(sb);
      System.Console.WriteLine("\n");

      //a little bit worse solution (creates an array with 100 integers)
      var dwa = Enumerable.Range(1, 100).ToList();
      System.Console.WriteLine(String.Join("", dwa));

      // var p1 = new Person(); //error, because Person doesn't have default constructor when
         // we have defined constructor with parameters
      Person.Ok(); //OK
   }

   public class User{
      public static void Ok(){
         Console.WriteLine("OK");
      }
   }
   public class Person : User
   {
      public string Name { get; set; }
      public int Age { get; set; }
      public Person(string name, int age)
      {
         Name = name;
         Age = age;
      }
      public void TestParams(params Object[] arguments)
      {
         foreach(var arg in arguments)
            System.Console.WriteLine(arg);
      }
   }

   public class Missile : IDisposable
   {
      public int Speed { get; set; }
      public Missile(int speed)
      {
         Speed = speed;
      }
      
      ~Missile(){
      //destructor is the same thing as Finalize(),  Object.Finalize() sends obj to Garbage Collector
      //called when object is destroyed, the exact time is depends on Garbage Collector
      }

      public void Dispose()
      {
         //called when object is destroyed - when explicitly called Dispose()
         // or after using "using" statement
         //It is recommended to use Dispose instead of Finalize/Destructor
      }
   }

   
}