using System;

namespace HardwareNamespace
{
    public class Phone<T>
    {
        public Guid phoneId = Guid.NewGuid();
        public T Model { get; set; }
    }
}
