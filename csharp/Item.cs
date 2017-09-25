namespace csharp
{
    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public void DecreaseQuality()
        {
            if (Quality > 0)
            {
                Quality--;
            }
        }

        public void DropQuality()
        {
            Quality = 0;
        }

        public void DecreaseSellIn()
        {
            SellIn--;
        }

        public void IncreaseQuality()
        {
            if (IsQualityLowerThanMaxQuality())
            {
                Quality++;
            }
        }

        public bool IsQualityLowerThanMaxQuality() => Quality < GildeRoseConstants.MaxQuality;

    }
}
