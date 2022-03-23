using AuldShiteburn.EntityData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData
{
    [Serializable]
    internal abstract class Area
    {
        public abstract string Name { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }
        private Tile[] area;
        protected List<TilePlaceData> placeData = new List<TilePlaceData>();
        private bool firstEnter = true;
        public List<Entity> entities = new List<Entity>();

        public Area()
        {
            area = new Tile[Width * Height];
            AddSpecialTiles();
            TileGeneration();
            PlaceSpecialTiles();
        }

        protected abstract void OnFirstEnter();
        protected abstract void InitEntities();

        protected abstract void TileGeneration();

        private void PlaceSpecialTiles()
        {
            foreach (TilePlaceData specialTile in placeData)
            {
                SetTile(specialTile.x, specialTile.y, specialTile.specialTile);
            }
        }

        protected virtual void AddSpecialTiles()
        {
        }

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

        public void ConnectInDirection(Direction direction)
        {
            PassageTile pTile = new PassageTile();
            pTile.Direction = direction;
            switch (direction)
            {
                case Direction.North:
                    {
                        float middle = Width / 2f;
                        if (Width % 2 == 0)
                        {
                            SetTile((int)middle, 0, pTile);
                            SetTile((int)middle - 1, 0, pTile);
                        }
                        else
                        {
                            middle = MathF.Floor(middle);
                            SetTile((int)middle, 0, pTile);
                        }
                    }
                    break;
                case Direction.South:
                    {
                        float middle = Width / 2f;
                        if (Width % 2 == 0)
                        {
                            SetTile((int)middle, Height - 1, pTile);
                            SetTile((int)middle - 1, Height - 1, pTile);
                        }
                        else
                        {
                            middle = MathF.Floor(middle);
                            SetTile((int)middle, Height - 1, pTile);
                        }
                    }
                    break;
                case Direction.East:
                    {
                        float middle = Height / 2f;
                        if (Height % 2 == 0)
                        {
                            SetTile(Width - 1, (int)middle, pTile);
                            SetTile(Width - 1, (int)middle - 1, pTile);
                        }
                        else
                        {
                            middle = MathF.Floor(middle);
                            SetTile(Width - 1, (int)middle, pTile);
                        }
                    }
                    break;
                case Direction.West:
                    {
                        float middle = Height / 2f;
                        if (Height % 2 == 0)
                        {
                            SetTile(0, (int)middle, pTile);
                            SetTile(0, (int)middle - 1, pTile);
                        }
                        else
                        {
                            middle = MathF.Floor(middle);
                            SetTile(0, (int)middle, pTile);
                        }
                    }
                    break;
            }
        }
    }
}
