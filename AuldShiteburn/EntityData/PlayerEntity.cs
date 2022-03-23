using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.KeyData;
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
        public List<Item> inventory = new List<Item>();
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
            int sexChance = rand.Next(1, 101);
            if (sexChance <= GameSettings.Instance.SexRatio)
            {
                forenameStr = PlayerGenInfo.nameMale[rand.Next(PlayerGenInfo.nameMale.Count)];

                int willTitle = rand.Next(1, 101);
                if (willTitle >= 70)
                {
                    titleStr = PlayerGenInfo.titleMale[rand.Next(PlayerGenInfo.titleMale.Count)];
                    Instance.name = $"{titleStr} {forenameStr}";
                }
                else
                {
                    Instance.name = $"{forenameStr}";
                }                
            }
            else
            {
                forenameStr = PlayerGenInfo.nameFemale[rand.Next(PlayerGenInfo.nameFemale.Count)];

                int willTitle = rand.Next(1, 101);
                if (willTitle >= 70)
                {
                    titleStr = PlayerGenInfo.titleFemale[rand.Next(PlayerGenInfo.titleFemale.Count)];
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
