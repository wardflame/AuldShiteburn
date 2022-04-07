using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.Enemies;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using AuldShiteburn.MapData.TileData.Tiles.NPCs;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class LivingQuarterArea : Area
    {
        public override string Name => "Living Quarter";
        public override int Width => 20;
        public override int Height => 20;
        public override bool CombatEncounter => true;
        public override bool BossArea => false;
        private bool EnemiesDefeated { get; set; } = false;

        protected override void AddSpecialTiles()
        {
            placeData.Add(new TilePlaceData(1, 6, new EarhNPCTile()));
            #region Earh House
            for (int x = 1; x <= 5; x++)
            {
                placeData.Add(new TilePlaceData(x, 7, Tile.WallTile));
            }
            for (int y = 2; y <= 6; y++)
            {
                placeData.Add(new TilePlaceData(5, y, Tile.WallTile));

            }
            placeData.Add(new TilePlaceData(5, 1, new DoorTile(true, KeyItem.WestResidenceKey)));
            #endregion Earh House
            #region East House
            for (int x = 16; x <= 19; x++)
            {
                if (x == 16)
                {
                    placeData.Add(new TilePlaceData(x, 13, new DoorTile(true, KeyItem.EastResidenceKey)));
                }
                else
                {
                    placeData.Add(new TilePlaceData(x, 13, Tile.WallTile));
                }
            }
            for (int x = 10; x <= 15; x++)
            {
                placeData.Add(new TilePlaceData(x, 6, Tile.WallTile));
            }
            for (int y = 1; y <= 6; y++)
            {
                placeData.Add(new TilePlaceData(10, y, Tile.WallTile));
            }
            for (int y = 6; y <= 13; y++)
            {
                placeData.Add(new TilePlaceData(15, y, Tile.WallTile));
            }
            #endregion East House
            #region South-West House
            
            #endregion South-West House
        }

        public override void InitEnemies()
        {
            Enemies.Add(new ShiteHuskEnemyEntity());
            Enemies.Add(new ShiteHuskEnemyEntity());
            Enemies.Add(new ShiteHuskEnemyEntity());
        }

        public override void OnFirstEnter()
        {
            if (!EnemiesDefeated)
            {
                if (Combat.CombatEncounter(Enemies))
                {
                    SetTile(PlayerEntity.Instance.PosX + 1, PlayerEntity.Instance.PosY, new LootTile("Spoils of War", new List<Item>(), true, true));
                    Map.Instance.PrintTile(PlayerEntity.Instance.PosX + 1, PlayerEntity.Instance.PosY);
                    EnemiesDefeated = true;
                }
                EarhNPCTile earh = GetTile(1, 6) as EarhNPCTile;
                earh.Interaction();
                FirstEnter = false;
            }
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
                        SetTile(x, y, Tile.AirTile);
                    }
                }
            }
        }
    }
}
