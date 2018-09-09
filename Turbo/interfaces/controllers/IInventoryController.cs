using System.Collections.Generic;

namespace Turbo.Plugins
{

    public interface IInventoryController
    {
        int MaxStashPageCount { get; } // 2
        int MaxStashTabCountPerPage { get; } // 5
        int SelectedStashPageIndex { get; }
        int SelectedStashTabIndex { get; }
        int HoveredStashTabIndex { get; }
        int GetStashTabUsedSpace(int pageIndex, int tabIndex);

        IEnumerable<IItem> ItemsInStash { get; }
        IEnumerable<IItem> ItemsInInventory { get; }

        IUiElement StashMainUiElement { get; }
        IUiElement GetStashPageUiElement(int index);
        IUiElement GetStashTabUiElement(int index);

        IUiElement InventoryMainUiElement { get; }
        IUiElement InventoryItemsUiElement { get; }
        IUiElement GetEquippedItemUiElement(ItemLocation location);

        System.Drawing.RectangleF GetItemRect(IItem item);
        System.Drawing.RectangleF GetRectInStash(int x, int y, int width, int height);
        System.Drawing.RectangleF GetRectInInventory(int x, int y, int width, int height);

        ISnoItem GetSnoItem(uint sno);

        System.Drawing.Rectangle InventoryLockArea { get; }

        IUiElement GetHoveredItemMainUiElement();
        IUiElement GetHoveredItemTopUiElement();
        IItem HoveredItem { get; }
    }

}