using System;
using PlaneNamespace;

namespace WorkShopNamespace
{
    public class HeavyWorkShop : WorkShop
    {
        public HeavyWorkShop(uint number) : base(number) { }
        public override Plane CreatePlane(string name, uint capacityForPeople, uint carryingCapacity, uint rangeOfFlight, uint fuelConsumption)
        {
            return new HeavyPlane(name, capacityForPeople, carryingCapacity, rangeOfFlight, fuelConsumption);
        }
    }
}
