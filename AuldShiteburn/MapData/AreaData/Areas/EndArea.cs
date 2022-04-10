using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.Enemies;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles.NPCs;
using AuldShiteburn.MapData.TileData.Tiles.NPCs.NarrationNPCs;
using System;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class EndArea : Area
    {
        public override string Name => "Foulstench, Heart of the Shite";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => true;
        public override bool BossArea => true;

        protected override void AddSpecialTiles()
        {
            placeData.Add(new TilePlaceData(1, 1, new FoulstenchNarrationNPCTile()));
        }

        public override void InitEnemies()
        {
            Enemies.Add(new ShiteAvatarEnemyEntity());
        }

        public override void OnFirstEnter()
        {
            Tile narrationTile = GetTile(1, 1);
            FoulstenchNarrationNPCTile narration = (FoulstenchNarrationNPCTile)narrationTile;
            narration.Interaction();
            Utils.ClearInteractInterface();
            Tile tile = Tile.FinalArenaTile;
            if (!PlayerEntity.Instance.TookFromOrmod)
            {
                tile.Foreground = ConsoleColor.Cyan;
                for (int y = 1; y <= 18; y++)
                {
                    for (int x = 1; x <= 18; x++)
                    {
                        SetTile(x, y, tile);
                        Map.Instance.PrintTile(x, y);
                    }
                }
                Map.Instance.PrintTile(PlayerEntity.Instance.PosX, PlayerEntity.Instance.PosY);
                Enemies[0].HP -= 60;
            }
            if (!InitiateCombat(true)) return;
            BossDefeated = true;
            StartArea shitebreach = (StartArea)Map.Instance.ActiveAreas[Map.Instance.GetIndex(0, 0)];
            shitebreach.BossesRemaining--;
        }

        protected override void TileGeneration()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                    {
                        SetTile(x, y, Tile.WallTile);
                    }
                    else
                    {
                        SetTile(x, y, Tile.FinalArenaTile);
                    }
                }
            }
        }
    }
}
