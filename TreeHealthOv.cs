using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimateCheatmenu
{
    class TreeHealthOv : TreeHealth
    {
        protected override void Hit()
        {
            if (UCheatmenu.InstantTree)
            {
                this.Explosion(100f);
                return;
            }
            base.Hit();
        }
    }
}
