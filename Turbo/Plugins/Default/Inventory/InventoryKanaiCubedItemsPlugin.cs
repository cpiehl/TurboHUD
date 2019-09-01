namespace Turbo.Plugins.Default
{
    public class InventoryKanaiCubedItemsPlugin : BasePlugin, IInGameTopPainter
    {
        public InventoryKanaiCubedItemsPlugin()
        {
            Enabled = true;
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.Inventory)
                return;

            if (Hud.Game.Me.CubeSnoItem1 != null)
                DrawKanaiItem(Hud.Game.Me.CubeSnoItem1, 0);
            if (Hud.Game.Me.CubeSnoItem2 != null)
                DrawKanaiItem(Hud.Game.Me.CubeSnoItem2, 1);
            if (Hud.Game.Me.CubeSnoItem3 != null)
                DrawKanaiItem(Hud.Game.Me.CubeSnoItem3, 2);
        }

        private void DrawKanaiItem(ISnoItem snoItem, int index)
        {
            var inventoryRect = Hud.Inventory.InventoryMainUiElement.Rectangle;

            var itemRect = Hud.Inventory.GetRectInInventory(0, 0, snoItem.ItemWidth, snoItem.ItemHeight);
            itemRect.Offset((index * inventoryRect.Width * 0.095f) + (inventoryRect.Width * 0.023f), -inventoryRect.Height * 0.502f);
            if (snoItem.ItemHeight == 1)
                itemRect.Offset(0, itemRect.Height * 0.5f);

            var slotTexture = Hud.Texture.InventorySlotTexture;
            slotTexture.Draw(itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height);

            if (Hud.Window.CursorInsideRect(itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height))
            {
                var description = snoItem.NameLocalized;

                var power = snoItem.LegendaryPower;
                if (power != null)
                {
                    description += "\n\n" + power.DescriptionLocalized;
                }

                Hud.Render.SetHint(description);
            }

            var backgroundTexture = snoItem.ItemHeight == 2 ? Hud.Texture.InventoryLegendaryBackgroundLarge : Hud.Texture.InventoryLegendaryBackgroundSmall;
            backgroundTexture.Draw(itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height);

            var itemTexture = Hud.Texture.GetItemTexture(snoItem);
            itemTexture?.Draw(itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height);
        }
    }
}