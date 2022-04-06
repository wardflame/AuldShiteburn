using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    [Serializable]
    internal class StaminaRegenConsumable : ConsumableItem
    {
        public override string Name => "Renewal Elixir";
        public override string Description => "Applies the potion status effect Renewal to Stamina users, restoring 3-5 Stamina each round for 3 rounds.";
        public int MinStamina { get; } = 6;
        public int MaxStamina { get; } = 12;
        public override void OnInventoryUse(InventorySortData sortData)
        {
            Utils.SetCursorInteract(Console.CursorTop);
            if (PlayerEntity.Instance.UsesMana)
            {
                Utils.WriteColour($"You feel renewed.", ConsoleColor.Green);
                PlayerEntity.Instance.PotionStatusEffect = new ReplenishStatusEffect
                    ("Renewal", 3, ConsoleColor.Green,
                    3, 5, false, false, true);
            }
            else
            {
                Utils.WriteColour($"Moonlight waters, a pleasant reprieve in this horrid place.", ConsoleColor.DarkYellow);
            }
            Stock--;
            Utils.SetCursorInteract(Console.CursorTop - 1);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
