using System;
using Client;

namespace CallNamespace
{
    public class Call
    {
        public DateTime StartCall { get; set; }

        public DateTime EndCall { get; set; }

        public string ClientAcceptedTheCall { get; set; }

        public string ClientStartedTheCall { get; set; }

        public string ClientFinishedTheCall { get; set; }

        public double Cost { get; set; }
    }
}
