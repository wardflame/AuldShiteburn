using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using AuldShiteburn.MapData.TileData.Tiles.NPCs;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.AreaData.Areas
{
    [Serializable]
    class StartArea : Area
    {
        public override string Name => "Shitebreach";
        public override int Width => 20;
        public override int Height => 20;

        protected override void AddSpecialTiles()
        {
            placeData.Add(new TilePlaceData(5, 1, new OrmodNPCTile()));
            for (int i = 1; i < 4; i++)
            {
                placeData.Add(new TilePlaceData(6, i, Tile.WallTile));
            }
            for (int i = 1; i < 7; i++)
            {
                placeData.Add(new TilePlaceData(i, 5, Tile.WallTile));
            }
            placeData.Add(new TilePlaceData(6, 4, new DoorTile(false)));
            placeData.Add(new TilePlaceData(1, 4,
                new LootTile(
                    new List<Item>()
                    {
                        WeaponItem.GenerateWeapon(),
                        ArmourItem.Maille
                    },
                    false)));
            placeData.Add(new TilePlaceData(5, 4, new TrapTile()));
        }

        protected override void InitEntities()
        {
        }

        protected override void OnFirstEnter()
        {
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
