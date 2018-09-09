using System.Collections.Generic;
using System.Linq;

namespace Turbo.Plugins.Default
{

    public class BuffRuleCalculator
    {

        public IController Hud { get; private set; }

        public List<BuffRule> Rules { get; private set; }
        public List<BuffPaintInfo> PaintInfoList { get; private set; }

        public float SizeMultiplier { get; set; }

        public BuffRuleCalculator(IController hud)
        {
            Hud = hud;
            Rules = new List<BuffRule>();
            PaintInfoList = new List<BuffPaintInfo>();
        }

        public void CalculatePaintInfo(IPlayer player)
        {
            var iconSize = StandardIconSize;

            PaintInfoList.Clear();
            foreach (var rule in Rules)
            {
                GetPaintInfo(player, PaintInfoList, rule, iconSize);
            }
        }

        public void CalculatePaintInfo(IPlayer player, IEnumerable<BuffRule> customRules)
        {
            var iconSize = StandardIconSize;

            PaintInfoList.Clear();
            foreach (var rule in customRules)
            {
                GetPaintInfo(player, PaintInfoList, rule, iconSize);
            }
        }

        public float StandardIconSize
        {
            get
            {
                return 55f / 1200.0f * Hud.Window.Size.Height * SizeMultiplier;
            }
        }

        public float StandardIconSpacing
        {
            get
            {
                return 3.0f / 1200.0f * Hud.Window.Size.Height * SizeMultiplier;
            }
        }

        private void GetPaintInfo(IPlayer player, List<BuffPaintInfo> container, BuffRule rule, float iconSize)
        {
            var buff = player.Powers.GetBuff(rule.PowerSno);
            if (buff == null || !buff.Active)
            {
                return;
            }

            for (int iconIndex = 0; iconIndex < buff.TimeLeftSeconds.Length; iconIndex++)
            {
                var timeLeft = buff.TimeLeftSeconds[iconIndex];
                if (timeLeft < 0) timeLeft = 0;

                if ((rule.IconIndex != null) && (iconIndex != rule.IconIndex.Value)) continue;
                if ((rule.NoIconIndex != null) && (iconIndex == rule.IconIndex.Value)) continue;
                if (buff.IconCounts[iconIndex] < rule.MinimumIconCount) continue;

                var stacks = rule.ShowStacks ? buff.IconCounts[iconIndex] : -1;
                if (!rule.ShowTimeLeft) timeLeft = 0;

                var icon = buff.SnoPower.Icons[iconIndex];

                if (!icon.MergesTooltip || !rule.AllowInGameMergeRules)
                {
                    var id = (buff.SnoPower.Sno << 32) + (uint)iconIndex;
                    if (container.Any(x => x.Id == id)) return;

                    var info = new BuffPaintInfo()
                    {
                        Id = id,
                        SnoPower = buff.SnoPower,
                        Icons = new List<SnoPowerIcon>() { icon },
                        TimeLeft = timeLeft,
                        Elapsed = buff.TimeElapsedSeconds[iconIndex],
                        Stacks = stacks,
                        Rule = rule,
                    };
                    if (info.Rule != null && info.Rule.UseLegendaryItemTexture != null)
                    {
                        if (info.Rule.UseLegendaryItemTexture.ItemHeight == 1)
                        {
                            info.BackgroundTexture = info.Rule.UseLegendaryItemTexture.SetItemBonusesSno == uint.MaxValue ? Hud.Texture.InventoryLegendaryBackgroundSmall : Hud.Texture.InventorySetBackgroundSmall;
                        }
                        else
                        {
                            info.BackgroundTexture = info.Rule.UseLegendaryItemTexture.SetItemBonusesSno == uint.MaxValue ? Hud.Texture.InventoryLegendaryBackgroundLarge : Hud.Texture.InventorySetBackgroundLarge;
                        }
                    }
                    info.Texture = GetIconTexture(info);
                    info.Size = iconSize * rule.IconSizeMultiplier;
                    if (info.Texture != null)
                    {
                        container.Add(info);
                    }
                }
                else
                {
                    var id = (buff.SnoPower.Sno << 32) + icon.MergesTooltipIndex;
                    var info = container.FirstOrDefault(x => x.Id == id);
                    if (info != null)
                    {
                        info.Icons.Add(icon);
                    }
                }
            }
        }

        private ITexture GetIconTexture(BuffPaintInfo info)
        {
            uint textureId = 0;
            if (info.Rule != null)
            {
                if (info.Rule.UseLegendaryItemTexture != null)
                {
                    return Hud.Texture.GetItemTexture(info.Rule.UseLegendaryItemTexture);
                }

                textureId = info.SnoPower.NormalIconTextureId;
                if (!info.Rule.UsePowersTexture && info.Icons[0].Exists && (info.Icons[0].TextureId != 0)) textureId = info.Icons[0].TextureId;
            }
            else
            {
                textureId = info.Icons[0].TextureId;
            }
            if (textureId <= 0) return null;

            return Hud.Texture.GetTexture(textureId);
        }

    }

}