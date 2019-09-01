using System.Globalization;

namespace Turbo.Plugins.Default
{
    public class StandardItemRenderer : IItemRenderer
    {
        public IController Hud { get; }
        public IFont QuantityFont { get; set; }

        public StandardItemRenderer(IController hud)
        {
            Hud = hud;
            QuantityFont = Hud.Render.CreateFont("tahoma", 8, 255, 200, 200, 200, false, false, false);
            QuantityFont.SetShadowBrush(128, 0, 0, 0, true);
        }

        public void RenderItem(IItem item, System.Drawing.RectangleF rect)
        {
            var backroundTexture = Hud.Texture.GetItemBackgroundTexture(item);
            backroundTexture?.Draw(rect.X, rect.Y, rect.Width, rect.Height);

            var itemTexture = Hud.Texture.GetItemTexture(item.SnoItem);
            if (itemTexture == null)
                return;
            itemTexture.Draw(rect.X, rect.Y, rect.Width, rect.Height);

            var socketCount = item.SocketCount;
            if (socketCount > 0)
            {
                var socketTexture = Hud.Texture.EmptySocketTexture;
                var iw = rect.Width * 0.45f;
                var ih = iw;
                var ix = rect.X + ((rect.Width - iw) / 2.0f);
                var iy = rect.Y + ((rect.Height - ih) / 2.0f);
                switch (socketCount)
                {
                    case 1:
                        socketTexture.Draw(ix, iy, iw, ih, 0.5f);
                        break;
                    case 2:
                        socketTexture.Draw(ix, iy - (ih * 0.5f), iw, ih, 0.5f);
                        socketTexture.Draw(ix, iy + (ih * 0.5f), iw, ih, 0.5f);
                        break;
                    case 3:
                        socketTexture.Draw(ix, iy - (ih * 1.0f), iw, ih, 0.5f);
                        socketTexture.Draw(ix, iy, iw, ih, 0.5f);
                        socketTexture.Draw(ix, iy + (ih * 1.0f), iw, ih, 0.5f);
                        break;
                }

                if (item.ItemsInSocket?.Length > 0)
                {
                    for (var i = 0; i < item.ItemsInSocket.Length; i++)
                    {
                        var socketedItem = item.ItemsInSocket[i];
                        itemTexture = Hud.Texture.GetItemTexture(socketedItem.SnoItem);
                        if (itemTexture == null)
                            continue;
                        switch (socketCount)
                        {
                            case 1:
                                itemTexture.Draw(ix, iy, iw, ih, 0.5f);
                                break;
                            case 2:
                                if (i == 0)
                                    itemTexture.Draw(ix, iy - (ih * 0.5f), iw, ih, 0.5f);
                                else
                                    itemTexture.Draw(ix, iy + (ih * 0.5f), iw, ih, 0.5f);
                                break;
                            case 3:
                                if (i == 0)
                                    itemTexture.Draw(ix, iy - (ih * 1.0f), iw, ih, 0.5f);
                                else if (i == 1)
                                    itemTexture.Draw(ix, iy, iw, ih, 0.5f);
                                else
                                    itemTexture.Draw(ix, iy + (ih * 1.0f), iw, ih, 0.5f);
                                break;
                        }
                    }
                }
            }

            if (item.Unidentified)
            {
                var unidTexture = Hud.Texture.UnidTexture;
                var uw = rect.Width * 1.0f;
                var uh = uw;
                unidTexture.Draw(rect.X + ((rect.Width - uw) / 2), rect.Y + ((rect.Height - uh) / 2), uw, uh, 1.0f);
            }

            var rv = 32.0f / 600.0f * Hud.Window.Size.Height;
            DrawItemQuantity(item, rect, rv);
        }

        private void DrawItemQuantity(IItem item, System.Drawing.RectangleF rect, float rv)
        {
            if ((item.Location != ItemLocation.Inventory) && (item.Location != ItemLocation.Stash))
                return;
            if (item.Quantity <= 1)
                return;

            var textLayout = QuantityFont.GetTextLayout(item.Quantity.ToString("D", CultureInfo.InvariantCulture));
            QuantityFont.DrawText(textLayout, rect.Right - (rv / 20.0f) - textLayout.Metrics.Width, rect.Bottom - (rv / 70.0f) - textLayout.Metrics.Height);
        }
    }
}