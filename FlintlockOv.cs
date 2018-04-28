using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest.Utils;
using TheForest.Items;
using TheForest.Items.World;

namespace UltimateCheatmenu
{
    class FlintlockOv : flintLockAnimSetup
    {
        protected override void Update()
        {
            if (UCheatmenu.FastFlint)
            {
                
                if (LocalPlayer.Inventory.AmountOf(this._flintAmmoId, true) > 0)
                {
                    LocalPlayer.Animator.SetBool("canReload", false);
                }
                else
                {
                    LocalPlayer.Animator.SetBool("canReload", false);
                }
                if (!LocalPlayer.Animator)
                {
                    return;
                }
                this.currState1 = LocalPlayer.Animator.GetCurrentAnimatorStateInfo(1);
                this.nextState1 = LocalPlayer.Animator.GetNextAnimatorStateInfo(1);
                this.currState2 = LocalPlayer.Animator.GetCurrentAnimatorStateInfo(2);
                if (this.currState1.tagHash == this.knockBackHash || this.currState2.shortNameHash == this.sittingHash)
                {
                    this.animator.CrossFade(this.idleHash, 0f, 0, 0f);
                }
                if (this.nextState1.shortNameHash == this.playerShootHash)
                {
                    this.animator.SetBool("shoot", true);
                }
                else
                {
                    this.animator.SetBool("shoot", false);
                }
                if (this.currState1.shortNameHash == this.playerReloadHash && !this.reloadSync && !LocalPlayer.Inventory.IsLeftHandEmpty())
                {
                    this.leftHandFull = true;
                    LocalPlayer.Inventory.MemorizeItem(Item.EquipmentSlot.LeftHand);
                    LocalPlayer.Inventory.StashLeftHand();
                }
                if (this.nextState1.shortNameHash != this.playerIdleHash)
                {
                    return;
                }
                LocalPlayer.Inventory.CancelReloadDelay();
                if (!this.leftHandFull)
                {
                    return;
                }
                LocalPlayer.Inventory.EquipPreviousUtility();
                this.leftHandFull = false;
                
            }
            else
            {
                base.Update();
            }
        }
    }

    class BowOv : BowController
    {

        protected override void Update()
        {
            if (UCheatmenu.FastFlint)
            {
                if (this._player.CurrentView == TheForest.Items.Inventory.PlayerInventory.PlayerViews.World)
                {
                    if (this._player.AmountOf(this._ammoItemId, true) > 0)
                    {
                        TheForest.Utils.LocalPlayer.Animator.SetBool("noAmmo", false);
                    }
                    else
                    {
                        TheForest.Utils.LocalPlayer.Animator.SetBool("noAmmo", true);
                    }
                    if (!TheForest.Utils.LocalPlayer.Create.Grabber.Target && TheForest.Utils.LocalPlayer.MainCamTr.forward.y < -0.85f)
                    {
                        TheForest.Items.Craft.WeaponStatUpgrade.Types types = this.NextAvailableArrowBonus(this.BowItemView.ActiveBonus);
                        if (types != this.BowItemView.ActiveBonus)
                        {
                            this._showRotateArrowType = true;
                            if (!TheForest.Utils.Scene.HudGui.ToggleArrowBonusIcon.activeSelf)
                            {
                                TheForest.Utils.Scene.HudGui.ToggleArrowBonusIcon.SetActive(true);
                            }
                            if (TheForest.Utils.Input.GetButtonDown("Rotate"))
                            {
                                TheForest.Utils.LocalPlayer.Sfx.PlayWhoosh();
                                this.SetActiveBowBonus(types);
                                TheForest.Utils.Scene.HudGui.ToggleArrowBonusIcon.SetActive(false);
                            }
                        }
                        else if (this._showRotateArrowType)
                        {
                            this._showRotateArrowType = false;
                            TheForest.Utils.Scene.HudGui.ToggleArrowBonusIcon.SetActive(false);
                        }
                    }
                    else if (this._showRotateArrowType)
                    {
                        this._showRotateArrowType = false;
                        TheForest.Utils.Scene.HudGui.ToggleArrowBonusIcon.SetActive(false);
                    }
                    if (this.CurrentArrowItemView.ActiveBonus != this.BowItemView.ActiveBonus)
                    {
                        TheForest.Utils.LocalPlayer.Inventory.SortInventoryViewsByBonus(this.CurrentArrowItemView, this.BowItemView.ActiveBonus, false);
                        if (this.CurrentArrowItemView.ActiveBonus != this.BowItemView.ActiveBonus)
                        {
                            this.SetActiveBowBonus(this.CurrentArrowItemView.ActiveBonus);
                        }
                        this.UpdateArrowRenderer();
                    }
                    TheForest.Items.Craft.WeaponStatUpgrade.Types activeBonus = this.CurrentArrowItemView.ActiveBonus;
                    bool canSetArrowOnFire = this.CanSetArrowOnFire;
                    if (canSetArrowOnFire && TheForest.Utils.Input.GetButton("Lighter"))
                    {
                        TheForest.Utils.Scene.HudGui.SetDelayedIconController(this);
                    }
                    else
                    {
                        TheForest.Utils.Scene.HudGui.UnsetDelayedIconController(this);
                    }
                    if (canSetArrowOnFire)
                    {
                        if (TheForest.Utils.Input.GetButtonAfterDelay("Lighter", 0.5f, false))
                        {
                            this.SetArrowOnFire();
                        }
                    }
                    else if (activeBonus != TheForest.Items.Craft.WeaponStatUpgrade.Types.BurningAmmo && this._activeAmmoBonus != activeBonus)
                    {
                        this.SetActiveArrowBonus(activeBonus);
                    }
                    if (!this._lightingArrow)
                    {
                        if (TheForest.Utils.Input.GetButtonDown("Fire1") && !TheForest.Utils.LocalPlayer.Animator.GetBool("ballHeld"))
                        {
                            this._animator.speed = 20f;
                            this._bowAnimator.speed = 20f;
                            TheForest.Utils.LocalPlayer.Inventory.CancelNextChargedAttack = false;
                            if (this._aimingReticle)
                            {
                                this._aimingReticle.enabled = true;
                            }
                            this.ReArm();
                            this._animator.SetBoolReflected("drawBowBool", true);
                            this._bowAnimator.SetBoolReflected("drawBool", true);
                            this._bowAnimator.SetBoolReflected("bowFireBool", false);
                            this._animator.SetBoolReflected("bowFireBool", false);
                            this._player.StashLeftHand();
                            this._animator.SetBoolReflected("checkArms", false);
                            this._animator.SetBoolReflected("onHand", false);

                        }
                        else if (TheForest.Utils.Input.GetButtonDown("AltFire") || TheForest.Utils.LocalPlayer.Animator.GetBool("ballHeld"))
                        {
                            TheForest.Utils.LocalPlayer.AnimControl.animEvents.enableSpine();
                            this._player.CancelNextChargedAttack = true;
                            this._animator.SetBool("drawBowBool", false);
                            this._bowAnimator.SetBool("drawBool", false);
                            this.ShutDown(false);
                        }
                        if (TheForest.Utils.Input.GetButtonUp("Fire1") || TheForest.Utils.LocalPlayer.Animator.GetBool("ballHeld"))
                        {
                            this._currentAmmo = this.CurrentArrowItemView;
                            if (this._aimingReticle)
                            {
                                this._aimingReticle.enabled = false;
                            }
                            base.CancelInvoke();
                            if (this._animator.GetCurrentAnimatorStateInfo(1).tagHash == this._attackHash && this._animator.GetBool("drawBowBool") && !TheForest.Utils.LocalPlayer.Animator.GetBool("ballHeld") && !TheForest.Utils.LocalPlayer.Inventory.blockRangedAttack)
                            {
                                this._animator.SetBoolReflected("bowFireBool", true);
                                this._bowAnimator.SetBoolReflected("bowFireBool", true);
                                this._animator.SetBoolReflected("drawBowBool", false);
                                this._bowAnimator.SetBoolReflected("drawBool", false);
                                TheForest.Utils.LocalPlayer.TargetFunctions.sendPlayerAttacking();
                                this.InitReArm();
                            }
                            else if (TheForest.Utils.LocalPlayer.Animator.GetBool("ballHeld"))
                            {
                                TheForest.Utils.LocalPlayer.AnimControl.animEvents.enableSpine();
                                this._player.CancelNextChargedAttack = true;
                                this._animator.SetBoolReflected("drawBowBool", false);
                                this._bowAnimator.SetBoolReflected("drawBool", false);
                                this.ShutDown(false);
                            }
                            else if (TheForest.Utils.LocalPlayer.Inventory.blockRangedAttack)
                            {
                                this.ShutDown(false);
                            }
                            else
                            {
                                this.ShutDown(true);
                            }
                            this._animator.speed = 1f;
                            this._bowAnimator.speed = 1f;
                        }
                        else if (Time.time+0.1f < Time.time)
                        {
                            this.ReArm();
                        }
                        
                    }
                    else
                    {
                        TheForest.Utils.LocalPlayer.Inventory.CancelNextChargedAttack = true;
                    }
                }
            }
            else
            {
                base.Update();
            }
            
        }
    }
}
