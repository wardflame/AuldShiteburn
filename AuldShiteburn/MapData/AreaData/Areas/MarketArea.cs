using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.ConsumableData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using AuldShiteburn.MapData.TileData.Tiles.NPCs;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class MarketArea : Area
    {
        public override string Name => "Market";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => false;
        public override bool BossArea => false;

        protected override void AddSpecialTiles()
        {
            #region Market Stalls
            // Central stall.
            for (int y = 6; y <= 12; y++)
            {
                for (int x = 6; x <= 12; x++)
                {
                    if (x < 7 || x > 11 || y == 6 || y == 12) placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                    if (y == 12 && x == 9) placeData.Add(new TilePlaceData(x, y, new DoorTile(false)));
                }
            }
            // North-west stall.
            for (int y = 1; y <= 4; y++)
            {
                for (int x = 2; x <= 6; x++)
                {
                    if (x < 3 || x > 5 || y == 4) placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                    if (y == 4 && x == 4) placeData.Add(new TilePlaceData(x, y, new DoorTile(false)));
                }
            }
            // South building.
            for (int x = 1; x <= 5; x++)
            {
                placeData.Add(new TilePlaceData(x, 14, Tile.WallTile));
            }
            for (int x = 5; x <= 13; x++)
            {
                placeData.Add(new TilePlaceData(x, 16, Tile.WallTile));
            }
            for (int y = 14; y <= 16; y++)
            {
                placeData.Add(new TilePlaceData(5, y, Tile.WallTile));
            }
            placeData.Add(new TilePlaceData(4, 16, Tile.WallTile));
            placeData.Add(new TilePlaceData(13, 17, Tile.WallTile));
            placeData.Add(new TilePlaceData(13, 18, new DoorTile(false)));
            #endregion Market Stalls
            #region Loot
            // Loot.
            Random rand = new Random();
            placeData.Add(new TilePlaceData(11, 7, new LootTile("Guild Master's Satchel", false, false,
                new List<Item>()
                {
                    KeyItem.GuildMastersKey,
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)],
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)],
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)]
                })));
            #endregion Loot
            // NPCs
            placeData.Add(new TilePlaceData(4, 15, new BashfulEadwynNPCTile()));
        }

        public override void InitEnemies()
        {
        }

        public override void OnFirstEnter()
        {
        }

        protected override void TileGeneration()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                    {
                        SetTile(x, y, Tile.WallTile);
                    }
                    else
                    {
                        SetTile(x, y, Tile.AirTile);
                    }
                }
            }
        }
    }
}
