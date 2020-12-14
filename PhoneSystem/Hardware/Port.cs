using System;

namespace HardwareNamespace
{
    public class Port
    {
        public Guid portId = Guid.NewGuid();
        public PortState state = Port.PortState.Off;
        public enum PortState
        {
            Off = 1,
            On = 2,
            Blocked = 3,
            Call = 4,
            CallIsGoing = 5
        }
    }
}
