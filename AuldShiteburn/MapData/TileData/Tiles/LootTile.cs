using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.ConsumableData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal class LootTile : Tile
    {
        private const float CHANCE_KEY = 0.95f;
        private const float CHANCE_WEAPON = 0.6f;
        private const float CHANCE_ARMOUR = 0.4f;
        private const float CHANCE_CONSUMABLE = 0.0f;

        public override bool Collidable => Looted ? false : true;
        public override ConsoleColor Foreground => Looted ? ConsoleColor.DarkGray : ConsoleColor.Magenta;

        public List<Item> items = new List<Item>();
        public bool Temporary { get; set; }
        public bool Looted { get; set; } = false;

        public LootTile(List<Item> items, bool randomised, bool temporary = false) : base("?", false)
        {
            Temporary = temporary;
            if (!randomised)
            {
                this.items = items;
            }
            /// If the items in the loot tile aren't fixed, generate them by chance.
            else
            {
                Random rand = new Random();
                double chance = rand.NextDouble();
                if (chance >= CHANCE_KEY)
                {
                    KeyItem lootKey = KeyItem.AllKeys[rand.Next(KeyItem.AllKeys.Count)];
                    this.items.Add(lootKey);
                }
                chance = rand.NextDouble();
                if (chance >= CHANCE_WEAPON)
                {
                    this.items.Add(WeaponItem.GenerateWeapon());
                }
                chance = rand.NextDouble();
                if (chance >= CHANCE_ARMOUR)
                {
                    List<Item> armours = items.FindAll(armour => armour is ArmourItem);
                    Item armourItem = armours[rand.Next(armours.Count)];
                    this.items.Add(armourItem);
                }
                chance = rand.NextDouble();
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
            foreach (Item item in items.ToArray())
            {
                LootStock();
                if (PlayerEntity.Instance.Inventory.AddItem(item, true))
                {
                    items.Remove(item);
                }
            }
            if (LootStock() == 0)
            {
                if (Temporary)
                {
                    Map.Instance.CurrentArea.SetTile(entity.PosX, entity.PosY, AirTile);
                }
                else
                {
                    items.Clear();
                    Looted = true;
                }                
            }
            Utils.ClearInteractInterface();
        }

        private int LootStock()
        {
            Utils.ClearInteractInterface(5);
            Utils.SetCursorInteract();
            Console.WriteLine("Loot Items: ");
            int lootStock = 0;
            foreach (Item item in items)
            {
                lootStock++;
                Utils.SetCursorInteract(lootStock);
                Console.WriteLine($"{lootStock}. {item.Name}");
            }
            return lootStock;
        }
    }
}
