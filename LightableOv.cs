using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest.Items.World;
using TheForest.Utils;
using TheForest.Items;
using TheForest.Items.Special;

namespace UltimateCheatmenu
{

    class LightableOv : LightableItemController
    {

        protected override void OnEnable()
        {
            base.OnEnable();

            if (!this.isLit && UCheatmenu.InstLighter)
            {
                base.CancelInvoke("ResetIsLighting");
                this.isLighting = true;
                LocalPlayer.SpecialItems.SendMessage("LightHeldFire");
            }
        }
        protected override void Update()
        {
            if (!this.isLit && UCheatmenu.InstLighter)
            {
                base.CancelInvoke("ResetIsLighting");
                this.isLighting = true;
                LocalPlayer.SpecialItems.SendMessage("LightHeldFire");
            }
            if(!UCheatmenu.InstLighter)
            {
                base.Update();
            }
        }
    }
}
