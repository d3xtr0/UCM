using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.World;

namespace UltimateCheatmenu
{
    class DestroyBuildings
    {
        public static LocalizedHitData GetLocalizedHitData(LocalizedHitData data)
        {
            if (!UCheatmenu.DestroyBuildings)
            {
                return data;
            }
            return new LocalizedHitData(data._position, 100000f);
        }
    }

    public class ExplodeOv : Explode
    {
        protected override void RunExplode()
        {
            radius = UCheatmenu.ExplosionRadius;
            base.RunExplode();
        }
    }
}
