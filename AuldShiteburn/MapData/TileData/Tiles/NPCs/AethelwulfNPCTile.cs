using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    class AethelwulfNPCTile : NPCTile
    {
        public override string NPCName => "Aethulwulf";
        public AethelwulfNPCTile() : base("&&")
        {
        }

        protected override void Interaction(Area area)
        {
            Map.Instance.ClearInteractInterface();

            Narration("Before you sits a dirty, lowly man in sackcloth.", area, 3);
            Dialogue("Ah, yes. Thou comst to Shiteburn, cursed plot of filth. I am Aethelwulf, a...umm...-", area, 4);
            Dialogue("It would seem I can't get myself out of this place. Perhaps you could help?", area, 5);
            Narration("Accept (y) Refuse (n)", area, 6);

            bool decision = false;
            while (!decision)
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.Y:
                        {
                            Dialogue("Yes. Good. My memories are scattered, the journey all but forgotten. All that remains of me now is this shell, ", area, 7);
                            Dialogue("and the shite on my hands.", area, 8);
                            Narration("He looks at his hands in silence. Feces are smeared around the tips of his fingers. He looks up at you.", area, 9);
                            Dialogue("I beg you, cleanse me of this filth.", area, 10);
                            decision = true;
                        }
                        break;
                    case ConsoleKey.N:
                        {
                            Dialogue("Hmph. You mongrels... All the same. Shame on you.", area, 7);
                            decision = true;
                        }
                        break;
                }
            }
            
        }
    }
}
