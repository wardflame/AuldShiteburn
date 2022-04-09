using AuldShiteburn.ArtData;
using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.MapData.AreaData.Areas;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs.GameFeatureNPCs
{
    [Serializable]
    internal class GameStatusAltarNPCTile : NPCTile
    {
        public override ConsoleColor Foreground => GameFinished ? ConsoleColor.DarkGray : ConsoleColor.Cyan;
        public bool GameFinished { get; set; } = false;

        public GameStatusAltarNPCTile() : base("(")
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

            #region Lost Souls Check
            string npcs = numbers[shitebreach.NPCsRemaining];
            Utils.SetCursorInteract(2);
            Utils.WriteColour($"{npcs} lost souls still linger Shiteburn.", ConsoleColor.White);
            #endregion Lost Souls Check

            #region Bosses Remaining Check
            if (shitebreach.BossesRemaining > 0)
            {
                string bosses = numbers[shitebreach.BossesRemaining];
                Utils.SetCursorInteract(3);
                Utils.WriteColour($"{bosses} dark creatures remain to be felled.", ConsoleColor.DarkGray);
            }
            #endregion Bosses Remaining Check

            #region Dull Amulet Check
            if (PlayerEntity.Instance.CarryingDullAmulet)
            {
                Utils.SetCursorInteract(5);
                if (Utils.VerificationQuery("Cleanse the Shite-stained Amulet? (Y/N)", ConsoleColor.Cyan))
                {
                    for (int i = 0; i < PlayerEntity.Instance.Inventory.Row; i++)
                    {
                        if (PlayerEntity.Instance.Inventory.ItemList[i, 3].Name == KeyItem.ShitestainedAmulet.Name)
                        {
                            PlayerEntity.Instance.Inventory.ItemList[i, 3] = KeyItem.MoonlitAmulet;
                            break;
                        }
                    }
                    Utils.SetCursorInteract(6);
                    Utils.WriteColour("It is done...", ConsoleColor.White);
                    PlayerEntity.Instance.PrintInventory();
                }
                else
                {
                    return;
                }
            }
            #endregion Dull Amulet Check

            GameFinished = shitebreach.NPCsRemaining == 0 && shitebreach.BossesRemaining == 0;
            if (GameFinished)
            {
                if (PlayerEntity.Instance.CarryingDullAmulet) Utils.SetCursorInteract(5);
                else Utils.SetCursorInteract(8);
                if (Utils.VerificationQuery("Embrace the Moonlight? (Y/N)", ConsoleColor.Cyan))
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
            }
        }

        protected override void InitLines()
        {
        }
    }
}
