using System;
using PlaneNamespace;
using AirlineNamespace;
using WorkShopNamespace;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ProjectNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MethodHelper methodHelper = new MethodHelper();

                //Planes from a file:                
                var fileData = File.ReadAllText(@"D:\it-academy\new\Hometasks\Airline\Plane,carCapacity,Range,Capacity,FuelConsumption.txt");
                ParserOfPlanesInfoFromDoc parser = new ParserOfPlanesInfoFromDoc();
                var lightWorkShop1 = new LightWorkShop(1);
                var heavyWorkShop2 = new HeavyWorkShop(2);
                //Validate the file data
                parser.Validate(fileData);
                var allPlanesFromFile = parser.ParseAllPlanes(parser.ParseLightPlanes(fileData), parser.ParseHeavyPlanes(fileData));

                //Custom planes:
                var customPlanes = new List<Plane>();
                var myLightPlane1 = lightWorkShop1.CreatePlane("Custom Light1", 666, 666, 666, 666);
                var myHeavyPlane2 = heavyWorkShop2.CreatePlane("Custom Heavy2", 666, 666, 666, 666);
                customPlanes.Add(myLightPlane1);
                customPlanes.Add(myHeavyPlane2);

                //All planes:
                var allPlanes = allPlanesFromFile.Concat(customPlanes).ToList();

                //Required output info:
                Console.WriteLine($"Company: {Airline.CompanyName} with workshops:" +
                    $"\nLightWorkShop #{lightWorkShop1.Number}" +
                    $"\nHeavyWorkSHop #{heavyWorkShop2.Number}");

                Console.WriteLine("\nLight aircraft characteristics: ");
                foreach (var item in parser.ParseLightPlanes(fileData))
                {
                    Console.WriteLine($"Plane {item.Name} - CarryingCapacity: {item.CarryingCapacity} tons, RangeOfFlight: {item.RangeOfFlight} km, " +
                        $"CapacityForPeople: {item.CapacityForPeople} people, FuelConsumption: {item.FuelConsumption} kg/h;");
                }
                Console.WriteLine("\nHeavy aircraft characteristics: ");
                foreach (var item in parser.ParseHeavyPlanes(fileData))
                {
                    Console.WriteLine($"Plane {item.Name} - CarryingCapacity: {item.CarryingCapacity} tons, RangeOfFlight: {item.RangeOfFlight} km, " +
                        $"CapacityForPeople: {item.CapacityForPeople} people, FuelConsumption: {item.FuelConsumption} kg/h;");
                }
                Console.WriteLine("\nCustom planes (not from the file):");
                foreach (var item in customPlanes)
                {
                    Console.WriteLine($"Plane {item.Name} - CarryingCapacity: {item.CarryingCapacity} tons, RangeOfFlight: {item.RangeOfFlight} km, " +
                        $"CapacityForPeople: {item.CapacityForPeople} people, FuelConsumption: {item.FuelConsumption} kg/h;");
                }

                Console.WriteLine("\nGeneralCapacityForPeople - {0}", methodHelper.GetGeneralCapacityForPeople(allPlanes));
                Console.WriteLine("\nGeneralCarryingCapacity - {0}", methodHelper.GetGeneralCarryingCapacity(allPlanes));


                Console.WriteLine("\nSorting of planes by RangeOfFlight:");
                methodHelper.SortByRangeOfFlight(allPlanes);

                uint x1 = 2400;
                uint x2 = 2900;
                Console.WriteLine($"\nResults of range search by FuelConsumption from {x1} to {x2}:");
                methodHelper.SearchPlaneByFuelConsumption(allPlanes, x1, x2);
            }
            catch (FileNotFoundException)
            { Console.WriteLine("File not found"); }
            catch (Exception)
            { Console.WriteLine("Sorry, something went wrong"); }
        }
    }
}
