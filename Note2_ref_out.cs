using System;


namespace Note2_ref_out;


public class Program
{

    public static void IncreaseNumber(int a)
    {
        a += 10;
    }
    public static void IncreaseNumber2(ref int a)
    {
        a += 10;
    }
    public static void IncreaseNumber3(out int a)
    { //this function takes only pointer without value
        a = 0; // must be assigned before use
        a += 10;
    }

    public static int IncreaseNumber4(out int a, out int b)
    {

        a = 0;
        //a += 10 + b; //not allowed
        b = 0;
        a += 10;
        b += 11;
        return 1;
    }

    public static void Mainw(string[] args)
    {
        //ref and out are used to pass the pointer of the variable to the function and work with exactly with that pointer
        // IncreaseNumber(int a)
        // IncreaseNumber2(ref int a)
        // IncreaseNumber3(out int a)

        int number = 20;
        IncreaseNumber(20);
        Console.WriteLine(number); //20

        // IncreaseNumber(ref number); // error

        IncreaseNumber2(ref number);
        Console.WriteLine(number); //30

        // IncreaseNumber2(number); // error

        IncreaseNumber3(out number);
        Console.WriteLine(number); //10

        // IncreaseNumber3(number); // error

        int b;
        IncreaseNumber3(out b); //b can be unassigned
        Console.WriteLine(b); //10

        int c1, c2;
        Console.WriteLine(IncreaseNumber4(out c1, out c2)); //1
        Console.WriteLine(c1); //10
        Console.WriteLine(c2); //11


        string str1 = "C:\\folder";
        string str2 = "file";
        Console.WriteLine(Path.Combine(str1, str2)); //C:\folder\file

    }
}


