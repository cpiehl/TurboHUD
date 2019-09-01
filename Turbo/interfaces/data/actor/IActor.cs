namespace Turbo.Plugins
{
    public interface IActor
    {
        ISnoActor SnoActor { get; }
        GizmoType GizmoType { get; }

        uint AcdId { get; }
        uint AnnId { get; }
        IWorldCoordinate FloorCoordinate { get; }
        IWorldCoordinate CollisionCoordinate { get; }
        float RadiusScaled { get; }
        float RadiusBottom { get; }

        uint WorldId { get; }
        uint WorldSno { get; }
        ISnoWorld SnoWorld { get; }
        IScene Scene { get; }

        double GetAttributeValue(IAttribute attribute, uint modifier, double defaultValue = -1.0f);
        int GetAttributeValueAsInt(IAttribute attribute, uint modifier, int defaultValue = int.MaxValue);
        uint GetAttributeValueAsUInt(IAttribute attribute, uint modifier, uint defaultValue = uint.MaxValue);

        // ---------------------------
        // hint: use these members because they are cached and not calculated at every call
        IScreenCoordinate ScreenCoordinate { get; }
        bool IsOnScreen { get; }
        double CentralXyDistanceToMe { get; }
        double NormalizedXyDistanceToMe { get; }
        double ZDistanceToMeAbsolute { get; }
        // ---------------------------

        float Hitpoints { get; }
        uint SummonerId { get; }
        uint SummonerAcdDynamicId { get; }

        bool Untargetable { get; }

        bool IsClickable { get; }
        bool IsDisabled { get; }
        bool IsOperated { get; }
        bool DisplayOnOverlay { get; } // !Disabled && !Operated

        // valid only for gold
        uint Amount { get; }

        int CreatedAtInGameTick { get; }

        IWatch LastSpeak { get; set; } // null by default to spare memory
    }
}