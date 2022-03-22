using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    class PassageTile : Tile
    {
        public Direction Direction { get; set; }

        public PassageTile() : base("-", true, ConsoleColor.DarkYellow)
        {
        }

        public override void OnCollision(Entity entity)
        {
            if (entity is PlayerEntity)
            {
                Map.Instance.MoveArea(Direction);
            }
        }
    }
}
