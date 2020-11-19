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
                uint generalCapacityForPeople = 0;
                foreach (var item in planes)
                {
                    generalCapacityForPeople += Convert.ToUInt32(item.CapacityForPeople);
                }
                return generalCapacityForPeople;
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
                uint generalCarryingCapacity = 0;
                foreach (var item in planes)
                {
                    generalCarryingCapacity += Convert.ToUInt32(item.CarryingCapacity);
                }
                return generalCarryingCapacity;
            }
            catch (Exception)
            {
                throw new Exception("GeneralCarryingCapacity reached max limit");
            }
        }
        public void SearchPlaneByFuelConsumption(List<Plane> planes, uint startRangeOfFuelConsumption, uint endRangeOfFuelConsumption)
        {
            var sortedList = new List<Plane>();
            foreach (var item in planes)
            {
                if (item.FuelConsumption >= startRangeOfFuelConsumption & item.FuelConsumption <= endRangeOfFuelConsumption)
                {
                    sortedList.Add(item);
                }
            }
            foreach (var item in sortedList)
            {
                Console.WriteLine($"Plane {item.Name} - CarryingCapacity: {item.CarryingCapacity} tons, RangeOfFlight: {item.RangeOfFlight} km, " +
                    $"CapacityForPeople: {item.CapacityForPeople} people, FuelConsumption: {item.FuelConsumption} kg/h;");
            }
        }
        public void SortByRangeOfFlight(List<Plane> planes)
        {
            var sortedList = planes.OrderByDescending(o => o.RangeOfFlight).ToList();
            foreach (var item in sortedList)
            {
                Console.WriteLine($"Plane {item.Name} - CarryingCapacity: {item.CarryingCapacity} tons, RangeOfFlight: {item.RangeOfFlight} km, " +
                    $"CapacityForPeople: {item.CapacityForPeople} people, FuelConsumption: {item.FuelConsumption} kg/h;");
            }
        }
    }
}
