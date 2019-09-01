using System.Linq;

namespace Turbo.Plugins.Default
{
    public class ClickableChestGizmoPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }
        public bool PaintOnlyWhenHarringtonWaistguardIsActive { get; set; } = true;

        public ClickableChestGizmoPlugin()
        {
            Enabled = false;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 230, 200, 230, 1),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 6.0f,
                    ShapePainter = new CrossShapePainter(Hud),
                },
                new GroundShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(192, 230, 200, 230, 2),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 2.0f,
                    ShapePainter = WorldStarShapePainter.NewCross(Hud),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            if (PaintOnlyWhenHarringtonWaistguardIsActive && !Hud.Game.Me.Powers.BuffIsActive(318881, 0))
                return;

            var actors = Hud.Game.Actors.Where(x => !x.IsDisabled && !x.IsOperated && x.GizmoType == GizmoType.Chest);
            foreach (var actor in actors)
            {
                Decorator.Paint(layer, actor, actor.FloorCoordinate, null);
            }
        }
    }
}