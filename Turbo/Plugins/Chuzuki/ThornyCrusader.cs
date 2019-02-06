using System;
using System.Collections.Generic;
using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Chuzuki
{
	public class ThornyCrusader : BasePlugin, IInGameWorldPainter
	{
		public enum BuffState { Active, Cooldown, Idle }
		public Dictionary<BuffState, WorldDecoratorCollection> Decorators { get; set; }
		public Dictionary<BuffState, Arc> Arcs { get; set; }

		private double buffLength = -1;
		private float pixelsPerYardX = -1;
		private float pixelsPerYardY = -1;

        public ThornyCrusader()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
			InitDecorators();

			int pixels = 100;
			var pos1 = Hud.Game.Me.ScreenCoordinate.ToWorldCoordinate();
			var screenCoords = Hud.Game.Me.ScreenCoordinate;
			screenCoords.X += pixels;
			var pos2 = screenCoords.ToWorldCoordinate();
			screenCoords.X -= pixels;
			screenCoords.Y += pixels;
			var pos3 = screenCoords.ToWorldCoordinate();
			pixelsPerYardX = pixels / pos1.XYDistanceTo(pos2);
			pixelsPerYardY = pixels / pos1.XYDistanceTo(pos3);
			Arc.pixelsPerYardX = pixelsPerYardX;
			Arc.pixelsPerYardY = pixelsPerYardY;
        }

		private void InitDecorators()
		{
			Decorators = new Dictionary<BuffState, WorldDecoratorCollection>();
			Decorators[BuffState.Idle] = GetDecorator(new Color(255, 255, 64), 15f);
			Decorators[BuffState.Active] = GetDecorator(new Color(64, 255, 64), 15f);
			Decorators[BuffState.Cooldown] = GetDecorator(new Color(255, 64, 64), 15f);

			Arcs = new Dictionary<BuffState, Arc>();
			Arcs[BuffState.Cooldown] = new Arc(Hud, Hud.Render.CreateBrush(176, 255, 64, 64, 2));
			Arcs[BuffState.Active] = new Arc(Hud, Hud.Render.CreateBrush(176, 64, 255, 64, 2));
			Arcs[BuffState.Idle] = new Arc(Hud, Hud.Render.CreateBrush(176, 255, 255, 64, 2));
		}

		private WorldDecoratorCollection GetDecorator(Color color, float radius)
		{
			int stroke = (radius == -1) ? 0 : 2;
			return new WorldDecoratorCollection
			(
				new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(color.A, color.R, color.G, color.B, stroke),
                    Radius = radius,
				},
				new GroundLabelDecorator(Hud)
				{
					BackgroundBrush = Hud.Render.CreateBrush(255, 0, 0, 0, 0),
					TextFont = Hud.Render.CreateFont("tahoma", 12.0f, 255, 255, 255, 255, false, false, false),
				}
			);
		}

		public void PaintWorld(WorldLayer layer)
		{
			var me = Hud.Game.Me;
			var slot = me.Powers.SkillSlots.FirstOrDefault(s => s.SnoPower.Sno == Hud.Sno.SnoPowers.Crusader_IronSkin.Sno);
			if (slot == null ) return;

			int cooldownLength = slot.CooldownFinishTick - slot.CooldownStartTick;
			int cooldownLeft = slot.CooldownFinishTick - Hud.Game.CurrentGameTick;
			int cooldownElapsed = Hud.Game.CurrentGameTick - slot.CooldownStartTick;
			double buffElapsed = slot.Buff.TimeElapsedSeconds[0];
			double buffLength = slot.Buff.TimeLeftSeconds[0] + buffElapsed;
			double uptimeRatio = 60d * buffLength / (double)cooldownLength;
			int uptimeDeg = (int)(360 * uptimeRatio);
			int downtimeDeg = 360 - uptimeDeg;

			BuffState state = BuffState.Idle;

			if (me.Powers.BuffIsActive(Hud.Sno.SnoPowers.Crusader_IronSkin.Sno))
			{
				Arcs[BuffState.Active].Paint(layer, me.FloorCoordinate.ToScreenCoordinate(), 15f, 40, buffLength, buffElapsed, 270 + uptimeDeg, uptimeDeg);
				Arcs[BuffState.Active].Paint(layer, me.FloorCoordinate.ToScreenCoordinate(), 15f, 15, 1, 0, 270, downtimeDeg);
				Arcs[BuffState.Idle].Paint(layer, me.FloorCoordinate.ToScreenCoordinate(), 15f, 3, cooldownLength, cooldownLeft, 270, -360);
				state = BuffState.Active;
			}
			else if (slot.IsOnCooldown)
			{
				Arcs[BuffState.Cooldown].Paint(layer, me.FloorCoordinate.ToScreenCoordinate(), 15f, 15, cooldownLength, cooldownElapsed, 270, 360);
				Arcs[BuffState.Idle].Paint(layer, me.FloorCoordinate.ToScreenCoordinate(), 15f, 3, cooldownLength, cooldownLeft, 270, -360);
				state = BuffState.Cooldown;
			}
			else
			{
				Arcs[BuffState.Idle].Paint(layer, me.FloorCoordinate.ToScreenCoordinate(), 15f, 3, 1, 0, 270, -360);
				state = BuffState.Idle;
			}

			//Decorators[state].Paint(layer, me, me.FloorCoordinate, slot.CooldownStartTick.ToString() + " " + slot.CooldownFinishTick.ToString() + " " + cooldownLength.ToString() + " " + cooldownLeft.ToString());
			//Decorators[state].Paint(layer, me, me.FloorCoordinate, Hud.Stat.RenderPerfCounter.LastValue.ToString("F0") + " (" + Hud.Stat.RenderPerfCounter.LastCount.ToString("F0") + " FPS)");



			//Decorators[state].Paint(layer, me, me.FloorCoordinate, "");
		}
	}
}
