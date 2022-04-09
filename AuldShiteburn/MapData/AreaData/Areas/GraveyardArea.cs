using AuldShiteburn.EntityData.Enemies;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using AuldShiteburn.MapData.TileData.Tiles.NPCs.NarrationNPCs;
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
            placeData.Add(new TilePlaceData(9, 14, new LootTile("Slain Warlock", false, false,
                new List<Item>()
                {
                    KeyItem.DrainCellKey,
                    ArmourItem.GrandWarlockGarb,
                    new WeaponItem()
                    {
                        Type = WeaponType.HandAxe,
                        Material = WeaponMaterial.WeaponMaterialHardshite,
                        Property = WeaponProperty.WeaponPropertyShiteSlick
                    }
                })));
            placeData.Add(new TilePlaceData(4, 5, new LootTile("Tribute", false, true)));
            placeData.Add(new TilePlaceData(8, 7, new LootTile("Tribute", false, true)));
            placeData.Add(new TilePlaceData(6, 11, new LootTile("Tribute", false, false,
                new List<Item>()
                {
                    ArmourItem.FullPlate
                })));
            placeData.Add(new TilePlaceData(12, 9, new LootTile("Tribute", false, true)));
            #endregion Loot
            // Narration
            placeData.Add(new TilePlaceData(1, 1, new GraveyardNarrationNPCTile()));
        }

        public override void InitEnemies()
        {
            Enemies.Add(new GrandWarlockEnemyEntity());
        }

        public override void OnFirstEnter()
        {
            Tile narrationTile = GetTile(1, 1);
            GraveyardNarrationNPCTile narration = (GraveyardNarrationNPCTile)narrationTile;
            narration.Interaction();
            Utils.ClearInteractInterface();
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
