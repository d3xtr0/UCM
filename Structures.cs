using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest.Buildings.Creation;

namespace UltimateCheatmenu
{
    class KeepAboveTerrainOv : KeepAboveTerrain
    {
        protected override void Awake()
        {
            maxBuildingHeight = 5000f;
            maxAirBorneHeight = 5000f;

            base.Awake();
        }

        public override void SetColliding()
        {
            if (UCheatmenu.BuildingCollision)
            {
                base.SetColliding();
            }
            else
            {
                this.ClearOfCollision = true;
            }
        }
        protected override void Update()
        {
            base.Update();
            if (!UCheatmenu.BuildingCollision)
            {
                this._clearInternal = true;
                this._clearDynamicBuilding = true;
                this._clearSmallStructures = true;
                this.ClearOfCollision = true;
            }
        }
    }

    class gardenArchitectOv : GardenArchitect
    {
        protected override void LateUpdate()
        {
            this._maxSize = 2000f;
            base.LateUpdate();
        }
    }
}
