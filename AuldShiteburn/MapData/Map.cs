using AuldShiteburn.EntityData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData
{
    internal abstract class Map
    {
        public abstract string Name { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }
        protected List<Area> AvailableAreas { get; } = new List<Area>();
        private Area[] areas;

        public Area CurrentArea => areas[0];

        public Map()
        {
            areas = new Area[Width * Height];
        }
        protected int GetIndex(int posX, int posY)
        {
            return posX + Width * posY;
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
        }



        protected abstract void SetFixedAreas();

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

        private void MoveEntities()
        {
            MoveEntity(PlayerEntity.Instance);
            CheckNPCTile();
            foreach (Entity entity in CurrentArea.entities)
            {
                MoveEntity(entity);
            }
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

        public void UpdateMap()
        {
            MoveEntities();
        }

        public void CheckNPCTile()
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
                for (int y = 0; y < Console.WindowHeight; y++)
                {
                    Console.CursorLeft = CurrentArea.Width * 2;
                    Console.CursorTop = y;
                    Console.Write(new string(' ', Console.WindowWidth - CurrentArea.Width * 2));
                }
            }
            

        }

        public void ClearRightInterface()
        {
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                Console.CursorLeft = CurrentArea.Width * 2;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - CurrentArea.Width * 2));
            }
        }
    }
}
