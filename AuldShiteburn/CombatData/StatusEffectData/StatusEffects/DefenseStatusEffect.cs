using AuldShiteburn.CombatData.PayloadData;
using System;

namespace AuldShiteburn.CombatData.StatusEffectData.StatusEffects
{
    [Serializable]
    /// <summary>
    /// Defensive status which either nullifies or mitigates incoming damage.
    /// </summary>
    internal class DefenseStatusEffect : StatusEffect
    {
        #region Global Defense Effects
        // Stagger: Debuff
        public static DefenseStatusEffect Staggered1
        {
            get
            {
                return new DefenseStatusEffect(
                    "Staggered", 1,
                    ConsoleColor.Magenta, EffectType.Debuff,
                    physicalEffectLevel: EffectLevel.Moderate, propertyEffectLevel: EffectLevel.Moderate,
                    allPhysicalDefense: true, allPropertyDefense: true);
            }
        }
        public static DefenseStatusEffect Staggered2
        {
            get
            {
                return new DefenseStatusEffect(
                    "Staggered", 2,
                    ConsoleColor.Magenta, EffectType.Debuff,
                    physicalEffectLevel: EffectLevel.Moderate, propertyEffectLevel: EffectLevel.Moderate,
                    allPhysicalDefense: true, allPropertyDefense: true);
            }
        }
        public static DefenseStatusEffect Staggered3
        {
            get
            {
                return new DefenseStatusEffect(
                    "Staggered", 3,
                    ConsoleColor.Magenta, EffectType.Debuff,
                    physicalEffectLevel: EffectLevel.Moderate, propertyEffectLevel: EffectLevel.Moderate,
                    allPhysicalDefense: true, allPropertyDefense: true);
            }
        }
        #endregion
        #region Fighter Defense Effects
        public static DefenseStatusEffect ParryAndRiposte
        {
            get
            {
                return new DefenseStatusEffect
                    ("Parry and Riposte", 1, ConsoleColor.Cyan, EffectType.Buff,
                    EffectLevel.Major, allPhysicalDefense: true);
            }
        }
        #endregion Fighter Defense Effects
        #region Monk Defense Effects
        public static DefenseStatusEffect MoonWard
        {
            get
            {
                return new DefenseStatusEffect(
                    "Moon Ward", 3,
                    ConsoleColor.Cyan, EffectType.Buff,
                    propertyEffectLevel: EffectLevel.Moderate,
                    allPropertyDefense: true);
            }
        }
        #endregion Monk Defense Effects
        #region Grand Warlock Effects
        public static DefenseStatusEffect HardshiteFlesh
        {
            get
            {
                return new DefenseStatusEffect
                    ("Hardshite Flesh", 4, ConsoleColor.DarkYellow, EffectType.Buff,
                    physicalEffectLevel: EffectLevel.Minor, propertyEffectLevel: EffectLevel.Moderate,
                    allPhysicalDefense: true, allPropertyDefense: true);
            }
        }
        #endregion Grand Warlock Effects

        public EffectLevel PhysicalEffectLevel { get; }
        public EffectLevel PropertyEffectLevel { get; }
        public PhysicalDamageType PhysicalTypeDefense { get; }
        public PropertyDamageType PropertyTypeDefense { get; }

        /// <summary>
        /// Will this nullification/mitigation apply to all physical types?
        /// </summary>
        public bool AllPhysicalDefense { get; }

        /// <summary>
        /// Will this nullification/mitigation apply to all property types?
        /// </summary>
        public bool AllPropertyDefense { get; }

        /// <summary>
        /// If true, nullify physical damage. If false, mitigate it.
        /// </summary>
        public bool PhysicalNulOrMit { get; }

        /// <summary>
        /// If true, nullify property damage. If false, mitigate it.
        /// </summary>
        public bool PropertyNulOrMit { get; }

        public DefenseStatusEffect
            (string name, int duration, ConsoleColor colour, EffectType type = EffectType.None, EffectLevel physicalEffectLevel = EffectLevel.None, EffectLevel propertyEffectLevel = EffectLevel.None,
            bool allPhysicalDefense = false, bool allPropertyDefense = false,
            PhysicalDamageType physicalDamageType = PhysicalDamageType.None, PropertyDamageType propertyDamageType = PropertyDamageType.None,
            bool physicalNulOrMit = false, bool propertyNulOrMit = false)
        {
            Name = name;
            Duration = duration;
            DisplayColor = colour;
            Type = type;
            PhysicalEffectLevel = physicalEffectLevel;
            PropertyEffectLevel = propertyEffectLevel;
            AllPhysicalDefense = allPhysicalDefense;
            AllPropertyDefense = allPropertyDefense;
            PhysicalTypeDefense = physicalDamageType;
            PropertyTypeDefense = propertyDamageType;
            PhysicalNulOrMit = physicalNulOrMit;
            PropertyNulOrMit = propertyNulOrMit;
        }

        public override CombatPayload EffectActive(CombatPayload combatPayload)
        {
            if (combatPayload.HasPhysical)
            {
                if (combatPayload.PhysicalAttackType == PhysicalTypeDefense || AllPhysicalDefense)
                {
                    if (PhysicalNulOrMit)
                    {
                        combatPayload.PhysicalDamage = 0;
                    }
                    else
                    {
                        if (Type == EffectType.Buff)
                        {
                            switch (PhysicalEffectLevel)
                            {
                                case EffectLevel.Minor:
                                    {
                                        combatPayload.PhysicalDamage -= Combat.STATUS_MITIGATION_MINOR;
                                    }
                                    break;
                                case EffectLevel.Moderate:
                                    {
                                        combatPayload.PhysicalDamage -= Combat.STATUS_MITIGATION_MODERATE;
                                    }
                                    break;
                                case EffectLevel.Major:
                                    {
                                        combatPayload.PhysicalDamage -= Combat.STATUS_MITIGATION_MAJOR;
                                    }
                                    break;
                            }
                        }
                        else if (Type == EffectType.Debuff)
                        {
                            switch (PhysicalEffectLevel)
                            {
                                case EffectLevel.Minor:
                                    {
                                        combatPayload.PhysicalDamage += Combat.STATUS_MITIGATION_MINOR;
                                    }
                                    break;
                                case EffectLevel.Moderate:
                                    {
                                        combatPayload.PhysicalDamage += Combat.STATUS_MITIGATION_MODERATE;
                                    }
                                    break;
                                case EffectLevel.Major:
                                    {
                                        combatPayload.PhysicalDamage += Combat.STATUS_MITIGATION_MAJOR;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            if (combatPayload.HasProperty)
            {
                if (combatPayload.PropertyAttackType == PropertyTypeDefense || AllPropertyDefense)
                {
                    if (PropertyNulOrMit)
                    {
                        combatPayload.PropertyDamage = 0;
                    }
                    else
                    {
                        if (Type == EffectType.Buff)
                        {
                            switch (PropertyEffectLevel)
                            {
                                case EffectLevel.Minor:
                                    {
                                        combatPayload.PropertyDamage -= Combat.STATUS_MITIGATION_MINOR;
                                    }
                                    break;
                                case EffectLevel.Moderate:
                                    {
                                        combatPayload.PropertyDamage -= Combat.STATUS_MITIGATION_MODERATE;
                                    }
                                    break;
                                case EffectLevel.Major:
                                    {
                                        combatPayload.PropertyDamage -= Combat.STATUS_MITIGATION_MAJOR;
                                    }
                                    break;
                            }
                        }
                        else if (Type == EffectType.Debuff)
                        {
                            if (combatPayload.PropertyAttackType != PropertyDamageType.Damaged && combatPayload.PropertyAttackType != PropertyDamageType.Standard)
                            {
                                switch (PropertyEffectLevel)
                                {
                                    case EffectLevel.Minor:
                                        {
                                            combatPayload.PropertyDamage += Combat.STATUS_MITIGATION_MINOR;
                                        }
                                        break;
                                    case EffectLevel.Moderate:
                                        {
                                            combatPayload.PropertyDamage += Combat.STATUS_MITIGATION_MODERATE;
                                        }
                                        break;
                                    case EffectLevel.Major:
                                        {
                                            combatPayload.PropertyDamage += Combat.STATUS_MITIGATION_MAJOR;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return combatPayload;
        }
    }
}
