using AuldShiteburn.EntityData.Enemies;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ConsumableData.Consumables;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using AuldShiteburn.MapData.TileData.Tiles.NPCs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
