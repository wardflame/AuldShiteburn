using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ConsumableData.Consumables;
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

        }

        public override void InitEnemies()
        {
        }
    }
}
