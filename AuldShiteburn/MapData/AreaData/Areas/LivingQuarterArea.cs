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

        protected override void AddSpecialTiles()
        {
            placeData.Add(new TilePlaceData(3, 1, new OrmodNPCTile()));
            placeData.Add(new TilePlaceData(4, 4, new DoorTile(true, KeyItem.ResidenceKey)));
        }

        protected override void InitEnemies()
        {
            Random rand = new Random();
            enemies.Add(new ShiteHuskEnemyEntity(rand.Next(6, 13)));
        }

        public override void OnFirstEnter()
        {
            if (Combat.CombatEncounter(enemies))
            {
                SetTile(PlayerEntity.Instance.PosX + 1, PlayerEntity.Instance.PosY, new LootTile("Spoils of War", new List<Item>(), true, true));
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
