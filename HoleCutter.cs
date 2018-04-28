using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ModAPI;
using TheForest.Buildings.Creation;

namespace UltimateCheatmenu
{
    class HoleCutter : FloorHoleArchitect
    {
        protected override void Update()
        {
            if (UnityEngine.Input.mouseScrollDelta != Vector2.zero)
            {
                this._holeSize = new Vector2(
                    Mathf.Clamp(this._holeSize.x + (UnityEngine.Input.mouseScrollDelta.y/2), 1f, 50f), 
                    Mathf.Clamp(this._holeSize.y + (UnityEngine.Input.mouseScrollDelta.y/2), 1f, 50f)
                    );
                this._renderer.transform.localScale = new Vector3(Mathf.Clamp(this._renderer.transform.localScale.x + (UnityEngine.Input.mouseScrollDelta.y/4), 0.5f, 25f),
                    this._renderer.transform.localScale.y,
                    Mathf.Clamp(this._renderer.transform.localScale.z + (UnityEngine.Input.mouseScrollDelta.y/4), 0.5f, 25f));
            }
            base.Update();
        }
    }
}
