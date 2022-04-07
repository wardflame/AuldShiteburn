using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    internal class GameStatusNPCTile : NPCTile
    {
        public override ConsoleColor Foreground => GameFinished ? ConsoleColor.DarkGray : ConsoleColor.DarkYellow;
        public bool GameFinished { get; set; } = false;

        public GameStatusNPCTile() : base("[")
        {
        }

        public override void Interaction()
        {
            if (!GameFinished)
            {
                List<string> numbers = new List<string>()
                {
                    "One",
                    "Two",
                    "Three",
                    "Four"
                };
            }
        }

        protected override void InitLines()
        {
        }
    }
}
