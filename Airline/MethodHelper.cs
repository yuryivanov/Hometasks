using System;
using System.Collections.Generic;
using PlaneNamespace;
using System.Linq;

namespace ProjectNamespace
{
    class MethodHelper : ICountable
    {
        public uint GetGeneralCapacityForPeople(List<Plane> planes)
        {
            try
            {
                uint _generalCapacityForPeople = 0;
                foreach (var item in planes)
                {

                    _generalCapacityForPeople += Convert.ToUInt32(item.CapacityForPeople);
                }
                return _generalCapacityForPeople;
            }
            catch (Exception)
            {
                throw new Exception("GeneralCapacityForPeople reached max limit");
            }
        }
        public uint GetGeneralCarryingCapacity(List<Plane> planes)
        {
            try
            {
                uint _generalCarryingCapacity = 0;
                foreach (var item in planes)
                {
                    _generalCarryingCapacity += Convert.ToUInt32(item.CarryingCapacity);
                }
                return _generalCarryingCapacity;
            }
            catch (Exception)
            {
                throw new Exception("GeneralCarryingCapacity reached max limit");
            }
        }
        public void SearchPlaneByFuelConsumption(List<Plane> planes, uint startRangeOfFuelConsumption, uint endRangeOfFuelConsumption)
        {
            var SortedList = new List<Plane>();
            foreach (var item in planes)
            {
                if (item.FuelConsumption >= startRangeOfFuelConsumption & item.FuelConsumption <= endRangeOfFuelConsumption)
                {
                    SortedList.Add(item);
                }
            }
            foreach (var item in SortedList)
            {
                Console.WriteLine($"Plane {item.Name} - CarryingCapacity: {item.CarryingCapacity} tons, RangeOfFlight: {item.RangeOfFlight} km, " +
                    $"CapacityForPeople: {item.CapacityForPeople} people, FuelConsumption: {item.FuelConsumption} kg/h;");
            }
        }
        public void SortByRangeOfFlight(List<Plane> planes)
        {
            var SortedList = planes.OrderByDescending(o => o.RangeOfFlight).ToList();
            foreach (var item in SortedList)
            {
                Console.WriteLine($"Plane {item.Name} - CarryingCapacity: {item.CarryingCapacity} tons, RangeOfFlight: {item.RangeOfFlight} km, " +
                    $"CapacityForPeople: {item.CapacityForPeople} people, FuelConsumption: {item.FuelConsumption} kg/h;");
            }
        }
    }
}
