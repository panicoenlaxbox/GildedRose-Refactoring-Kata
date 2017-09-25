using System.Collections.Generic;

namespace csharp
{
    public static class GildeRoseConstants
    {
        public const int MaxQuality = 50;
    }

    public class GildedRose
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePassesToATafkal80EtcConcert = "Backstage passes to a TAFKAL80ETC concert";
        private const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";

        readonly IList<Item> _items;

        public GildedRose(IList<Item> items)
        {
            this._items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                UpdateQualityItem(item);
            }
        }

        private static void UpdateQualityItem(Item item)
        {
            if (item.Name == SulfurasHandOfRagnaros) return;

            if (item.Name != AgedBrie && item.Name != BackstagePassesToATafkal80EtcConcert)
            {
                item.DecreaseQuality();
            }
            else
            {
                item.IncreaseQuality();

                if (item.Name == BackstagePassesToATafkal80EtcConcert)
                {
                    if (item.SellIn < 11)
                    {
                        item.IncreaseQuality();
                        if (item.SellIn < 6)
                        {
                            item.IncreaseQuality();
                        }
                    }
                }
            }


            item.DecreaseSellIn();


            if (item.SellIn < 0)
            {
                if (item.Name == AgedBrie)
                {
                    item.IncreaseQuality();
                }
                else
                {
                    if (item.Name == BackstagePassesToATafkal80EtcConcert)
                    {
                        item.DropQuality();
                    }

                    item.DecreaseQuality();
                }
            }
        }
    }
}
