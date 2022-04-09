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
    class GraveyardArea : Area
    {
        public override string Name => "The Graveyard";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => true;
        public override bool BossArea => true;

        protected override void AddSpecialTiles()
        {
            Random rand = new Random();
            #region Tombstones
            // Tombstones
            for (int y = 4; y <= 12; y++)
            {
                for (int x = 4; x <= 14; x++)
                {
                    if (x % 2 == 0 && y % 2 == 0) placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                    if (y == 4 && x == 15) placeData.Add(new TilePlaceData(x, y, new DoorTile(true, KeyItem.GuildMastersKey)));
                }
            }
            #endregion Tombstones
            #region Defiled Shrine
            for (int y = 15; y <= 17; y++)
            {
                for (int x = 6; x <= 13; x++)
                {
                    if (y == 16 || x > 7 && x < 12) placeData.Add(new TilePlaceData(x, y, Tile.DefiledStoneTile));
                    if (y == 15 && (x == 9 || x == 10)) placeData.Add(new TilePlaceData(x, y, Tile.BrokenAltar));
                }
            }
            #endregion Defiled Shrine
            #region Loot
            // Loot.
            placeData.Add(new TilePlaceData(9, 14, new LootTile("Disembowled Sacrifice", false, false,
                new List<Item>()
                {
                    new WeaponItem()
                    {
                        Type = WeaponType.Mace,
                        Material = WeaponMaterial.WeaponMaterialSteel,
                        Property = WeaponProperty.WeaponPropertyStandard
                    },
                    ArmourItem.SplintPlate,
                    KeyItem.GranaryKey
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
