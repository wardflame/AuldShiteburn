using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.ConsumableData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class ShiterootGardenArea : Area
    {
        public override string Name => "Shiteroot Garden";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => false;
        public override bool BossArea => false;

        protected override void AddSpecialTiles()
        {
            Random rand = new Random();
            #region Grass
            // Grass
            for (int y = 1; y <= 8; y++)
            {
                for (int x = 1; x <= 10; x++)
                {
                    placeData.Add(new TilePlaceData(x, y, Tile.GrassTile));
                }
            }
            for (int y = 11; y <= 18; y++)
            {
                for (int x = 1; x <= 8; x++)
                {
                    placeData.Add(new TilePlaceData(x, y, Tile.GrassTile));
                }
            }
            #endregion Grass
            #region Trees
            for (int y = 11; y <= 13; y++)
            {
                placeData.Add(new TilePlaceData(14, y, Tile.WallTile));
            }
            for (int x = 13; x <= 15; x++)
            {
                placeData.Add(new TilePlaceData(x, 12, Tile.WallTile));
            }
            for (int y = 15; y <= 17; y++)
            {
                placeData.Add(new TilePlaceData(16, y, Tile.WallTile));
            }
            for (int x = 15; x <= 17; x++)
            {
                placeData.Add(new TilePlaceData(x, 16, Tile.WallTile));
            }
            for (int y = 15; y <= 17; y++)
            {
                placeData.Add(new TilePlaceData(11, y, Tile.WallTile));
            }
            for (int x = 10; x <= 12; x++)
            {
                placeData.Add(new TilePlaceData(x, 16, Tile.WallTile));
            }
            #endregion Trees
            #region House
            for (int y = 1; y <= 5; y++)
            {
                placeData.Add(new TilePlaceData(11, y, Tile.WallTile));
            }
            for (int y = 5; y <= 8; y++)
            {
                placeData.Add(new TilePlaceData(15, y, Tile.WallTile));
            }
            for (int x = 11; x <= 13; x++)
            {
                placeData.Add(new TilePlaceData(x, 5, Tile.WallTile));
            }
            for (int x = 15; x <= 18; x++)
            {
                placeData.Add(new TilePlaceData(x, 8, Tile.WallTile));
            }
            placeData.Add(new TilePlaceData(14, 5, new DoorTile(false)));
            #endregion House
            #region Loot
            // Loot.
            placeData.Add(new TilePlaceData(16, 7, new LootTile("Husk Apothecary", false, false,
                new List<Item>()
                {
                    KeyItem.EastResidenceKey,
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)],
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)],
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)]
                })));
            #endregion Loot
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
