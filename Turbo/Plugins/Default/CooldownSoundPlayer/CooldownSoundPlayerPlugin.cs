using System;
using System.Collections.Generic;
using System.Threading;

namespace Turbo.Plugins.Default
{
    public class CooldownSoundPlayerPlugin : BasePlugin, ISkillCooldownHandler
    {
        public bool EnableOnlyWhenIngameSoundIsEnabled { get; set; } = true;
        public Dictionary<ISnoPower, CoolDownRule> CoolDownRules { get; } = new Dictionary<ISnoPower, CoolDownRule>();

        public CooldownSoundPlayerPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            // AddRule(Hud.Sno.SnoPowers.Wizard_ArchonTeleport, "notification_1.wav");
        }

        public void RemoveRule(ISnoPower snoPower)
        {
            if (!CoolDownRules.ContainsKey(snoPower))
                return;

            var rule = CoolDownRules[snoPower];
            rule.DisposeSoundPlayer();

            CoolDownRules.Remove(snoPower);
        }

        public void AddRule(ISnoPower snoPower, string fileName)
        {
            var soundPlayer = Hud.Sound.LoadSoundPlayer(fileName);
            if (soundPlayer == null)
            {
                return;
            }

            var rule = new CoolDownRule(snoPower, soundPlayer);
            CoolDownRules.Add(snoPower, rule);
        }

        public void OnCooldown(IPlayerSkill playerSkill, bool expired)
        {
            if (!expired)
                return;
            if (playerSkill.Player != Hud.Game.Me)
                return;
            if (EnableOnlyWhenIngameSoundIsEnabled && !Hud.Sound.IsIngameSoundEnabled)
                return;

            if (!CoolDownRules.TryGetValue(playerSkill.CurrentSnoPower, out var rule))
                return;
            if (rule == null)
                return;

            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    rule.SoundPlayer.PlaySync();
                }
                catch (Exception)
                {
                }
            });
        }
    }
}