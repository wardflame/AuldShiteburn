using AuldShiteburn.CombatData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData.ArmourData
{
    [Serializable]
    internal class ArmourItem : Item
    {
        #region Light Armours
        public static ArmourItem SackClothRobe
        {
            get { return new ArmourItem("Sack Cloth Robe", ArmourFamily.LightArmour, 2, 1, PhysicalDamageType.None, PropertyDamageType.None); }
        }
        public static ArmourItem WoolenRags
        {
            get { return new ArmourItem("Woolen Rags", ArmourFamily.LightArmour, 2, 1, PhysicalDamageType.None, PropertyDamageType.None); }
        }
        public static ArmourItem Gambeson
        {
            get { return new ArmourItem("Gambeson", ArmourFamily.LightArmour, 4, 1, PhysicalDamageType.None, PropertyDamageType.None); }
        }
        #endregion Light Armours
        #region Medium Armours
        public static ArmourItem HideWrappings
        {
            get { return new ArmourItem("Hide Wrappings", ArmourFamily.MediumArmour, 4, 2, PhysicalDamageType.None, PropertyDamageType.None); }
        }
        public static ArmourItem FurPadding
        {
            get { return new ArmourItem("Fur Padding", ArmourFamily.MediumArmour, 4, 2, PhysicalDamageType.None, PropertyDamageType.None); }
        }
        public static ArmourItem Brigandine
        {
            get { return new ArmourItem("Brigandine", ArmourFamily.MediumArmour, 6, 2, PhysicalDamageType.None, PropertyDamageType.None); }
        }
        #endregion Medium Armours
        #region Heavy Armours
        public static ArmourItem Maille
        {
            get { return new ArmourItem("Maille", ArmourFamily.HeavyArmour, 6, 3, PhysicalDamageType.None, PropertyDamageType.None); }
        }
        public static ArmourItem ScaleCoat
        {
            get { return new ArmourItem("Scale Coat", ArmourFamily.HeavyArmour, 6, 3, PhysicalDamageType.None, PropertyDamageType.None); }
        }
        public static ArmourItem Breastplate
        {
            get { return new ArmourItem("Breastplate", ArmourFamily.HeavyArmour, 8, 3, PhysicalDamageType.None, PropertyDamageType.None); }
        }
        #endregion Heavy Armours

        public List<ArmourItem> AllArmours { get; } = new List<ArmourItem>()
        {
            SackClothRobe,
            WoolenRags,
            Gambeson,
            HideWrappings,
            FurPadding,
            Brigandine,
            Maille,
            ScaleCoat,
            Breastplate
        };
        public List<ArmourItem> LightArmours { get; } = new List<ArmourItem>()
        {
            SackClothRobe,
            WoolenRags,
            Gambeson
        };
        public List<ArmourItem> MediumArmours { get; } = new List<ArmourItem>()
        {
            HideWrappings,
            FurPadding,
            Brigandine
        };
        public List<ArmourItem> HeavyArmours { get; } = new List<ArmourItem>()
        {
            Maille,
            ScaleCoat,
            Breastplate
        };
        public override string Name { get; }
        public ArmourFamily ArmourFamily { get; }
        public int PhysicalMitigation { get; }
        public int PropertyMitigation { get; }
        public PhysicalDamageType PrimaryPhysicalResistance { get; }
        public PhysicalDamageType SecondaryPhysicalResistance { get; }
        public PropertyDamageType PrimaryPropertyResistance { get; }
        public PropertyDamageType SecondaryPropertyResistance { get; }

        public ArmourItem
            (string name, ArmourFamily armourFamily, int physicalMitigation, int propertyMitigation,
            PhysicalDamageType primaryPhysResist, PropertyDamageType primaryPropResist,
            PhysicalDamageType secondaryPhysResist = PhysicalDamageType.None, PropertyDamageType secondaryPropResist = PropertyDamageType.None)
        {
            Name = name;
            ArmourFamily = armourFamily;
            PhysicalMitigation = physicalMitigation;
            PropertyMitigation = propertyMitigation;
            PrimaryPhysicalResistance = primaryPhysResist;
            SecondaryPhysicalResistance = secondaryPhysResist;
            PrimaryPropertyResistance = primaryPropResist;
            SecondaryPhysicalResistance = secondaryPhysResist;
        }
    }
}
