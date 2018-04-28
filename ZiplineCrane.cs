using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest.Buildings.Creation;
using TheForest.Utils;

namespace UltimateCheatmenu
{
            
    class CraneArchitectOv : CraneArchitect
    {

        public int _lockModes = 0;

        protected override void LateUpdate()
        {
            if (this._logMat)
            {
                this._logMat.SetColor("_TintColor", (!LocalPlayer.Create.BuildingPlacer.ClearOfCollision) ? LocalPlayer.Create.BuildingPlacer.RedMat.GetColor("_TintColor") : LocalPlayer.Create.BuildingPlacer.ClearMat.GetColor("_TintColor"));
                if (this._lockModes == 0)
                {
                    LocalPlayer.Create.BuildingPlacer.IgnoreLookAtCollision = false;
                    LocalPlayer.Create.BuildingPlacer.Airborne = false;
                    LocalPlayer.Create.BuildingPlacer.SetNotclear();
                    if (LocalPlayer.Create.BuildingPlacer.ClearOfCollision && TheForest.Utils.Input.GetButtonDown("Fire1"))
                    {
                        LocalPlayer.Create.BuildingPlacer.SetClear();
                        if (LocalPlayer.Create.BuildingPlacer.LastHit.HasValue)
                        {
                            BoltEntity componentInParent = LocalPlayer.Create.BuildingPlacer.LastHit.Value.transform.GetComponentInParent<BoltEntity>();
                            if (componentInParent)
                            {
                                LocalPlayer.Create.BuildingPlacer.ForcedParent = componentInParent.gameObject;
                            }
                        }
                        this._bottomY = LocalPlayer.Create.BuildingPlacer.MinHeight;
                        this._lockModes = 1;
                        base.transform.parent = null;
                        LocalPlayer.Sfx.PlayWhoosh();
                    }
                    if (this._holeCutter.gameObject.activeSelf)
                    {
                        this._holeCutter.gameObject.SetActive(false);
                    }
                }
                else
                {
                    LocalPlayer.Create.BuildingPlacer.IgnoreLookAtCollision = true;
                    LocalPlayer.Create.BuildingPlacer.Airborne = true;
                    if (!this._holeCutter.gameObject.activeSelf)
                    {
                        this._holeCutter.gameObject.SetActive(true);
                    }
                    BoxCollider component = this._holeCutter.GetComponent<BoxCollider>();
                    component.size = new Vector3(this._logLength * 0.9f * 2f, base.transform.position.y - this._bottomY, this._logLength * 0.9f * 2f);
                    component.center = new Vector3(0f, component.size.y / -2f, 0f);
                    base.transform.eulerAngles = new Vector3(0f, LocalPlayer.Create.BuildingPlacer.YRotation, 0f);
                    base.transform.position = new Vector3(base.transform.position.x, Mathf.Clamp(LocalPlayer.Create.BuildingPlacer.AirBorneHeight, this._bottomY + this._logLength, this._bottomY + this._logLength + 5000f), base.transform.position.z);
                    if (TheForest.Utils.Input.GetButtonDown("AltFire"))
                    {
                        base.transform.parent = LocalPlayer.Create.BuildingPlacer.transform;
                        base.transform.localPosition = LocalPlayer.Create.GetGhostOffsetWithPlacer(base.gameObject);
                        base.transform.localRotation = Quaternion.identity;
                        this._lockModes = 0;
                        base.GetComponent<Renderer>().sharedMaterial = this._logMat;
                        LocalPlayer.Create.BuildingPlacer.SetRenderer(null);
                        LocalPlayer.Create.BuildingPlacer.ForcedParent = null;
                        LocalPlayer.Sfx.PlayWhoosh();
                    }
                    if (TheForest.Utils.Input.GetButtonDown("Take"))
                    {
                        this._holeCutter.OnPlaced();
                    }
                }
                this._aboveGround = !LocalPlayer.IsInCaves;
                this.CreateStructure(false);
                bool showManualfillLockIcon = this._lockModes == 0 && LocalPlayer.Create.BuildingPlacer.ClearOfCollision;
                bool flag = this._lockModes == 1;
                bool showManualPlace = this._lockModes == 1;
                Scene.HudGui.FoundationConstructionIcons.Show(showManualfillLockIcon, false, false, showManualPlace, false, flag, false);
                if (Scene.HudGui.RotateIcon.activeSelf == flag)
                {
                    Scene.HudGui.RotateIcon.SetActive(!flag);
                }
            }

        }

        /*
        protected override Vector3 GetSegmentPointFloorPosition(Vector3 segmentPoint)
        {
            try
            {
                if (this._lockModes == 1 || this._wasBuilt || this._wasPlaced)
                {
                    segmentPoint.y = this._bottomY;
                    return segmentPoint;
                }
                segmentPoint.y -= this._logLength;
                return segmentPoint;
            }catch(Exception e)
            {
                return segmentPoint;
            }
        }
        */

    }

}
