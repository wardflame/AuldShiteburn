using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
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
    class TheDrainArea : Area
    {
        public override string Name => "The Drain";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => false;
        public override bool BossArea => false;

        protected override void AddSpecialTiles()
        {
            #region Tunnel Walls
            // Central tunnel north wall.
            for (int y = 6; y <= 8; y++)
            {
                for (int x = 1; x <= 8; x++)
                {
                    placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                }
            }
            // Central tunnel south wall.
            for (int y = 11; y <= 14; y++)
            {
                for (int x = 1; x <= 8; x++)
                {
                    placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                }
            }
            // Central tunnel north-east wall.
            for (int y = 1; y <= 11; y++)
            {
                for (int x = 11; x <= 14; x++)
                {
                    placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                }
            }
            // Central tunnel south-east wall.
            for (int y = 13; y <= 14; y++)
            {
                for (int x = 11; x <= 18; x++)
                {
                    placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                }
            }
            // North tunnel west wall.
            for (int y = 1; y <= 4; y++)
            {
                for (int x = 5; x <= 8; x++)
                {
                    placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                }
            }
            // West cell grate wall & door.
            for (int x = 1; x <= 4; x++)
            {
                if (x == 1) placeData.Add(new TilePlaceData(x, 3, new DoorTile(true, KeyItem.DrainCellKey)));
                else placeData.Add(new TilePlaceData(x, 3, Tile.MetalGrateTile));
            }
            // East cell grate wall & door.
            for (int x = 15; x <= 18; x++)
            {
                if (x == 17) placeData.Add(new TilePlaceData(x, 5, new DoorTile(true, KeyItem.DrainCellKey)));
                else placeData.Add(new TilePlaceData(x, 5, Tile.MetalGrateTile));
            }
            // South wall, grate & door.
            for (int x = 1; x <= 18; x++)
            {
                if (x == 18) placeData.Add(new TilePlaceData(x, 16, new DoorTile(true, KeyItem.DrainGateKey)));
                else if (x <= 15) placeData.Add(new TilePlaceData(x, 16, Tile.WallTile));
                else placeData.Add(new TilePlaceData(x, 16, Tile.MetalGrateTile));
            }
            for (int y = 17; y <= 18; y++)
            {
                for (int x = 1; x < 7; x++)
                {
                    placeData.Add(new TilePlaceData(x, y, Tile.WallTile));
                }
            }
            #endregion Tunnel Walls
            #region Loot
            // Loot.
            Random rand = new Random();
            placeData.Add(new TilePlaceData(1, 15, new LootTile("Withered Ealdorman", false, false,
                new List<Item>()
                {
                    KeyItem.ScrappyNote,
                    ArmourItem.HeavyGambeson,
                    new WeaponItem()
                    {
                        Type = WeaponType.Shortsword,
                        Material = WeaponMaterial.WeaponMaterialSteel,
                        Property = WeaponProperty.WeaponPropertyStandard
                    }
                })));
            placeData.Add(new TilePlaceData(7, 18, new LootTile("Fallen Knight", false, false,
                new List<Item>()
                {
                    KeyItem.ScrappyNote,
                    ArmourItem.GrailKnightPlate,
                    new WeaponItem()
                    {
                        Type = WeaponType.Longsword,
                        Material = WeaponMaterial.WeaponMaterialSteel,
                        Property = WeaponProperty.WeaponPropertyHoly
                    }
                })));
            placeData.Add(new TilePlaceData(4, 1, new LootTile("Rat-gnawed Prisoner", false, false,
                new List<Item>()
                {
                    KeyItem.ShitestainedAmulet,
                    new WeaponItem()
                    {
                        Type = WeaponType.Dagger,
                        Material = WeaponMaterial.WeaponMaterialHardshite,
                        Property = WeaponProperty.WeaponPropertyCold
                    }
                })));
            #endregion Loot
            // NPCs
            placeData.Add(new TilePlaceData(18, 1, new BoudicaNPCTile()));
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
