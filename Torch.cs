using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items.World;
using TheForest.Utils;
using UnityEngine;

namespace UltimateCheatmenu
{
    class Torch : BatteryBasedLight
    {
        protected override void Update()
        {
            if (!BoltNetwork.isRunning || (BoltNetwork.isRunning && this.entity.isAttached && this.entity.isOwner))
            {
                if (UCheatmenu.TorchToggle)
                {
                    LocalPlayer.Stats.BatteryCharge = 100f;
                    Color tColor = new Color(UCheatmenu.TorchR, UCheatmenu.TorchG, UCheatmenu.TorchB);

                    if (!BoltNetwork.isRunning || (BoltNetwork.isRunning && this.entity.isAttached && this.entity.isOwner))
                    {
                        this.SetIntensity(Convert.ToSingle(UCheatmenu.TorchI));
                        this.SetColor(tColor);

                        if (BoltNetwork.isRunning)
                        {
                            base.state.BatteryTorchIntensity = UCheatmenu.TorchI;
                            base.state.BatteryTorchEnabled = this._mainLight.enabled;
                            base.state.BatteryTorchColor = tColor;
                        }
                    }
                }
                else
                {
                    Color tColor = new Color(1, 1, 1);
                    this.SetColor(tColor);

                    LocalPlayer.Stats.BatteryCharge -= this._batterieCostPerSecond * Time.deltaTime;
                    if (LocalPlayer.Stats.BatteryCharge > 50f)
                    {
                        this.SetIntensity(0.45f);
                    }
                    else if (LocalPlayer.Stats.BatteryCharge < 20f)
                    {
                        if (LocalPlayer.Stats.BatteryCharge < 10f)
                        {
                            if (LocalPlayer.Stats.BatteryCharge < 5f)
                            {
                                if (LocalPlayer.Stats.BatteryCharge <= 0f)
                                {
                                    this._player.StashLeftHand();
                                }
                                else
                                {
                                    this.TorchLowerLightEvenMore();
                                }
                            }
                            else
                            {
                                this.TorchLowerLightMore();
                            }
                        }
                        else
                        {
                            this.TorchLowerLight();
                        }
                    }
                    if (BoltNetwork.isRunning)
                    {
                        base.state.BatteryTorchIntensity = this._mainLight.intensity;
                        base.state.BatteryTorchEnabled = this._mainLight.enabled;
                        base.state.BatteryTorchColor = this._mainLight.color;
                    }
                }
            }
            
        }
    }
}
