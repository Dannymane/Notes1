using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

 struct MyStruct{
   int a;
   int b;
   public MyStruct()
   {
      a = 0;
      b = 10;
   }
}

public class Person{
   public string? Name {get; set;}
}
public class Student : Person{
      public int StudentID { get; set; }
      public string StudentName { get; set; }
      public int? age { get; set; }
      public int? GrPoint  { get; set; }
   }
public class Linq{
   public static async Task Main(string[] args){

   Person p = new Student(){StudentID = 1, StudentName = "John", age = 18}; //it sees only Person properties
   Student s12;
   
   // downcasting
   s12 = (Student)p;//it sees student properties 
   Console.WriteLine(s12.age); //18
   

   string[] names = {"Bill", "Steve", "James", "Mohan" };
   
   // LINQ Query 
   var myLinqQuery = from name in names
                  where name.Contains('a')
                  select name;
   System.Console.WriteLine(myLinqQuery.Last()); //Mohan
   // Query execution
   foreach(var name in myLinqQuery)
      Console.Write(name + " "); //James Mohan

         Student[] studentArray = { 
      new Student() { StudentID = 1, StudentName = "John", age = 18 } ,
      new Student() { StudentID = 2, StudentName = "Steve",  age = 21 } ,
      new Student() { StudentID = 3, StudentName = "Bill",  age = 25 } ,
      new Student() { StudentID = 4, StudentName = "Ram" , age = 20 } ,
      new Student() { StudentID = 5, StudentName = "Ron" , age = 31 } ,
      new Student() { StudentID = 6, StudentName = "Chris",  age = 17 } ,
      new Student() { StudentID = 7, StudentName = "Rob",age = 19  } ,
   };
   // var sts = studentArray.Select(st => new  {Id = st.StudentID, StudentName = st.StudentName});
   // foreach(var s in sts)
   //    System.Console.WriteLine(s.StudentName);
   // Use LINQ Method Syntax to find teenager students
   Student[] teenagers = studentArray.Where(s => s.age > 12 && s.age < 18).ToArray(); 

   // LINQ Query Syntax to find out teenager students
   var teenAgerStudent = from s in studentArray
                      where s.age > 12 && s.age < 20
                      select s;



   // Use LINQ to find first student whose name is Bill 
Student bill = studentArray.Where(s => s.StudentName == "Bill").FirstOrDefault() ?? new Student{ StudentID = 1, StudentName = "Bill", age = 18 } ;
   System.Console.WriteLine(bill.StudentName); //Bill

   // Use LINQ to find students whose StudentID is 5
   List<Student> studentsWithID5 = studentArray.Where(s => s.StudentID == 5).ToList();
   foreach(var s in studentsWithID5){
      System.Console.WriteLine(s.StudentID);
   }
   
   // string collection
   IList<string> stringList = new List<string>() { 
      "C# Tutorials",
      "VB.NET Tutorials",
      "Learn C++",
      "MVC Tutorials" ,
      "Java" 
   };
   // LINQ Query Syntax
   var result = from name in stringList where name.Contains("Tutorials") select name;
      // LINQ Method Syntax (.Where is calling extension method)
   var resultMethodSyntax = stringList.Where(s => s.Contains("Tutorials"));
      foreach (var str in result)
   {
      Console.WriteLine(str);
   }
   
   var a = new int[5]{1,2,3,4,5};
   
   var resultAverage = a.Average();
      Console.WriteLine(resultAverage); //3
   var resultMax = a.Max();
   System.Console.WriteLine(resultMax); //5


  


   //Extension method
   Student s1 = new Student() { StudentID = 1, StudentName = "John", age = 18 };
   s1.IncreaseAge(10);
   System.Console.WriteLine(s1.age);

   int[] t1 = new int[3] {1,2,3};
   t1 = t1.Select(i => i*i).ToArray();
   System.Console.WriteLine($"{t1[0]} {t1[1]} {t1[2]}");

   System.Console.WriteLine("The quick brown fox jumps over the lazy dog".Split()[0]);//The

   //1 Find even numbers
   int[] n1 = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      //even numbers
   System.Console.WriteLine(string.Join(" ", n1.Where(i => i%2 == 0)));

   
   //2 Using multiple WHERE clause to find the positive numbers from 1 to 10 within the list 

   int[] n2 = {  1, 3, -2, -4, -7, -3, -8, 12, 19, 6, 9, 10, 14  };  


   System.Console.WriteLine(string.Join(" ", n2.Where(i => i > 0 && i < 11).OrderByDescending(x => x)));

   
   //3 Find the number and its square of an array which is more than 20 : 
   var arr1 = new[] { 3, 9, 2, 8, 6, 5 };
   // Number = 9, SqrNo = 81



   System.Console.WriteLine(string.Join(" ", arr1.Where(i => i*i > 20).Select(i => i*i)));
   string[] resultArray = arr1.Where(i => i * i > 20)
                           .Select(i => ("Number = " + i.ToString() + ", SqrNo = " + (i * i).ToString()))
                           .ToArray(); 
   System.Console.WriteLine(string.Join("\n",resultArray));
   
   var sqNo = from int Number in arr1 //sqNo is anonymous type, like "= new { Name = "John", Age = 30 };" 
               let SqrNo = Number * Number
               where SqrNo > 20
               select new {Number, SqrNo};//sqNo is read-only
   foreach(var n in sqNo)
   System.Console.WriteLine(n);

   var SqNo = arr1.Where(i => i*i > 20).Select(i => new {Number = i, SqrNo = i*i});
   foreach(var sq in SqNo)
      System.Console.WriteLine(sq);
   // System.Console.WriteLine(arr1.Aggregate((s1, s2) => s1 + ", " + s2));

   //4 Display the number and frequency of number from given array:
   System.Console.WriteLine("\n//4\n");
   int[] arr2 = new int[] { 5, 9, 1, 2, 3, 7, 5, 6, 7, 3, 7, 6, 8, 5, 4, 9, 6, 2 };  


   var standardList = arr2.Distinct().ToArray();
var groupJoin = standardList.GroupJoin(arr2,       //inner sequence
                                       sl => sl,  //outer Key (in this case  - just value)
                                       i => i,   //inner Key (in this case  - just value)
                                       (s, i) => new {
                                          s = s,
                                          i = i
                                       });

   // foreach(var n in groupJoin)
   // System.Console.WriteLine("Number " + n.s.ToString() +" appears " + n.i.Count().ToString() + " times");

   //another way - just use group by
   var group = arr2.GroupBy(x => x);
   foreach(var value in group)
      System.Console.WriteLine("Number " + value.Key +" appears " + value.Count() + " times");

   string apple = "apple";
   var group2 = apple.GroupBy(x => x);

   foreach(var ch in group2)
      System.Console.WriteLine($"Character {ch.Key}: {ch.Count()} times");

   //7 Write a program in C#  to display numbers, multiplication of numbers with frequency and the frequency of a number !!!in an array!!!.
   System.Console.WriteLine("\n//7");

   int[] numbers = { 5, 1, 9, 2, 3, 7, 4, 5, 6, 8, 7, 6, 3, 4, 5, 2 };
   



   var res = numbers.GroupBy(n => n).Select(n => new int[] {n.Key, n.Key*n.Count(), n.Count()});

   foreach(var r in res)
      System.Console.WriteLine(r[0] + " " + r[1] + " " + r[2]);

   //8 Find a string that starts and ends with a specific character.
   System.Console.WriteLine("\n//8");

   string[] cities =  { "ROME","LONDON","NAIROBI","CALIFORNIA","ZURICH","NEW DELHI","AMSTERDAM","ABU DHABI", "PARIS"}; 

   

   // var resString = cities.Where(c => c[0].ToString() == "A" && c[c.Length-1].ToString() == "M").FirstOrDefault();
   // var resString = cities.Where(c => c.StartsWith("A") && c.EndsWith("M")).FirstOrDefault();
   var resString = cities.FirstOrDefault(str => str.StartsWith("A") && str.EndsWith("M"));;
   System.Console.WriteLine(resString);


   //11 Display the top 3 records. 
   System.Console.WriteLine("\n//11");
   int[] arr11 = {5, 7, 13, 24, 6, 9, 8, 7};
   
   //top 3 records
   var res11 = arr11.OrderByDescending(x => x).Take(3);
   foreach(var i in res11)
      System.Console.WriteLine(i);

   //12 Find uppercase words in a string.
   System.Console.WriteLine("\n//12");

   string str12 = "this IS a STRING";


   var res12 = str12.Split().ToArray().Where(s => s == s.ToUpper());
   foreach (var s in res12)
      System.Console.WriteLine(s);
   
   //13 Convert a string array to a string.
   System.Console.WriteLine("\n//13");

   string[] arr13 = {"cat", "dog", "rat"};

   System.Console.WriteLine(String.Join(", ", arr13));

   //14 Find the n-th maximum grade point achieved by the students from the list of students.
   System.Console.WriteLine("\n//14");

   List<Student> stulist = new List<Student>
   {
      new Student { StudentID = 1, StudentName = " Joseph ", GrPoint = 800 },
      new Student { StudentID = 2, StudentName = "Alex", GrPoint = 458 },
      new Student { StudentID = 3, StudentName = "Harris", GrPoint = 900 },
      new Student { StudentID = 4, StudentName = "Taylor", GrPoint = 900 },
      new Student { StudentID = 5, StudentName = "Smith", GrPoint = 458 },
      new Student { StudentID = 6, StudentName = "Natasa", GrPoint = 700 },
      new Student { StudentID = 7, StudentName = "David", GrPoint = 750 },
      new Student { StudentID = 8, StudentName = "Harry", GrPoint = 700 },
      new Student { StudentID = 9, StudentName = "Nicolash", GrPoint = 597 },
      new Student { StudentID = 10, StudentName = "Jenny", GrPoint = 750 }
   };  

   
   var res14Group = stulist.OrderByDescending(st => st.GrPoint).GroupBy(st => st.GrPoint).ToArray();
   var thirdGrade = res14Group[2].Key; 
   var res14t = stulist.Where(st => st.GrPoint == thirdGrade).ToArray();

   foreach(var st in res14t)
      System.Console.WriteLine(st.StudentID + " " + st.StudentName + " " + st.GrPoint);

   //first 4 best grades
   var res14 = stulist.OrderByDescending(st => st.GrPoint).Take(4)
      .Select(st => new {Id = st.StudentID, Name = st.StudentName, Grade = st.GrPoint});

   foreach(var st in res14)
      System.Console.WriteLine(st);

   
   //15 Count file extensions and group it using LINQ
   System.Console.WriteLine("\n//15");

   string[] arr15 = { "aaa.frx", "bbb.TXT", "xyz.dbf","abc.pdf", "aaaa.PDF","xyz.frt", "abc.xml", "ccc.txt", "zzz.txt" };



   var res15 = arr15.Select(str => str.Split(".").Last()).GroupBy(ext => ext.ToLower());
   foreach(var ext in res15)
      System.Console.WriteLine($"{ext.Count()} File(s) with .{ext.Key} Extension");

   // Remove string at index 5, "m" and all "o"
   List<string> listOfString = new List<string>
   {
      "m",
      "m",
      "n",
      "o",
      "o",
      "p",
      "q"
   };  


   listOfString.RemoveAt(5); // p
   listOfString.Remove("m"); //one m
   listOfString.RemoveAll(ch => ch == "o"); //all o

   listOfString.ForEach(ch => Console.WriteLine(ch)); // m n q

   listOfString = new List<string>
   {
      "m",
      "n",
      "o",
      "p",
      "q"
   };  
   System.Console.WriteLine();
   listOfString.RemoveRange(1,2);
   listOfString.ForEach(ch => System.Console.WriteLine(ch)); // m p q

   //22 Find the strings for a specific minimum length

   string[] arr22 = new string[]{"this","is","a","specific", "minimum", "length"};
   //minimum length - 5
   var res22 = arr22.Where(str => str.Length >= 5).ToArray();
   Array.ForEach(res22, str => System.Console.WriteLine(str)); //specific minimum length

   //23 Generate a cartesian product of two sets.
   System.Console.WriteLine("\n//23\n");

   char[] charset1 = { 'X', 'Y', 'Z' };
	int[] numset1 = { 1, 2, 3, 4 };



   var res23 = charset1.SelectMany(ch => numset1.Select(num => new {letterList = ch, numberList = num}));
   foreach(var record in res23)
      System.Console.WriteLine(record);

   //Flattening nested List
   List<List<string>> listOfLists = new List<List<string>>
      {
            new List<string> { "apple", "banana", "cherry" },
            new List<string> { "dog", "cat", "elephant" },
            new List<string> { "red", "green", "blue" }
      };

   
   
   // listOfLists.Aggregate((l1, l2) => l1.Union(l2).ToList()).ToList().ForEach(r => System.Console.WriteLine(r));
   var resLOL = listOfLists.SelectMany(list => list);
   System.Console.WriteLine(String.Join(" ", resLOL));

   //Cross Joining two sequences 
   string[] colors = { "red", "green", "blue" };
   string[] sizes = { "small", "medium", "large" };

   var colorSizes = colors.SelectMany(c => sizes.Select(s => c + " " + s)).ToList();
   colorSizes.ForEach(cs => System.Console.WriteLine(cs));

   //Select with index
   string[] names2 = { "Alice", "Bob", "Charlie", "David" };



   var indexedNames = names2.Select((name, index) => name + index);
   foreach (var entry in indexedNames)
      {
         Console.WriteLine(entry);
      }
   // indexedNames.ForEach(n => System.Console.WriteLine(n));

   //24 Generate a cartesian product of three sets.

   char[] charset2 = { 'X', 'Y', 'Z' };
   int[] numset2 = { 1, 2, 3 };
   string[] colorset2 = { "Green", "Orange" };


   var tripleCrossJoin = charset2.SelectMany(ch => numset2.SelectMany(n => colorset2.Select(c => new {letter = ch, number = n, color = c})));
   foreach(var record in tripleCrossJoin)
      System.Console.WriteLine(record);

   //25 Generate an Inner Join between two data sets.
   System.Console.WriteLine("\n//25\n");
   List<Item_mast> itemlist = new List<Item_mast>{  
      new Item_mast { ItemId = 1, ItemDes = "Biscuit" }, 
      new Item_mast { ItemId = 2, ItemDes = "Chocolate" }, 
      new Item_mast { ItemId = 3, ItemDes = "Butter" },  
      new Item_mast { ItemId = 4, ItemDes = "Brade" },  
      new Item_mast { ItemId = 5, ItemDes = "Honey" }  
   }; 
		  
   List<Purchase> purchlist = new List<Purchase>{  
      new Purchase { InvNo=100, ItemId = 3,  PurQty = 800 }, 
      new Purchase { InvNo=101, ItemId = 2,  PurQty = 650 }, 
      new Purchase { InvNo=102, ItemId = 3,  PurQty = 900 },  
      new Purchase { InvNo=103, ItemId = 4,  PurQty = 700 },
      new Purchase { InvNo=104, ItemId = 3,  PurQty = 900 },  
      new Purchase { InvNo=105, ItemId = 4,  PurQty = 650 },  		   
      new Purchase { InvNo=106, ItemId = 1,  PurQty = 458 }  
   }; 


   var join25 = itemlist.Join(purchlist,
      i => i.ItemId,
      p => p.ItemId,
      (i, p) => new {
         ItemId = i.ItemId,
         Name = i.ItemDes,
         Quantity = p.PurQty
      }).ToList();

   join25.ForEach(r => System.Console.WriteLine(r));

   //26  Generate a Left Join between two data sets.
      System.Console.WriteLine("\n//26\n");

   List<Item_mast> itemlist26 = new List<Item_mast>{  
      new Item_mast { ItemId = 1, ItemDes = "Biscuit  " }, 
      new Item_mast { ItemId = 2, ItemDes = "Chocolate" }, 
      new Item_mast { ItemId = 3, ItemDes = "Butter   " },  
      new Item_mast { ItemId = 4, ItemDes = "Brade    " },  
      new Item_mast { ItemId = 5, ItemDes = "Honey    " }  
   };

   List<Purchase> purchlist26 = new List<Purchase>{
      new Purchase { InvNo=100, ItemId = 3,  PurQty = 800 },
      new Purchase { InvNo=101, ItemId = 2,  PurQty = 650 },
      new Purchase { InvNo=102, ItemId = 3,  PurQty = 900 },
      new Purchase { InvNo=103, ItemId = 4,  PurQty = 700 },
      new Purchase { InvNo=104, ItemId = 3,  PurQty = 900 },
      new Purchase { InvNo=105, ItemId = 4,  PurQty = 650 },
      new Purchase { InvNo=106, ItemId = 1,  PurQty = 458 }
   };




   var leftJoin2 = itemlist26.GroupJoin(purchlist26,
      i => i.ItemId,
      p => p.ItemId,
      (i, purchases) => new {Item = i, Purchases = purchases})
      .SelectMany(x => x.Purchases.DefaultIfEmpty(new Purchase()),
      (x, p) => new 
      {
         Id = x.Item.ItemId,
         Name = x.Item.ItemDes,
         Quantity = p.PurQty
         }).ToList();


   leftJoin2.ForEach(r => System.Console.WriteLine(r));




   MyStruct ms = new MyStruct();

   var list132 = new List<Object>(){"ok", 2, new Purchase(), ms};
   list132.ForEach(r => System.Console.WriteLine(r));
   System.Console.WriteLine(list132[0].GetType()); //System.String
   System.Console.WriteLine(list132[0].GetHashCode()); //224025732
   
   System.Console.WriteLine(list132[1].GetType()); //System.Int32
   System.Console.WriteLine(list132[1].GetHashCode()); //2

   System.Console.WriteLine(list132[2].GetType()); //Purchase
   System.Console.WriteLine(list132[2].GetHashCode()); //54267293

   System.Console.WriteLine(list132[3].GetType()); //MyStruct
   System.Console.WriteLine(list132[3].GetHashCode()); //807938613

   //27 Generate a Right Join between two data sets
      System.Console.WriteLine("\n//27\n");

   List<Item_mast> Items = new List<Item_mast>{
      new Item_mast { ItemId = 1, ItemDes = "Biscuit  " },
      new Item_mast { ItemId = 2, ItemDes = "Chocolate" },
      new Item_mast { ItemId = 3, ItemDes = "Butter   " },
      new Item_mast { ItemId = 4, ItemDes = "Brade    " },
      new Item_mast { ItemId = 5, ItemDes = "Honey    " }
   };

   List<Purchase> Purchases = new List<Purchase>{
      new Purchase { InvNo=100, ItemId = 3,  PurQty = 800 },
      new Purchase { InvNo=101, ItemId = 5,  PurQty = 650 },
      new Purchase { InvNo=102, ItemId = 3,  PurQty = 900 },
      new Purchase { InvNo=103, ItemId = 4,  PurQty = 700 },
      new Purchase { InvNo=104, ItemId = 3,  PurQty = 900 },
      new Purchase { InvNo=105, ItemId = 4,  PurQty = 650 },
      new Purchase { InvNo=106, ItemId = 1,  PurQty = 458 },
      new Purchase { InvNo=107, ItemId = 6,  PurQty = 458 }
   };

   //Only purchases that has relative Item_mast in Items 
   var res27 = Purchases.Join(Items,
      p => p.ItemId,
      i => i.ItemId,
      (p, i) => new {
         Id = p.ItemId,
         Name = i.ItemDes,
         Quantity = p.PurQty
      }).ToList();

   res27.ForEach(r => System.Console.WriteLine(r));

   System.Console.WriteLine("\nWithout item:\n");
   //All purchases even if purchase doesn't have relative Item_mast in Items 
   var res27_2 = Purchases.GroupJoin(Items,
      p => p.ItemId,
      i => i.ItemId,
      (p, i) => new {
         Purchase = p,
         Items = i
      })
      .SelectMany(x => x.Items.DefaultIfEmpty(),
      (x, item) => new {
         Id = item?.ItemId,
         Name = item?.ItemDes,
         Quantity = x.Purchase.PurQty
      }).ToList();

   res27_2.ForEach(r => System.Console.WriteLine(r));

   //28 Display the list of items in the array according to the length of the string then by name in ascending order.
   System.Console.WriteLine("\n//28\n");

   string[] cities28 =  
            {  
                "ROME","LONDON","NAIROBI","CALIFORNIA","ZURICH","NEW DELHI","MASTERDAM","AMSTERDAM","ABU DHABI", "PARIS"  
            }; 

   // var res28 = cities28.OrderBy(str => (str.Length, str)).ToList();
   var res28 = cities28.OrderBy(str => str.Length).ThenBy(str => str).ToList();
   res28.ForEach(r => System.Console.WriteLine(r));

   //29 Split a collection of strings into some groups (every line = new group).
   System.Console.WriteLine("\n//29\n");

   string[] cities29 =  
   {  
         "ROME","LONDON","NAIROBI",
         "CALIFORNIA", "ZURICH","NEW DELHI",
         "AMSTERDAM", "ABU DHABI", "PARIS",
         "NEW YORK"  
   };

   var res29 = Enumerable.Range(0, cities29.Length)
   .GroupBy(i => i / 3, i => cities29[i])
   .Select(g => g.ToList()).ToList();

   var l29 = new List<List<string>>();
   for(int i = 0;i < cities29.Length;i++){
      if(i%3 == 0)
         l29.Add(new List<string>());
      l29[i/3].Add(cities29[i]);
   }

   l29.ForEach(r => System.Console.WriteLine(String.Join(", ", r)));

   //30 Arrange the distinct elements in the list in ascending order.
   System.Console.WriteLine("\n//30\n");
   List<Item_mast> items30 = new List<Item_mast>();
   items30.Add(new Item_mast() { ItemId = 1, ItemDes = "Biscuit  " });
   items30.Add(new Item_mast() { ItemId = 2, ItemDes = "Honey    " });
   items30.Add(new Item_mast() { ItemId = 3, ItemDes = "Butter   " });
   items30.Add(new Item_mast() { ItemId = 4, ItemDes = "Brade    " });
   items30.Add(new Item_mast() { ItemId = 2, ItemDes = "Honey    " });
   items30.Add(new Item_mast() { ItemId = 1, ItemDes = "Biscuit  " });

   



   items30.OrderBy(i => i.ItemDes)
      .Distinct(new Item_mast_comparer())
      .ToList()
      .ForEach(r => System.Console.WriteLine(r.ItemId + " " + r.ItemDes));

   // Lock Object
   LockClass lc = new LockClass();//value = 1
   await Task.Run( () => {
      Parallel.For(1, 20, (i) => {
         lc.MultiplyValue(2);
         // System.Console.Out.WriteLineAsync(i + " " + Thread.CurrentThread.ManagedThreadId);
      });
   });
   System.Console.WriteLine(lc.Value());

   lc.Value(1);

   await Task.Run( () => {
      Parallel.For(1, 20, (i) => {
         lc.MultiplyValueLock(2);
         // System.Console.Out.WriteLineAsync(i + " " + Thread.CurrentThread.ManagedThreadId);
      });
   });
   System.Console.WriteLine(lc.Value());

   Test t11 = new Test();
   t11.Age = 10;
   
   }
}
public interface ITest{
   protected int Age {get; set;}

}
public class Test : ITest
{
   public int Age { get; set; }
}

//extension class
public static class ExtensionMethodClass{
   //extension method
   public static Student IncreaseAge(this Student s, int increase){
      s.age += increase;
      return s;
   }
}

public class Item_mast
{
    public int ItemId { get; set; }
    public string ItemDes { get; set; }
}
public class Item_mast_comparer : IEqualityComparer<Item_mast>
{
   public bool Equals(Item_mast? x, Item_mast? y)
   {
      if (x?.ItemId == y?.ItemId && x?.ItemDes == y?.ItemDes)
      {
         return true;
      }
      return false;
   }

   public int GetHashCode([DisallowNull] Item_mast obj)
   {
      return obj.ItemId.GetHashCode()+obj.ItemDes.GetHashCode();
   }
}
public class Purchase
{
    public int InvNo { get; set; }
    public int ItemId { get; set; }
    public int PurQty { get; set; }
}

public class LockClass{
   private Object lockObject;
   private int _value;
   public void Value(int v){
      _value = v;
   }

   public void MultiplyValue(int v){
      _value = _value*v;
   }
   public void MultiplyValueLock(int v){
      lock (lockObject){
      _value = _value*v;
      }
   }
   public int Value(){
      return _value;
   }
   
   public LockClass()
   {
      lockObject = new Object();
      _value = 1;
   }
}
