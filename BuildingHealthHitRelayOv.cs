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
    class BuildingHealthChunkHitRelayOv : BuildingHealthChunkHitRelay
    {
        public override void LocalizedHit(LocalizedHitData data)
        {
            base.LocalizedHit(DestroyBuildings.GetLocalizedHitData(data));
        }
    }
    class FoundationChunkTierOv : FoundationChunkTier
    {
        public override void LocalizedHit(LocalizedHitData data)
        {
            base.LocalizedHit(DestroyBuildings.GetLocalizedHitData(data));
        }
    }
    class BuildingHealthOv : BuildingHealth
    {
        public override void LocalizedHit(LocalizedHitData data)
        {
            base.LocalizedHit(DestroyBuildings.GetLocalizedHitData(data));
        }
    }
}
