using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
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
            get { return new ArmourItem("Sack Cloth Robe", ArmourFamily.LightArmour, 2, 1); }
        }
        public static ArmourItem WoolenRags
        {
            get { return new ArmourItem("Woolen Rags", ArmourFamily.LightArmour, 2, 1); }
        }
        public static ArmourItem Gambeson
        {
            get { return new ArmourItem("Gambeson", ArmourFamily.LightArmour, 4, 1); }
        }
        #endregion Light Armours
        #region Medium Armours
        public static ArmourItem HideWrappings
        {
            get { return new ArmourItem("Hide Wrappings", ArmourFamily.MediumArmour, 4, 2); }
        }
        public static ArmourItem FurPadding
        {
            get { return new ArmourItem("Fur Padding", ArmourFamily.MediumArmour, 4, 2); }
        }
        public static ArmourItem Brigandine
        {
            get { return new ArmourItem("Brigandine", ArmourFamily.MediumArmour, 6, 2); }
        }
        #endregion Medium Armours
        #region Heavy Armours
        public static ArmourItem Maille
        {
            get { return new ArmourItem("Maille", ArmourFamily.HeavyArmour, 6, 3); }
        }
        public static ArmourItem ScaleCoat
        {
            get { return new ArmourItem("Scale Coat", ArmourFamily.HeavyArmour, 6, 3); }
        }
        public static ArmourItem Breastplate
        {
            get { return new ArmourItem("Breastplate", ArmourFamily.HeavyArmour, 8, 3); }
        }
        #endregion Heavy Armours

        #region Armour Lists
        public static List<ArmourItem> AllArmours
        {
            get
            {
                return new List<ArmourItem>()
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
            }
        }
        public static List<ArmourItem> LightArmours
        {
            get
            {
                return new List<ArmourItem>()
                    {
                        SackClothRobe,
                        WoolenRags,
                        Gambeson
                    };
            }
        }
        public static List<ArmourItem> MediumArmours
        {
            get
            {
                return new List<ArmourItem>()
                    {
                        HideWrappings,
                        FurPadding,
                        Brigandine
                    };
            }
        }
        public static List<ArmourItem> HeavyArmours
        {
            get
            {
                return new List<ArmourItem>()
                    {
                        Maille,
                        ScaleCoat,
                        Breastplate
                    };
            }
        }
        #endregion Armour Lists
        public override string Name { get; }
        public ArmourFamily ArmourFamily { get; }
        private int physicalMitigation, propertyMitigation;
        public int PhysicalMitigation
        {
            get
            {
                if (IsProficient)
                {
                    return physicalMitigation + Combat.PROFICIENCY_ARMOUR_MITIGATION_MINOR;
                }
                return physicalMitigation;
            }
            private set
            {
                physicalMitigation = value;
            }
        }
        public int PropertyMitigation
        {
            get
            {
                return propertyMitigation;
            }
            private set
            {
                propertyMitigation = value;
            }
        }
        public bool IsProficient
        {
            get
            {
                if (PlayerEntity.Instance.Class.Proficiencies.ArmourProficiency == ArmourFamily)
                {
                    return true;
                }
                return false;
            }
        }

        public ArmourItem(string name, ArmourFamily armourFamily, int physicalMitigation, int propertyMitigation)
        {
            Name = name;
            ArmourFamily = armourFamily;
            PhysicalMitigation = physicalMitigation;
            PropertyMitigation = propertyMitigation;
        }

        /// <summary>
        /// Replace currently equipped player armour with new armour.
        /// </summary>
        /// <param name="sortData"></param>
        public override void OnInventoryUse(InventorySortData sortData)
        {
            if (IsProficient)
            {
                ArmourItem equippedWeapon = PlayerEntity.Instance.EquippedArmour;
                PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn] = equippedWeapon;
                PlayerEntity.Instance.EquippedArmour = this;
            }
            else
            {
                Utils.SetCursorInteract(Console.CursorTop);
                Utils.WriteColour("You are not proficient with this type of armour.", ConsoleColor.Red);
                Utils.SetCursorInteract(Console.CursorTop);
                Utils.WriteColour("Press any key to continue.");
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// For a new character, create an armour that suits the class proficiency.
        /// </summary>
        /// <param name="playerClass">The class of the player.</param>
        /// <returns>An armour of a class-proficient type.</returns>
        public static ArmourItem GenerateSpawnArmour(ClassType playerClass)
        {
            ArmourItem armour = WoolenRags;
            switch (playerClass)
            {
                case ClassType.Heathen:
                    {
                        armour = WoolenRags;
                    }
                    break;
                case ClassType.Fighter:
                    {
                        armour = Maille;
                    }
                    break;
                case ClassType.Marauder:
                    {
                        armour = HideWrappings;
                    }
                    break;
                case ClassType.Monk:
                    {
                        armour = SackClothRobe;
                    }
                    break;
                case ClassType.Rogue:
                    {
                        armour = WoolenRags;
                    }
                    break;
            }
            return armour;
        }
    }
}
