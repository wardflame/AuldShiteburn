using AuldShiteburn.EntityData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData
{
    /// <summary>
    /// Base map class. Holds an array of 'areas' which the player navigates.
    /// Maps use a Width and Height property, multiplied, to determine how many
    /// areas it contains. 3 width and 3 height = 9 total areas. Within, various
    /// methods are used to generate, connect, set, and randomise areas, as well
    /// as move entities around in each area.
    /// </summary>
    internal abstract class Map
    {
        public static Map Instance { get; set; }
        public abstract string Name { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }
        private int posX;
        private int posY;

        protected List<Area> AvailableAreas { get; } = new List<Area>();
        private Area[] areas;

        public Area CurrentArea => areas[GetIndex(posX, posY)];

        public Map()
        {
            areas = new Area[Width * Height];
        }

        /// <summary>
        /// Get the 
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        protected int GetIndex(int posX, int posY)
        {
            return posX + Width * posY;
        }

        public void UpdateMap()
        {
            MoveEntities();
        }

        private void ConnectAreas()
        {
            for (int y = 0; y < Height - 1; y++)
            {
                for (int x = 0; x < Width - 1; x++)
                { 
                    areas[GetIndex(x, y)].ConnectInDirection(Direction.East);
                    areas[GetIndex(x, y)].ConnectInDirection(Direction.South);
                    areas[GetIndex(x + 1, y)].ConnectInDirection(Direction.West);
                    areas[GetIndex(x, y + 1)].ConnectInDirection(Direction.North);
                }
            }

        }

        protected void SetArea(int posX, int posY, Area area)
        {
            if (posX < 0 || posY < 0 || posX >= Width || posY >= Height)
            {
                return;
            }
            areas[GetIndex(posX, posY)] = area;
        }

        public void RandomiseAreas()
        {
            Random rand = new Random();
            List<int> uniqueIndexes = new List<int>();
            for (int i = 0; i < Width * Height; i++)
            { 
                int index;
                do
                {
                    index = rand.Next(0, AvailableAreas.Count);
                } while (uniqueIndexes.Contains(index));
                uniqueIndexes.Add(index);
            }
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    areas[GetIndex(x, y)] = AvailableAreas[uniqueIndexes[GetIndex(x, y)]];
                }
            }
            SetFixedAreas();
            ConnectAreas();
        }

        protected abstract void SetFixedAreas();

        public void MoveArea(Direction direction)
        {
            switch(direction)
            {
                case Direction.North:
                    {
                        posY--;
                    }
                    break;
                case Direction.South:
                    {
                        posY++;
                    }
                    break;
                case Direction.East:
                    {
                        posX++;
                    }
                    break;
                case Direction.West:
                    {
                        posX--;
                    }
                    break;
            }
            posX = Math.Clamp(posX, 0, Width - 1);
            posY = Math.Clamp(posY, 0, Height - 1);
            Console.Clear();
            switch(direction)
            {
                case Direction.North:
                    {
                        PlayerEntity.Instance.PosY = CurrentArea.Height - 2;
                    }
                    break;
                case Direction.South:
                    {
                        PlayerEntity.Instance.PosY = 1;
                    }
                    break;
                case Direction.East:
                    {
                        PlayerEntity.Instance.PosX = 1;
                    }
                    break;
                case Direction.West:
                    {
                        PlayerEntity.Instance.PosX = CurrentArea.Width - 2;
                    }
                    break;
            }
            PrintArea();
            PrintEntities();
        }

        public void PrintArea()
        {
            Tile[] tiles = CurrentArea.GetArea();

            for (int y = 0; y < CurrentArea.Height; y++)
            {
                for (int x = 0; x < CurrentArea.Width; x++)
                {
                    Tile currentTile = CurrentArea.GetTile(x, y);
                    Console.CursorLeft = x * 2;
                    Console.CursorTop = y;
                    Console.Write(currentTile.DisplayChar);
                    Console.Write(currentTile.DisplayChar);
                }
            }
        }

        public void PrintAreaName()
        {
            Console.CursorLeft = CurrentArea.Width * 2 + 1;
            Console.CursorTop = 0;
            Console.Write("Area: " + CurrentArea.Name);
        }

        private void PrintEntity(Entity entity)
        {
            if (entity.PosX < 0)
            {
                entity.PosX = 0;
            }
            else if (entity.PosX >= CurrentArea.Width - 1)
            {
                entity.PosX = CurrentArea.Width - 1;
            }

            if (entity.PosY < 0)
            {
                entity.PosY = 0;
            }
            else if (entity.PosY >= CurrentArea.Height - 1)
            {
                entity.PosY = CurrentArea.Height - 1;
            }

            Console.CursorLeft = entity.PosX * 2;
            Console.CursorTop = entity.PosY;

            Console.Write(entity.EntityChar);
        }

        public void PrintEntities()
        {
            PrintEntity(PlayerEntity.Instance);
            foreach (Entity entity in CurrentArea.entities)
            {
                PrintEntity(entity);
            }
        }

        public void PrintPlayerInfo()
        {
            Console.CursorTop = CurrentArea.Height + 1;
            Console.CursorLeft = 0;
            Console.WriteLine(PlayerEntity.Instance.name);
            Console.WriteLine("Health: " + PlayerEntity.Instance.HP);
            Console.WriteLine("Stamina: " + PlayerEntity.Instance.Stamina);
            Console.WriteLine("Mana: " + PlayerEntity.Instance.Mana);
        }

        private void MoveEntity(Entity entity)
        {
            int entX = entity.PosX;
            int entY = entity.PosY;
            entity.Move();

            Tile currentTile = CurrentArea.GetTile(entity.PosX, entity.PosY);

            if (CurrentArea.GetTile(entity.PosX, entity.PosY).Collidable)
            {
                entity.PosX = entX;
                entity.PosY = entY;
            }
            currentTile.OnCollision(entity, CurrentArea);

            if (entity.PosX != entX || entity.PosY != entY)
            {
                Tile previousTile = CurrentArea.GetTile(entX, entY);
                Console.SetCursorPosition(entX * 2, entY);
                Console.Write(previousTile.DisplayChar);
                Console.Write(previousTile.DisplayChar);
                PrintEntity(entity);
            }
        }

        private void MoveEntities()
        {
            MoveEntity(PlayerEntity.Instance);
            ClearNPCTextQuery();
            foreach (Entity entity in CurrentArea.entities)
            {
                MoveEntity(entity);
            }
        }

        public void ClearNPCTextQuery()
        {
            int minusX = PlayerEntity.Instance.PosX - 1;
            int minusY = PlayerEntity.Instance.PosY - 1;
            int plusX = PlayerEntity.Instance.PosX + 1;
            int plusY = PlayerEntity.Instance.PosY + 1;

            if (!(CurrentArea.GetTile(minusX, PlayerEntity.Instance.PosY) is NPCTile) &&
                !(CurrentArea.GetTile(plusX, PlayerEntity.Instance.PosY) is NPCTile) &&
                !(CurrentArea.GetTile(PlayerEntity.Instance.PosX, minusY) is NPCTile) &&
                !(CurrentArea.GetTile(PlayerEntity.Instance.PosX, plusY) is NPCTile))
            {
                ClearInteractInterface();
            }
        }

        public void ClearInteractInterface()
        {
            for (int y = 1; y <= CurrentArea.Height; y++)
            {
                Console.CursorLeft = CurrentArea.Width * 2;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - CurrentArea.Width * 2));
            }
        }
    }
}
