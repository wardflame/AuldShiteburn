using AuldShiteburn.EntityData;

namespace AuldShiteburn.MapData.TileData
{
    internal abstract class Tile
    {
        public static BasicTile AirTile { get; } = new BasicTile(" ", false);
        public static BasicTile WallTile { get; } = new BasicTile("+", true);


        public string DisplayChar { get; }
        public bool Collidable { get; }

        public Tile(string displayChar, bool collidable)
        {
            DisplayChar = displayChar;
            Collidable = collidable;
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
