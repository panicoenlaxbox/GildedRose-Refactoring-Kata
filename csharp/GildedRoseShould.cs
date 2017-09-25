using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace csharp
{
    class Builder
    {
        public static ItemBuilder ItemBuilder()
        {
            return new ItemBuilder();
        }

    }
    class ItemBuilder
    {
        private readonly Item _item;

        public ItemBuilder()
        {
            _item = new Item();
        }

        public ItemBuilder WithSellin(int sellin)
        {
            _item.SellIn = sellin;
            return this;
        }

        public ItemBuilder WithQuality(int quality)
        {
            _item.Quality = quality;
            return this;
        }

        public ItemBuilder WithName(string name)
        {
            _item.Name = name;
            return this;
        }

        public Item Build()
        {
            return _item;
        }

    }
    [TestFixture]
    public class GildedRoseShould
    {
        [Test]
        public void decrease_sellin_and_quality_at_the_end_of_the_day()
        {
            var items = new List<Item>
            {
                Builder.ItemBuilder().WithSellin(5).WithQuality(10).Build()
            };
            var app = new GildedRose(items);
            app.UpdateQuality();
            var item = items.First();
            item.SellIn.Should().Be(4);
            item.Quality.Should().Be(9);
        }

        [Test]
        public void once_the_sell_by_date_has_passed_quality_degrades_twice_as_fast()
        {
            var items = new List<Item>
            {
                Builder.ItemBuilder().WithQuality(10).Build()
            };
            var app = new GildedRose(items);
            app.UpdateQuality();
            var item = items.First();
            item.Quality.Should().Be(8);
        }

        [Test]
        public void quality_of_an_item_is_never_negative()
        {
            var items = new List<Item>
            {
                Builder.ItemBuilder().WithSellin(0).WithQuality(0).Build()
            };
            var app = new GildedRose(items);
            app.UpdateQuality();
            var item = items.First();
            item.Quality.Should().Be(0);
        }

        [Test]
        public void aged_brie_actually_increases_in_quality_the_older_it_gets()
        {
            var items = new List<Item>
            {
                Builder.ItemBuilder().WithName("Aged Brie").WithSellin(5).WithQuality(10).Build()
            };
            var app = new GildedRose(items);
            app.UpdateQuality();
            var item = items.First();
            item.Quality.Should().Be(11);
        }

        [Test]
        public void the_quality_of_an_item_is_never_more_than_50()
        {
            var items = new List<Item>
            {
                Builder.ItemBuilder().WithName("Aged Brie").WithSellin(5).WithQuality(50).Build()
            };
            var app = new GildedRose(items);
            app.UpdateQuality();
            var item = items.First();
            item.Quality.Should().Be(50);
        }

        [Test]
        public void sulfuras_being_a_legendary_item_never_has_to_be_sold_or_decreases_in_quality()
        {
            var items = new List<Item>
            {
                Builder.ItemBuilder().WithName("Sulfuras, Hand of Ragnaros").WithSellin(10).WithQuality(5).Build()
            };
            var app = new GildedRose(items);
            app.UpdateQuality();
            var item = items.First();
            item.SellIn.Should().Be(10);
            item.Quality.Should().Be(5);
        }

        [Test]
        [TestCase(10, 6, 8)]
        [TestCase(5, 3, 6)]
        [TestCase(0, 3, 0)]
        public void backstage_passes_like_aged_brie_increases_in_quality_as_its_sellin_value_approaches(int initialSellin, int initialQuality, int expectedQuality)
        {
            var items = new List<Item>
            {
                Builder.ItemBuilder().WithName("Backstage passes to a TAFKAL80ETC concert").WithSellin(initialSellin).WithQuality(initialQuality).Build()
            };
            var app = new GildedRose(items);
            app.UpdateQuality();
            var item = items.First();
            item.SellIn.Should().Be(initialSellin - 1);
            item.Quality.Should().Be(expectedQuality);
        }

    }
}
