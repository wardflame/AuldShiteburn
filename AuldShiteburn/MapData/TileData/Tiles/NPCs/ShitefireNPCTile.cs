using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    internal class ShitefireNPCTile : NPCTile
    {
        public override string NPCName => "Shitefire";
        public override ConsoleColor Foreground => ConsoleColor.DarkRed;
        private List<InteractionData> randomiseAreas = new List<InteractionData>();

        public ShitefireNPCTile() : base("$")
        {
        }

        public override void Interaction()
        {
            if (CycleInteraction(randomiseAreas, shitefire: true))
            {
                Map.Instance.ActiveAreas[Map.Instance.GetIndex(0, 1)] = null;
                Map.Instance.ActiveAreas[Map.Instance.GetIndex(0, 2)] = null;
                Map.Instance.ActiveAreas[Map.Instance.GetIndex(1, 2)] = null;
                Map.Instance.RandomiseAreas();
                Map.Instance.ConnectAreas();
                foreach (Area area in Map.Instance.ActiveAreas)
                {
                    if (area.CombatEncounter && area.Enemies.Count < 1)
                    {
                        if (!area.BossArea)
                        {
                            area.InitEnemies();
                            area.EnemiesDefeated = false;
                        }
                    }
                }
                Random rand = new Random();
                if (rand.NextDouble() < 0.05)
                {
                    LootTile.GenerateLootTile();
                }
                Utils.ClearInteractInterface();
                Utils.SetCursorInteract();
                Utils.WriteColour("It is done...", ConsoleColor.DarkYellow);
                Utils.SetCursorInteract(2);
                Utils.WriteColour("Press any key to continue.");
                Console.ReadKey(true);
            }
            Utils.ClearInteractInterface();
        }

        protected override void InitLines()
        {
            randomiseAreas.Add(new InteractionData(Description($"A lowly burning pile of shite. Take in its fumes and see the world anew.")));
            randomiseAreas.Add(new InteractionData(Description($"Enemies will be refreshed and some areas will randomised. Continue?"), true));
        }
    }
}
