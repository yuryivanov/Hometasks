namespace TariffNamespace
{
    public abstract class Tariff
    {
        public abstract decimal OneMinuteCost { get; set; }

        public abstract bool InternetIsUnlimited { get; set; }
    }
}