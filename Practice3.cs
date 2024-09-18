using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using Note1;


public class Practice3{
	public string TestProperty { get; set; }

	// public void DO(){
	// _testProperty = "ok";
	// }
public static void Mainw(string[] args){
	var arr1 = new[] { 3, 9, 2, 8, 6, 5 };
	arr1.Select(n => true).ToList().ForEach(n => Console.WriteLine(n));

	//Leetcode 1768 Merge Strings Alternately
	string word1 = "abc";
	string word2 = "pqrmm";

	StringBuilder result = new StringBuilder();

	int smallest = word1.Length > word2.Length ? word2.Length : word1.Length;
	for(int i = 0; i < smallest; i++)
	{
		result.Append(word1[i]);
		result.Append(word2[i]);
	}

   if (word1.Length > word2.Length)
	{
		result.Append(word1.Substring(smallest));
	}
	else
	{
		result.Append(word2.Substring(smallest));
	}
	Console.WriteLine(result);

		//1071. Greatest Common Divisor of 2 Strings
		//      Example 1:
		//Input: str1 = "ABCABC", str2 = "ABC"
		//Output: "ABC"

		//Example 2:
		//Input: str1 = "ABABAB", str2 = "ABAB"
		//Output: "AB"

		//Example 3:
		//Input: str1 = "LEET", str2 = "CODE"
		//Output: ""

		string str1 = "ABAB";
		string str2 = "ABABAB";

		if(str1.Length > str2.Length)
		{
			string strTemp = str2;
			str2 = str1;
			str1 = strTemp;
		}

		string largestString = "", substr = "";

		for(int i =0; i < str1.Length; i++)
		{
			for(int j = 1; j <= str1.Length-i; j++)
			{
				substr = str1.Substring(i, j);

            if (str2.Replace(substr, "") == "" && str1.Replace(substr, "") == "" && substr.Length > largestString.Length)
				{
					largestString = substr;
				}
         }
		}

        Console.WriteLine(largestString);


   //   public class Solution
   //{
   //   public int gcd(int n1, int n2)
   //   {
   //      if (n2 == 0)
   //      {
   //         return n1;
   //      }
   //      else
   //      {
   //         return gcd(n2, n1 % n2);
   //      }
   //   }
   //   public string GcdOfStrings(string str1, string str2)
   //   {
   //      int len1 = str1.Length;
   //      int len2 = str2.Length;
   //      if ((str1 + str2).Equals(str2 + str1))
   //      {
   //         int index = gcd(len1, len2);
   //         return str1.Substring(0, index);
   //      }
   //      else
   //      {
   //         return "";
   //      }
   //   }
   //}

}
}