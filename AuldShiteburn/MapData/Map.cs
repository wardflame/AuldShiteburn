using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData
{
    [Serializable]
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
        private static List<Type> interactionTiles = new List<Type>()
        {
            typeof(InteractionTile),
            typeof(DoorTile)
        };

        public PlayerEntity player;
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
        /// Get index by simulating rows and columns. Multiply posY by Width to search down rows,
        /// then add by posX to search across columns.
        /// </summary>
        /// <param name="posX">Location across the columns we're searching.</param>
        /// <param name="posY">Location down the rows we're searching.</param>
        /// <returns></returns>
        public int GetIndex(int posX, int posY)
        {
            return (Width * posY) + posX;
        }

        #region Areas
        /// <summary>
        /// Move all the entities around an area.
        /// </summary>
        public void UpdateArea()
        {
            MoveEntities();
        }

        /// <summary>
        /// Should an area exist in any cardinal direction to the
        /// area at the current index, replace the border tiles in
        /// each to represent a passage to move between the areas.
        /// For instance, if an area exists to the east of area 0, 0,
        /// create passage tiles in area 0, 0, in the east and area 0, 1,
        /// in the west.
        /// </summary>
        private void ConnectAreas()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x > 0)
                    {
                        areas[GetIndex(x, y)].ConnectInDirection(Direction.West);
                    }
                    if (y > 0)
                    {
                        areas[GetIndex(x, y)].ConnectInDirection(Direction.North);
                    }
                    if (x < Width - 1)
                    {
                        areas[GetIndex(x, y)].ConnectInDirection(Direction.East);
                    }
                    if (y < Height - 1)
                    {
                        areas[GetIndex(x, y)].ConnectInDirection(Direction.South);
                    }

                }
            }
        }

        /// <summary>
        /// So long as the posX/Y values are within the map's bounds,
        /// set the area at posX/Y index to the desired area.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="area"></param>
        protected void SetArea(int posX, int posY, Area area)
        {
            if (posX < 0 || posY < 0 || posX >= Width || posY >= Height)
            {
                return;
            }
            areas[GetIndex(posX, posY)] = area;
        }

        /// <summary>
        /// Create a list of random integers between 0 and the
        /// number of available area. Each number must be unique.
        /// Take these numbers and iterate through the bounds of
        /// the map. For each index, take an integer from the random
        /// list, select an area for the available areas according to
        /// that number and populate the areas array.
        /// 
        /// After, ensure fixed areas are set and then connect the areas
        /// to each other.
        /// </summary>
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

        /// <summary>
        /// Per map, assign fixed areas to the area array. This is used for
        /// setting the starting and ending areas, placed at the start and end
        /// of the array.
        /// </summary>
        protected abstract void SetFixedAreas();

        /// <summary>
        /// Increment/decrement the posX/Y index according to the desired cardinal direction
        /// to traverse. Clamp the result in the event player manages to get out of bounds.
        /// Then, place player position according to the location of entry. If going south,
        /// place north in next area.
        /// </summary>
        /// <param name="direction">The cardinal direction we're attempting to traverse.</param>
        public void MoveArea(Direction direction)
        {
            switch (direction)
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
            switch (direction)
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
            ClearAreaName();
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            PrintMap();
        }
        #endregion Areas

        #region Printing
        public void PrintMap()
        {
            PrintAreaName();
            PrintPlayerInfo();
            PrintArea();
            PrintEntities();
        }

        /// <summary>
        /// Return the tile array for the current area. Then,
        /// until x and y have reached the bounds of the area,
        /// write the display character for each tile. Because
        /// the area is intend to be square, characters are written
        /// twice to produce the square look.
        /// </summary>
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
                    Console.ForegroundColor = currentTile.Foreground;
                    Console.BackgroundColor = currentTile.Background;
                    Console.Write(currentTile.DisplayChar);
                    Console.Write(currentTile.DisplayChar);
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Place cursor to the top and just-right of the
        /// area and print the current area name.
        /// </summary>
        public void PrintAreaName()
        {
            Console.CursorLeft = CurrentArea.Width * 2 + 1;
            Console.CursorTop = 0;
            Console.Write("Area: " + CurrentArea.Name);
        }

        /// <summary>
        /// Ensure the entity is not outside the area bounds,
        /// then print the entity by its display character to the
        /// x/y position it claims to inhabit in the area.
        /// </summary>
        /// <param name="entity">Entity to print.</param>
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

        /// <summary>
        /// Print the player display character, then any other entities.
        /// </summary>
        public void PrintEntities()
        {
            PrintEntity(PlayerEntity.Instance);
            if (CurrentArea.entities.Count > 0)
            {
                foreach (Entity entity in CurrentArea.entities)
                {
                    PrintEntity(entity);
                }
            }
        }

        /// <summary>
        /// Print the player's name and stats beneath the area map.
        /// </summary>
        public void PrintPlayerInfo()
        {
            Utils.SetCursorPlayerStat();
            Console.Write($"{PlayerEntity.Instance.Name} the {PlayerEntity.Instance.Class.Name}");
            Utils.SetCursorPlayerStat(offsetY: 1);
            Console.Write($"Health: {PlayerEntity.Instance.HP}");
            if (PlayerEntity.Instance.UsesStamina)
            {
                Utils.SetCursorPlayerStat(offsetY: 2);
                Console.Write($"Stamina: {PlayerEntity.Instance.Stamina}");
            }
            if (PlayerEntity.Instance.UsesMana)
            {
                Utils.SetCursorPlayerStat(offsetY: 2);
                Console.Write($"Mana: {PlayerEntity.Instance.Mana}");
            }
            Utils.SetCursorPlayerStat(offsetY: 3);
            Console.Write($"Equipped Weapon: {PlayerEntity.Instance.Mana}");

            Utils.SetCursorInventory();
            foreach (WeaponItem item in PlayerEntity.Instance.Inventory)
            {
                Console.Write($"(i) Inventory");
            }
        }
        #endregion Printing

        #region Moving Entities
        /// <summary>
        /// Store entity's starting position, allow entity to move. Get the tile at entity's new location.
        /// If new tile is collidable, move the entity back to its former location. If the entity has
        /// successfully moved, check the previous tile location. Replace old tile display character to what
        /// it was when the area was generated. In most cases, the 'air tile.'
        /// </summary>
        /// <param name="entity">Entity to move.</param>
        private void MoveEntity(Entity entity)
        {
            int entX = entity.PosX;
            int entY = entity.PosY;
            entity.Move();

            Tile currentTile = CurrentArea.GetTile(entity.PosX, entity.PosY);

            currentTile.OnCollision(entity);
            if (CurrentArea.GetTile(entity.PosX, entity.PosY).Collidable)
            {
                entity.PosX = entX;
                entity.PosY = entY;
            }

            if (entity.PosX != entX || entity.PosY != entY)
            {
                Tile previousTile = CurrentArea.GetTile(entX, entY);
                Console.SetCursorPosition(entX * 2, entY);
                Console.ForegroundColor = previousTile.Foreground;
                Console.Write(previousTile.DisplayChar);
                Console.Write(previousTile.DisplayChar);
                Console.ResetColor();
                PrintEntity(entity);
            }
        }

        /// <summary>
        /// Move the player, clear any NPC text that might be on the left if out of range of NPC,
        /// then move each entity that could be in the area too.
        /// </summary>
        private void MoveEntities()
        {
            MoveEntity(PlayerEntity.Instance);
            ClearNPCTextQuery();
            foreach (Entity entity in CurrentArea.entities)
            {
                MoveEntity(entity);
            }
        }
        #endregion Moving Entities

        #region Clear UI
        /// <summary>
        /// Place cursor to the top and just-right of the
        /// area and print space characters to the end of
        /// the row.
        /// </summary>
        public void ClearAreaName()
        {
            Console.CursorLeft = CurrentArea.Width * 2;
            Console.CursorTop = 0;
            for (int x = CurrentArea.Width * 2; x < Console.WindowWidth; x++)
            {
                Console.Write(new string(' ', Console.WindowWidth - CurrentArea.Width * 2));
            }
        }

        /// <summary>
        /// If the player is not within the area of a tile type, remove text from the interaction
        /// region of the screen.
        /// </summary>
        public void ClearNPCTextQuery()
        {
            int minusX = PlayerEntity.Instance.PosX - 1;
            int minusY = PlayerEntity.Instance.PosY - 1;
            int plusX = PlayerEntity.Instance.PosX + 1;
            int plusY = PlayerEntity.Instance.PosY + 1;

            if (!interactionTiles.Exists(text => CheckTileType(minusX, PlayerEntity.Instance.PosY, text)) &&
                !interactionTiles.Exists(text => CheckTileType(plusX, PlayerEntity.Instance.PosY, text)) &&
                !interactionTiles.Exists(text => CheckTileType(PlayerEntity.Instance.PosX, minusY, text)) &&
                !interactionTiles.Exists(text => CheckTileType(PlayerEntity.Instance.PosX, plusY, text)))
            {
                ClearInteractInterface(offsetY: 3);
            }
        }

        /// <summary>
        /// Get the tile at posX/Y and check if it's the class, or subclass, of type.
        /// </summary>
        /// <param name="posX">Area x coordinate.</param>
        /// <param name="posY">Area y coordinate.</param>
        /// <param name="type">Type to check.</param>
        /// <returns>Returns true if tile is class/subclass of type.</returns>
        private bool CheckTileType(int posX, int posY, Type type)
        {
            Tile tile = CurrentArea.GetTile(posX, posY);
            return tile.GetType().IsSubclassOf(type) || tile.GetType() == type;
        }

        /// <summary>
        /// Place the cursor 1 row down in final column of area width. Then, until reaching
        /// the area height, go down each row and replace any text with space characters until
        /// the end of the line.
        /// </summary>
        public void ClearInteractInterface(int offsetX = 0, int offsetY = 2)
        {
            for (int y = 1; y <= Utils.UIInteractHeight + offsetY; y++)
            {
                Console.CursorLeft = Utils.UIInteractOffset + offsetX;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - Utils.UIInteractOffset));
            }
        }
        #endregion Clear UI
    }
}
