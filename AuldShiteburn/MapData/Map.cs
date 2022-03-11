using AuldShiteburn.EntityData;
using AuldShiteburn.MapData.TileData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData
{
    internal class Map
    {
        public string Name { get; set; }
        public List<Area> Zones = new List<Area>();
        public Area CurrentArea => Zones[0];

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
            PrintEntity(PlayerEntity.Instance);
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

        private void MoveEntities()
        {
            MoveEntity(PlayerEntity.Instance);
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
        }

        public void UpdateMap()
        {
            MoveEntities();
            PrintArea();
        }
    }
}
