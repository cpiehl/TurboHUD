namespace Turbo.Plugins
{

    public interface ITextureController
    {

        ITexture GetTexture(string name);
        ITexture GetTexture(uint id);

        ITexture GetItemBackgroundTexture(IItem item);
        ITexture GetItemTexture(ISnoItem snoItem);

        ITexture EmptySocketTexture { get; }
        ITexture UnidTexture { get; }
        ITexture KanaiCubeTexture { get; }

        ITexture ButtonTextureGray { get; }
        ITexture ButtonTextureBlue { get; }
        ITexture ButtonTextureOrange { get; }
        ITexture BackgroundTextureOrange { get; }
        ITexture BackgroundTextureGreen { get; }
        ITexture BackgroundTextureYellow { get; }
        ITexture BackgroundTextureBlue { get; }
        ITexture Button2TextureGray { get; }
        ITexture Button2TextureOrange { get; }
        ITexture Button2TextureBrown { get; }

        ITexture BuffFrameTexture { get; }
        ITexture DebuffFrameTexture { get; }

        ITexture InventorySlotTexture { get; }
        ITexture InventoryLegendaryBackgroundSmall { get; }
        ITexture InventoryLegendaryBackgroundLarge { get; }
        ITexture InventorySetBackgroundSmall { get; }
        ITexture InventorySetBackgroundLarge { get; }
    }

}