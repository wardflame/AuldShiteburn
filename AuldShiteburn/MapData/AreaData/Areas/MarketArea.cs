using AuldShiteburn.MapData.TileData;
using System;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class MarketArea : Area
    {
        public override string Name => "Market";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => false;
        public override bool BossArea => false;

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
