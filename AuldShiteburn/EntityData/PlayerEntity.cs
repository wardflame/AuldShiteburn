using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using System;
using System.Collections.Generic;
using static AuldShiteburn.EntityData.PlayerData.PlayerGenInfo;

namespace AuldShiteburn.EntityData
{
    [Serializable]
    internal class PlayerEntity : LivingEntity
    {
        public static PlayerEntity Instance { get; set; }
        List<Item> inventory;
        public override string EntityChar => "PL";
        public bool inMenu { get; set; }
        public long playtime;

        public override void Move()
        {
            if (!inMenu)
            {
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.W:
                        {
                            PosY--;
                        }
                        break;

                    case ConsoleKey.S:
                        {
                            PosY++;
                        }
                        break;

                    case ConsoleKey.A:
                        {
                            PosX--;
                        }
                        break;

                    case ConsoleKey.D:
                        {
                            PosX++;
                        }
                        break;
                    case ConsoleKey.Escape:
                        {
                            Menu.Instance = new PauseMenu();
                            Menu.Instance.InMenu();
                        }
                        break;
                }
            }
        }

        public static PlayerEntity GenerateCharacter()
        {
            Instance = new PlayerEntity();
            Instance.PosX = 1;
            Instance.PosY = 1;

            string titleStr;
            string forenameStr;

            #region Name Generation
            Random rand = new Random();
            int sexChance = rand.Next(0, 101);
            if (sexChance <= GameSettings.Instance.SexRatio)
            {
                int forenameNum = rand.Next(0, Enum.GetValues(typeof(NameMale)).Length);
                NameMale forename = (NameMale)forenameNum;
                forenameStr = forename.ToString();

                int willTitle = rand.Next(0, 101);
                if (willTitle >= 70)
                {
                    int title = rand.Next(0, Enum.GetValues(typeof(TitleMale)).Length);
                    TitleMale titleMale = (TitleMale)title;
                    titleStr = titleMale.ToString();
                    Instance.name = $"{titleStr} {forenameStr}";
                }
                else
                {
                    Instance.name = $"{forenameStr}";
                }                
            }
            else
            {
                int forenameNum = rand.Next(0, Enum.GetValues(typeof(NameFemale)).Length);
                NameFemale forename = (NameFemale)forenameNum;
                forenameStr = forename.ToString();

                int willTitle = rand.Next(0, 101);
                if (willTitle >= 70)
                {
                    int title = rand.Next(0, Enum.GetValues(typeof(TitleFemale)).Length);
                    TitleFemale titleMale = (TitleFemale)title;
                    titleStr = titleMale.ToString();
                    Instance.name = $"{titleStr} {forenameStr}";
                }
                else
                {
                    Instance.name = $"{forenameStr}";
                }                
            }
            #endregion Name Generation

            return Instance;
        }
    }
}
