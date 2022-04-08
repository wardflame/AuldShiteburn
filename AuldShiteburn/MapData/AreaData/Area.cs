using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData;
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
        public virtual bool EnemiesDefeated { get; set; } = false;
        public abstract bool CombatEncounter { get; }
        public abstract bool BossArea { get; }
        public virtual bool BossDefeated { get; protected set; }
        public bool FirstEnter { get; set; } = true;
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
        /// Experience generated on each entry.
        /// </summary>
        public virtual void OnEnter()
        {
            InitiateCombat();
        }

        /// <summary>
        /// If EnemiesDefeated is false and there are enemies in the Enemies
        /// list, run a combat encounter.
        /// </summary>
        public void InitiateCombat()
        {
            if (!EnemiesDefeated && Enemies.Count > 0)
            {
                if (Combat.CombatEncounter(Enemies))
                {
                    SetTile(PlayerEntity.Instance.PosX + 1, PlayerEntity.Instance.PosY, new LootTile("Spoils of War", new List<Item>(), true, true));
                    Map.Instance.PrintTile(PlayerEntity.Instance.PosX + 1, PlayerEntity.Instance.PosY);
                    EnemiesDefeated = true;
                }
            }
        }

        /// <summary>
        /// Generate enemies for first entry.
        /// </summary>
        public abstract void InitEnemies();

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
        public void ConnectInDirection(Direction direction, bool valid)
        {
            Tile tile;
            if (valid)
            {
                PassageTile pTile = new PassageTile();
                pTile.Direction = direction;
                tile = pTile;
            }
            else
            {
                BasicTile wTile = Tile.WallTile;
                tile = wTile;
            }
            switch (direction)
            {
                case Direction.North:
                    {
                        float middle = Width / 2f;
                        if (Width % 2 == 0)
                        {
                            SetTile((int)middle, 0, tile);
                            SetTile((int)middle - 1, 0, tile);
                        }
                        else
                        {
                            middle = MathF.Floor(middle);
                            SetTile((int)middle, 0, tile);
                        }
                    }
                    break;
                case Direction.South:
                    {
                        float middle = Width / 2f;
                        if (Width % 2 == 0)
                        {
                            SetTile((int)middle, Height - 1, tile);
                            SetTile((int)middle - 1, Height - 1, tile);
                        }
                        else
                        {
                            middle = MathF.Floor(middle);
                            SetTile((int)middle, Height - 1, tile);
                        }
                    }
                    break;
                case Direction.East:
                    {
                        float middle = Height / 2f;
                        if (Height % 2 == 0)
                        {
                            SetTile(Width - 1, (int)middle, tile);
                            SetTile(Width - 1, (int)middle - 1, tile);
                        }
                        else
                        {
                            middle = MathF.Floor(middle);
                            SetTile(Width - 1, (int)middle, tile);
                        }
                    }
                    break;
                case Direction.West:
                    {
                        float middle = Height / 2f;
                        if (Height % 2 == 0)
                        {
                            SetTile(0, (int)middle, tile);
                            SetTile(0, (int)middle - 1, tile);
                        }
                        else
                        {
                            middle = MathF.Floor(middle);
                            SetTile(0, (int)middle, tile);
                        }
                    }
                    break;
            }
        }
    }
}
