using System;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal abstract class NPCTile : InteractionTile
    {
        public override ConsoleColor Foreground => ConsoleColor.DarkCyan;
        public NPCTile(string displayChar) : base(displayChar)
        {
        }

        protected override void InitLines()
        {
        }

        public override void Interaction()
        {
        }
    }
}
