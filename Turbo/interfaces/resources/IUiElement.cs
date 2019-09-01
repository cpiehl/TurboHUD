namespace Turbo.Plugins
{
    public interface IUiElement
    {
        string Path { get; }
        bool Visible { get; }
        System.Drawing.RectangleF Rectangle { get; }
        int AnimState { get; }
        uint TextureSno { get; }

        float MinimapOffsetX { get; }   // specific to 'Root.NormalLayer.map_dialog_mainPage.localmap'
        float MinimapOffsetY { get; }   // specific to 'Root.NormalLayer.map_dialog_mainPage.localmap'
        uint LegendaryGemAcdId { get; } // specific to 'Root.NormalLayer.vendor_dialog_mainPage.riftReward_dialog.LayoutRoot.gemUpgradePane.items_list._content._stackpanel._tilerow0._item0.Item'
        uint AcdId { get; }             // specific to 'Root.TopLayer.item 2.stack'

        IUiElement ReplacementWhenNotVisible { get; }

        void Refresh();
        string ReadText(System.Text.Encoding encoding, bool removeColors);
    }
}