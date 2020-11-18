using System;

namespace ICountableNamespace
{
    public interface ICountable
    {
        int GetGeneralCapacityForPeople();    
        int GetGeneralCarryingCapacity();
        void SortPlanesByFuelConsumption();
        void SearchPlaneByFuelConsumption();
    }        
}
