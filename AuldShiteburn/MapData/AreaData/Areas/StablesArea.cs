using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.Enemies;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ConsumableData.Consumables;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class StablesArea : Area
    {
        public override string Name => "The Stables";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => true;
        public override bool BossArea => false;

        protected override void AddSpecialTiles()
        {
            #region Stable
            // Stable Border
            for (int y = 4; y <= 16; y++)
            {
                placeData.Add(new TilePlaceData(3, y, Tile.WallTile));
            }
            for (int y = 4; y <= 7; y++)
            {
                placeData.Add(new TilePlaceData(8, y, Tile.WallTile));
            }
            for (int y = 10; y <= 14; y++)
            {
                placeData.Add(new TilePlaceData(8, y, Tile.WallTile));
            }
            for (int y = 13; y <= 15; y++)
            {
                placeData.Add(new TilePlaceData(15, y, Tile.WallTile));
            }
            for (int x = 4; x <= 7; x++)
            {
                placeData.Add(new TilePlaceData(x, 4, Tile.WallTile));
            }
            for (int x = 4; x <= 15; x++)
            {
                placeData.Add(new TilePlaceData(x, 16, Tile.WallTile));
            }
            for (int x = 6; x <= 15; x++)
            {
                placeData.Add(new TilePlaceData(x, 12, Tile.WallTile));
            }
            #endregion Stable
        }

        public override void InitEnemies()
        {
            Enemies.Add(new MadHunterEnemyEntity());
            Enemies.Add(new MadHunterEnemyEntity());
        }

        public override void OnFirstEnter()
        {
            Random rand = new Random();
            WeaponType wType = WeaponType.Longsword;
            switch (PlayerEntity.Instance.Class.ClassType)
            {
                case ClassType.Heathen:
                    {
                        wType = WeaponType.PrimitiveWeaponTypes[rand.Next(WeaponType.PrimitiveWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Fighter:
                    {
                        wType = WeaponType.MartialWeaponTypes[rand.Next(WeaponType.MartialWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Marauder:
                    {
                        wType = WeaponType.StrengthWeaponTypes[rand.Next(WeaponType.StrengthWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Monk:
                    {
                        wType = WeaponType.PrimitiveWeaponTypes[rand.Next(WeaponType.PrimitiveWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Rogue:
                    {
                        wType = WeaponType.DextrousWeaponTypes[rand.Next(WeaponType.DextrousWeaponTypes.Count)];
                    }
                    break;
            }
            SetTile(14, 13, new LootTile("Hunter Corpse", false, false,
                new List<Item>()
                {
                    KeyItem.MillHouseKey,
                    new WeaponItem()
                    {
                        Type = wType,
                        Material = WeaponMaterial.WeaponMaterialSteel,
                        Property = WeaponProperty.WeaponPropertyFlaming
                    }
                }));
            Map.Instance.PrintTile(14, 13);
            if (!InitiateCombat(true)) return;
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
