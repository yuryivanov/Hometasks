using System;
using PlaneNamespace;

namespace WorkShopNamespace
{
    public class LightWorkShop : WorkShop
    {
        public LightWorkShop(uint number) : base(number) { }
        public override Plane CreatePlane(string name, uint capacityForPeople, uint carryingCapacity, uint rangeOfFlight, uint fuelConsumption)
        {
            return new LightPlane(name, capacityForPeople, carryingCapacity, rangeOfFlight, fuelConsumption);
        }
    }
}
