using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.MonkAbilities
{
    internal class MoonlightBurstAbility : Ability
    {
        public override string Name => "Moonlight Burst";
        public override string Description => $"Blind enemies, stunning them for 3 turns, damaging them with Holy, and heal you for 8-14 HP.";
        public override int Cooldown => 8;
        public override int ResourceCost => 10;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Holy;
        public override int PropertyMinDamage => 5;
        public override int PropertyMaxDamage => 7;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            int offsetY = Console.CursorTop + 2;
            Utils.SetCursorInteract(offsetY - 2);
            if (ActiveCooldown > 0)
            {
                ActiveCooldown--;
                Utils.WriteColour($"{Name} is on cooldown {ActiveCooldown}/{Cooldown}.", ConsoleColor.Red);
                Console.ReadKey(true);
                return new CombatPayload(false);
            }
            else if (!PlayerEntity.Instance.CheckResourceLevel(ResourceCost))
            {
                Utils.WriteColour($"You lack the resources to use this ability.", ConsoleColor.Red);
                Console.ReadKey(true);
                return new CombatPayload(false);
            }
            else if (ActiveCooldown <= 0)
            {
                PlayerEntity.Instance.Stamina -= ResourceCost;
                Random rand = new Random();
                for (int i = 0; i < enemies.Count; i++)
                {
                    Utils.ClearInteractArea(offsetY, 10);
                    Utils.SetCursorInteract(offsetY - 2);
                    int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
                    if (enemies[i].ReceiveAttack
                        (new CombatPayload(false, true, true, hasProperty: true,
                        propertyAttackType: PropertyDamageType, propertyDamage: propDamage, stunCount: 3), Console.CursorTop + 1))
                    {
                        enemies.Remove(enemies[i]);
                        i = -1;
                    }
                    Console.ReadKey(false);
                }
                ActiveCooldown = Cooldown;
                return new CombatPayload(false, true);
            }
            return new CombatPayload(false);
        }
    }
}
