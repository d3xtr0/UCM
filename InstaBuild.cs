using ModAPI;
using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TheForest.Buildings.Creation;
using TheForest.Items.Craft;
using TheForest.Buildings.Interfaces;
using TheForest.Buildings.World;
using UnityEngine;
using Bolt;

namespace UltimateCheatmenu
{
    class InstaBuild : UCheatmenu
    {
        public static void InstantBuilder(Craft_Structure structure)
        {
            try
            {
                if (structure._requiredIngredients.Count > 0)
                {
                    for (int i = 0; i < structure._requiredIngredients.Count; i++)
                    {
                        Craft_Structure.BuildIngredients buildIngredients = structure._requiredIngredients[i];
                        if (structure.GetPresentIngredients().Length > i)
                        {
                            ReceipeIngredient receipeIngredient = structure.GetPresentIngredients()[i];
                            if (receipeIngredient._amount < buildIngredients._amount)
                            {
                                for (int j = 0; j < buildIngredients._amount - receipeIngredient._amount; j++)
                                {
                                    if (BoltNetwork.isRunning)
                                    {
                                        AddIngredient ingredient = AddIngredient.Create(GlobalTargets.OnlyServer);
                                        ingredient.IngredientNum = i;
                                        ingredient.ItemId = buildIngredients._itemID;
                                        ingredient.Construction = structure.entity;
                                        ingredient.Send();
                                    }
                                    else
                                    {
                                        structure.AddIngrendient_Actual(i, true, null);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }

        public static void exec()
        {
            if (UCheatmenu.ARadiusGlobal)
            {
                Craft_Structure[] structures = UnityEngine.Object.FindObjectsOfType<Craft_Structure>();
                if (structures != null && structures.Length != 0)
                {
                    for (int i = 0; i < structures.Length; i++)
                    {
                        InstaBuild.InstantBuilder(structures[i]);
                    }
                }
                else
                {
                    UCheatmenu.InstBuild = false;
                }
            }
            else
            {
                if (UCheatmenu.InstBuild)
                {
                    RaycastHit[] array = Physics.SphereCastAll(TheForest.Utils.LocalPlayer.GameObject.transform.position, UCheatmenu.ARadius, new Vector3(1f, 0f, 0f));

                    cstruct(array);
                }
                UCheatmenu.InstBuild = false;
            }
        }

        private static void cstruct(RaycastHit[] array)
        {

            for (int i = 0; i < array.Length; i++)
            {
                RaycastHit raycastHit = array[i];

                if (raycastHit.collider.GetComponent<Craft_Structure>() != null)
                {
                    InstaBuild.InstantBuilder(raycastHit.collider.GetComponent<Craft_Structure>());
                }
            }
            
        }

        public static void repairAll()
        {
            for (int k = 0; k < 5; k++)
            {
                BuildingRepair[] buildings = UnityEngine.Object.FindObjectsOfType<BuildingRepair>();
                try
                {
                    foreach (BuildingRepair component in buildings)
                    {
                        for (int i = 0; i < component._target.CalcMissingRepairLogs(); i++)
                        {
                            component._target.AddRepairMaterial(true);
                        }
                        for (int j = 0; j < component._target.CalcMissingRepairMaterial(); j++)
                        {
                            component._target.AddRepairMaterial(false);
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}
