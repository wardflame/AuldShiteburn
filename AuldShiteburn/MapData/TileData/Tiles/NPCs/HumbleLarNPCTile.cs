using AuldShiteburn.MapData.AreaData.Areas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
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
                shitebreach.SetTile(10, 6, this);
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
            millHouseMeet.Add(new InteractionData(Description("A rotund and surprisingly relaxed man sits on a stool.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"'Allo there. I've not seen you before. Welcome, friend. Oh, no, don't you try foolin' me.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"I can see you're a good'un. Now, if you're in 'ure then I presume you've dealt with those pests. Good.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"I'll make my back to Shitebreach—t'was where I was 'eadin before those bastards arrived.")));
            millHouseMeet.Add(new InteractionData(Dialogue($"Righto, enough chit-chat. I'll see you there.")));
            shitebreachMeet.Add(new InteractionData(Dialogue($"Siwmae, friend? I'm settled and comfortable, thanks to you. I am ever grateful.")));
            shitebreachMeet.Add(new InteractionData(Dialogue($"Seems you're doin' Elatha's work 'round 'ure. Keep at it.")));
        }
    }
}
