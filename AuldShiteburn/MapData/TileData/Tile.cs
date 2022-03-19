using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.MapData.TileData
{
    [Serializable]
    internal abstract class Tile
    {
        public static BasicTile AirTile { get; } = new BasicTile(" ", false);
        public static BasicTile WallTile { get; } = new BasicTile("#", true, ConsoleColor.DarkGray);


        public string DisplayChar { get; }
        public bool Collidable { get; }
        public ConsoleColor Foreground { get; }
        public ConsoleColor Background { get; }

        public Tile(string displayChar, bool collidable, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            DisplayChar = displayChar;
            Collidable = collidable;
            Foreground = foreground;
            Background = background;
        }

        public override string ToString()
        {
            return DisplayChar;
        }

        public Tile Clone()
        {
            Tile tile = (Tile)MemberwiseClone();
            return tile;
        }

        public abstract void OnCollision(Entity entity, Area area);
    }
}
