using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Buildings.World;
using TheForest.World;

namespace UltimateCheatmenu
{
    class BuildingHealthHitRelayOv : BuildingHealthHitRelay
    {
        public override void LocalizedHit(LocalizedHitData data)
        {
            base.LocalizedHit(DestroyBuildings.GetLocalizedHitData(data));
        }
    }
}
