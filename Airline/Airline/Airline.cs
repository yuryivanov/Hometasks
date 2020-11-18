namespace AirlineNamespace
{
    public abstract class Airline
    {
        public static string CompanyName { get; set; }
        static Airline() => CompanyName = "Yury's Airline Company";
    }
}
