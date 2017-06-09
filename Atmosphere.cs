using System;
using UnityEngine;

namespace UltimateCheatmenu
{
    internal class Atmosphere : TheForestAtmosphere
    {
        [ModAPI.Attributes.Priority(200)]
        protected override void Update()
        {

            base.Update();
            Shader.SetGlobalColor("_AmbientSkyColor", RenderSettings.ambientSkyColor.linear * RenderSettings.ambientIntensity);
            Shader.SetGlobalFloat("CaveAmount", UCheatmenu.CaveLight);
            Shader.SetGlobalFloat("_ForestCaveSetting", Mathf.Lerp(1f, -1f, UCheatmenu.CaveLight));

            if (UCheatmenu.NightLightOriginal < this.MoonBrightness)
            {
                UCheatmenu.NightLightOriginal += 0.01f * Time.deltaTime;
            }
            else if (UCheatmenu.NightLightOriginal > this.MoonBrightness)
            {
                UCheatmenu.NightLightOriginal -= 0.01f * Time.deltaTime;
            }
            this.MoonLightIntensity = UCheatmenu.NightLight * UCheatmenu.NightLightOriginal;

            if (TheForest.Utils.LocalPlayer.IsInCaves) //IsInClosedArea
            {
                global::TheForestAtmosphere.ReflectionAmount = 0.2f;
                this.temp_AmbientIntensity = Mathf.Lerp(this.SunsetAmbientIntensity, UCheatmenu.CaveLight, Time.deltaTime);
                this.temp_SunIntensity = Mathf.Lerp(this.temp_SunIntensity, 0f, Time.deltaTime);
                this.temp_SunBounceIntensity = Mathf.Lerp(this.temp_SunBounceIntensity, 0f, Time.deltaTime);
                this.temp_BounceColor = Color.Lerp(this.temp_BounceColor, Color.black, Time.deltaTime);
                this.temp_SkyColor = Color.Lerp(this.temp_SkyColor, this.CaveSkyColor, Time.deltaTime);
                this.temp_EquatorColor = new Color(UCheatmenu.CaveLight, UCheatmenu.CaveLight, UCheatmenu.CaveLight);
                this.temp_GroundColor = new Color(UCheatmenu.CaveLight, UCheatmenu.CaveLight, UCheatmenu.CaveLight);
                this.temp_AddLight1 = Color.Lerp(this.SunsetAddLight1, this.CaveAddLight1, Time.deltaTime);
                this.temp_AddLight1Dir = Vector3.Lerp(this.SunsetAddLight1Dir, this.CaveAddLight1Dir, Time.deltaTime);
                this.temp_AddLight2 = Color.Lerp(this.SunsetAddLight2, this.CaveAddLight2, Time.deltaTime);
                this.temp_AddLight2Dir = Vector3.Lerp(this.SunsetAddLight2Dir, this.CaveAddLight2Dir, Time.deltaTime);
                this.SetSHAmbientLighting();
            }

            if (!UCheatmenu.Fog)
            {
                this.FogColor = new Color(0f, 0f, 0f, 0f);
                if (global::Sunshine.Instance.Ready)
                {
                    global::Sunshine.Instance.ScatterColor = this.FogColor;
                }
                if (TheForest.Utils.LocalPlayer.IsInClosedArea)
                {
                    this.FogColor = new Color(0f, 0f, 0f, 0f);
                }
                this.CurrentFogColor = this.FogColor;
                Shader.SetGlobalColor("_FogColor", this.FogColor);
                this.ChangeFogAmount();
            }
            else
            {
                this.FogColor = Color.Lerp(Color.Lerp(new Color(0f, 0f, 0f, 0f), this.NoonFogColor, Mathf.Clamp01(this.LdotUp)), new Color(0f, 0f, 0f, 0f), Mathf.Clamp01(this.LdotDown));
                if (Sunshine.Instance.Ready)
                {
                    Sunshine.Instance.ScatterColor = this.FogColor;
                }
                if (TheForest.Utils.LocalPlayer.IsInClosedArea)
                {
                    this.FogColor = new Color(0f, 0f, 0f, 0f);
                }
                this.CurrentFogColor = this.FogColor;
                Shader.SetGlobalColor("_FogColor", this.FogColor);
                this.ChangeFogAmount();
            }


        }

        [ModAPI.Attributes.Priority(200)]
        override protected void ChangeFogAmount()
        {
            if (!UCheatmenu.Fog)
            {
                this.FogCurrent = 300000f;
                TheForestAtmosphere.Instance.FogCurrent = 300000f;
            }else
            {
                if (TheForest.Utils.LocalPlayer.IsInEndgame)
                {
                    this.FogCurrent = 900f;
                    TheForestAtmosphere.Instance.FogCurrent = 900f;
                }
                else if (TheForest.Utils.LocalPlayer.IsInCaves)
                {
                    this.FogCurrent = 3000f;
                    TheForestAtmosphere.Instance.FogCurrent = 3000f;
                }
                else
                {
                    float cur = (float)UnityEngine.Random.Range(700, 2000);
                    this.FogCurrent = cur;
                    TheForestAtmosphere.Instance.FogCurrent = cur;
                }
            }
        }
    }
}
