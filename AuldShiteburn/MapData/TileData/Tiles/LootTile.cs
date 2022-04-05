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
        string Message { get; }
        public List<Item> Items { get; } = new List<Item>();
        public bool Temporary { get; set; }
        public bool Looted { get; set; } = false;

        public LootTile(string message, List<Item> itemList, bool randomised, bool temporary = false) : base("?", false)
        {
            Message = message;
            Temporary = temporary;
            if (!randomised)
            {
                Items = itemList;
            }
            /// If the items in the loot tile aren't fixed, generate them by chance.
            else
            {
                Random rand = new Random();
                double chance = rand.NextDouble();
                if (chance >= CHANCE_KEY)
                {
                    KeyItem lootKey = KeyItem.AllKeys[rand.Next(KeyItem.AllKeys.Count)];
                    Items.Add(lootKey);
                }
                chance = rand.NextDouble();
                if (chance >= CHANCE_WEAPON)
                {
                    Items.Add(WeaponItem.GenerateWeapon());
                }
                chance = rand.NextDouble();
                if (chance >= CHANCE_ARMOUR)
                {
                    ArmourItem armour = ArmourItem.AllArmours[rand.Next(ArmourItem.AllArmours.Count)];
                    Items.Add(armour);
                }
                chance = rand.NextDouble();
                if (chance >= CHANCE_CONSUMABLE)
                {
                    ConsumableItem consumable = ConsumableItem.AllConsumables[rand.Next(ConsumableItem.AllConsumables.Count)];
                    Items.Add(consumable);
                }
            }
        }

        public override void OnCollision(Entity entity)
        {
            foreach (Item item in Items.ToArray())
            {
                LootStock();
                if (PlayerEntity.Instance.Inventory.AddItem(item, true))
                {
                    Items.Remove(item);
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
                    Items.Clear();
                    Looted = true;
                }                
            }
            Utils.ClearInteractInterface();
            Utils.ClearPlayerInventoryInterface();
            PlayerEntity.Instance.PrintInventory();
        }

        private int LootStock()
        {
            Utils.ClearInteractInterface(5);
            Utils.SetCursorInteract();
            Utils.WriteColour($"{Message}: ", ConsoleColor.DarkYellow);
            int lootStock = 0;
            foreach (Item item in Items)
            {
                lootStock++;
                Utils.SetCursorInteract(lootStock);
                Console.WriteLine($"{lootStock}. {item.Name}");
            }
            return lootStock;
        }
    }
}
