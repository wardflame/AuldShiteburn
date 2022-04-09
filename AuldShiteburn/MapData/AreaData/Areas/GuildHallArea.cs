using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class GuildHallArea : Area
    {
        public override string Name => "Guild Hall";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => false;
        public override bool BossArea => false;

        protected override void AddSpecialTiles()
        {
            #region Walls
            // North-east office.
            for (int y = 1; y <= 4; y++)
            {
                for (int x = 12; x <= 17; x++)
                {
                    if (x < 13 || x > 16 || y == 4 || (y == 2 && x == 13)) placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                    if (y == 4 && x == 15) placeData.Add(new TilePlaceData(x, y, new DoorTile(true, KeyItem.GuildMastersKey)));
                }
            }
            // West wall.
            for (int y = 1; y <= 13; y++)
            {
                for (int x = 6; x <= 15; x++)
                {
                    if (y == 13) placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                    else placeData.Add(new TilePlaceData(6, y, Tile.WallTile));
                }
            }
            for (int y = 14; y <= 18; y++)
            {
                for (int x = 8; x <= 12; x++)
                {
                    if (x < 9 || x > 11) placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                    if (x == 8 && y == 15) placeData.Add(new TilePlaceData(x, y, new DoorTile(true, KeyItem.GuildMastersKey)));
                    if (x == 12 && y == 15) placeData.Add(new TilePlaceData(x, y, new DoorTile(true, KeyItem.GuildMastersKey)));
                }
            }
            #endregion Walls
            #region Loot
            // Loot.
            Random rand = new Random();
            placeData.Add(new TilePlaceData(13, 1, new LootTile("Guild Master's Chest", false, false,
                new List<Item>()
                {
                    new WeaponItem()
                    {
                        Type = WeaponType.Greatsword,
                        Material = WeaponMaterial.WeaponMaterialSteel,
                        Property = WeaponProperty.WeaponPropertyStandard
                    },
                    ArmourItem.PaddedMaille,
                    KeyItem.GuildHallEastKey
                })));
            placeData.Add(new TilePlaceData(5, 4, new LootTile("Crumpled Sack", false, true)));
            placeData.Add(new TilePlaceData(2, 1, new LootTile("Small Box", false, true)));
            placeData.Add(new TilePlaceData(1, 11, new LootTile("Drawer", false, true)));
            placeData.Add(new TilePlaceData(7, 18, new LootTile("Writing Desk", false, true)));
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
