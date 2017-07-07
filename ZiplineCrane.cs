using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest.Buildings.Creation;
using TheForest.Utils;

namespace UltimateCheatmenu
{
    class ZiplineArchitectOv : ZiplineTreeArchitect
    {
        [ModAPI.Attributes.Priority(200)]
        protected virtual bool CheckLockGateOv()
        {
            bool flag = this.CheckLockOnTree();
            bool flag2 = !this._gate1.transform.parent;
            Transform transform = (!flag2) ? this._gate1.transform : this._gate2.transform;
            if (flag)
            {
                float num = Terrain.activeTerrain.SampleHeight(transform.position);
                if (LocalPlayer.Create.TargetTree)
                {
                    transform.position = new Vector3(LocalPlayer.Create.TargetTree.position.x, Mathf.Clamp(LocalPlayer.Create.BuildingPlacer.AirBorneHeight, num + 5f, num + 2000f), LocalPlayer.Create.TargetTree.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, num - 1f, transform.position.z);
                }
            }
            else if (transform.transform.parent)
            {
                transform.localPosition = Vector3.zero;
            }
            return flag;
        }
    }
    class ZiplineArchitect2Ov : ZiplineArchitect
    {
        // TheForest.Buildings.Creation.ZiplineArchitect
        protected bool CheckLockGateOv()
        {
            bool flag = this._gate2.transform.parent && LocalPlayer.Create.BuildingPlacer.ClearOfCollision;
            if (flag)
            {
                bool flag2 = !this._gate1.transform.parent;
                if (flag2)
                {
                    float num = Vector3.Distance(this._gate1.transform.position, this._gate2.transform.position);
                    RaycastHit raycastHit;
                    if (Physics.SphereCast(this.Gate1RopePosition + Vector3.down, 1.5f, this.Gate2RopePosition - this.Gate1RopePosition, out raycastHit, num, LocalPlayer.Create.BuildingPlacer.FloorLayers | 1 << LayerMask.NameToLayer("treeMid")))
                    {
                        return false;
                    }
                }
                if (TheForest.Utils.Input.GetButtonDown("Fire1"))
                {
                    if (!flag2)
                    {
                        this._gate1.transform.parent = null;
                    }
                    else
                    {
                        this._gate2.transform.parent = null;
                        if (this._ziplineRoot)
                        {
                            UnityEngine.Object.Destroy(this._ziplineRoot.gameObject);
                        }
                        this._ziplineRoot = this.CreateZipline(this.Gate1RopePosition, this.Gate2RopePosition);
                    }
                }
            }
            return flag;
        }
        protected override void Update()
        {
            bool flag = !this._gate1.transform.parent;
            bool flag2 = !this._gate2.transform.parent;
            if (flag)
            {
                if (!flag2)
                {
                    Vector3 gate2RopePosition = this.Gate2RopePosition;
                    if (Vector3.Distance(this.Gate1RopePosition, gate2RopePosition) > 1f)
                    {
                        Transform ziplineRoot = this.CreateZipline(this.Gate1RopePosition, gate2RopePosition);
                        if (this._ziplineRoot)
                        {
                            UnityEngine.Object.Destroy(this._ziplineRoot.gameObject);
                        }
                        this._ziplineRoot = ziplineRoot;
                        if (!this._gate2.activeSelf)
                        {
                            this._gate2.SetActive(true);
                        }
                        this._gate1.transform.LookAt(new Vector3(this._gate2.transform.position.x, this._gate1.transform.position.y, this._gate2.transform.position.z));
                        this._gate2.transform.LookAt(this.Gate2LookAtTarget);
                    }
                    else if (this._gate2.activeSelf)
                    {
                        this._gate2.SetActive(false);
                    }
                }
            }
            else if (this._ziplineRoot)
            {
                UnityEngine.Object.Destroy(this._ziplineRoot.gameObject);
                this._ziplineRoot = null;
                this._ropePool.Clear();
            }
            bool flag3 = this.CheckLockGateOv();
            this.CheckUnlockGate();
            bool flag4 = flag2;
            if (LocalPlayer.Create.BuildingPlacer.Clear != flag4 || Scene.HudGui.RotateIcon.activeSelf == flag)
            {
                Scene.HudGui.RotateIcon.SetActive(!flag);
                LocalPlayer.Create.BuildingPlacer.Clear = flag4;
            }
            this._ropeMat.SetColor("_TintColor", (!flag3 && !flag4) ? LocalPlayer.Create.BuildingPlacer.RedMat.GetColor("_TintColor") : LocalPlayer.Create.BuildingPlacer.ClearMat.GetColor("_TintColor"));
            bool showManualfillLockIcon = !flag && flag3;
            bool canLock = flag && flag3;
            bool canUnlock = flag;
            Scene.HudGui.RoofConstructionIcons.Show(showManualfillLockIcon, false, false, flag4, canLock, canUnlock, false);
        }
    }
    /*
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
                    base.transform.position = new Vector3(base.transform.position.x, Mathf.Clamp(LocalPlayer.Create.BuildingPlacer.AirBorneHeight, this._bottomY + this._logLength, this._bottomY + this._logLength + 2000f), base.transform.position.z);
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


    }
    */
}
