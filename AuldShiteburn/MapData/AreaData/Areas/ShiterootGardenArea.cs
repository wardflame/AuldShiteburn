using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles.NPCs;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    class ShiterootGardenArea : Area
    {
        public override string Name => "Shiteroot Garden";
        public override int Width => 20;
        public override int Height => 20;

        protected override void InitEntities()
        {
        }

        protected override void OnFirstEnter()
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
                    else if (x == 8 && y == 9)
                    {
                        SetTile(x, y, new AethelwulfNPCTile());
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
