using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turbo.Plugins.Chuzuki
{
	public class SkillSlotArc : Arc
	{
		public enum BuffState { Active, Cooldown, Idle }
		public Dictionary<BuffState, Arc> Arcs { get; set; }

		public SkillSlotArc(IController Hud, IBrush Brush)
		{
			this.Hud = Hud;
			this.Brush = Brush;
		}

		public new void Paint(WorldLayer layer, IScreenCoordinate coord, float innerRadius, float outerRadius, double maxValue, double value, int startDeg = 0, int maxDeg = 360)
		{
			var me = Hud.Game.Me;
			var slot = me.Powers.SkillSlots.FirstOrDefault(s => s.SnoPower.Sno == Hud.Sno.SnoPowers.Crusader_IronSkin.Sno);
			if (slot == null) return;

			int cooldownLength = slot.CooldownFinishTick - slot.CooldownStartTick;
			//int cooldownLeft = slot.CooldownFinishTick - Hud.Game.CurrentGameTick;
			int cooldownElapsed = Hud.Game.CurrentGameTick - slot.CooldownStartTick;
			double buffElapsed = slot.Buff.TimeElapsedSeconds[0];
			double buffLength = slot.Buff.TimeLeftSeconds[0] + buffElapsed;
			double uptimeRatio = 60d * buffLength / (double)cooldownLength;
			int uptimeDeg = (int)(360 * uptimeRatio);
			int downtimeDeg = 360 - uptimeDeg;

			BuffState state = BuffState.Idle;

			if (me.Powers.BuffIsActive(Hud.Sno.SnoPowers.Crusader_IronSkin.Sno))
			{
				Arcs[BuffState.Active].Paint(layer, me.ScreenCoordinate, 15f, 1, buffLength, buffElapsed, 270 + uptimeDeg, uptimeDeg);
				Arcs[BuffState.Cooldown].Paint(layer, me.ScreenCoordinate, 15f, 1, 1, 0, 270, downtimeDeg);
				state = BuffState.Active;
			}
			else if (slot.IsOnCooldown)
			{
				Arcs[BuffState.Cooldown].Paint(layer, me.ScreenCoordinate, 15f, 1, cooldownLength, cooldownElapsed, 270, 360);
				state = BuffState.Cooldown;
			}
			else
			{
				buffLength = -1;
				state = BuffState.Idle;
			}
		}
	}
}
