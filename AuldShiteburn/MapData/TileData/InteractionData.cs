using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData
{
    [Serializable]
    internal struct InteractionData
    {
        public string line;
        public bool decision;
        public ConsoleColor foreground;
        public ConsoleColor background;

        public InteractionData(string line, bool decision = false, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            this.line = line;
            this.decision = decision;
            this.foreground = foreground;
            this.background = background;
        }
    }
}
