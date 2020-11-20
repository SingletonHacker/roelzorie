using System;

namespace TextToCsv
{
    public class Car : Vehicle
    {
        public Car(string name, Color color, int topSpeed)
            : base(name)
        {
            Color = color;
            TopSpeedKm = topSpeed;
        }

        public override void Transport()
        {
            Console.WriteLine($"I am a {Color} car, driving {TopSpeedKm} km/h on the highway");
        }
    }
}
