using System;
using TheForest.Utils;
using TheForest.World;
using UnityEngine;

namespace UltimateCheatmenu
{
    public class NewWeatherSystem : WeatherSystem
    {
        protected float ResetCloudTime;

        protected override void TryRain()
        {
            if (UCheatmenu.FreezeWeather)
            {
                return;
            }
            base.TryRain();
        }

        protected override void Update()
        {
            if (this.ResetCloudTime > 0f)
            {
                this.ResetCloudTime -= Time.deltaTime;
                if (this.ResetCloudTime <= 0f)
                {
                    this.CloudSmoothTime = 20f;
                }
            }
            if (UCheatmenu.ForceWeather >= 0)
            {
                this.AllOff();
                Scene.RainFollowGO.SetActive(true);
                Scene.RainTypes.SnowConstant.SetActive(false);
                if (UCheatmenu.ForceWeather == 1)
                {
                    this.TurnOn(WeatherSystem.RainTypes.Light);
                    this.CloudSmoothTime = 1f;
                }
                if (UCheatmenu.ForceWeather == 2)
                {
                    this.TurnOn(WeatherSystem.RainTypes.Medium);
                    this.CloudSmoothTime = 1f;
                }
                if (UCheatmenu.ForceWeather == 3)
                {
                    this.TurnOn(WeatherSystem.RainTypes.Heavy);
                    this.CloudSmoothTime = 1f;
                }
                if (UCheatmenu.ForceWeather == 4)
                {
                    this.RainDiceStop = 1;
                    this.GrowClouds();
                    this.ReduceClouds();
                    this.CloudOvercastTargetValue = UnityEngine.Random.Range(0.55f, 1f);
                    this.CloudOpacityScaleTargetValue = UnityEngine.Random.Range(1f, 1.2f);
                }
                if (UCheatmenu.ForceWeather == 5)
                {
                    this.State = WeatherSystem.States.Raining;
                    Scene.RainTypes.SnowLight.SetActive(true);
                    this.CloudSmoothTime = 1f;
                }
                if (UCheatmenu.ForceWeather == 6)
                {
                    this.State = WeatherSystem.States.Raining;
                    Scene.RainTypes.SnowMedium.SetActive(true);
                    this.CloudSmoothTime = 1f;
                }
                if (UCheatmenu.ForceWeather == 7)
                {
                    this.State = WeatherSystem.States.Raining;
                    Scene.RainTypes.SnowHeavy.SetActive(true);
                    this.CloudSmoothTime = 1f;
                }
                UCheatmenu.ForceWeather = -1;
            }
            base.Update();
        }
    }
}
