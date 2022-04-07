using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.CombatData.StatusEffectData.StatusEffects
{
    [Serializable]
    internal class ReplenishStatusEffect : StatusEffect
    {
        public int MinReplenish { get; }
        public int MaxReplenish { get; }
        public bool RepHealth { get; }
        public bool RepMana { get; }
        public bool RepStamina { get; }

        public ReplenishStatusEffect
            (string name, int duration, ConsoleColor colour, int minReplenish, int maxReplenish, bool repHealth, bool repMana, bool repStamina)
        {
            Name = name;
            Duration = duration;
            DisplayColor = colour;
            MinReplenish = minReplenish;
            MaxReplenish = maxReplenish;
            RepHealth = repHealth;
            RepMana = repMana;
            RepStamina = repStamina;
        }

        public override CombatPayload EffectActive(CombatPayload combatPayload)
        {
            Random rand = new Random();
            Console.CursorLeft = Utils.UIInteractOffset;
            Console.CursorTop += 4;
            if (RepHealth)
            {
                int hpRestored = rand.Next(MinReplenish, MaxReplenish + 1);
                Utils.WriteColour($"{Name} restores ");
                Utils.WriteColour($"{hpRestored} ", ConsoleColor.Red);
                Utils.WriteColour($"HP!");
                PlayerEntity.Instance.HP += hpRestored;
            }
            else if (RepMana)
            {
                int manaRestored = rand.Next(MinReplenish, MaxReplenish + 1);
                Utils.WriteColour($"{Name} restores ");
                Utils.WriteColour($"{manaRestored} ", ConsoleColor.Red);
                Utils.WriteColour($"Mana!");
                PlayerEntity.Instance.HP += manaRestored;
            }
            else if (RepStamina)
            {
                int staminaRestored = rand.Next(MinReplenish, MaxReplenish + 1);
                Utils.WriteColour($"{Name} restores ");
                Utils.WriteColour($"{staminaRestored} ", ConsoleColor.Red);
                Utils.WriteColour($"Stamina!");
                PlayerEntity.Instance.Stamina += staminaRestored;
            }

            return combatPayload;
        }
    }
}
