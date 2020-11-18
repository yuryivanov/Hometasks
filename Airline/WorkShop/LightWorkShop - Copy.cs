using System;
using PlaneNamespace;

namespace WorkShopNamespace
{
    public class LightWorkShop : WorkShop
    {
        public LightWorkShop(int number) : base(number) { }
        public override Plane CreatePlane() => new LightPlane();
    }
}
