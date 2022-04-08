using AuldShiteburn.MapData.TileData;
using System;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class TheDrainArea : Area
    {
        public override string Name => "The Drain";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => true;
        public override bool BossArea => false;

        public override void InitEnemies()
        {
        }

        public override void OnFirstEnter()
        {
        }

        public override void OnEnter()
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
