using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SharpDX;

namespace Turbo.Plugins.Default
{
    public class SkillPainter : ITransparentCollection
    {
        public bool Enabled { get; set; }
        public IController Hud { get; set; }

        public IFont CooldownFont { get; set; }
        public IFont SkillDamageFont { get; set; }
        public IFont SkillDpsFont { get; set; }
        public IBrush[] SkillDpsBackgroundBrushesByElementalType { get; set; }
        public IFont DamageBonusFont { get; set; }
        public IFont DamageBonusEliteFont { get; set; }

        public float TextureOpacity { get; set; }
        public bool EnableSkillDpsBar { get; set; }
        public bool EnableDetailedDpsHint { get; set; }

        private readonly IWatch _lastTownEliteSimulation;

        public SkillPainter(IController hud, bool setDefaultStyle)
        {
            Enabled = true;
            Hud = hud;

            _lastTownEliteSimulation = Hud.Time.CreateWatch();

            if (setDefaultStyle)
            {
                CooldownFont = Hud.Render.CreateFont("arial", 14, 255, 255, 255, 255, true, false, 255, 0, 0, 0, true);
                SkillDamageFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 200, 100, false, false, 160, 0, 0, 0, true);
                SkillDpsFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 255, 255, false, false, 160, 0, 0, 0, true);
                SkillDpsBackgroundBrushesByElementalType = new IBrush[]
                {
                    Hud.Render.CreateBrush(200, 255, 6, 0, 0),
                    Hud.Render.CreateBrush(200, 183, 57, 7, 0),
                    Hud.Render.CreateBrush(200, 0, 38, 119, 0),
                    Hud.Render.CreateBrush(200, 77, 102, 155, 0),
                    Hud.Render.CreateBrush(200, 50, 106, 21, 0),
                    Hud.Render.CreateBrush(200, 138, 0, 94, 0),
                    Hud.Render.CreateBrush(200, 190, 117, 0, 0),
                };

                DamageBonusFont = Hud.Render.CreateFont("tahoma", 7, 255, 200, 200, 200, false, false, 160, 0, 0, 0, true);
                DamageBonusEliteFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 160, 160, false, false, 160, 0, 0, 0, true);
            }
        }

        public void Paint(IPlayerSkill skill, RectangleF rect)
        {
            if (skill == null)
                return;

            if (TextureOpacity > 0)
            {
                var texture = Hud.Texture.GetTexture(skill.CurrentSnoPower.NormalIconTextureId);
                texture?.Draw(rect.X, rect.Y, rect.Width, rect.Height, TextureOpacity);
            }

            if (skill.IsOnCooldown)
            {
                var remainingSeconds = (skill.CooldownFinishTick - Hud.Game.CurrentGameTick) / 60.0d;
                var text = remainingSeconds > 1.0f
                    ? remainingSeconds.ToString("F0", CultureInfo.InvariantCulture)
                    : remainingSeconds.ToString("F1", CultureInfo.InvariantCulture);

                var textLayout = CooldownFont.GetTextLayout(text);
                var x = rect.X + ((rect.Width - (float)Math.Ceiling(textLayout.Metrics.Width)) / 2);
                var y = rect.Y + ((rect.Height - textLayout.Metrics.Height) / 2);

                CooldownFont.DrawText(textLayout, x, y);
            }

            if (!EnableSkillDpsBar || skill.CurrentSnoPower.ElementalDamageTypesByRune == null)
                return;

            var elite = Hud.Game.IsEliteOnScreen || Hud.Game.IsGoblinOnScreen;
            if (Hud.Game.IsInTown)
            {
                if (_lastTownEliteSimulation.TimerTest(3000))
                {
                    elite = true;
                    if (_lastTownEliteSimulation.TimerTest(6000))
                    {
                        _lastTownEliteSimulation.Restart();
                    }
                }
            }

            var elementalType = skill.ElementalType;
            if (elementalType == -1)
                return;

            var resourceCost = skill.ResourceCost;
            var weaponDamageMultiplier = skill.WeaponDamageMultiplier;
            if (weaponDamageMultiplier != -1)
            {
                var dotSeconds = skill.DotSeconds;

                var skillBonus = 1 + skill.DamageBonus;
                var elementalBonus = 1 + skill.ElementalDamageBonus;
                var eliteBonus = 1 + (elite ? Hud.Game.Me.Offense.BonusToElites : 0);

                var skillBonusTotal = skillBonus * elementalBonus * eliteBonus;

                double plainBonusTotal = 1;
                foreach (var bonus in Hud.Game.Me.Offense.PlainDamageBonuses)
                {
                    plainBonusTotal *= 1 + bonus.Item2;
                }

                var weaponDamage = Hud.Game.Me.Offense.MainHandIsActive ? Hud.Game.Me.Offense.WeaponDamageMainHand : Hud.Game.Me.Offense.WeaponDamageSecondHand;
                var attackSpeed = Hud.Game.Me.Offense.MainHandIsActive ? Hud.Game.Me.Offense.AttackSpeedMainHand : Hud.Game.Me.Offense.AttackSpeedOffHand;

                var skillDamage = weaponDamage * (weaponDamageMultiplier / 100.0f) * skillBonusTotal * plainBonusTotal;
                var skillDps = skillDamage;
                if (dotSeconds > 0)
                    skillDps /= dotSeconds;
                else
                    skillDps *= attackSpeed;

                var textLayout = SkillDamageFont.GetTextLayout(BasePlugin.ValueToString(skillDamage, ValueFormat.LongNumber));
                SkillDamageFont.DrawText(textLayout, rect.Left, rect.Bottom - textLayout.Metrics.Height + 1);

                var rHeight = rect.Height * 0.33f;
                var rTop = rect.Bottom + (rHeight * 0.1f);

                SkillDpsBackgroundBrushesByElementalType[elementalType].DrawRectangle(rect.Left, rTop, rect.Width, rHeight);

                textLayout = SkillDpsFont.GetTextLayout(BasePlugin.ValueToString(skillDps, ValueFormat.LongNumber));
                SkillDpsFont.DrawText(textLayout, rect.Left + ((rect.Width - textLayout.Metrics.Width) / 2), rTop + ((rHeight - textLayout.Metrics.Height) / 2));

                if (skillBonusTotal > 1)
                {
                    var bonusFont = elite ? DamageBonusEliteFont : DamageBonusFont;
                    textLayout = bonusFont.GetTextLayout("+" + ((skillBonusTotal * 100) - 100).ToString("F0") + "%");
                    bonusFont.DrawText(textLayout, rect.Left, rect.Top - 1);
                }

                if (EnableDetailedDpsHint && Hud.Window.CursorInsideRect(rect.Left, rect.Top, rect.Width, rect.Height))
                {
                    var plainBonusText = "";
                    var plainBonuses = Hud.Game.Me.Offense.PlainDamageBonuses.ToList();
                    if (plainBonuses.Count > 0)
                    {
                        plainBonusText = "\nPLAIN BONUSES: +" + ((plainBonusTotal * 100) - 100).ToString("F0", CultureInfo.InvariantCulture) + "%\n";
                        foreach (var bonus in plainBonuses)
                        {
                            plainBonusText += "\t" + (bonus.Item2 * 100).ToString("F0", CultureInfo.InvariantCulture) + "% " + bonus.Item1.Icons[0].TitleLocalized + "\n";
                        }
                    }

                    var hint =
                        "WEAPON DAMAGE: " + BasePlugin.ValueToString(weaponDamage, ValueFormat.LongNumber) + " (" + (Hud.Game.Me.Offense.MainHandIsActive ? "left" : "right") + " hand)\n" +
                        (Hud.Game.Me.Offense.MainHandIsActive ?
                        "\t" + ((Hud.Game.Me.Offense.WeaponBaseDamageMinAmainHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxAmainHand / 2)).ToString("F0", CultureInfo.InvariantCulture) + " (" + Hud.Game.Me.Offense.WeaponBaseDamageMinAmainHand.ToString("F0", CultureInfo.InvariantCulture) + "-" + Hud.Game.Me.Offense.WeaponBaseDamageMaxAmainHand.ToString("F0", CultureInfo.InvariantCulture) + ")\n" :
                        "\t" + ((Hud.Game.Me.Offense.WeaponBaseDamageMinAoffHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxAoffHand / 2)).ToString("F0", CultureInfo.InvariantCulture) + " (" + Hud.Game.Me.Offense.WeaponBaseDamageMinAoffHand.ToString("F0", CultureInfo.InvariantCulture) + "-" + Hud.Game.Me.Offense.WeaponBaseDamageMaxAoffHand.ToString("F0", CultureInfo.InvariantCulture) + ")\n") +
                        (((Hud.Game.Me.Offense.DamageMin > 0) || (Hud.Game.Me.Offense.DamageMax > 0)) ?
                        ((Hud.Game.Me.Offense.DamageMin > 0) || (Hud.Game.Me.Offense.DamageMax > 0) ? "\t+ damage from armor: " + Hud.Game.Me.Offense.DamageMin.ToString("F0", CultureInfo.InvariantCulture) + "-" + Hud.Game.Me.Offense.DamageMax.ToString("F0", CultureInfo.InvariantCulture) + "\n" : "") +
                        (Hud.Game.Me.Offense.MainHandIsActive ?
                        "\t= " + ((Hud.Game.Me.Offense.WeaponBaseDamageMinBmainHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxBmainHand / 2)).ToString("F0", CultureInfo.InvariantCulture) + " (" + Hud.Game.Me.Offense.WeaponBaseDamageMinBmainHand.ToString("F0", CultureInfo.InvariantCulture) + "-" + Hud.Game.Me.Offense.WeaponBaseDamageMaxBmainHand.ToString("F0", CultureInfo.InvariantCulture) + ")\n" :
                        "\t= " + ((Hud.Game.Me.Offense.WeaponBaseDamageMinBoffHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxBoffHand / 2)).ToString("F0", CultureInfo.InvariantCulture) + " (" + Hud.Game.Me.Offense.WeaponBaseDamageMinBoffHand.ToString("F0", CultureInfo.InvariantCulture) + "-" + Hud.Game.Me.Offense.WeaponBaseDamageMaxBoffHand.ToString("F0", CultureInfo.InvariantCulture) + ")\n")
                        : "") +
                        "\tincreased by main stat: " +
                        (Hud.Game.Me.Offense.MainHandIsActive ?
                        "" + BasePlugin.ValueToString(((Hud.Game.Me.Offense.WeaponBaseDamageMinBmainHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxBmainHand / 2)) * (1 + (Hud.Game.Me.Stats.MainStat / 100)), ValueFormat.LongNumber) + " (" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMinBmainHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)), ValueFormat.LongNumber) + "-" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMaxBmainHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)), ValueFormat.LongNumber) + ")\n" :
                        "" + BasePlugin.ValueToString(((Hud.Game.Me.Offense.WeaponBaseDamageMinBoffHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxBoffHand / 2)) * (1 + (Hud.Game.Me.Stats.MainStat / 100)), ValueFormat.LongNumber) + " (" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMinBoffHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)), ValueFormat.LongNumber) + "-" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMaxBoffHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)), ValueFormat.LongNumber) + ")\n") +
                        "\tincreased by crit: " +
                        (Hud.Game.Me.Offense.MainHandIsActive ?
                        "" + BasePlugin.ValueToString(((Hud.Game.Me.Offense.WeaponBaseDamageMinBmainHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxBmainHand / 2)) * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)), ValueFormat.LongNumber) + " (" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMinBmainHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)), ValueFormat.LongNumber) + "-" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMaxBmainHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)), ValueFormat.LongNumber) + ")\n" :
                        "" + BasePlugin.ValueToString(((Hud.Game.Me.Offense.WeaponBaseDamageMinBoffHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxBoffHand / 2)) * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)), ValueFormat.LongNumber) + " (" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMinBoffHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)), ValueFormat.LongNumber) + "-" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMaxBoffHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)), ValueFormat.LongNumber) + ")\n") +
                        ((Hud.Game.Me.Offense.WeaponDamageIncreasedBySkills > 0) ?
                        "\tincreased by skills: " +
                        (Hud.Game.Me.Offense.MainHandIsActive ?
                        "" + BasePlugin.ValueToString(((Hud.Game.Me.Offense.WeaponBaseDamageMinBmainHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxBmainHand / 2)) * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)) * (1 + Hud.Game.Me.Offense.WeaponDamageIncreasedBySkills), ValueFormat.LongNumber) + " (" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMinBmainHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)) * (1 + Hud.Game.Me.Offense.WeaponDamageIncreasedBySkills), ValueFormat.LongNumber) + "-" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMaxBmainHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)) * (1 + Hud.Game.Me.Offense.WeaponDamageIncreasedBySkills), ValueFormat.LongNumber) + ")\n" :
                        "" + BasePlugin.ValueToString(((Hud.Game.Me.Offense.WeaponBaseDamageMinBoffHand / 2) + (Hud.Game.Me.Offense.WeaponBaseDamageMaxBoffHand / 2)) * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)) * (1 + Hud.Game.Me.Offense.WeaponDamageIncreasedBySkills), ValueFormat.LongNumber) + " (" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMinBoffHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)) * (1 + Hud.Game.Me.Offense.WeaponDamageIncreasedBySkills), ValueFormat.LongNumber) + "-" + BasePlugin.ValueToString(Hud.Game.Me.Offense.WeaponBaseDamageMaxBoffHand * (1 + (Hud.Game.Me.Stats.MainStat / 100)) * (1 + (Hud.Game.Me.Offense.CriticalHitChance / 100 * Hud.Game.Me.Offense.CritDamage / 100)) * (1 + Hud.Game.Me.Offense.WeaponDamageIncreasedBySkills), ValueFormat.LongNumber) + ")\n")
                        : "") +
                        "\nTOTAL BONUS: +" + ((skillBonusTotal * 100) - 100).ToString("F0", CultureInfo.InvariantCulture) + "%\n" +
                        "\t" + (skill.ElementalDamageBonus * 100).ToString("F0", CultureInfo.InvariantCulture) + "% elemental\n" +
                        "\t" + (skill.DamageBonus * 100).ToString("F0", CultureInfo.InvariantCulture) + "% skill\n" +
                        (elite ? "\t" + (Hud.Game.Me.Offense.BonusToElites * 100).ToString("F0", CultureInfo.InvariantCulture) + "% elite\n" : "") +
                        plainBonusText +
                        "\n'" + skill.CurrentSnoPower.NameLocalized + "' / '" + skill.RuneNameLocalized + "' does " + weaponDamageMultiplier.ToString("F0", CultureInfo.InvariantCulture) + "% weapon damage" + (dotSeconds > 0 ? " over " + dotSeconds.ToString("F1", CultureInfo.InvariantCulture) + " seconds" : "") + "\n" +
                        "\tDMG: " + BasePlugin.ValueToString(skillDamage, ValueFormat.LongNumber) + " (= " + BasePlugin.ValueToString(weaponDamage, ValueFormat.LongNumber) + " * " + weaponDamageMultiplier.ToString("F0", CultureInfo.InvariantCulture) + "% + " + ((skillBonusTotal * 100) - 100).ToString("F0", CultureInfo.InvariantCulture) + "% + " + ((plainBonusTotal * 100) - 100).ToString("F0", CultureInfo.InvariantCulture) + "%)\n" +
                        "\tDPS: " + BasePlugin.ValueToString(skillDps, ValueFormat.LongNumber) + " (= " + BasePlugin.ValueToString(skillDamage, ValueFormat.LongNumber) + (dotSeconds > 0 ? " / " + dotSeconds.ToString("F1", CultureInfo.InvariantCulture) + " seconds" : " * " + attackSpeed.ToString("F2", CultureInfo.InvariantCulture) + " attack/s") + ")" +
                        ((resourceCost != -1) ? "\n\nResource cost\n\tdefault: " + BasePlugin.ValueToString(Math.Abs(resourceCost), ValueFormat.NormalNumberNoDecimal) + " " + (resourceCost > 0 ? Hud.Game.Me.HeroClassDefinition.PrimaryResourceName : Hud.Game.Me.HeroClassDefinition.SecondaryResourceName) + "\n\treal: " + BasePlugin.ValueToString(skill.GetResourceRequirement(), ValueFormat.NormalNumber) + " " + (resourceCost > 0 ? Hud.Game.Me.HeroClassDefinition.PrimaryResourceName : Hud.Game.Me.HeroClassDefinition.SecondaryResourceName) : "");
                    Hud.Render.SetHint(hint, "tooltip-bottom-right");
                }
            }
            else if (resourceCost != -1)
            {
                if (Hud.Window.CursorInsideRect(rect.Left, rect.Top, rect.Width, rect.Height))
                {
                    Hud.Render.SetHint("Resource cost\n\tdefault: " + BasePlugin.ValueToString(Math.Abs(resourceCost), ValueFormat.NormalNumberNoDecimal) + " " + (resourceCost > 0 ? Hud.Game.Me.HeroClassDefinition.PrimaryResourceName : Hud.Game.Me.HeroClassDefinition.SecondaryResourceName) + "\n\treal: " + BasePlugin.ValueToString(skill.GetResourceRequirement(), ValueFormat.NormalNumber) + " " + (resourceCost > 0 ? Hud.Game.Me.HeroClassDefinition.PrimaryResourceName : Hud.Game.Me.HeroClassDefinition.SecondaryResourceName), "tooltip-bottom-right");
                }
            }
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield return CooldownFont;
            yield return SkillDamageFont;
            yield return SkillDpsFont;
            foreach (var brush in SkillDpsBackgroundBrushesByElementalType)
                yield return brush;
            yield return DamageBonusFont;
            yield return DamageBonusEliteFont;
        }
    }
}