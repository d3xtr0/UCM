using System;
using TheForest.Buildings.World;

namespace UltimateCheatmenu
{
    public static class BuildingRepairHelper
    {
        public static void RepairBuildingInstantly(this BuildingRepair building)
        {
            try
            {
                for (int i = 0; i < building._target.CalcMissingRepairLogs(); i++)
                {
                    building._target.AddRepairMaterial(true);
                }
                for (int j = 0; j < building._target.CalcMissingRepairMaterial(); j++)
                {
                    building._target.AddRepairMaterial(false);
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
