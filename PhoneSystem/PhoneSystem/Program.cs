using System;
using ClientNamespace;
using CompanyNamespace;
using HardwareNamespace;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TariffNamespace;
using System.Linq;
using System.IO;

namespace PhoneSystemNamespace
{
    class Program
    {
        //General data for two threads:
        static string[] fileDate = null;
        private static List<Client<XiaomiRedmiNote4X>> clientsFromFile = new List<Client<XiaomiRedmiNote4X>>();

        static void Main(string[] args)
        {
            var thread2 = new Thread(GetClientsFromFileAsync);
            thread2.Start();
            thread2.Join();

            string[] randomListOfNames = { "Yury Ivanou", "Eugen Mikheev", "Ilya Rubtsov", "Tatiana Oharchuk", "Sergey Balmont", "Igor Plotnikov", "Tamara Lubova", "Maria Avsievich",
            "Yulya Ivanova", "Alexander Maikov", "Irina Rubtsova", "Vlada Nekrasova", "Svatlana Balchuk", "Inna Petrova", "Anastasiya Lubova", "Sonya Abramovich",
            "Petr Ivanou", "Viktoria Kudretz", "Ksenya Rudenko", "Sergey Pavlov", "Vlad Tarasov", "Ivan Obabko", "Viktoriya Kladisheva", "Ilya Chernov"};

            var myCompany = new Company<XiaomiRedmiNote4X> { Name = "MyCompany" };
            var myCompany2 = new Company<IPhone6> { Name = "MyCompany2" };               //Alternative variant that can be used

            var rnd = new Random();

            List<Client<XiaomiRedmiNote4X>> randomClientsOfTheFirstTariff = new List<Client<XiaomiRedmiNote4X>>();
            for (int i = 0; i < 5; i++)
            {
                randomClientsOfTheFirstTariff.Add(myCompany.AddClientOfTariff1(rnd.Next(-5, 5),
                randomListOfNames[rnd.Next(0, randomListOfNames.Length - 1)],
                $"+375(11){rnd.Next(100, 999)}-{rnd.Next(10, 99)}-{rnd.Next(10, 99)}",
                new DateTime(rnd.Next(2015, 2020),
                rnd.Next(1, 12),
                rnd.Next(1, 28),
                rnd.Next(0, 23),
                rnd.Next(0, 59),
                rnd.Next(0, 59)).AddDays(1),
                new Port(),
                new Phone<XiaomiRedmiNote4X>(),
                new Tariff1()));
            }
            List<Client<XiaomiRedmiNote4X>> randomClientsOfTheSecondTariff = new List<Client<XiaomiRedmiNote4X>>();
            for (int i = 0; i < 5; i++)
            {
                randomClientsOfTheSecondTariff.Add(myCompany.AddClientOfTariff2(rnd.Next(1, 5),
                randomListOfNames[rnd.Next(0, randomListOfNames.Length - 1)],
                $"+375(22){rnd.Next(100, 999)}-{rnd.Next(10, 99)}-{rnd.Next(10, 99)}",
                new DateTime(rnd.Next(2015, 2020),
                rnd.Next(1, 12),
                rnd.Next(1, 28),
                rnd.Next(0, 23),
                rnd.Next(0, 59),
                rnd.Next(0, 59)).AddDays(1),
                new Port(),
                new Phone<XiaomiRedmiNote4X>(),
                new Tariff2()));
            }

            Console.WriteLine($"Client {randomClientsOfTheFirstTariff[0].FirstAndLastName}, to change Tariff1 to Tariff2, please enter \"yes\" or enter something else to continue");
            randomClientsOfTheFirstTariff[0].ChangeTariff();
            Console.WriteLine($"Client {randomClientsOfTheSecondTariff[0].FirstAndLastName}, to change Tariff2 to Tariff1, please enter \"yes\" or enter something else to continue");
            randomClientsOfTheSecondTariff[0].ChangeTariff();

            var allClients = new List<Client<XiaomiRedmiNote4X>>();

            allClients = randomClientsOfTheFirstTariff.Concat(randomClientsOfTheSecondTariff).ToList();
            allClients = allClients.Concat(clientsFromFile).ToList();

            Console.WriteLine("All Clients added by MyCompany and from the file:");
            foreach (var VARIABLE in allClients)
            {
                Console.WriteLine($"Sum: {VARIABLE.Sum} rub, " +
                                  $"PhoneNumber: {VARIABLE.PhoneNumber}, " +
                                  $"Name: {VARIABLE.FirstAndLastName}, " +
                                  $"Registration Date: {VARIABLE.RegistrationDate}, " +
                                  $"Port state: {VARIABLE.Port.state.ToString()}, " +
                                  $"Phone Id: {VARIABLE.Phone.phoneId}, " +
                                  $"One minute cost: {VARIABLE.Tariff.OneMinuteCost.ToString()} rub");
            }

            foreach (var VARIABLE in allClients)
            {
                VARIABLE.AddToHistoryEvent += VARIABLE.AddHistory;
                VARIABLE.AddToHistoryEvent += myCompany.AddHistory;
                VARIABLE.CheckBalanceEvent += myCompany.WaitForCheckBalanceDay;
            }                              //This foreach is for subscribing to the events

            Console.WriteLine();
            foreach (var VARIABLE in allClients)
            {
                VARIABLE.TurnPortOn();
            }                              //This foreach is to turn On ports for all clients
            Console.WriteLine("All ports have been turned On");

            List<Call<XiaomiRedmiNote4X>> callsInProgress = new List<Call<XiaomiRedmiNote4X>>();

            Console.WriteLine();                                                                      //Collected all methods of main call flow below
            for (int i = 0; i < 10; i += 3)                                                           
            {
            Label:
                Console.WriteLine($"{allClients[i].FirstAndLastName} please enter phone number to call in format: +375(33)-678-87-99");
                var phoneNumber = Console.ReadLine();

                if (allClients[i].StartCall(allClients[i], phoneNumber, allClients))
                {
                    foreach (var VARIABLE in allClients)
                    {
                        if (VARIABLE.PhoneNumber == phoneNumber)
                        {
                            Console.WriteLine($"{VARIABLE.FirstAndLastName}, {allClients[i].PhoneNumber} is calling to you, please enter \"yes\" to answer or something else to drop the call:");
                            var choiceToAnswerTheCall = Console.ReadLine();
                            bool callIsAccepted;

                            if (choiceToAnswerTheCall.Contains("yes"))
                            {
                                callIsAccepted = true;
                            }
                            else
                            {
                                callIsAccepted = false;
                            }

                            DateTime acceptCall = allClients[i + 1].AcceptCall(VARIABLE, allClients[i].PhoneNumber, allClients, callIsAccepted);

                            if (acceptCall.Year != 1)
                            {
                                callsInProgress.Add(new Call<XiaomiRedmiNote4X>
                                {
                                    ClientStartedTheCall = allClients[i],
                                    ClientAcceptedTheCall = allClients[i + 1],
                                    StartCall = acceptCall
                                });

                                allClients[i + 2].StartCall(allClients[i + 2], phoneNumber, allClients); //To show how one person call to one of two speakers
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("If you want to try again, please enter \"yes\", else enter something:");
                    var tryAgainToStartCall = Console.ReadLine();

                    if (tryAgainToStartCall.Contains("yes"))
                    {
                        goto Label;
                    }
                }
            }
            foreach (var VARIABLE in callsInProgress)
            {

                var endCall = VARIABLE.ClientStartedTheCall.EndCall(VARIABLE.ClientStartedTheCall, VARIABLE.ClientAcceptedTheCall.PhoneNumber, allClients, VARIABLE.StartCall);
                VARIABLE.EndCall = endCall;

                if (endCall.Year != 1)
                {
                    VARIABLE.ClientStartedTheCall.WithdrawMoney(VARIABLE.StartCall, endCall, VARIABLE.ClientStartedTheCall.Tariff.OneMinuteCost, VARIABLE.ClientStartedTheCall.Port.state, allClients);
                }
                else
                {
                    throw new Exception("Something went wrong");
                }
            }

            allClients[6].AddHistory(new Call<XiaomiRedmiNote4X>
            {
                ClientStartedTheCall = allClients[6],
                ClientAcceptedTheCall = allClients[8],
                ClientFinishedTheCall = allClients[6],
                StartCall = DateTime.Now,
                EndCall = DateTime.Now.AddMinutes(3),
                Cost = ((decimal)(3 * allClients[6].Tariff.OneMinuteCost))
            }, allClients);                       //To add a new row for history of this client just to check filters work

            foreach (var VARIABLE in allClients)
            {
                VARIABLE.TurnPortOff();
            }
            Console.WriteLine("\nAll ports have been turned Off\n");

            string firstAndLastName;
            Console.WriteLine(myCompany.GetPortState(out firstAndLastName, allClients));

            decimal moneyPut;
            Console.WriteLine($"\n{allClients[0].FirstAndLastName}, please enter sum to put on your balance");

            if (decimal.TryParse(Console.ReadLine(), out moneyPut))
            {
                allClients[0].PutMoney(moneyPut, allClients);
            }
            else
            {
                Console.WriteLine("Invalid input");
            }

            allClients[6].GetFilteredHistory();

            Console.WriteLine();
            foreach (var VARIABLE in allClients)
            {
                VARIABLE.GetBalance();
            }

            Console.ReadLine();
        }

        static void GetClientsFromFile()
        {
            fileDate = File.ReadAllLines(@"D:\Clients.txt");

            foreach (var VARIABLE in fileDate)
            {
                clientsFromFile.Add(new Client<XiaomiRedmiNote4X>(
                    decimal.Parse(VARIABLE.Split(',')[0]),
                    VARIABLE.Split(',')[1],
                    VARIABLE.Split(',')[2],
                    DateTime.Parse(VARIABLE.Split(',')[3]),
                    new Port(),
                    new Phone<XiaomiRedmiNote4X>(),
                    new Tariff1()));
            }
        }

        static async void GetClientsFromFileAsync()
        {
            await Task.Run(() => Console.WriteLine("Extracting the date from the file... Completed\n"));
            await Task.Run(() => GetClientsFromFile());
            Task.WaitAll();
        }
    }
}
