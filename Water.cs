using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Ceto;
using TheForest.Graphics;
using TheForest.Utils;
using UnityEngine;

namespace UltimateCheatmenu
{
    class Water : UnderWaterPostEffect
    {
        protected override void LateUpdate()
        {
            base.LateUpdate();
            if (UCheatmenu.NoUnderwaterBlur)
            {
                blurIterations = 0;
            }
            else
            {
                blurIterations = 3;
            }
        }
    }

    class Water2 : WaterBlurEffect
    {
        protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (UCheatmenu.NoUnderwaterBlur)
            {
                iterations = 0;
            }
            else
            {
                iterations = 3;
            }
            base.OnRenderImage(source, destination);
        }
    }
}
