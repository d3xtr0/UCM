using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimateCheatmenu
{
    class FireOv : Fire
    {
        protected override void Drain()
        {
            if (!UCheatmenu.InfFire)
            {
                base.Drain();
            }
        }
    }

    class FireStandOv : FireStand
    {
        protected override void Drain()
        {
            if (!UCheatmenu.InfFire)
            {
                base.Drain();
            }
        }
    }

    class Fire2Ov : Fire2
    {
        protected override void UpdateLit()
        {
            base.UpdateLit();
            if (UCheatmenu.InfFire)
            {
                this.Fuel = 120;
            }
        }
    }

    class FireEffigyOv : enableEffigy
    {
        protected override void Update()
        {
            base.Update();
            if (UCheatmenu.InfFire)
            {
                this.duration = 1200;
            }
        }
    }
}
