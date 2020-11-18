using System;
using AirlineNamespace;
using PlaneNamespace;

namespace WorkShopNamespace
{
    public abstract class WorkShop : Airline
    {
        public uint Number { get; set; }
        public WorkShop(uint number) => Number = number;
        public abstract Plane CreatePlane(string name, uint capacityForPeople, uint carryingCapacity, uint rangeOfFlight, uint fuelConsumption);
    }
}
