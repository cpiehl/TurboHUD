using System.Linq;

namespace Turbo.Plugins.Default
{
    // todo: rename to DeadPlayerNamesPlugin in the future
    public class HeadStonePlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }

        public HeadStonePlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new MapLabelDecorator(Hud)
                {
                    LabelFont = Hud.Render.CreateFont("tahoma", 7f, 255, 255, 100, 100, true, false, 0, 0, 0, 0, true),
                    Up = true,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 0, 0, 0, 0),
                    BorderBrush = Hud.Render.CreateBrush(200, 255, 100, 100, 1),
                    TextFont = Hud.Render.CreateFont("tahoma", 7f, 255, 255, 100, 100, true, false, 0, 0, 0, 0, true),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var deadPlayers = Hud.Game.Players
                .Where(player => !player.IsMe && player.CoordinateKnown && player.IsDeadSafeCheck);

            foreach (var player in deadPlayers)
            {
                Decorator.Paint(layer, player, player.FloorCoordinate, player.BattleTagAbovePortrait);
            }
        }
    }
}