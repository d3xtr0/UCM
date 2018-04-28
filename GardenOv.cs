using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest.Buildings.World;
using TheForest.Utils;
using TheForest.Items.World;

namespace UltimateCheatmenu
{
    class GardenOv : GardenDirtPile
    {
        public override void Growth()
        {
            if (UCheatmenu.instgrow)
            {
                this._plantedTime = Scene.Clock.ElapsedGameTime - UCheatmenu.instgrowspeed;
            }
            base.Growth();
            UCheatmenu.instgrow = false;
        }
    }

    class PlantOv: Garden
    {
        public override void PlantSeed()
        {
            if (UCheatmenu.seedtype >= 0) {
                this._currentSeedType = UCheatmenu.seedtype;
            }
            if (BoltNetwork.isRunning)
            {
                if (BoltNetwork.isClient)
                {
                    LocalPlayer.Sfx.PlayDigDirtPile(base.gameObject);
                }
                GrowGarden growGarden = GrowGarden.Create(Bolt.GlobalTargets.OnlyServer);
                growGarden.Garden = base.GetComponentInParent<BoltEntity>();
                growGarden.SeedNum = this._currentSeedType;
                growGarden.Send();
            }
            else
            {
                this.PlantSeed_Real(this._currentSeedType);
            }
        }
    }
}
