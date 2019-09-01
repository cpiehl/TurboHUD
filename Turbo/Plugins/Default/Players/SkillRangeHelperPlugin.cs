namespace Turbo.Plugins.Default
{
    public class SkillRangeHelperPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection[] DecoratorsByElementalType { get; set; }

        public SkillRangeHelperPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            DecoratorsByElementalType = new WorldDecoratorCollection[] {
            // physical
            new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud) { Brush = Hud.Render.CreateBrush(132, 235, 6, 0, -3), }
            ),
            // fire
            new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud) { Brush = Hud.Render.CreateBrush(132, 183, 57, 7, -3), }
            ),
            // lightning
            new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud) { Brush = Hud.Render.CreateBrush(132, 0, 68, 149, -3), }
            ),
            // cold
            new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud) { Brush = Hud.Render.CreateBrush(132, 77, 102, 155, -3), }
            ),
            // poison
            new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud) { Brush = Hud.Render.CreateBrush(132, 70, 126, 41, -3), }
            ),
            // arcane
            new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud) { Brush = Hud.Render.CreateBrush(132, 158, 20, 114, -3), }
            ),
            // holy
            new WorldDecoratorCollection(
                new GroundCircleDecorator(Hud) { Brush = Hud.Render.CreateBrush(132, 190, 117, 0, -3), }
            )
            };
        }

        public void PaintWorld(WorldLayer layer)
        {
            if (!Hud.Game.IsInTown)
                return;
            if (Hud.Render.UiHidden)
                return;
            if ((Hud.Game.MapMode == MapMode.WaypointMap) || (Hud.Game.MapMode == MapMode.ActMap) || (Hud.Game.MapMode == MapMode.Map))
                return;

            IPlayerSkill hoveredSkill = null;
            foreach (var skill in Hud.Game.Me.Powers.CurrentSkills)
            {
                var ui = Hud.Render.GetPlayerSkillUiElement(skill.Key);
                if (Hud.Window.CursorInsideRect(ui.Rectangle.X, ui.Rectangle.Y, ui.Rectangle.Width, ui.Rectangle.Height))
                {
                    hoveredSkill = skill;
                    break;
                }
            }

            if (hoveredSkill == null)
                return;

            var range = Hud.Game.Me.GetPowerTagValue(hoveredSkill.CurrentSnoPower, 329808);
            if (range <= 0)
                return;

            var elementalType = hoveredSkill.ElementalType;
            if (elementalType < 0)
                return;

            var currentDecorator = DecoratorsByElementalType[elementalType];
            foreach (var subDecorator in currentDecorator.GetDecorators<GroundCircleDecorator>())
            {
                subDecorator.Radius = range;
            }

            currentDecorator.Paint(layer, null, Hud.Game.Me.FloorCoordinate, null);
        }
    }
}