using System;
using System.Collections.Generic;
using System.Linq;
using HardwareNamespace;
using TariffNamespace;

namespace ClientNamespace
{
    public partial class Client<T>
    {
        public delegate void AddToHistoryDelegate(Call<T> call, List<Client<T>> allClients);
        public event AddToHistoryDelegate AddToHistoryEvent;
        public delegate void CheckBalanceDelegate(List<Client<T>> allClients);
        public CheckBalanceDelegate CheckBalanceEvent;

        public void GetNotificationBlocked()
        {
            if (Port.state == Port.PortState.Blocked)
            {
                Console.WriteLine($"Your Port, {FirstAndLastName}, is blocked, please top up balance to unlock");
            }
        }

        public void GetNotificationOn()
        {
            if (Port.state == Port.PortState.On)
            {
                Console.WriteLine($"Your Port, {FirstAndLastName}, is On");
            }
        }

        public void GetBalance()
        {
            Console.WriteLine($"Your balance, {FirstAndLastName}, is {Sum}");
            GetNotificationBlocked();
        }

        public void PutMoney(decimal sum, List<Client<T>> allClients)
        {
            CheckBalanceEvent?.Invoke(allClients);
            Sum += sum;
            Console.WriteLine($"{FirstAndLastName}, you've put {sum} to your balance, your current balance is {Sum}");
            if (Port.state == Port.PortState.Blocked && Sum >= 0)
            {
                Port.state = Port.PortState.On;
                GetNotificationOn();
            }
        }

        public void WithdrawMoney(DateTime start, DateTime end, decimal oneMinuteCost, Port.PortState state, List<Client<T>> allClients)
        {
            var time = end.Subtract(start).TotalMinutes;
            Sum -= (decimal)time * oneMinuteCost;
            CheckBalanceEvent?.Invoke(allClients);
        }

        public void ChangeTariff()
        {
            var confirmation = Console.ReadLine();
            if (confirmation.Contains("yes"))
            {
                if (Tariff is Tariff1)
                {
                    Tariff = new Tariff2();
                }
                else if (Tariff is Tariff2)
                {
                    Tariff = new Tariff1();
                }
                else
                {
                    throw new Exception("Something went wrong");
                }
            }
        }

        public void TurnPortOn()
        {
            Port.state = Port.PortState.On;
            GetNotificationOn();
        }

        public void TurnPortOff()
        {
            Port.state = Port.PortState.Off;
        }

        public bool StartCall(Client<T> client1, string phoneNumber2, List<Client<T>> allClients)
        {
            CheckBalanceEvent?.Invoke(allClients);
            foreach (var item in allClients)
            {
                if (item.PhoneNumber == phoneNumber2 && item.Port.state == Port.PortState.On && client1.Port.state == Port.PortState.On && phoneNumber2 != client1.PhoneNumber)
                {
                    client1.Port.state = Port.PortState.Call;
                    item.Port.state = Port.PortState.Call;
                    Console.WriteLine($"{client1.FirstAndLastName} is calling to {phoneNumber2}...");
                    return true;
                }
            }
            if (client1.Port.state == Port.PortState.Blocked)
            {
                Console.WriteLine($"You, {client1.FirstAndLastName}, have failed to call to {phoneNumber2}, cause you are blocked, please top up your balance to unlock");
            }
            else if (client1.Port.state == Port.PortState.Call || client1.Port.state == Port.PortState.CallIsGoing)
            {
                Console.WriteLine($"You, {client1.FirstAndLastName}, have failed to call to {phoneNumber2}, cause you have had a call yet");
            }
            else
            {
                Console.WriteLine($"You, {client1.FirstAndLastName}, have failed to call to {phoneNumber2}, cause {phoneNumber2} is not available now");
            }
            return false;
        }

        public DateTime AcceptCall(Client<T> client2, string phoneNumber1, List<Client<T>> allClients, bool callIsAccepted)
        {
            CheckBalanceEvent?.Invoke(allClients);
            foreach (var item in allClients)
            {
                if (item.PhoneNumber == phoneNumber1 && item.Port.state == Port.PortState.Call && callIsAccepted && client2.Port.state == Port.PortState.Call && callIsAccepted == true)
                {
                    item.Port.state = Port.PortState.CallIsGoing;
                    client2.Port.state = Port.PortState.CallIsGoing;
                    Console.WriteLine($"{client2.PhoneNumber} answered. Call started.");
                    return DateTime.Now;
                }
            }
            foreach (var item in allClients)
            {
                if (item.PhoneNumber == phoneNumber1)
                {
                    item.Port.state = Port.PortState.On;
                    item.GetNotificationOn();
                    client2.Port.state = Port.PortState.On;
                    client2.GetNotificationOn();
                    Console.WriteLine($"{client2.PhoneNumber} not answered");
                    return new DateTime(1, 1, 1);
                }
            }
            throw new Exception("Something went wrong");
        }

        public DateTime EndCall(Client<T> client1, string phoneNumber2, List<Client<T>> allClients, DateTime startDateTime)
        {
            client1.Port.state = Port.PortState.On;
            client1.GetNotificationOn();
            Console.WriteLine($"{client1.FirstAndLastName} ended the call and is available now");
            foreach (var item in allClients)
            {
                if (item.PhoneNumber == phoneNumber2)
                {
                    item.Port.state = Port.PortState.On;
                    item.GetNotificationOn();
                    Console.WriteLine($"Call ended. {phoneNumber2} is available now");
                    decimal cost;
                    string text = ((decimal)(DateTime.Now - startDateTime).TotalMinutes * client1.Tariff.OneMinuteCost).ToString();
                    if (decimal.TryParse(text, out cost))
                    {
                        AddToHistoryEvent?.Invoke(new Call<T>
                        {
                            ClientStartedTheCall = client1,
                            ClientAcceptedTheCall = item,
                            ClientFinishedTheCall = client1,
                            StartCall = startDateTime,
                            EndCall = DateTime.Now,
                            Cost = cost
                        }, allClients);
                        return DateTime.Now;
                    }
                }
            }
            Console.WriteLine("Something went wrong");
            return new DateTime(1, 1, 1);
        }

        public void AddHistory(Call<T> call, List<Client<T>> allClients)
        {
            foreach (var VARIABLE in allClients)
            {
                if (call.ClientStartedTheCall == VARIABLE || call.ClientAcceptedTheCall == VARIABLE)
                {
                    VARIABLE.historyOfClient.historyOfCalls.Add(call);
                }
            }
        }

        public void GetFilteredHistory()
        {
            Console.WriteLine("\nPlease enter one of filter options:\"date\", \"cost\" or \"contact\"");
            var filter = Console.ReadLine();
            Console.WriteLine($"Filtered history for {FirstAndLastName}:");
            switch (filter)
            {
                case "date":
                    historyOfClient.historyOfCalls.OrderByDescending(x => x.StartCall)
                        .ToList()
                        .ForEach(x => Console.WriteLine($"StartCall: {x.StartCall}, EndCall: {x.EndCall}, Cost: {x.Cost} rub, Contact: {x.ClientAcceptedTheCall.PhoneNumber}"));
                    break;
                case "cost":
                    historyOfClient.historyOfCalls.OrderByDescending(x => x.Cost)
                        .ToList()
                        .ForEach(x => Console.WriteLine($"StartCall: {x.StartCall}, EndCall: {x.EndCall}, Cost: {x.Cost} rub, Contact: {x.ClientAcceptedTheCall.PhoneNumber}"));
                    break;
                case "contact":
                    historyOfClient.historyOfCalls.OrderByDescending(x => x.ClientAcceptedTheCall)
                        .ToList()
                        .ForEach(x => Console.WriteLine($"StartCall: {x.StartCall}, EndCall: {x.EndCall}, Cost: {x.Cost} rub, Contact: {x.ClientAcceptedTheCall.PhoneNumber}"));
                    break;
                default:
                    Console.WriteLine("None option chosen, so history will be displayed ordered by Start Call datetime:");
                    historyOfClient.historyOfCalls.OrderByDescending(x => x.StartCall)
                        .ToList()
                        .ForEach(x => Console.WriteLine($"StartCall: {x.StartCall}, EndCall: {x.EndCall}, Cost: {x.Cost} rub, Contact: {x.ClientAcceptedTheCall.PhoneNumber}"));
                    break;
            }
        }
    }
}

