using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    internal class ShitefireNPCTile : NPCTile
    {
        private List<InteractionData> randomiseAreas = new List<InteractionData>();

        public ShitefireNPCTile() : base("@")
        {
            Foreground = ConsoleColor.DarkYellow;
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
                        }
                    }
                }
                Random rand = new Random();
                if (rand.NextDouble() < 0.05)
                {
                    LootTile.GenerateLootTile();
                }
                Utils.ClearInteractInterface();
            }
        }

        protected override void InitLines()
        {
            randomiseAreas.Add(new InteractionData(Description($"A lowly burning pile of shite. Take in its fumes and see the world anew.")));
            randomiseAreas.Add(new InteractionData(Description($"(Enemies will be refreshed and some areas will randomise.) Continue?"), true));
        }
    }
}
