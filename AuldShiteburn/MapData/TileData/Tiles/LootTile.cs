using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.ConsumableData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal class LootTile : Tile
    {
        private const float CHANCE_KEY = 0.95f;
        private const float CHANCE_WEAPON = 0.6f;
        private const float CHANCE_ARMOUR = 0.4f;
        private const float CHANCE_CONSUMABLE = 0.0f;

        private List<Item> items = new List<Item>();
        private bool looted = false;

        public LootTile(List<Item> items, bool randomised) : base("?", false, ConsoleColor.Magenta)
        {
            if (!randomised)
            {
                this.items = items;
            }
            else
            {
                Random rand = new Random();
                double chance = rand.NextDouble();
                if (chance >= CHANCE_KEY)
                {
                    List<Item> keys = items.FindAll(key => key is KeyItem);
                    Item keyItem = keys[rand.Next(keys.Count)];
                    this.items.Add(keyItem);
                }
                if (chance >= CHANCE_WEAPON)
                {
                    List<Item> weapons = items.FindAll(weapon => weapon is WeaponItem);
                    Item weaponItem = weapons[rand.Next(weapons.Count)];
                    this.items.Add(weaponItem);
                }
                if (chance >= CHANCE_ARMOUR)
                {
                    List<Item> armours = items.FindAll(armour => armour is ArmourItem);
                    Item armourItem = armours[rand.Next(armours.Count)];
                    this.items.Add(armourItem);
                }
                if (chance >= CHANCE_CONSUMABLE)
                {
                    List<Item> consumables = items.FindAll(consumable => consumable is ConsumableItem);
                    Item consumableItem = consumables[rand.Next(consumables.Count)];
                    this.items.Add(consumableItem);
                }
            }
        }

        public override void OnCollision(Entity entity)
        {
            
        }
    }
}
