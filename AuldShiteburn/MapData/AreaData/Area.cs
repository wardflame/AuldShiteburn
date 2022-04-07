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
        public virtual bool BossRoom { get; }
        public bool FirstEnter { get; set; } = false;
        private Tile[] area;
        protected List<TilePlaceData> placeData = new List<TilePlaceData>();
        public List<EnemyEntity> Enemies { get; set; } = new List<EnemyEntity>();

        public Area()
        {
            area = new Tile[Width * Height];
            AddSpecialTiles();
            TileGeneration();
            PlaceSpecialTiles();
            InitEnemies();
        }

        /// <summary>
        /// Experience generated on first entry.
        /// </summary>
        public abstract void OnFirstEnter();

        /// <summary>
        /// Generate enemies for first entry.
        /// </summary>
        protected abstract void InitEnemies();

        /// <summary>
        /// Generate tiles for area.
        /// </summary>
        protected abstract void TileGeneration();

        /// <summary>
        /// Read a struct of tiles and their coordinates and place tiles
        /// accordingly.
        /// </summary>
        private void PlaceSpecialTiles()
        {
            foreach (TilePlaceData specialTile in placeData)
            {
                SetTile(specialTile.x, specialTile.y, specialTile.specialTile);
            }
        }

        /// <summary>
        /// Add special tiles to a list so that they can be placed by
        /// the method above.
        /// </summary>
        protected virtual void AddSpecialTiles()
        {
        }

        /// <summary>
        /// Get an index in the array based on int coordinates.
        /// </summary>
        /// <param name="posX">X coordinate.</param>
        /// <param name="posY">Y coordinate.</param>
        /// <returns></returns>
        protected int GetIndex(int posX, int posY)
        {
            return posX + Width * posY;
        }

        /// <summary>
        /// Set the tile at a coordinate in the area.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="tile"></param>
        public void SetTile(int posX, int posY, Tile tile)
        {
            if (posX < 0 || posY < 0 || posX >= Width || posY >= Height)
            {
                return;
            }
            area[GetIndex(posX, posY)] = tile.Clone();
        }

        /// <summary>
        /// Get the tile at a coordinate in the area.
        /// </summary>
        /// <param name="posX">X coordinate.</param>
        /// <param name="posY">Y coordinate.</param>
        /// <returns>Tile at coordinate.</returns>
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

        /// <summary>
        /// Connect areas to each other based on cardinal directions.
        /// </summary>
        /// <param name="direction"></param>
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
