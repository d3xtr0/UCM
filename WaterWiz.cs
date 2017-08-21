using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UltimateCheatmenu
{
    class WaterVizOv : WaterViz
    {
        protected override void Update()
        {
            if (UCheatmenu.Rebreather)
            {
                this.TurnOnRebreatherLights();
                this.Bubbles.SetActive(true);
            }
            else
            {
                this.UnderwaterInstance = null;
                this.Bubbles.SetActive(false);
                if (!this.lightOffRoutineActive && base.gameObject.activeInHierarchy && this.LightGO.activeSelf)
                {
                    base.StartCoroutine("OutOfWaterTurnOffLights");
                }
            }
            base.Update();
        }
    }
}
