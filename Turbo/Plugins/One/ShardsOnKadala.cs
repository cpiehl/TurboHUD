using System;
using System.Collections.Generic;
using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.One
{
    public class ShardsOnKadala : BasePlugin, IInGameWorldPainter
    {
        public TopLabelDecorator KadalaDecorator { get; set; }
        private bool showOk { get; set; }
        private bool showWarning { get; set; }
        private int limitBad { get; set; }
        private int limitWarning { get; set; }
        private string kadalaText { get; set; }

        public ShardsOnKadala()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            showOk = true;
            showWarning = true;
            limitBad = 300; // last {limitBad} space shards show as Bad
            limitWarning = 500; // show Warning between last {limitWarning} and {limitBad} space shards

            KadalaDecorator = new TopLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 8, 200, 255, 255, 255, true, false, false),
                TextFunc = () => kadalaText,
            };
        }

        public void PaintWorld(WorldLayer layer)
        {
            if (!Hud.Game.IsInTown) return;
            var kadalaPos = Hud.Game.Actors.Where(x => x.SnoActor.Sno == ActorSnoEnum._x1_randomitemnpc && x.IsOnScreen);
            var maxShards = 500 + (Hud.Game.Me.HighestSoloRiftLevel * 10);
            var actualShards = Hud.Game.Me.Materials.BloodShard;
            kadalaText = "" + actualShards + "/" + maxShards;
            var KadalaTextureBad = Hud.Texture.GetTexture("Tooltip_Equipped_Frame_Title_Primal");
            var KadalaTextureWarning = Hud.Texture.GetTexture("Tooltip_Equipped_Frame_Title_Ancient");
            var KadalaTextureOk = Hud.Texture.GetTexture("Tooltip_Equipped_Frame_Title");
            var Warning = Hud.Texture.GetTexture("MarkerExclamation");
            var ScreenWidth = Hud.Window.Size.Width;
            var ScreenHeight = Hud.Window.Size.Height;
            foreach (var actor in kadalaPos)
            {
                if (maxShards - actualShards <= limitBad)
                {
                    Warning.Draw(actor.ScreenCoordinate.X - (ScreenWidth / 75), actor.ScreenCoordinate.Y - (ScreenWidth / 15), (float)(ScreenWidth / 30), (float)(ScreenWidth / 30), 1f);
                    KadalaTextureBad.Draw(actor.ScreenCoordinate.X - (ScreenWidth / 25), actor.ScreenCoordinate.Y + (ScreenWidth / 45), (float)(ScreenWidth / 12), (float)(ScreenHeight / 28), 1f);
                    KadalaDecorator.Paint(actor.ScreenCoordinate.X - (ScreenWidth / 25), actor.ScreenCoordinate.Y + (ScreenWidth / 41), (float)(ScreenWidth / 12), (float)(ScreenHeight / 28), HorizontalAlign.Center);
                }
                else if (maxShards - actualShards > limitBad && maxShards - actualShards <= limitWarning)
                {
                    KadalaTextureWarning.Draw(actor.ScreenCoordinate.X - (ScreenWidth / 25), actor.ScreenCoordinate.Y + (ScreenWidth / 45), (float)(ScreenWidth / 12), (float)(ScreenHeight / 28), 1f);
                    KadalaDecorator.Paint(actor.ScreenCoordinate.X - (ScreenWidth / 25), actor.ScreenCoordinate.Y + (ScreenWidth / 41), (float)(ScreenWidth / 12), (float)(ScreenHeight / 28), HorizontalAlign.Center);
                }
                else if (maxShards - actualShards > limitWarning)
                {
                    KadalaTextureOk.Draw(actor.ScreenCoordinate.X - (ScreenWidth / 25), actor.ScreenCoordinate.Y + (ScreenWidth / 45), (float)(ScreenWidth / 12), (float)(ScreenHeight / 28), 1f);
                    KadalaDecorator.Paint(actor.ScreenCoordinate.X - (ScreenWidth / 25), actor.ScreenCoordinate.Y + (ScreenWidth / 46), (float)(ScreenWidth / 12), (float)(ScreenHeight / 28), HorizontalAlign.Center);
                }
                else return;
            }
        }
    }
}
