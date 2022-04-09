using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    internal class CrestfallenGrimrNPCTile : NPCTile
    {
        public override string NPCName => "Crestfallen Grimr";
        private List<InteractionData> introduction = new List<InteractionData>();
        private List<InteractionData> afterIntro = new List<InteractionData>();
        private bool finishedIntro = false;

        public CrestfallenGrimrNPCTile() : base("%")
        {
        }

        public override void Interaction()
        {
            Utils.ClearInteractInterface();
            if (!finishedIntro)
            {
                finishedIntro = CycleInteraction(introduction);
            }
            else
            {
                CycleInteraction(afterIntro);
            }
        }

        protected override void InitLines()
        {
            introduction.Add(new InteractionData(Description("A slender man, grim-faced, passes a look at you.")));
            introduction.Add(new InteractionData(Dialogue($"New blood comes to find glory in Shiteburn? Hmph. Forget it.")));
            introduction.Add(new InteractionData(Dialogue($"I am Grimr, native to the village. This place should be left to rot.")));
            introduction.Add(new InteractionData(Dialogue($"I would have tried to leave this place were it not for that Ormod.")));
            introduction.Add(new InteractionData(Dialogue($"Now...I feel partially responsible for his safety, fool that I am.")));
            introduction.Add(new InteractionData(Dialogue($"I'll presume he's asked you about his friend, Earh. He's off East, apparently.")));
            introduction.Add(new InteractionData(Dialogue($"I won't be going for 'im. I know full well what lies that way; stear clear.")));
            introduction.Add(new InteractionData(Description($"Grimr sighs.")));
            introduction.Add(new InteractionData(Dialogue($"If you're going to stay, you can use that rotting chest behind me. I'll keep your bits safe.")));
            introduction.Add(new InteractionData(Dialogue($"Off West there is the Moonlight Obelisk, once a gathering place for us Shiteburners.")));
            introduction.Add(new InteractionData(Dialogue($"I haven't dared go near it since everything fell apart, and each day it grows dimmer.")));
            introduction.Add(new InteractionData(Dialogue($"It calls to me no longer, but it might to you. Now, leave me to rest.")));
            afterIntro.Add(new InteractionData(Dialogue($"If you're going to stay, you can use that rotting chest behind me. I'll keep it safe.")));
            afterIntro.Add(new InteractionData(Dialogue($"Off West there is the Moonlight Obelisk, once a gathering place for is Shiteburners.")));
            afterIntro.Add(new InteractionData(Dialogue($"I haven't dared go near it since everything fell apart, and each day it grows dimmer.")));
            afterIntro.Add(new InteractionData(Dialogue($"It calls to me no longer, but it might to you. Now, leave me to rest.")));
        }
    }
}
