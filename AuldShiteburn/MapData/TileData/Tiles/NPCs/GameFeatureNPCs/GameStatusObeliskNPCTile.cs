using AuldShiteburn.ArtData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData.AreaData.Areas;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs.GameFeatureNPCs
{
    [Serializable]
    internal class GameStatusObeliskNPCTile : NPCTile
    {
        public override ConsoleColor Foreground => GameFinished ? ConsoleColor.DarkGray : ConsoleColor.Cyan;
        public bool GameFinished { get; set; } = false;

        public GameStatusObeliskNPCTile() : base("(")
        {
        }

        public override void Interaction()
        {
            List<string> numbers = new List<string>()
                {
                    "No",
                    "One",
                    "Two",
                    "Three",
                    "Four"
                };
            Area startArea = Map.Instance.ActiveAreas[Map.Instance.GetIndex(0, 0)];
            StartArea shitebreach = (StartArea)startArea;
            Utils.ClearInteractInterface();
            Utils.SetCursorInteract();
            Utils.WriteColour("Moonlight Altar", ConsoleColor.Cyan);
            if (shitebreach.NPCsRemaining > 0)
            {
                string npcs = numbers[shitebreach.NPCsRemaining];
                Utils.SetCursorInteract(2);
                Utils.WriteColour($"{npcs} lost souls still linger Shiteburn.", ConsoleColor.White);

            }
            if (shitebreach.BossesRemaining > 0)
            {
                string bosses = numbers[shitebreach.BossesRemaining];
                Utils.SetCursorInteract(3);
                Utils.WriteColour($"{bosses} dark creatures remain to be felled.", ConsoleColor.DarkGray);
            }
            GameFinished = shitebreach.NPCsRemaining == 0 && shitebreach.BossesRemaining == 0;
            if (GameFinished)
            {
                Utils.SetCursorInteract(5);
                Utils.WriteColour("Embrace the Moonlight? ", ConsoleColor.Cyan);
                Utils.WriteColour("(Y/N)");
                while (true)
                {
                    if (Utils.VerificationQuery(null))
                    {
                        Console.Clear();
                        ASCIIArt.PrintASCII(ASCIIArt.BANNER_AULDSHITEBURN, ConsoleColor.Cyan);
                        Console.CursorLeft = 5;
                        Console.CursorTop += 2;
                        Utils.WriteColour($"After many grueling years, Shiteburn is free of its taint, due to none other than {PlayerEntity.Instance.Name}", ConsoleColor.Cyan);
                        Console.CursorLeft = 5;
                        Console.CursorTop += 2;
                        Utils.WriteColour("Thank you for playing,");
                        Console.CursorLeft = 5;
                        Console.CursorTop += 1;
                        Utils.WriteColour("SID: 1543493");
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        protected override void InitLines()
        {
        }
    }
}
