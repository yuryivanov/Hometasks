namespace PlaneNamespace
{
    public abstract class Plane
    {
        public string Name { get; set; }
        public uint CapacityForPeople { get; set; }
        public uint CarryingCapacity { get; set; }
        public uint RangeOfFlight { get; set; }
        public uint FuelConsumption { get; set; }
    }
}
