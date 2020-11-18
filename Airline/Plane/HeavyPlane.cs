namespace PlaneNamespace
{
    public class HeavyPlane : Plane
    {
        public HeavyPlane(string name, uint capacityForPeople, uint carryingCapacity, uint rangeOfFlight, uint fuelConsumption)
        {
            Name = name;
            CapacityForPeople = capacityForPeople;
            CarryingCapacity = carryingCapacity;
            RangeOfFlight = rangeOfFlight;
            FuelConsumption = fuelConsumption;
        }
    }
}
