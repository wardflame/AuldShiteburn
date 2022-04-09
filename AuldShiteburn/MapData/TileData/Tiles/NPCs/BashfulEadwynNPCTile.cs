using AuldShiteburn.MapData.AreaData.Areas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    internal class BashfulEadwynNPCTile : NPCTile
    {
        public override string NPCName => savedInMarket ? "Eadwyn" : "Bashful Eadwyn";
        private List<InteractionData> marketMeet = new List<InteractionData>();
        private List<InteractionData> shitebreachMeet = new List<InteractionData>();
        private bool savedInMarket = false;

        public BashfulEadwynNPCTile() : base("%")
        {
        }

        public override void Interaction()
        {
            Utils.ClearInteractInterface();
            if (!savedInMarket)
            {
                savedInMarket = CycleInteraction(marketMeet);
                StartArea shitebreach = (StartArea)Map.Instance.ActiveAreas[Map.Instance.GetIndex(0, 0)];
                shitebreach.SetTile(5, 17, this);
                Map.Instance.CurrentArea.SetTile(4, 15, AirTile);
                Map.Instance.PrintTile(4, 15);
                shitebreach.NPCsRemaining--;
            }
            else
            {
                CycleInteraction(shitebreachMeet);
            }
        }

        protected override void InitLines()
        {
            marketMeet.Add(new InteractionData(Description("A little, scrawny man cowers behind the small pantry before you.")));
            marketMeet.Add(new InteractionData(Dialogue($"H- H- H- Hello. I- I- I'm Eadwyn. Y- You killed the husks outside, didn't you?")));
            marketMeet.Add(new InteractionData(Dialogue($"Thank you, even if it wasn't i- intended for me. I will make my way back to Shitebreach.")));
            marketMeet.Add(new InteractionData(Dialogue($"I will impart a little knowledge to you, to show my gr- gratitude, yes."), true));
            marketMeet.Add(new InteractionData(Dialogue($"The Ealdorman and I were together, but we were assailed by husks...")));
            marketMeet.Add(new InteractionData(Dialogue($"We made haste from the Stables toward the Shitepile when we were separated.")));
            marketMeet.Add(new InteractionData(Dialogue($"I reckon he went to hide in the Drain—he said he would; that was our plan.")));
            marketMeet.Add(new InteractionData(Dialogue($"We left poor Lar locked in the Granary, and I believe the key was left behind with those Mad Hunters.")));
            marketMeet.Add(new InteractionData(Dialogue($"Having witnessed your might, I reckon you could help him out of there. Just a thought...")));
            shitebreachMeet.Add(new InteractionData(Dialogue($"Oh, hello again. Don't mind me, I'm just enjoying the warmth of the fire.")));
            shitebreachMeet.Add(new InteractionData(Dialogue($"You have my thanks for freeing the way back for me. You give us Shiteburners hope...")));
        }
    }
}
