using AuldShiteburn.MapData.AreaData.Areas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    internal class HumbleLarNPCTile : NPCTile
    {
        public override string NPCName => savedInMillHouse ? "Humble Lar" : "Lar";
        private List<InteractionData> millHouseMeet = new List<InteractionData>();
        private List<InteractionData> shitebreachMeet = new List<InteractionData>();
        private bool savedInMillHouse = false;

        public HumbleLarNPCTile() : base("%")
        {
        }

        public override void Interaction()
        {
            Utils.ClearInteractInterface();
            if (!savedInMillHouse)
            {
                savedInMillHouse = CycleInteraction(millHouseMeet);
                StartArea shitebreach = (StartArea)Map.Instance.ActiveAreas[Map.Instance.GetIndex(0, 0)];
                shitebreach.SetTile(5, 17, this);
                Map.Instance.CurrentArea.SetTile(10, 11, AirTile);
                Map.Instance.PrintTile(10, 11);
                shitebreach.NPCsRemaining--;
            }
            else
            {
                CycleInteraction(shitebreachMeet);
            }
        }

        protected override void InitLines()
        {
            millHouseMeet.Add(new InteractionData(Description("A little, scrawny man cowers behind the small pantry before you.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"H- H- H- Hello. I- I- I'm Eadwyn. Y- You killed the husks outside, didn't you?")));
            millHouseMeet.Add(new InteractionData(Dialogue($"Thank you, even if it wasn't i- intended for me. I will make my way back to Shitebreach.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"I will impart a little knowledge to you, to show my gr- gratitude, yes.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"The Ealdorman and I were together, but we were assailed by husks...")));
            millHouseMeet.Add(new InteractionData(Dialogue($"We made haste from the Stables toward the Shitepile when we were separated.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"I reckon he went to hide in the Drain—he said he would; that was our plan.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"We left poor Lar locked in the Mill House, and I believe the key was left behind with those Mad Hunters.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"Having witnessed your might, I reckon you could help him out of there. Just a thought...")));
            shitebreachMeet.Add(new InteractionData(Dialogue($"Oh, hello again. Don't mind me, I'm just enjoying the warmth of the fire.")));
            shitebreachMeet.Add(new InteractionData(Dialogue($"You have my thanks for freeing the way back for me. You give us Shiteburners hope...")));
        }
    }
}
