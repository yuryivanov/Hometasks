using System;
using HardwareNamespace;
using TariffNamespace;

namespace ClientNamespace
{
    public partial class Client<T>
    {
        public History<T> historyOfClient = new History<T>();
        public DateTime RegistrationDate { get; set; }
        public decimal Sum { get; set; }
        public string FirstAndLastName { get; set; }
        public string PhoneNumber { get; set; }
        public Tariff Tariff { get; set; }
        public Port Port { get; set; }
        public Phone<T> Phone { get; set; }
        public Client(decimal sum, string firstAndLastName, string phoneNumber, DateTime registration, Port port, Phone<T> phone, Tariff tariff)
        {
            Sum = sum;
            FirstAndLastName = firstAndLastName;
            PhoneNumber = phoneNumber;
            RegistrationDate = registration;
            this.Port = port;
            this.Phone = phone;
            this.Tariff = tariff;
        }
    }
}
