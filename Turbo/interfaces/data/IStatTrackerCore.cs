namespace Turbo.Plugins
{
    public interface IStatTrackerCore
    {
        double GainedExperience { get; }
        long GainedGold { get; }
        long MonsterKill { get; }
        long EliteKill { get; }
        long DropAll { get; }
        long DropWhite { get; }
        long DropMagic { get; }
        long DropRare { get; }
        long DropLegendary { get; }
        long DropAncient { get; }
        long DropPrimalAncient { get; }
        long DropGold { get; }
        long DropBloodShard { get; }
        long Death { get; }
        double DamageDealtAll { get; }
        double DamageDealtCrit { get; }
        double DamageTaken { get; }
        double Healing { get; }
        double WalkYards { get; }

        long ElapsedMilliseconds { get; }
        long TownElapsedMilliseconds { get; }
        long PlayElapsedMilliseconds { get; }

        double GainedExperiencePerHourFull { get; }
        double GainedExperiencePerHourPlay { get; }
        double GainedGoldPerHour { get; }
        double MonsterKillPerHour { get; }
        double EliteKillPerHour { get; }
        double DropAllPerHour { get; }
        double DropWhitePerHour { get; }
        double DropMagicPerHour { get; }
        double DropRarePerHour { get; }
        double DropLegendaryPerHour { get; }
        double DropAncientPerHour { get; }
        double DropPrimalAncientPerHour { get; }
        double DropGoldPerHour { get; }
        double DropBloodShardPerHour { get; }
        double DeathPerHour { get; }
        double DamageDealtAllPerSecond { get; }
        double DamageDealtCritPerSecond { get; }
        double DamageTakenPerSecond { get; }
        double HealingPerSecond { get; }

        double MonsterKillPerLegendary { get; }
        double EliteKillPerLegendary { get; }
    }
}