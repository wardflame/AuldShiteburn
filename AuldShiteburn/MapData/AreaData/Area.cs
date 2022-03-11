using AuldShiteburn.EntityData;
using AuldShiteburn.MapData.TileData;
using System.Collections.Generic;

namespace AuldShiteburn.MapData
{
    internal abstract class Area
    {
        public abstract string Name { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }
        private Tile[] area;
        public List<Entity> entities = new List<Entity>();

        public Area()
        {
            area = new Tile[Width * Height];
            TileGeneration();
        }

        protected abstract void TileGeneration();

        protected int GetIndex(int posX, int posY)
        {
            return posX + Width * posY;
        }

        protected void SetTile(int posX, int posY, Tile tile)
        {
            if (posX < 0 || posY < 0 || posX >= Width || posY >= Height)
            {
                return;
            }
            area[GetIndex(posX, posY)] = tile.Clone();
        }

        public Tile GetTile(int posX, int posY)
        {
            if (posX < 0 || posY < 0 || posX >= Width || posY >= Height)
            {
                return null;
            }
            return area[GetIndex(posX, posY)];
        }

        public Tile[] GetArea()
        {
            return area;
        }
    }
}
