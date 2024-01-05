using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions; //Regex

namespace RegexNamespace;
// https://www.csharptutorial.net/csharp-regular-expression/

public class Program{
public static void Main(){
   
   //Regex
   //using System.Text.RegularExpressions;

   // - IsMatch(string input)	Returns true if the pattern matches the input string.
   // - Match(string input)	Returns the first occurrence of the pattern in the input string.
   // - Matches(string input)	Returns all occurrences of the pattern in the input string.
   // - Replace(string input, string replacement)	Replaces all occurrences of the pattern in the input string
      // with the replacement string.
   // - Split(String)	Splits an input string into an array of substrings at the positions defined by a
      // regular expression pattern.

   Regex regex0 = new Regex(@"hello"); // @ is used to ignore the escape characters like \n \t
   string inputString = "hello world! hello";
   Match match = regex0.Match(inputString);

   if (match.Success){
      Console.WriteLine("Match found at index {0} with length {1}", match.Index, match.Length);
      //Match found at index 0 with length 5
      Console.WriteLine(match.Value + " " + match.Name + " " + match.NextMatch());// hello 0 hello
   }else{
      Console.WriteLine("No match found.");
   }

   MatchCollection matches = regex0.Matches("hello world! hello again!");

   foreach (Match match2 in matches){
      Console.WriteLine("Match found at index {0} with length {1}", match2.Index, match2.Length);
   }

   string input = @"INSERT INTO  pracownicy VALUES (1234, 'Michalski', 'Adam', 'Prezes', NULL,
  To_date('1950/07/11','yyyy/mm/dd'), To_date('1981/08/15','yyyy/mm/dd'), NULL, 12000, 5500, 20, 200, 10);
INSERT INTO  pracownicy VALUES (1250, 'Nowakowska', 'Anna', 'Czlonek zarzadu', 1234,
  To_date('1973/12/15','yyyy/mm/dd'), To_date('2001/01/01','yyyy/mm/dd'), NULL, 10000, 3500, 17, 150, 10);";


   Regex regex2 = new Regex(@"To_date\('(?<year>\d{4})\/(?<month>\d{2})\/(?<day>\d{2})','yyyy\/mm\/dd'\)");
   string result = regex2.Replace(input, match =>
   {
      return $"'{match.Groups["year"]}-{match.Groups["month"]}-{match.Groups["day"]}'";
   });
   
   Console.WriteLine(result);

   //Practice
    // 1
   Console.WriteLine("\n--- 1 ---");
   //check whether a given string is a valid Hex code or not
   //A valid Hex code must start with # and it must contain six characters

   // ("#CD5C5C") -> True
   // ("#f08080") -> True
   // ("#E9967A") -> True
   // ("#EFFA07A") -> False




   Regex regex1 = new Regex(@"^#.{6}$") ;
   string s1 = "#CD5C5C";
   string s2 = "#f08080";
   string s3 = "#E9967A";
   string s4 = "#EFFA07A";

   Console.WriteLine(regex1.IsMatch(s1));    //T
   Console.WriteLine(regex1.IsMatch(s2));    //T
   Console.WriteLine(regex1.IsMatch(s3));    //T
   Console.WriteLine(regex1.IsMatch(s4));    //F

   // -----------------------
   string[] str = new string[] {"ok", "one", "four123d"};
   Console.WriteLine(str.Average(s => s.Length)); //4.333333333333333
   
   Regex regex = new Regex(@"\d{3}");
   MatchCollection mc = regex.Matches("123 452.");

   mc.ToList().ForEach(x => Console.WriteLine(x.Value)); //123
   // -----------------------

   //-------------------------------------------
   Console.WriteLine("\n--- 2 ---");
   //2
   //calculate the average word length in a given string. Round the average length up to two decimal places

   // ("CPP Exercises." -> 6
   // ("C# syntax is highly expressive, yet it is also simple and easy to learn.") -> 4
   // ("C# is an elegant and type-safe object-oriented language") -> 6.57


   Console.WriteLine(GetAverageWordLength("023 123"));
   Console.WriteLine(GetAverageWordLength("CPP Exercises.")); //6
   Console.WriteLine(GetAverageWordLength("C# syntax is highly expressive, " +
    "yet it is also simple and easy to learn.")); //4
   Console.WriteLine(GetAverageWordLength("C# is an elegant and type-safe object-oriented language"));//6.57

   //-------------------------------------------
   Console.WriteLine("\n--- 3 ---");
   //3 check whether a given string of characters can be transformed into a palindrome.
   // Return true otherwise false.
   // ("amamd") -> True
   // ("pamamd") -> False
   // ("ferre") -> True
   
   Console.WriteLine(CanTransformToPalindrome("amamd"));    //T
   Console.WriteLine(CanTransformToPalindrome("pamamd"));   //F
   Console.WriteLine(CanTransformToPalindrome("ferre"));    //T
   Console.WriteLine(CanTransformToPalindrome("dodoo"));    //T
   Console.WriteLine(CanTransformToPalindrome("mammam"));   //T   

   Console.WriteLine();

   //-------------------------------------------
   Console.WriteLine("\n--- 4 ---");
   /* validate a password of length 7 to 16 characters with the following guidelines:
   • Length between 7 and 16 characters.
   • At least one lowercase letter (a-z).
   • At least one uppercase letter (A-Z).
   • At least one digit (0-9).
   • Supported special characters: ! @ # $ % ^ & * ( ) + = _ - { } [ ] : ; " ' ? < > , .
   */
   
   Console.WriteLine(IsPasswordValid("Suuu$21g@"));            //T
   Console.WriteLine(IsPasswordValid("Suuu$21g@~"));           //F
   Console.WriteLine(IsPasswordValid("W#1g@"));                //F
   Console.WriteLine(IsPasswordValid("sdsd723#$Amid"));        //T
   Console.WriteLine(IsPasswordValid("sdsd723#$Amidkiouy"));   //F
   Console.WriteLine(IsPasswordValid("a&&g@"));                //F

   //-------------------------------------------
   Console.WriteLine("\n--- 5 ---");
   //check for repeated occurrences of words in a given string
   /*
   ("C# C# syntax is highly expressive, yet it is is also simple and easy to to learn learn.") -> 3 matches found
   ("Red Green Green Black Black Green.") -> 2 matches found
   */

   Console.WriteLine(CountRepeatedWords(
      "C# C# syntax is highly expressive, yet it is is is also simple and easy to to learn learn.")); //3
   Console.WriteLine(CountRepeatedWords("Red Green Green Black Black Green.")); //2

   //-------------------------------------------
   Console.WriteLine("\n--- 6 ---");
   //
   
//    string input2 = @"P1105321901262011231!32300600251080
// P1105321901262111231!32300950246140
// P1105321901262211231!32300950246140
// P1105321901262311231!32300600251080
// P1105321901262411231!32300600251080";
//    var lines = input2.Split("\n");
 
//    Array.ForEach(lines, l => Console.WriteLine(l));
//    Console.WriteLine(lines.All(line => Regex.IsMatch(line, @"^P\d{19}!\d{14}$"))); 

   string input2 = @"P1105321901262011231!32300600251080
P1105321901262111231!32300950246140
P1105321901262211231!32300950246140
P1105321901262311231!32300600251080
P1105321901262411231!32300600251080";
var lines = input2.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

Array.ForEach(lines, l => Console.WriteLine(l));
Console.WriteLine(lines.All(line => Regex.IsMatch(line, @"^P\d{19}!\d{14}$")));

}
//2 calculate the average word length in a given string. Round the average length up to two decimal places
public static double GetAverageWordLength(string input){

   // Regex regex = new Regex(@"\w+");
   Regex regex = new Regex(@"[a-zA-Z-]{2,}");
   MatchCollection mc = regex.Matches(input);

   if (mc != null && mc.Count() > 0)
      return Math.Round(mc.Average(word => word.Length), 2);
   return 0;
}

//3 check whether a given string of characters can be transformed into a palindrome.
   // Return true otherwise false.
public static bool CanTransformToPalindrome(string inp){
   return Regex.Replace(string.Concat(inp.OrderBy(x => x)),@"([a-z])\1{1}",string.Empty).Length <= 1;
}

/* 4 validate a password of length 7 to 16 characters with the following guidelines:
   • Length between 7 and 16 characters.
   • At least one lowercase letter (a-z).
   • At least one uppercase letter (A-Z).
   • At least one digit (0-9).
   • Supported special characters: ! @ # $ % ^ & * ( ) + = _ - { } [ ] : ; " ' ? < > , .
   */
public static bool IsPasswordValid(string inp){
   if(inp.Length < 7 || inp.Length > 16)
      return false;

   if(!Regex.IsMatch(inp, @"[a-z]"))
      return false;

   if(!Regex.IsMatch(inp, @"[A-Z]"))
      return false;

   if(!Regex.IsMatch(inp, @"\d"))
      return false;

   if(Regex.IsMatch(inp, "[^a-zA-Z0-9!@#$%^&*()+=_-{}\\[\\]:;\"'?<>,]"))
      return false;

   return true;
}

//check for repeated occurrences of words in a given string
/*
   ("C# C# syntax is highly expressive, yet it is is also simple and easy to to learn learn.") -> 3 matches found
   ("Red Green Green Black Black Green.") -> 2 matches found
*/
public static int CountRepeatedWords(string inp){
   
   ;
   Console.WriteLine(String.Join(" ",
   Regex.Replace(inp, @"[.,!?]", "")
      .Split()
      .OrderBy(x => x)) + " ");
   return Regex.Matches(String.Join(" ",
      Regex.Replace(inp, @"[.,!?]", "")
         .Split()
         .OrderBy(x => x)),
      @"(\w{2,}\s)\1").Distinct(new MatchComparer()).Count();
}

    public class MatchComparer : IEqualityComparer<Match>
    {
        public bool Equals(Match? x, Match? y)
        {
            if(x.Value == y.Value)
               return true;

            return false;
        }

        public int GetHashCode([DisallowNull] Match obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
