namespace TariffNamespace
{
    public class Tariff1 : Tariff
    {
        decimal _oneMinuteCost;
        bool _internetIsUnlimited;
        public override decimal OneMinuteCost { get => _oneMinuteCost; set => _oneMinuteCost = 1; }
        public override bool InternetIsUnlimited { get => _internetIsUnlimited; set => _internetIsUnlimited = false; }

        public Tariff1()
        {
            OneMinuteCost = this._oneMinuteCost;
            InternetIsUnlimited = this._internetIsUnlimited;
        }
    }
}
