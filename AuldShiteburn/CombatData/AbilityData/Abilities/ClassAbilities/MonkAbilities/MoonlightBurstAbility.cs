using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.MonkAbilities
{
    [Serializable]
    internal class MoonlightBurstAbility : Ability
    {
        public override string Name => "Moonlight Burst";
        public override string Description => $"Blind enemies, stunning them for 3 turns, damaging them with Holy, and heal you for 8-14 HP.";
        public override int Cooldown => 8;
        public override int ResourceCost => 10;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Holy;
        public override int PropertyMinDamage => 5;
        public override int PropertyMaxDamage => 7;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            int offsetY = Console.CursorTop + 1;
            Utils.SetCursorInteract(Console.CursorTop);
            if (ActiveCooldown <= 0)
            {
                PlayerEntity.Instance.Mana -= ResourceCost;
                Random rand = new Random();
                for (int i = 0; i < enemies.Count; i++)
                {
                    Utils.ClearInteractArea(offsetY, 10);
                    Utils.SetCursorInteract(offsetY - 2);
                    int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
                    if (enemies[i].ReceiveAttack
                        (new CombatPayload(
                            isAttack: false, isUtility: true, isStun: true,
                            hasProperty: true,
                            propertyAttackType: PropertyDamageType,
                            propertyDamage: propDamage, stunCount: 3), Console.CursorTop + 1))
                    {
                        enemies.Remove(enemies[i]);
                        i = -1;
                    }
                    Utils.SetCursorInteract(Console.CursorTop);
                    if (i < enemies.Count - 1)
                    {
                        Utils.WriteColour("Press any key to continue.");
                        Console.ReadKey(true);
                    }
                }
                Utils.ClearInteractArea(Console.CursorTop - 3, 1);
                Utils.SetCursorInteract(Console.CursorTop - 4);
                ActiveCooldown = Cooldown;
                return new CombatPayload(false, true);
            }
            return new CombatPayload(false);
        }
    }
}
