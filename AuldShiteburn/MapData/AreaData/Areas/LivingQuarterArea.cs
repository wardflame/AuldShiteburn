using AuldShiteburn.EntityData.Enemies;
using AuldShiteburn.ItemData;
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
    class LivingQuarterArea : Area
    {
        public override string Name => "Living Quarter";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => true;
        public override bool BossArea => false;

        protected override void AddSpecialTiles()
        {
            #region Earh House
            // South wall.
            for (int x = 1; x <= 5; x++)
            {
                placeData.Add(new TilePlaceData(x, 7, Tile.WallTile));
            }
            // East wall.
            for (int y = 2; y <= 6; y++)
            {
                placeData.Add(new TilePlaceData(5, y, Tile.WallTile));

            }
            // Door.
            placeData.Add(new TilePlaceData(5, 1, new DoorTile(true, KeyItem.WestResidenceKey)));
            // NPC
            placeData.Add(new TilePlaceData(1, 6, new EarhNPCTile()));
            #endregion Earh House
            #region East House
            for (int x = 16; x <= 19; x++)
            {
                if (x == 16)
                {
                    placeData.Add(new TilePlaceData(x, 13, new DoorTile(true, KeyItem.EastResidenceKey)));
                }
                else
                {
                    placeData.Add(new TilePlaceData(x, 13, Tile.WallTile));
                }
            }
            for (int x = 10; x <= 15; x++)
            {
                placeData.Add(new TilePlaceData(x, 6, Tile.WallTile));
            }
            for (int y = 1; y <= 6; y++)
            {
                placeData.Add(new TilePlaceData(10, y, Tile.WallTile));
            }
            for (int y = 6; y <= 13; y++)
            {
                placeData.Add(new TilePlaceData(15, y, Tile.WallTile));
            }
            #endregion East House
            #region South-West House
            // North wall.
            for (int x = 2; x <= 6; x++)
            {
                placeData.Add(new TilePlaceData(x, 14, Tile.WallTile));
            }
            // South wall.
            for (int x = 2; x <= 6; x++)
            {
                placeData.Add(new TilePlaceData(x, 17, Tile.WallTile));
            }
            // East wall.
            placeData.Add(new TilePlaceData(6, 15, Tile.WallTile));
            placeData.Add(new TilePlaceData(6, 16, Tile.WallTile));
            // West Wall.
            placeData.Add(new TilePlaceData(2, 15, Tile.WallTile));
            // Door.
            placeData.Add(new TilePlaceData(2, 16, new DoorTile(false)));
            // Loot.
            placeData.Add(new TilePlaceData(5, 15,
                new LootTile("A burnt corpse in the fetal position", false,
                itemList: new List<Item>()
                {
                            WeaponItem.GenerateWeapon(),
                            KeyItem.ShitebreachSouthKey
                })
                ));
            #endregion South-West House
        }

        public override void InitEnemies()
        {
            Enemies.Add(new ShiteHuskEnemyEntity());
            Enemies.Add(new ShiteHuskEnemyEntity());
            Enemies.Add(new ShiteHuskEnemyEntity());
        }

        public override void OnFirstEnter()
        {
            InitiateCombat(true);
            EarhNPCTile earh = GetTile(1, 6) as EarhNPCTile;
            earh.Interaction();
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
