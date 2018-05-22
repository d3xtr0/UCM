using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Networking;

namespace UltimateCheatmenu
{
    class GreebleZ : CustomActiveValueGreebleSuitcase
    {
        public override void UpdateGreebleData()
        {
            this._initializing = true;
            this._lid.transform.localRotation = this.LidDefaultRotation;
            this._interior.SetActive(false);
            this._trigger.SetActive(true);
            this._pickup.SetActive(true);
            base.StartCoroutine(this.OnEnableRoutine());
            base.UpdateGreebleData();
        }
    }
}
