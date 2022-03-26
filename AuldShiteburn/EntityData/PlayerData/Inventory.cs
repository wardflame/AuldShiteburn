using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.PlayerData
{
    internal class Inventory
    {
        public void CycleInventory(List<Item> itemList)
        {
            itemList.Sort();
            Utils.SetCursorInventory();
            foreach (WeaponItem weapon in itemList)
            {

            }
        }
    }
}
