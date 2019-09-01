namespace Turbo.Plugins.Default
{
    public class StashPreviewPlugin : BasePlugin, IInGameTopPainter
    {
        public IItemRenderer ItemRenderer { get; set; }

        public StashPreviewPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
            ItemRenderer = new StandardItemRenderer(Hud);
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden)
                return;
            if (clipState != ClipState.Inventory)
                return;

            var uiElement = Hud.Inventory.StashMainUiElement;
            if (!uiElement.Visible)
                return;

            var selectedPage = Hud.Inventory.SelectedStashPageIndex;
            var hoveredTab = Hud.Inventory.HoveredStashTabIndex;
            if (hoveredTab == -1)
                return;

            hoveredTab += selectedPage * Hud.Inventory.MaxStashTabCountPerPage;

            var offsetX = uiElement.Rectangle.Left + (uiElement.Rectangle.Width * 0.95f);
            var offsetY = 0.0f;

            var slotTexture = Hud.Texture.InventorySlotTexture;
            for (var row = 0; row < 10; row++)
            {
                for (var col = 0; col < 7; col++)
                {
                    var rect = Hud.Inventory.GetRectInStash(col, row, 1, 1);
                    slotTexture.Draw(rect.X + offsetX, rect.Y + offsetY, rect.Width, rect.Height);
                }
            }

            foreach (var item in Hud.Inventory.ItemsInStash)
            {
                var tabIndex = item.InventoryY / 10;
                if (tabIndex != hoveredTab)
                    continue;

                var rect = Hud.Inventory.GetItemRect(item);
                rect.Offset(offsetX, offsetY);

                ItemRenderer.RenderItem(item, rect);
            }
        }
    }
}