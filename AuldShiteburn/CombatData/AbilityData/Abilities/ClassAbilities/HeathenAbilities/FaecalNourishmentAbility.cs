using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.HeathenAbilities
{
    [Serializable]
    internal class FaecalNourishmentAbility : Ability
    {
        public override string Name => "Faecal Nourishment";
        public override string Description => "Take nourishment in shite, restoring 6-12 HP.";
        public override int Cooldown => 3;
        public override int ResourceCost => 4;

        public override CombatPayload UseAbility()
        {
            Utils.SetCursorInteract(Console.CursorTop - 1);
            if (ActiveCooldown > 0)
            {
                ActiveCooldown--;
                Utils.WriteColour($"{Name} is on cooldown {ActiveCooldown}/{Cooldown}.", ConsoleColor.DarkRed);
                return new CombatPayload(false);
            }
            else if (!PlayerEntity.Instance.CheckResourceLevel(ResourceCost))
            {
                Utils.WriteColour($"You lack the resources to use this ability.", ConsoleColor.DarkRed);
                return new CombatPayload(false);
            }
            else if (ActiveCooldown <= 0)
            {
                Random rand = new Random();
                int heal = rand.Next(6, 13);
                PlayerEntity.Instance.HP += 
                ActiveCooldown = Cooldown;
                if (PlayerEntity.Instance.UsesStamina)
                {
                    PlayerEntity.Instance.Stamina -= ResourceCost;
                }
                else if (PlayerEntity.Instance.UsesMana)
                {
                    PlayerEntity.Instance.Mana -= ResourceCost;
                }
                return new CombatPayload(false, true);
            }
            return new CombatPayload(false);
        }
    }
}
