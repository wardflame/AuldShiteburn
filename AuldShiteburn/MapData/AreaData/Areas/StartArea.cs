using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ConsumableData.Consumables;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using AuldShiteburn.MapData.TileData.Tiles.NPCs;
using AuldShiteburn.MapData.TileData.Tiles.NPCs.GameFeatureNPCs;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class StartArea : Area
    {
        public override string Name => "Shitebreach";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => false;
        public override bool BossArea => false;
        public int NPCsRemaining { get; set; } = 4;
        public int BossesRemaining { get; set; } = 3;

        protected override void AddSpecialTiles()
        {
            #region Ormod House
            // South wall.
            for (int x = 1; x <= 6; x++)
            {
                placeData.Add(new TilePlaceData(x, 5, Tile.WallTile));
            }
            // South porch wall.
            placeData.Add(new TilePlaceData(7, 7, Tile.WallTile));
            // West porch wall.
            for (int y = 5; y <= 7; y++)
            {
                placeData.Add(new TilePlaceData(6, y, Tile.WallTile));
            }
            // East wall.
            for (int y = 1; y <= 7; y++)
            {
                placeData.Add(new TilePlaceData(9, y, Tile.WallTile));
            }
            // Loot.
            placeData.Add(new TilePlaceData(6, 4,
                new LootTile("The decaying corpse of a long-dead Heathen", false, false,
                new List<Item>()
                {
                    new HealthRegenConsumable(),
                    KeyItem.HideawayKey
                })
                ));
            // Door.
            placeData.Add(new TilePlaceData(8, 7, new DoorTile(true, KeyItem.HideawayKey)));
            // NPC.
            placeData.Add(new TilePlaceData(3, 1, new OrmodNPCTile()));
            #endregion Ormod House
            placeData.Add(new TilePlaceData(5, 6, new StorageTile("Rotting Chest")));
            placeData.Add(new TilePlaceData(7, 8, new CrestfallenGrimrNPCTile()));
            #region Moonlight Altar
            placeData.Add(new TilePlaceData(2, 10, new GameStatusAltarNPCTile()));
            placeData.Add(new TilePlaceData(1, 9, Tile.MoonlightStoneTile));
            for (int y = 8; y <= 12; y++)
            {
                int check = 1;
                if (y == 8) check = 1;
                else if (y == 9) check = 2;
                else if (y == 10) check = 1;
                else if (y == 11) check = 2;
                else if (y == 12) check = 1;
                for (int x = 1; x <= check; x++)
                {
                    placeData.Add(new TilePlaceData(x, y, Tile.MoonlightStoneTile));
                }
            }
            #endregion Moonlight Altar
            #region Barred South Door
            for (int x = 7; x <= 12; x++)
            {
                if (x == 7 || x == 12)
                {
                    for (int y = 16; y <= 19; y++)
                    {
                        placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                    }
                }
                else
                {
                    if (x == 9)
                    {
                        placeData.Add(new TilePlaceData(x, 16, new DoorTile(true, KeyItem.ShitebreachSouthKey)));
                        continue;
                    }
                    placeData.Add(new TilePlaceData(x, 16, Tile.WallTile));
                }
            }
            #endregion Barred South Door
            placeData.Add(new TilePlaceData(7, 15, new ShitefireNPCTile()));


        }

        public override void InitEnemies()
        {
        }

        public override void OnFirstEnter()
        {
        }

        public override void OnEnter()
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
