using System;

namespace TextToCsv
{
    public class Boat : Vehicle
    {
        public Boat(string name, Color color, int topSpeed, int minDepth)
            : base(name)
        {
            Color = color;
            TopSpeedKm = topSpeed;
            MinimumRequiredWaterDepth = minDepth;
        }

        public int MinimumRequiredWaterDepth { get; }

        public override void Transport()
        {
            Console.WriteLine($"I am a {Color} boat, sailing {TopSpeedKm} km/h on the sea bitches");
        }
    }
}
