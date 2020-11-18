namespace PlaneNamespace
{
    public class LightPlane : Plane
    {
        public LightPlane(string name, uint capacityForPeople, uint carryingCapacity, uint rangeOfFlight, uint fuelConsumption)
        {
            Name = name;
            CapacityForPeople = capacityForPeople;
            CarryingCapacity = carryingCapacity;
            RangeOfFlight = rangeOfFlight;
            FuelConsumption = fuelConsumption;
        }
    }
}
