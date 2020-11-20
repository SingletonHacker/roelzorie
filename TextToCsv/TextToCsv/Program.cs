using System;
using System.Collections.Generic;

namespace TextToCsv
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var vehicles = new List<Vehicle>();

            vehicles.Add(new Car("Ferri", Color.Blue, 50));
            vehicles.Add(new Car("Daewoo", Color.Red, 100));
            vehicles.Add(new Boat("Supertankert", Color.Purple, 510, 32));

            var winner = Race(vehicles);

            Console.WriteLine($"{winner.Name} has won the race");

            Console.WriteLine("All participants are transporting people now");
            vehicles.ForEach(v => v.Transport());
        }

        private static Vehicle Race(List<Vehicle> vehicles)
        {
            Vehicle fastestVehicle = vehicles[0];

            for (var i = 1; i < vehicles.Count; i++)
            {
                if (fastestVehicle.TopSpeedKm < vehicles[i].TopSpeedKm)
                {
                    fastestVehicle = vehicles[i];
                }
            }

            return fastestVehicle;
        }
    }
}
