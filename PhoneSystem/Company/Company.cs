using System;
using System.Collections.Generic;
using ClientNamespace;
using HardwareNamespace;
using TariffNamespace;

namespace CompanyNamespace
{
    public class Company<T>
    {
        public DateTime checkBalanceDate = new DateTime(2020, 11, 11);

        public History<T> historyOfCompany = new History<T>();
        public string Name { get; set; }

        public Client<T> AddClientOfTariff1(decimal sum, string firstAndLastName, string phoneNumber, DateTime registration, Port port, Phone<T> phone, Tariff tariff) =>
            new Client<T>(sum, firstAndLastName, phoneNumber, registration, port, phone, tariff);

        public Client<T> AddClientOfTariff2(decimal sum, string firstAndLastName, string phoneNumber, DateTime registration, Port port, Phone<T> phone, Tariff tariff) =>
            new Client<T>(sum, firstAndLastName, phoneNumber, registration, port, phone, tariff);

        public string GetPortState(out string firstAndLastName, List<Client<T>> allClients)
        {
            Console.WriteLine("Please enter First & Last Name to get this Port state, ex: \"Yury Ivanou\"");
            firstAndLastName = Console.ReadLine();
            foreach (var VARIABLE in allClients)
            {
                if (firstAndLastName == VARIABLE.FirstAndLastName)
                {
                    return $"Port state of this client is {VARIABLE.Port.state.ToString()}";
                }
            }
            return "This client not found";
        }

        public void WaitForCheckBalanceDay(List<Client<T>> allClients)
        {
            if (checkBalanceDate.AddMonths(1) <= DateTime.Now)
            {
                foreach (var VARIABLE in allClients)
                {
                    CheckBalanceMoreThanOrEqualToZero(VARIABLE);
                    GetNotificationBlocked(VARIABLE);
                }
                checkBalanceDate = checkBalanceDate.AddMonths(1);
            }
        }

        public void CheckBalanceMoreThanOrEqualToZero(Client<T> client)
        {
            if (client.Sum < 0)
            {
                client.Port.state = Port.PortState.Blocked;
            }
        }

        public void AddHistory(Call<T> call, List<Client<T>> allClients) => historyOfCompany.historyOfCalls.Add(call);

        public void GetNotificationBlocked(Client<T> client)
        {
            if (client.Port.state == Port.PortState.Blocked)
            {
                Console.WriteLine($"Your Port, {client.FirstAndLastName}, is blocked, please top up balance to unlock");
            }
        }
    }
}
