using AuldShiteburn.EntityData.Enemies;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using AuldShiteburn.MapData.TileData.Tiles.NPCs.NarrationNPCs;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class ShitepileArea : Area
    {
        public override string Name => "Shitepile";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => true;
        public override bool BossArea => true;

        protected override void AddSpecialTiles()
        {
            // Boss narration tile.
            placeData.Add(new TilePlaceData(1, 1, new ShitepileNarrationNPCTile()));
            #region Generate Shite Mound Tiles
            int radius = 6;
            Random rand = new Random();
            double variation = 0.4;
            for (int y = -radius; y < radius; y++)
            {
                for (int x = -radius; x < radius; x++)
                {
                    if ((x * x + y * y) <= (radius * radius) * Math.Clamp(rand.NextDouble(), variation, 1f))
                    {
                        placeData.Add(new TilePlaceData(9 + x, 9 + y, Tile.ShiteMoundTile));
                    }
                }
            }
            #endregion Shite Mound Tiles
        }

        public override void InitEnemies()
        {
            Enemies.Add(new DungEaterEnemyEntity());
        }

        public override void OnFirstEnter()
        {
            Tile narrationTile = GetTile(1, 1);
            ShitepileNarrationNPCTile narration = (ShitepileNarrationNPCTile)narrationTile;
            narration.Interaction();
            Utils.ClearInteractInterface();
            InitiateCombat(false);
            LootTile.GenerateLootTile(false, new List<Item>()
            {
                ArmourItem.IndomitableCuirass,
                KeyItem.WestResidenceKey
            });
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
