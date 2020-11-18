using PlaneNamespace;
using System.Collections.Generic;

namespace ProjectNamespace
{
    public interface ICountable
    {
        uint GetGeneralCapacityForPeople(List<Plane> planes);
        uint GetGeneralCarryingCapacity(List<Plane> planes);
        void SearchPlaneByFuelConsumption(List<Plane> planes, uint startRangeOfFuelConsumption, uint endRangeOfFuelConsumption);
        void SortByRangeOfFlight(List<Plane> planes);
    }
}
