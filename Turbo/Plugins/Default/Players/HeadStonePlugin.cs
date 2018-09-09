namespace Turbo.Plugins.Default
{

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
                    LabelFont = Hud.Render.CreateFont("tahoma", 6f, 255, 255, 100, 100, true, false, 128, 0, 0, 0, true),
                    Up = true,
                },
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 0, 0, 0, 0),
                    BorderBrush = Hud.Render.CreateBrush(200, 255, 100, 100, 1),
                    TextFont = Hud.Render.CreateFont("tahoma", 6f, 255, 255, 100, 100, true, false, 128, 0, 0, 0, true),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var headStones = Hud.Game.HeadStones;
            foreach (var headStone in headStones)
            {
                Decorator.Paint(layer, headStone, headStone.FloorCoordinate, headStone.Player.BattleTagAbovePortrait);
            }
        }

    }

}