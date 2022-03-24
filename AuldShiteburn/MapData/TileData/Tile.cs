using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.MapData.TileData
{
    [Serializable]
    internal abstract class Tile
    {
        public static BasicTile AirTile { get; } = new BasicTile(" ", false);
        public static BasicTile WallTile { get; } = new BasicTile("#", true, ConsoleColor.DarkGray);

        public virtual string DisplayChar { get; }
        public virtual bool Collidable { get; }
        public virtual ConsoleColor Foreground { get; protected set; }
        public virtual ConsoleColor Background { get; protected set; }

        public Tile(string displayChar, bool collidable, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            DisplayChar = displayChar;
            Collidable = collidable;
            Foreground = foreground;
            Background = background;
        }

        /// <summary>
        /// Get the display character of the tile.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayChar;
        }

        /// <summary>
        /// Make a clone of a tile.
        /// </summary>
        /// <returns>Returns a clone of the tile used on.</returns>
        public Tile Clone()
        {
            Tile tile = (Tile)MemberwiseClone();
            return tile;
        }

        /// <summary>
        /// Run unique code when a player, or another entity
        /// makes contact with this tile.
        /// </summary>
        /// <param name="entity">Entity making contact with tile.</param>
        public abstract void OnCollision(Entity entity);
    }
}
