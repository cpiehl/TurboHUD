using System.Collections.Generic;

namespace Turbo.Plugins.Default
{
    public class PickupRangePlugin : BasePlugin, ITransparentCollection, IInGameWorldPainter
    {
        public IBrush FillBrush { get; set; }
        public IBrush OutlineBrush { get; set; }
        public IFader Fader { get; set; }

        public PickupRangePlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            FillBrush = Hud.Render.CreateBrush(3, 255, 255, 255, 0);
            OutlineBrush = Hud.Render.CreateBrush(12, 0, 0, 0, 3);
            Fader = new StandardFader(Hud, this);
        }

        public void PaintWorld(WorldLayer layer)
        {
            var visible = !Hud.Game.IsInTown && (Hud.Game.Me.AnimationState == AcdAnimationState.Running) && (Hud.Game.MapMode == MapMode.Minimap);
            if (!Fader.TestVisiblity(visible))
                return;

            OutlineBrush.DrawWorldEllipse(Hud.Game.Me.Stats.PickupRange, -1, Hud.Game.Me.FloorCoordinate);
            FillBrush.DrawWorldEllipse(Hud.Game.Me.Stats.PickupRange, -1, Hud.Game.Me.FloorCoordinate);
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield return FillBrush;
            yield return OutlineBrush;
        }
    }
}