using System;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal abstract class NPCTile : InteractionTile
    {
        public NPCTile(string displayChar) : base(displayChar)
        {
        }

        protected override void InitLines()
        {
        }

        protected override void Interaction()
        {
        }
    }
}
