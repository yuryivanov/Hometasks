namespace TariffNamespace
{
    public class Tariff2 : Tariff
    {
        decimal _oneMinuteCost;
        bool _internetIsUnlimited;
        public override decimal OneMinuteCost { get => _oneMinuteCost; set => _oneMinuteCost = 4; }
        public override bool InternetIsUnlimited { get => _internetIsUnlimited; set => _internetIsUnlimited = true; }

        public Tariff2()
        {
            OneMinuteCost = this._oneMinuteCost;
            InternetIsUnlimited = this._internetIsUnlimited;
        }
    }
}
