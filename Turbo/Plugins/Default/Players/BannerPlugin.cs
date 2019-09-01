namespace Turbo.Plugins.Default
{
    public class BannerPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection Decorator { get; set; }

        public BannerPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            Decorator = new WorldDecoratorCollection(
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 200, 0, 0, 0),
                    BorderBrush = Hud.Render.CreateBrush(192, 255, 255, 255, 1),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 255, 255, 255, true, false, false),
                }
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var banners = Hud.Game.Banners;
            foreach (var banner in banners)
            {
                var onScreen = banner.FloorCoordinate.IsOnScreen();
                Decorator.ToggleDecorators<GroundLabelDecorator>(!onScreen);

                Decorator.Paint(layer, null, banner.FloorCoordinate, "BANNER");
            }
        }
    }
}