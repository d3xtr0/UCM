using System;
using TheForest.Utils;
using UnityEngine;

namespace UltimateCheatmenu
{
    internal class FPController : FirstPersonCharacter
    {
        protected float baseWalkSpeed;

        protected float baseRunSpeed;

        protected float baseJumpHeight;

        protected float baseCrouchSpeed;

        protected float baseStrafeSpeed;

        protected float baseSwimmingSpeed;

        protected float baseGravity;

        protected float baseMaxVelocityChange;

        protected float baseMaximumVelocity;

        protected float baseMaxSwimVelocity;

        protected new Collider[] AllChildColliders;

        protected new Collider[] AllColliders;

        protected bool[] AllChildCollidersEnabled;

        protected bool[] AllCollidersEnabled;

        protected new bool LastNoClip;

        protected new bool LastFlyMode;


        protected new void BaseValues()
        {
            this.baseMaxSwimVelocity = this.maxSwimVelocity;
            this.baseWalkSpeed = this.walkSpeed;
            this.baseRunSpeed = this.runSpeed;
            this.baseJumpHeight = this.jumpHeight;
            this.baseCrouchSpeed = this.crouchSpeed;
            this.baseStrafeSpeed = this.strafeSpeed;
            this.baseSwimmingSpeed = this.swimmingSpeed;
            this.baseMaxVelocityChange = this.maxVelocityChange;
            this.baseMaximumVelocity = this.maximumVelocity;
            this.baseGravity = this.gravity;
        }

        protected override void Start()
        {
            this.AllChildColliders = base.gameObject.GetComponentsInChildren<Collider>();
            this.AllColliders = base.gameObject.GetComponents<Collider>();

            this.AllChildCollidersEnabled = new bool[this.AllChildColliders.Length];
            this.AllCollidersEnabled = new bool[this.AllColliders.Length];

            for (int i = 0; i < this.AllColliders.Length; i++)
            {
                this.AllCollidersEnabled[i] = this.AllColliders[i].enabled;
            }
            for (int j = 0; j < this.AllChildColliders.Length; j++)
            {
                this.AllChildCollidersEnabled[j] = this.AllChildColliders[j].enabled;
            }

            base.Start();
            this.BaseValues();
        }

        protected override void FixedUpdate()
        {
            allowFallDamage = !UCheatmenu.GodMode;
            fallShakeBlock = UCheatmenu.GodMode;

            this.walkSpeed = this.baseWalkSpeed * UCheatmenu.SpeedMultiplier;
            this.runSpeed = this.baseRunSpeed * UCheatmenu.SpeedMultiplier;
            this.jumpHeight = this.baseJumpHeight * UCheatmenu.JumpMultiplier;
            this.crouchSpeed = this.baseCrouchSpeed * UCheatmenu.SpeedMultiplier;
            this.strafeSpeed = this.baseStrafeSpeed * UCheatmenu.SpeedMultiplier;
            this.swimmingSpeed = this.baseSwimmingSpeed * UCheatmenu.SpeedMultiplier;
            this.maxSwimVelocity = this.baseMaxSwimVelocity * UCheatmenu.SpeedMultiplier;

            if (!UCheatmenu.FreeCam)
            {
 
                if (UCheatmenu.FlyMode && !this.PushingSled)
                {
                    this.rb.useGravity = false;
                    if (UCheatmenu.NoClip)
                    {
                        if (!this.LastNoClip)
                        {
                            for (int i = 0; i < this.AllColliders.Length; i++)
                            {
                                this.AllColliders[i].enabled = false;
                            }
                            for (int j = 0; j < this.AllChildColliders.Length; j++)
                            {
                                this.AllChildColliders[j].enabled = false;
                            }
                            this.LastNoClip = true;
                        }
                    }
                    else if (this.LastNoClip)
                    {
                        for (int k = 0; k < this.AllColliders.Length; k++)
                        {
                            this.AllColliders[k].enabled = this.AllCollidersEnabled[k];
                        }
                        for (int l = 0; l < this.AllChildColliders.Length; l++)
                        {
                            this.AllChildColliders[l].enabled = this.AllChildCollidersEnabled[l];
                        }
                        this.LastNoClip = false;
                    }
                    bool button = TheForest.Utils.Input.GetButton("Crouch");
                    bool arg_19D_0 = TheForest.Utils.Input.GetButton("Run");
                    bool button2 = TheForest.Utils.Input.GetButton("Jump");
                    float num = this.baseWalkSpeed;
                    this.gravity = 0f;
                    if (arg_19D_0)
                    {
                        num = this.baseRunSpeed;
                    }
                    Vector3 arg_21F_0 = Camera.main.transform.rotation * (new Vector3(TheForest.Utils.Input.GetAxis("Horizontal"), 0f, TheForest.Utils.Input.GetAxis("Vertical")) * num * UCheatmenu.SpeedMultiplier);
                    Vector3 velocity = this.rb.velocity;
                    if (button2)
                    {
                        velocity.y -= num * UCheatmenu.SpeedMultiplier;
                    }
                    if (button)
                    {
                        velocity.y += num * UCheatmenu.SpeedMultiplier;
                    }
                    Vector3 force = arg_21F_0 - velocity;
                    this.rb.AddForce(force, ForceMode.VelocityChange);
                    this.LastFlyMode = true;
                    return;
                }
                if (this.LastFlyMode)
                {
                    if (!this.IsInWater())
                    {
                        this.rb.useGravity = true;
                    }
                    this.gravity = this.baseGravity;
                    if (this.LastNoClip)
                    {
                        for (int m = 0; m < this.AllColliders.Length; m++)
                        {
                            this.AllColliders[m].enabled = this.AllCollidersEnabled[m];
                        }
                        for (int n = 0; n < this.AllChildColliders.Length; n++)
                        {
                            this.AllChildColliders[n].enabled = this.AllChildCollidersEnabled[n];
                        }
                        this.LastNoClip = false;
                    }
                    this.LastFlyMode = false;
                }
                base.FixedUpdate();
            }
        }

       
    }

    class EnemyhealthOv : EnemyHealth
    {
        public override void Hit(int damage)
        {
            if (UCheatmenu.InstaKill)
            {
                this.Die();
                return;
            }

            this.HitReal(damage);
        }

    }

    public class PlayerHitReactionsEx : playerHitReactions
    {
        public override void enableExplodeShake(float dist)
        {
            if (!UCheatmenu.GodMode)
            {
                base.enableExplodeShake(dist);
            }
        }
    }


}

