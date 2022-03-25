using System;

namespace AuldShiteburn.MapData.TileData
{
    [Serializable]
    internal struct TilePlaceData
    {
        public int x;
        public int y;
        public Tile specialTile;

        public TilePlaceData(int x, int y, Tile specialTile)
        {
            this.x = x;
            this.y = y;
            this.specialTile = specialTile;
        }
    }
}
