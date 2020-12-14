using System;

namespace ClientNamespace
{
    public class Call<T>
    {
        public DateTime StartCall { get; set; }
        public DateTime EndCall { get; set; }
        public Client<T> ClientAcceptedTheCall { get; set; }
        public Client<T> ClientStartedTheCall { get; set; }
        public Client<T> ClientFinishedTheCall { get; set; }
        public decimal Cost { get; set; }
    }
}

