using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    internal class ManaElixirConsumableItem : ConsumableItem
    {
        public override string Name => "Moonlight Elixir";
        public override string Description => "Applies the potion status effect Moonlight to Mana users, restoring 3-5 Mana each round for 3 rounds.";
        public int MinMana { get; } = 6;
        public int MaxMana { get; } = 12;
        public override void OnInventoryUse(InventorySortData sortData)
        {
            if (PlayerEntity.Instance.UsesMana)
            {
                Utils.SetCursorInteract();
                Utils.WriteColour($"The Moonlight waters embolden your mind.", ConsoleColor.DarkYellow);
                PlayerEntity.Instance.PotionStatusEffect = new ReplenishStatusEffect("Moonlight", 3, ConsoleColor.DarkRed, 3, 5, true, false, false);
            }
            else
            {
                Utils.SetCursorInteract();
                Utils.WriteColour($"Moonlight waters, a pleasant reprieve in this horrid place.", ConsoleColor.DarkYellow);
            }
            
        }
    }
}
