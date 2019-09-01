using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface IPlayerArmorySet
    {
        IPlayer Player { get; }
        int Index { get; }
        string Name { get; }
        IEnumerable<uint> ItemAnnIds { get; }

        uint PotionAnnId { get; }
        ISnoItem PotionSno { get; }
        ISnoItem CubeSnoItem1 { get; }
        ISnoItem CubeSnoItem2 { get; }
        ISnoItem CubeSnoItem3 { get; }

        ISnoPower LeftSkillSnoPower { get; }
        byte LeftSkillRune { get; }
        ISnoPower RightSkillSnoPower { get; }
        byte RightSkillRune { get; }
        ISnoPower Skill1SnoPower { get; }
        byte Skill1Rune { get; }
        ISnoPower Skill2SnoPower { get; }
        byte Skill2Rune { get; }
        ISnoPower Skill3SnoPower { get; }
        byte Skill3Rune { get; }
        ISnoPower Skill4SnoPower { get; }
        byte Skill4Rune { get; }

        ISnoPower PassiveSnoPower1 { get; }
        ISnoPower PassiveSnoPower2 { get; }
        ISnoPower PassiveSnoPower3 { get; }
        ISnoPower PassiveSnoPower4 { get; }

        bool ContainsItem(IItem item);
    }
}