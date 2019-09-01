using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface ILegendaryGemInfo
    {
        IEnumerable<IBuff> AllBuffs();

        IBuff BaneOfThePowerfulPrimary { get; } // 383014
        IBuff BaneOfThePowerfulSecondary { get; } // 451157
        IBuff BaneOfTheStrickenPrimary { get; } // 428348
        IBuff BaneOfTheStrickenSecondary { get; } // 428349
        IBuff BaneOfTheTrappedPrimary { get; } // 403456
        IBuff BaneOfTheTrappedSecondary { get; } // 403457
        IBuff BoonOfTheHoarderPrimary { get; } // 403470
        IBuff BoonOfTheHoarderSecondary { get; } // 403784
        IBuff BoyarskysChipPrimary { get; } // 428352
        IBuff BoyarskysChipSecondary { get; } // 428353
        IBuff EnforcerPrimary { get; } // 403466
        IBuff EnforcerSecondary { get; } // 403472
        IBuff EsotericAlterationPrimary { get; } // 428029
        IBuff EsotericAlterationSecondary { get; } // 428030
        IBuff GemOfEasePrimary { get; } // 403459
        IBuff GemOfEaseSecondary { get; } // 428691
        IBuff GemOfEfficaciousToxinPrimary { get; } // 403461
        IBuff GemOfEfficaciousToxinSecondary { get; } // 403556
        IBuff GogokOfSwiftnessPrimary { get; } // 403464
        IBuff GogokOfSwiftnessSecondary { get; } // 403524
        IBuff IceblinkPrimary { get; } // 428354
        IBuff IceblinkSecondary { get; } // 428356
        IBuff InvigoratingGemstonePrimary { get; } // 403465
        IBuff InvigoratingGemstoneSecondary { get; } // 403624
        IBuff LegacyOfDreamsPrimary { get; } // 483319
        IBuff LegacyOfDreamsSecondary { get; } // 483320
        IBuff MirinaeTeardropOfTheStarweaverPrimary { get; } // 403463
        IBuff MirinaeTeardropOfTheStarweaverSecondary { get; } // 403620
        IBuff MoltenWildebeestsGizzardPrimary { get; } // 428031
        IBuff MoltenWildebeestsGizzardSecondary { get; } // 428032
        IBuff MoratoriumPrimary { get; } // 403467
        IBuff MoratoriumSecondary { get; } // 403687
        IBuff MutilationGuardPrimary { get; } // 428350
        IBuff MutilationGuardSecondary { get; } // 428351
        IBuff PainEnhancerPrimary { get; } // 403462
        IBuff PainEnhancerSecondary { get; } // 403600
        IBuff RedSoulShardPrimary { get; } // 454736
        IBuff RedSoulShardSecondary { get; } // 454737
        IBuff SimplicitysStrengthPrimary { get; } // 403469
        IBuff SimplicitysStrengthSecondary { get; } // 403473
        IBuff TaegukPrimary { get; } // 403471
        IBuff TaegukSecondary { get; } // 403785
        IBuff WreathOfLightningPrimary { get; } // 403460
        IBuff WreathOfLightningSecondary { get; } // 403560
        IBuff ZeisStoneOfVengeancePrimary { get; } // 403468
        IBuff ZeisStoneOfVengeanceSecondary { get; } // 403727
    }
}
