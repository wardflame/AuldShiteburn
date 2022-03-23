using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using AuldShiteburn.MapData.TileData.Tiles.NPCs;
using System;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class ResidentsArea : Area
    {
        public override string Name => "Residents";
        public override int Width => 20;
        public override int Height => 20;

        protected override void AddSpecialTiles()
        {
            placeData.Add(new TilePlaceData(3, 1, new AethelwulfNPCTile()));
            placeData.Add(new TilePlaceData(4, 4, new DoorTile(true, KeyItem.residentKey)));
        }

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
                    else
                    {
                        SetTile(x, y, Tile.AirTile);
                    }
                }
            }
        }
    }
}
