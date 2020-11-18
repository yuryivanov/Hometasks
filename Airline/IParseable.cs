using System.Collections.Generic;
using PlaneNamespace;

namespace ProjectNamespace
{
    public interface IParseable : IFileValidator
    {
        public abstract List<Plane> ParseLightPlanes(string text);
        public abstract List<Plane> ParseHeavyPlanes(string text);
        public abstract List<Plane> ParseAllPlanes(List<Plane> lightPlanes, List<Plane> heavyPlanes);
        public abstract bool Validate(string text);
    }
}
