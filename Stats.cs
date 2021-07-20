using ModAPI.Attributes;
using System;
using TheForest.Utils;

namespace UltimateCheatmenu
{
    internal class Stats : PlayerStats
    {
        [Priority(1000)]
        protected override void hitFallDown()
        {
            if (!UCheatmenu.GodMode)
            {
                base.hitFallDown();
            }
        }

        [Priority(1000)]
        protected override void HitFire()
        {
            if (!UCheatmenu.GodMode)
            {
                base.HitFire();
            }
        }

        [Priority(1000)]
        public override void hitFromEnemy(int getDamage)
        {
            if (!UCheatmenu.GodMode)
            {
                base.hitFromEnemy(getDamage);
            }
        }

        [Priority(1000)]
        public override void HitShark(int damage)
        {
            if (!UCheatmenu.GodMode)
            {
                base.HitShark(damage);
            }
        }

        [Priority(1000)]
        protected override void FallDownDead()
        {
            if (!UCheatmenu.GodMode)
            {
                base.FallDownDead();
            }
        }

        [Priority(1000)]
        protected override void HitFromPlayMaker(int damage)
        {
            if (!UCheatmenu.GodMode)
            {
                base.HitFromPlayMaker(damage);
            }
        }

        [Priority(1000)]
        protected override void Fell()
        {
            if (!UCheatmenu.GodMode)
            {
                base.Fell();
            }
        }

        [Priority(1000)]
        protected override void KnockOut()
        {
            if (!UCheatmenu.GodMode)
            {
                base.KnockOut();
            }
        }

        [Priority(1000)]
        protected override void Bleed()
        {
            if (!UCheatmenu.GodMode)
            {
                base.Bleed();
            }
        }

        [Priority(1000)]
        public override void Poison()
        {
            if (!UCheatmenu.GodMode)
            {
                base.Poison();
            }
        }

        [Priority(1000)]
        protected override void HitPoison()
        {
            if (!UCheatmenu.GodMode)
            {
                base.HitPoison();
            }
        }

        [Priority(1000)]
        public override void Hit(int damage, bool ignoreArmor, PlayerStats.DamageType type)
        {
            if (!UCheatmenu.GodMode)
            {
                base.Hit(damage, ignoreArmor, type);
            }
        }

        protected override void Update()
        {
            if (UCheatmenu.SleepTimer)
            {
                if (!BoltNetwork.isClient && TheForest.Utils.Scene.SceneTracker.allPlayers.Count >= 1)
                {
                    TheForest.Utils.Scene.MutantSpawnManager.offsetSleepAmounts();
                    TheForest.Utils.Scene.MutantControler.startSetupFamilies();
                    TheForest.Tools.EventRegistry.Player.Publish(TheForest.Tools.TfEvent.Slept, null);
                    this.NextSleepTime = TheForest.Utils.Scene.Clock.ElapsedGameTime;
                    base.Invoke("TurnOffSleepCam", 3f);
                    this.Tired = 0f;
                    this.Atmos.TimeLapse();
                    TheForest.Utils.Scene.HudGui.ToggleAllHud(false);
                    TheForest.Utils.Scene.Cams.SleepCam.SetActive(true);
                    this.Energy += 100f;
                    UCheatmenu.SleepTimer = false;
                    return;
                }
                this.Wake();
                UCheatmenu.SleepTimer = false;
            }

            if (UCheatmenu.GodMode)
            {
                base.IsBloody = false;
                this.FireWarmth = true;
                this.SunWarmth = true;
                base.IsCold = false;
                this.Health = 100f;
                this.Armor = 100;
                this.Fullness = 1f;
                this.Stamina = 100f;
                this.Energy = 100f;
                this.Hunger = 0;
                this.Thirst = 0f;
                this.Starvation = 0f;
                this.AirBreathing.CurrentLungAir = 300f;
            }
            if (UCheatmenu.UnlimitedFuel)
            {
                this.Fuel.CurrentFuel = 120f;
            }
            if (UCheatmenu.UnlimitedHairspray)
            {
                global::Cheats.UnlimitedHairspray = true;
            }
            else
            {
                global::Cheats.UnlimitedHairspray = false;
            }
            base.Update();
        }

        protected override void KillPlayer()
        {
            if (!UCheatmenu.GodMode)
            {
                base.KillPlayer();
            }
        }

        public override void HitFood()
        {
            if (!UCheatmenu.GodMode)
            {
                base.HitFood();
            }
        }

        public override void HitFoodDelayed(int damage)
        {
            if (!UCheatmenu.GodMode)
            {
                base.HitFoodDelayed(damage);
            }
        }

        protected override void Explosion(float dist)
        {
            if (!UCheatmenu.GodMode)
            {
                base.Explosion(dist);
            }
        }

        public override void Burn()
        {
            if (!UCheatmenu.GodMode)
            {
                base.Burn();
            }
        }

    }

}
