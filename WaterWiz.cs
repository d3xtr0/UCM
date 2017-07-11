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
                this.AudioOff();
                this.DepthParameter = null;
                base.ScreenCoverage = 0f;
            }
            base.Update();
        }
    }
}
