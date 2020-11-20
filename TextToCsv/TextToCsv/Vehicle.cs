namespace TextToCsv
{
    public abstract class Vehicle
    {
        protected Vehicle(string name)
        {
            Name = name;
        }

        public Color Color { get; set; }

        public int TopSpeedKm { get; set; }

        public string Name { get; }

        public abstract void Transport();

        public int CalculateDurationInHours(int distanceKm)
        {
            return distanceKm / TopSpeedKm;
        }
    }
}
