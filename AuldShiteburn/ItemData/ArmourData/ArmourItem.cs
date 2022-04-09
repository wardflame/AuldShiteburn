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
        public static ArmourItem WoolenRags
        {
            get { return new ArmourItem("Woolen Rags", ArmourFamily.Light, 2, 1); }
        }
        public static ArmourItem Gambeson
        {
            get { return new ArmourItem("Gambeson", ArmourFamily.Light, 3, 1); }
        }
        public static ArmourItem HeavyGambeson
        {
            get { return new ArmourItem("Heavy Gambeson", ArmourFamily.Light, 5, 2); }
        }
        #endregion Light Armours
        #region Medium Armours
        public static ArmourItem PaddedFur
        {
            get { return new ArmourItem("Padded Fur", ArmourFamily.Medium, 4, 2); }
        }
        public static ArmourItem Brigandine
        {
            get { return new ArmourItem("Brigandine", ArmourFamily.Medium, 5, 2); }
        }
        public static ArmourItem SplintPlate
        {
            get { return new ArmourItem("Splint Plate", ArmourFamily.Medium, 7, 3); }
        }
        #endregion Medium Armours
        #region Heavy Armours
        public static ArmourItem Lamellar
        {
            get { return new ArmourItem("Lamellar", ArmourFamily.Heavy, 6, 3); }
        }
        public static ArmourItem PaddedMaille
        {
            get { return new ArmourItem("Padded Maille", ArmourFamily.Heavy, 7, 3); }
        }
        public static ArmourItem FullPlate
        {
            get { return new ArmourItem("Full Plate", ArmourFamily.Heavy, 9, 4); }
        }
        #endregion Heavy Armours
        #region Special Armours
        public static ArmourItem GrandWarlockGarb
        {
            get { return new ArmourItem("Grand Warlock's Garb", ArmourFamily.Light, 6, 10); }
        }
        public static ArmourItem IndomitableCuirass
        {
            get { return new ArmourItem("Indomitable Cuirass", ArmourFamily.Medium, 8, 8); }
        }
        public static ArmourItem GrailKnightPlate
        {
            get { return new ArmourItem("Grail Knight Plate", ArmourFamily.Heavy, 10, 6); }
        }
        #endregion Special Armours

        #region Armour Lists
        public static List<ArmourItem> AllStandardArmours
        {
            get
            {
                return new List<ArmourItem>()
                    {
                        WoolenRags,
                        Gambeson,
                        HeavyGambeson,
                        PaddedFur,
                        Brigandine,
                        SplintPlate,
                        Lamellar,
                        PaddedMaille,
                        FullPlate
                    };
            }
        }
        public static List<ArmourItem> LightArmours
        {
            get
            {
                return new List<ArmourItem>()
                    {
                        WoolenRags,
                        Gambeson,
                        HeavyGambeson
                    };
            }
        }
        public static List<ArmourItem> MediumArmours
        {
            get
            {
                return new List<ArmourItem>()
                    {
                        PaddedFur,
                        Brigandine,
                        SplintPlate
                    };
            }
        }
        public static List<ArmourItem> HeavyArmours
        {
            get
            {
                return new List<ArmourItem>()
                    {
                        Lamellar,
                        PaddedMaille,
                        FullPlate
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
                        armour = Lamellar;
                    }
                    break;
                case ClassType.Marauder:
                    {
                        armour = PaddedFur;
                    }
                    break;
                case ClassType.Monk:
                    {
                        armour = WoolenRags;
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
