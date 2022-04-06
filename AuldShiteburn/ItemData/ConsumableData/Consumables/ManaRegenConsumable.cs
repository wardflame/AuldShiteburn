using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    [Serializable]
    internal class ManaRegenConsumable : ConsumableItem
    {
        public override string Name => "Moonlight Elixir";
        public override string Description => "Applies the potion status effect Moonlight to Mana users, restoring 3-5 Mana each round for 3 rounds.";
        public int MinMana { get; } = 6;
        public int MaxMana { get; } = 12;
        public override void OnInventoryUse(InventorySortData sortData)
        {
            Utils.SetCursorInteract(Console.CursorTop);
            if (PlayerEntity.Instance.UsesMana)
            {
                Utils.WriteColour($"The Moonlight waters embolden your mind.", ConsoleColor.DarkYellow);
                PlayerEntity.Instance.PotionStatusEffect = new ReplenishStatusEffect
                    ("Moonlight", 3, ConsoleColor.Cyan,
                    3, 5, false, true, false);
            }
            else
            {
                Utils.WriteColour($"Moonlight waters, a pleasant reprieve in this horrid place.", ConsoleColor.DarkYellow);
            }
            Stock--;
            Utils.SetCursorInteract(Console.CursorTop - 1);
            Utils.WriteColour("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
