namespace Turbo.Plugins
{
    public interface IPlayerMaterialInfo
    {
        long Gold { get; }
        long BloodShard { get; }
        long BNetStore { get; }

        long ReusableParts { get; }
        long ArcaneDust { get; }
        long VeiledCrystal { get; }
        long DeathsBreath { get; }
        long ForgottenSoul { get; }

        long KhanduranRune { get; }
        long CaldeumNightShade { get; }
        long ArreatWarTapestry { get; }
        long CorruptedAngelFlesh { get; }
        long WestmarchHolyWater { get; }

        long HeartOfFright { get; }
        long VialOfPutridness { get; }
        long IdolOfTerror { get; }
        long LeoricsRegret { get; }

        long GreaterRiftKeystone { get; }
    }
}