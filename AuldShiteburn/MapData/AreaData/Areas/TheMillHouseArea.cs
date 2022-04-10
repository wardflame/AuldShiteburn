using AuldShiteburn.EntityData.Enemies;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.ConsumableData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using AuldShiteburn.MapData.TileData.Tiles.NPCs;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class TheMillHouseArea : Area
    {
        public override string Name => "The Mill House";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => true;
        public override bool BossArea => false;

        protected override void AddSpecialTiles()
        {
            #region Mill House Structure
            for (int y = 3; y <= 16; y++)
            {
                placeData.Add(new TilePlaceData(4, y, Tile.WallTile));
                if (y >= 6) placeData.Add(new TilePlaceData(8, y, Tile.WallTile));
            }
            for (int y = 3; y <= 16; y++)
            {
                placeData.Add(new TilePlaceData(15, y, Tile.WallTile));
            }
            for (int x = 5; x <= 15; x++)
            {
                placeData.Add(new TilePlaceData(x, 3, Tile.WallTile));
            }
            for (int y = 6; y <= 14; y++)
            {
                placeData.Add(new TilePlaceData(11, y, Tile.WallTile));
            }
            for (int x = 9; x <= 10; x++)
            {
                placeData.Add(new TilePlaceData(x, 6, Tile.WallTile));
            }
            for (int x = 9; x <= 10; x++)
            {
                placeData.Add(new TilePlaceData(x, 6, Tile.WallTile));
            }
            placeData.Add(new TilePlaceData(10, 6, new DoorTile(false)));
            for (int x = 9; x <= 11; x++)
            {
                placeData.Add(new TilePlaceData(x, 10, Tile.WallTile));
            }
            for (int x = 5; x <= 15; x++)
            {
                placeData.Add(new TilePlaceData(x, 16, Tile.WallTile));
            }
            placeData.Add(new TilePlaceData(10, 14, Tile.WallTile));
            placeData.Add(new TilePlaceData(6, 16, new DoorTile(true, KeyItem.MillHouseKey)));
            #endregion Mill House Structure
            #region Loot
            // Loot.
            Random rand = new Random();
            placeData.Add(new TilePlaceData(9, 9, new LootTile("Shrivelled Warlord", false, false,
                new List<Item>()
                {
                    ArmourItem.SplintPlate,
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)],
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)],
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)]
                })));
            placeData.Add(new TilePlaceData(9, 11, new LootTile("Small Box", false, false,
                new List<Item>()
                {
                    KeyItem.DrainGateKey,
                    ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)]
                })));
            #endregion Loot
            // NPC
            placeData.Add(new TilePlaceData(10, 11, new HumbleLarNPCTile()));
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

        public override void OnFirstEnter()
        {
            InitiateCombat(true);
        }

        public override void InitEnemies()
        {
            Enemies.Add(new ShiteHuskEnemyEntity());
            Enemies.Add(new ShiteHuskEnemyEntity());
            Enemies.Add(new ShiteHuskEnemyEntity());
            Enemies.Add(new ShiteHuskEnemyEntity());
        }
    }
}
