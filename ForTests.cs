using System;
using System.Collections.Generic;

namespace MyNamespace
{
    public class TravelRoute
    {
        public string StartCity { get; set; }
        public string[] Waypoints { get; set; }

        // public TravelRoute(string startCity = null, string waypoints)
        // {
        //     StartCity = startCity;
        //     Waypoints = waypoints;
        // }

        public void PrintRoute()
        {
            Console.WriteLine("Start City: " + StartCity);
            Console.WriteLine("Waypoints:");
            foreach (var waypoint in Waypoints)
            {
                Console.WriteLine(waypoint);
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a TravelRoute object with waypoints but no start city
            // TravelRoute route = new TravelRoute(new List<string>() { "1", "2", "3" });

            // Print the route information
            // route.PrintRoute();
        DateTime b = new DateTime(2024, 1, 1);
        Console.WriteLine(b.Date);
      //  Console.WriteLine(typeof(DateTime.Now.AddDays(1)));


        }
    }
}