using PlaneNamespace;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ProjectNamespace
{
    public class ParserOfPlanesInfoFromDoc : IParseable
    {
        public List<Plane> ParseLightPlanes(string text)
        {
            text = text.Replace("\n", "");
            string[] array = text.Split(':', ',', ';');

            var lightPlanes = new List<Plane>();

            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].Contains("LightPlanes"))
                {
                    continue;
                }
                else if (i < Array.FindIndex(array, w => w.Contains("Heavy")))
                {
                    lightPlanes.Add(new LightPlane(array[i++], uint.Parse(array[i++]), uint.Parse(array[i++]), uint.Parse(array[i++]), uint.Parse(array[i])));
                }
            }
            return lightPlanes;
        }
        public List<Plane> ParseHeavyPlanes(string text)
        {
            text = text.Replace("\n", "");
            string[] array = text.Split(':', ',', ';');

            var heavyPlanes = new List<Plane>();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Contains("LightPlanes") || array[i].Contains("HeavyPlanes"))
                {
                    continue;
                }
                else if (i > Array.FindIndex(array, w => w.Contains("Heavy")))
                {
                    heavyPlanes.Add(new HeavyPlane(array[i++], uint.Parse(array[i++]), uint.Parse(array[i++]), uint.Parse(array[i++]), uint.Parse(array[i])));
                }
            }
            return heavyPlanes;
        }
        public List<Plane> ParseAllPlanes(List<Plane> lightPlanes, List<Plane> heavyPlanes)
        {
            var allPlanes = new List<Plane>();
            string text = File.ReadAllText(@"D:\it-academy\new\Hometasks\Airline\Plane,carCapacity,Range,Capacity,FuelConsumption.txt");
            allPlanes = ParseLightPlanes(text).Concat(ParseHeavyPlanes(text)).ToList();
            return allPlanes;
        }
        public bool Validate(string text)
        {
            text = text.Replace("\n", "");
            string[] array = text.Split(':', ',', ';');

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Contains("LightPlanes") || array[i].Contains("Heavy"))
                {
                    continue;
                }
                else
                {
                    i++;
                    uint x;
                    if (uint.TryParse(array[i++], out x) && uint.TryParse(array[i++], out x) && uint.TryParse(array[i++], out x) && uint.TryParse(array[i], out x))
                    { }
                    else
                    {
                        throw new Exception("File Data has incorrect format");
                    }
                }
            }
            return true;
        }
    }
}
