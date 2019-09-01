using System;
using System.Globalization;
using System.Linq;

namespace Turbo.Plugins.Default
{
    public class InventoryAndStashPlugin : BasePlugin, IInGameTopPainter
    {
        public IBrush ShadowBrush { get; set; }
        public IFont HoradricCacheFont { get; set; }
        public IBrush KeepBrush { get; set; }
        public IBrush ForceSellBrush { get; set; }
        public IBrush DarkenBrush { get; set; }
        public IFont QuantityFont { get; set; }
        public IBrush InventoryLockBorderBrush { get; set; }

        public bool LooksGoodDisplayEnabled { get; set; }
        public bool NotGoodDisplayEnabled { get; set; } = true;
        public bool DefinitelyBadDisplayEnabled { get; set; }

        public bool HoradricCacheEnabled { get; set; } = true;
        public bool CanCubedEnabled { get; set; } = true;

        public bool AncientRankEnabled { get; set; } = true;
        public IFont AncientRankFont { get; set; }
        public IFont PrimalRankFont { get; set; }

        public bool CaldesannRankEnabled { get; set; } = true;

        public bool SocketedLegendaryGemRankEnabled { get; set; } = true;
        public IFont SocketedLegendaryGemRankFont { get; set; }

        private IWatch _watch;

        public InventoryAndStashPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            ShadowBrush = Hud.Render.CreateBrush(175, 0, 0, 0, -1.6f);
            HoradricCacheFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 255, false, false, false);
            HoradricCacheFont.SetShadowBrush(128, 0, 0, 0, true);
            KeepBrush = Hud.Render.CreateBrush(255, 128, 255, 84, -1.6f);
            ForceSellBrush = Hud.Render.CreateBrush(255, 255, 0, 0, -1.6f);
            DarkenBrush = Hud.Render.CreateBrush(178, 38, 38, 38, 0);

            AncientRankFont = Hud.Render.CreateFont("arial", 7, 255, 0, 0, 0, true, false, 220, 227, 153, 25, true);
            PrimalRankFont = Hud.Render.CreateFont("arial", 7, 255, 0, 0, 0, true, false, 180, 255, 64, 64, true);

            SocketedLegendaryGemRankFont = Hud.Render.CreateFont("arial", 7, 255, 0, 0, 0, true, false, false);
            SocketedLegendaryGemRankFont.SetShadowBrush(128, 240, 240, 64, true);

            QuantityFont = Hud.Render.CreateFont("tahoma", 8, 255, 200, 200, 200, false, false, false);
            QuantityFont.SetShadowBrush(128, 0, 0, 0, true);

            InventoryLockBorderBrush = Hud.Render.CreateBrush(100, 0, 150, 200, -1.6f);

            _watch = Hud.Time.CreateAndStartWatch();
        }

        private int stashPage, stashTab, stashTabAbs;
        private float rv;

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.Inventory)
                return;

            stashTab = Hud.Inventory.SelectedStashTabIndex;
            stashPage = Hud.Inventory.SelectedStashPageIndex;
            stashTabAbs = stashTab + (stashPage * Hud.Inventory.MaxStashTabCountPerPage);

            rv = 32.0f / 600.0f * Hud.Window.Size.Height;

            if ((Hud.Inventory.InventoryLockArea.Width > 0) && (Hud.Inventory.InventoryLockArea.Height > 0))
            {
                var rect = Hud.Inventory.GetRectInInventory(Hud.Inventory.InventoryLockArea.X, Hud.Inventory.InventoryLockArea.Y, Hud.Inventory.InventoryLockArea.Width, Hud.Inventory.InventoryLockArea.Height);
                InventoryLockBorderBrush.DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height);
            }

            var items = Hud.Game.Items.Where(x => x.Location != ItemLocation.Merchant && x.Location != ItemLocation.Floor);
            foreach (var item in items)
            {
                if (item.Location == ItemLocation.Stash)
                {
                    var tabIndex = item.InventoryY / 10;
                    if (tabIndex != stashTabAbs)
                        continue;
                }

                if ((item.InventoryX < 0) || (item.InventoryY < 0))
                    continue;

                var rect = Hud.Inventory.GetItemRect(item);
                if (rect == System.Drawing.RectangleF.Empty)
                    continue;

                DrawItemLooksGood(item, rect);
                DrawItemSocketedLegendaryGemRank(item, rect);
                DrawItemAncientRank(item, rect);
                DrawItemHoradricCache(item, rect);
                DrawItemNotGood(item, rect);
                DrawItemCanCubed(item, rect);
            }
        }

        private void DrawItemLooksGood(IItem item, System.Drawing.RectangleF rect)
        {
            if (!LooksGoodDisplayEnabled)
                return;
            if ((item.Location != ItemLocation.Inventory) && (item.Location != ItemLocation.Stash))
                return;

            if (!item.Unidentified && (item.KeepDecision == ItemKeepDecision.LooksGood) && !item.IsInventoryLocked)
            {
                if (item.Location == ItemLocation.Stash)
                {
                    if (item.SnoItem.Kind == ItemKind.gem)
                        return;
                    if (item.SnoItem.Kind == ItemKind.craft)
                        return;
                    if (item.SnoItem.Kind == ItemKind.uberstuff)
                        return;
                    if (item.SnoItem.MainGroupCode == "riftkeystone")
                        return;
                    if (item.SnoItem.MainGroupCode == "gems_unique")
                        return;
                    if (item.SnoItem.MainGroupCode == "consumable")
                        return;
                    if (item.SnoItem.MainGroupCode == "horadriccache")
                        return;
                }

                Hud.Render.TurnOnAliasing();
                try
                {
                    ShadowBrush.DrawLine((float)Math.Round(rect.X + (rv / 10.0f)) - 2, (float)Math.Round(rect.Y + (rv / 10.0f)) + 2, (float)Math.Round(rect.X + (rv / 3.0f)) + 1, (float)Math.Round(rect.Y + (rv / 10.0f)) + 2, 2);
                    ShadowBrush.DrawLine((float)Math.Round(rect.X + (rv / 10.0f)), (float)Math.Round(rect.Y + (rv / 10.0f)) + 2 - 2, (float)Math.Round(rect.X + (rv / 10.0f)), (float)Math.Round(rect.Y + (rv / 3.0f)) + 2 + 1, 2);
                    KeepBrush.DrawLine((float)Math.Round(rect.X + (rv / 10.0f)) - 1, (float)Math.Round(rect.Y + (rv / 10.0f)) + 2, (float)Math.Round(rect.X + (rv / 3.0f)), (float)Math.Round(rect.Y + (rv / 10.0f)) + 2);
                    KeepBrush.DrawLine((float)Math.Round(rect.X + (rv / 10.0f)), (float)Math.Round(rect.Y + (rv / 10.0f)) + 2 - 1, (float)Math.Round(rect.X + (rv / 10.0f)), (float)Math.Round(rect.Y + (rv / 3.0f)) + 2);
                }
                finally
                {
                    Hud.Render.TurnOffAliasing();
                }
            }
        }

        private void DrawItemSocketedLegendaryGemRank(IItem item, System.Drawing.RectangleF rect)
        {
            if (!SocketedLegendaryGemRankEnabled)
                return;
            if (item.ItemsInSocket == null)
                return;

            var legendaryGem = Array.Find(item.ItemsInSocket, x => x.Quality == ItemQuality.Legendary && x.JewelRank > -1);
            if (legendaryGem == null)
                return;

            var jewelRank = legendaryGem.JewelRank;
            if (jewelRank > -1)
            {
                var text = jewelRank.ToString("D", CultureInfo.InvariantCulture);
                var layout = SocketedLegendaryGemRankFont.GetTextLayout(text);
                SocketedLegendaryGemRankFont.DrawText(layout, rect.X, rect.Y);
            }
        }

        private void DrawItemAncientRank(IItem item, System.Drawing.RectangleF rect)
        {
            if (!AncientRankEnabled)
                return;
            var ancientRank = item.AncientRank;
            if (ancientRank >= 1)
            {
                var caldesannRank = CaldesannRankEnabled ? item.CaldesannRank : 0;

                var ancientRankText = ancientRank == 1 ? "A" : "P";
                var font = ancientRank == 1 ? AncientRankFont : PrimalRankFont;

                var text = ancientRankText + (caldesannRank > 0 ? ("+" + caldesannRank.ToString("D", CultureInfo.InvariantCulture)) : "");
                var textLayout = font.GetTextLayout(text);
                font.DrawText(textLayout, rect.Right - (rv / 15.0f) - textLayout.Metrics.Width, rect.Bottom - (rv / 35.0f) - textLayout.Metrics.Height);
            }
        }

        private void DrawItemNotGood(IItem item, System.Drawing.RectangleF rect)
        {
            if (!NotGoodDisplayEnabled && !DefinitelyBadDisplayEnabled)
                return;
            if ((item.Location != ItemLocation.Inventory) && (item.Location != ItemLocation.Stash))
                return;

            var locked = item.IsInventoryLocked;
            if (!locked && !item.Unidentified && (item.KeepDecision != ItemKeepDecision.LooksGood))
            {
                if (NotGoodDisplayEnabled)
                {
                    DarkenBrush.DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height);
                }

                if (DefinitelyBadDisplayEnabled && item.KeepDecision == ItemKeepDecision.DefinitelyBad)
                {
                    Hud.Render.TurnOnAliasing();
                    try
                    {
                        ShadowBrush.DrawLine((float)Math.Round(rect.X + (rv / 10.0f)) - 2, (float)Math.Round(rect.Y + (rv / 10.0f)) + 2, (float)Math.Round(rect.X + (rv / 3.0f)) + 1, (float)Math.Round(rect.Y + (rv / 10.0f)) + 2, 2);
                        ShadowBrush.DrawLine((float)Math.Round(rect.X + (rv / 10.0f)), (float)Math.Round(rect.Y + (rv / 10.0f)) + 2 - 2, (float)Math.Round(rect.X + (rv / 10.0f)), (float)Math.Round(rect.Y + (rv / 3.0f)) + 2 + 1, 2);
                        ForceSellBrush.DrawLine((float)Math.Round(rect.X + (rv / 10.0f)) - 1, (float)Math.Round(rect.Y + (rv / 10.0f)) + 2, (float)Math.Round(rect.X + (rv / 3.0f)), (float)Math.Round(rect.Y + (rv / 10.0f)) + 2);
                        ForceSellBrush.DrawLine((float)Math.Round(rect.X + (rv / 10.0f)), (float)Math.Round(rect.Y + (rv / 10.0f)) + 2 - 1, (float)Math.Round(rect.X + (rv / 10.0f)), (float)Math.Round(rect.Y + (rv / 3.0f)) + 2);
                    }
                    finally
                    {
                        Hud.Render.TurnOffAliasing();
                    }
                }
            }
        }

        private void DrawItemCanCubed(IItem item, System.Drawing.RectangleF rect)
        {
            if (!CanCubedEnabled)
                return;
            if ((item.Location != ItemLocation.Inventory) && (item.Location != ItemLocation.Stash))
                return;
            var allowCube = item.SnoItem.CanKanaiCube && !Hud.Game.Me.IsCubed(item.SnoItem);
            if (allowCube && !item.IsInventoryLocked)
            {
                var cubeTexture = Hud.Texture.KanaiCubeTexture;
                var h = cubeTexture.Height * 0.6f / 1200.0f * Hud.Window.Size.Height;
                var rh = h;
                var mod = _watch.ElapsedMilliseconds % 1000;
                var ratio = 0.8f + (1.2f / 1000.0f * (mod < 500 ? mod : 1000 - mod));
                rh *= ratio;

                var x = rect.Right - (h * 0.80f) - ((rh - h) / 2);
                var y = rect.Top - (h * 0.20f) - ((rh - h) / 2);
                cubeTexture.Draw(x, y, rh, rh, 1);
            }
        }

        private void DrawItemHoradricCache(IItem item, System.Drawing.RectangleF rect)
        {
            if (!HoradricCacheEnabled)
                return;
            if (item.SnoItem.MainGroupCode != "horadriccache")
                return;

            var text = "";
            if (item.SnoItem.Code.Contains("A1"))
                text = "A1";
            if (item.SnoItem.Code.Contains("A2"))
                text = "A2";
            if (item.SnoItem.Code.Contains("A3"))
                text = "A3";
            if (item.SnoItem.Code.Contains("A4"))
                text = "A4";
            if (item.SnoItem.Code.Contains("A5"))
                text = "A5";
            if (item.SnoItem.Code.Contains("Act1"))
                text = "A1";
            if (item.SnoItem.Code.Contains("Act2"))
                text = "A2";
            if (item.SnoItem.Code.Contains("Act3"))
                text = "A3";
            if (item.SnoItem.Code.Contains("Act4"))
                text = "A4";
            if (item.SnoItem.Code.Contains("Act5"))
                text = "A5";
            if (item.SnoItem.Code.Contains("Hard"))
                text += ": H";
            if (item.SnoItem.Code.Contains("Expert"))
                text += ": E";
            if (item.SnoItem.Code.Contains("Master"))
                text += ": M";

            if (item.SnoItem.Code.Contains("T16"))
                text += ": T16";
            else if (item.SnoItem.Code.Contains("T15"))
                text += ": T15";
            else if (item.SnoItem.Code.Contains("T14"))
                text += ": T14";
            else if (item.SnoItem.Code.Contains("T13"))
                text += ": T13";
            else if (item.SnoItem.Code.Contains("T12"))
                text += ": T12";
            else if (item.SnoItem.Code.Contains("T11"))
                text += ": T11";
            else if (item.SnoItem.Code.Contains("T10"))
                text += ": T10";
            else if (item.SnoItem.Code.Contains("T9"))
                text += ": T9";
            else if (item.SnoItem.Code.Contains("T8"))
                text += ": T8";
            else if (item.SnoItem.Code.Contains("T7"))
                text += ": T7";
            else if (item.SnoItem.Code.Contains("T6"))
                text += ": T6";
            else if (item.SnoItem.Code.Contains("T5"))
                text += ": T5";
            else if (item.SnoItem.Code.Contains("T4"))
                text += ": T4";
            else if (item.SnoItem.Code.Contains("T3"))
                text += ": T3";
            else if (item.SnoItem.Code.Contains("T2"))
                text += ": T2";
            else if (item.SnoItem.Code.Contains("T1"))
                text += ": T1";

            if (item.SnoItem.Code.Contains("Torment13"))
                text += ": T13";
            else if (item.SnoItem.Code.Contains("Torment12"))
                text += ": T12";
            else if (item.SnoItem.Code.Contains("Torment11"))
                text += ": T11";
            else if (item.SnoItem.Code.Contains("Torment10"))
                text += ": T10";
            else if (item.SnoItem.Code.Contains("Torment9"))
                text += ": T9";
            else if (item.SnoItem.Code.Contains("Torment8"))
                text += ": T8";
            else if (item.SnoItem.Code.Contains("Torment7"))
                text += ": T7";
            else if (item.SnoItem.Code.Contains("Torment6"))
                text += ": T6";
            else if (item.SnoItem.Code.Contains("Torment5"))
                text += ": T5";
            else if (item.SnoItem.Code.Contains("Torment4"))
                text += ": T4";
            else if (item.SnoItem.Code.Contains("Torment3"))
                text += ": T3";
            else if (item.SnoItem.Code.Contains("Torment2"))
                text += ": T2";
            else if (item.SnoItem.Code.Contains("Torment1"))
                text += ": T1";

            if (text != null)
            {
                var textLayout = HoradricCacheFont.GetTextLayout(text);
                HoradricCacheFont.DrawText(textLayout, rect.Right - (rv / 20.0f) - textLayout.Metrics.Width, rect.Bottom - (rv / 70.0f) - textLayout.Metrics.Height);
            }
        }
    }
}