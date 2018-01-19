using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest;
using TheForest.Items.Craft;
using TheForest.Utils;
using TheForest.Items;
using TheForest.Items.Inventory;

namespace UltimateCheatmenu
{
    class PlayerInv : PlayerInventory
    {
        public override bool RemoveItem(int itemId, int amount = 1, bool allowAmountOverflow = false, bool shouldEquipPrevious = true)
        {
            try
            {
                if (UCheatmenu.ItemConsume)
                {
                    return true;
                }
                else
                {
                    return base.RemoveItem(itemId, amount, allowAmountOverflow, shouldEquipPrevious);
                }
            }catch(Exception e)
            {
                return base.RemoveItem(itemId, amount, allowAmountOverflow, shouldEquipPrevious);
            }
        }

        public override void Attack()
        {
            // Only handle our player
            if (this == LocalPlayer.Inventory && UCheatmenu.visible)
            {
                return;
            }

            base.Attack();
        }
    }
    
}
