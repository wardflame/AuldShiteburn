using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;

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
            if (PlayerEntity.Instance.UsesStamina)
            {
                Utils.WriteColour($"You feel renewed.", ConsoleColor.Green);
                PlayerEntity.Instance.PotionStatusEffect = new ReplenishStatusEffect
                    ("Renewal", 3, ConsoleColor.Green,
                    3, 5, false, false, true);
            }
            else
            {
                Utils.WriteColour($"A tangy and uplifting beverage.", ConsoleColor.DarkYellow);
            }
            Stock--;
            Utils.SetCursorInteract(Console.CursorTop - 1);
            Utils.WriteColour("Press any key to continue.");
            Console.ReadKey(true);
        }
    }
}
