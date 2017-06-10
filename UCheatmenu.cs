using ModAPI;
using ModAPI.Attributes;
using System;
using System.Collections;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using TheForest;
using TheForest.Utils;
using TheForest.Player;
using TheForest.Items;
using TheForest.Buildings.Creation;
using TheForest.World;
using TheForest.UI.Multiplayer;
using TheForest.Networking;
using Bolt;
using Steamworks;
using UnityEngine;
using PathologicalGames;
using System.Collections.Generic;

namespace UltimateCheatmenu
{
    public class UCheatmenu : MonoBehaviour
    {
        protected bool visible;

        protected bool firsttime = true;

        public static bool GodMode = false;

        public static bool UnlimitedFuel = false;

        public static bool InfFire = false;

        public static bool FastFlint = false;

        public static bool InstBuild = false;

        public static bool Fog = true;

        public static float SpeedMultiplier = 1f;

        public static float JumpMultiplier = 1f;

        public static bool FlyMode = false;

        public static bool NoClip = false;

        public static float TimeSpeed = 0.13f;

        public static bool InstantTree = false;

        public static bool InstantBuild = false;

        public static float CaveLight = 0f;

        public static float NightLight = 1f;
        public static float NightLightOriginal = 0.2f;

        public static int ForceWeather = -1;

        public static bool FreezeWeather = false;

        public static bool SphereTreeCut = false;
        public static bool SphereTreeStumps = false;
        public static bool SphereBushes = false;
        public static bool SphereCrates = false;
        public static bool SphereSuitcases = false;
        public static bool SphereFires = false;


        public static bool AutoBuild = false;

        public static bool SleepTimer = false;

        public static float ARadius = 10f; 

        public static bool ARadiusGlobal = false;

        protected float a = 2f;

        protected GUIStyle labelStyle;

        protected static bool Already = false;

        protected int Tab;

        public static bool InstaKill = false;

        public static bool Rebreather = false;

        protected static bool FixHealth = false;

        protected static bool FixBatteryCharge = false;

        protected static bool FixFullness = false;

        protected static bool FixStamina = false;

        protected static bool FixEnergy = false;

        protected static bool FixThirst = false;

        protected static bool FixStarvation = false;

        protected static bool FixBodyTemp = false;

        protected static float FixedHealth = -1f;

        protected static float FixedBatteryCharge = -1f;

        protected static float FixedFullness = -1f;

        protected static float FixedStamina = -1f;

        protected static float FixedEnergy = -1f;

        protected static float FixedThirst = -1f;

        protected static float FixedStarvation = -1f;

        protected static float FixedBodyTemp = -1f;

        public static bool FreeCam = false;

        public static bool FreezeTime = false;

        protected GameObject Sphere;

        //public static GameObject ASphere;

        protected float massTreeRadius = 20f;

        protected bool DestroyTree;

        protected bool LastFreezeTime;

        protected bool LastFreeCam;

        protected float rotationY;

        protected GUIStyle labelStylePos;

        public string Teleport = "0,0,0";

        public string tItem = "";
        public string iItem = "";
        public string pItem = "";

        public string setNumVar = "";

        public string setCurDay = "";

        public string setCutTrees = "";

        public string setCGrowGrass = "";

        public Vector2 scrollPosition = Vector2.zero;
        public Vector2 scrollPosition2 = Vector2.zero;

        private GameObject animalController;

        private float scroller;
        private float scroller2;

        public Color CurrentFogColor;

        public static bool DestroyBuildings = false;

        public static bool ItemHack = false;
        public static bool BuildHack = false;
        public static bool InvisibleToggle = false;
        public static bool AnimalToggle = true;
        public static bool BirdToggle = true;
        public static bool MutantToggle = true;
        public static bool VeganmodeToggle = false;
        public static bool SurvivalToggle = false;

        public static Dictionary<string, GameObject> AnimalPrefabs = new Dictionary<string, GameObject>();
        public static List<string> AnimalNames = new List<string>();

        public static Dictionary<string, GameObject> PropPrefabs = new Dictionary<string, GameObject>();
        public static List<string> PropNames = new List<string>();

        public static Dictionary<string, PrefabId> BoltPrefabsPre = new Dictionary<string, PrefabId>();
        public static Dictionary<string, PrefabId> BoltPrefabsDict = new Dictionary<string, PrefabId>();
        public static List<string> BoltNames = new List<string>();

        protected static bool Mutant_Skinned = false;
        protected static bool Mutant_Leader = false;
        protected static bool Mutant_Painted = false;
        protected static bool Mutant_Pale = false;
        protected static bool Mutant_Old = false;
        protected static bool Mutant_BBaby = false;
        protected static bool Mutant_Dynamite = false;


         [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            new GameObject("__UCheatmenu__").AddComponent<UCheatmenu>();
        }

        public static Vector3 Vector3FromString(string s)
        {
            string[] parts = s.Split(new string[] { "," }, StringSplitOptions.None);
            return new Vector3(
                float.Parse(parts[0]),
                float.Parse(parts[1]),
                float.Parse(parts[2]));
        }

        private void OnGUI()
        {
            // Only in Multiplayer
            if (BoltNetwork.isRunning && Scene.SceneTracker != null && Scene.SceneTracker.allPlayerEntities != null)
            {
                // Refresh players
                PlayerManager.Players.Clear();
                PlayerManager.Players.AddRange(Scene.SceneTracker.allPlayerEntities
                    .Where(o => o.isAttached &&
                                o.StateIs<IPlayerState>() &&
                                LocalPlayer.Entity != o &&
                                o.gameObject.activeSelf &&
                                o.gameObject.activeInHierarchy &&
                                o.GetComponent<BoltPlayerSetup>() != null)
                    .OrderBy(o => o.GetState<IPlayerState>().name)
                    .Select(o => new Player(o)));
            }

            


            if (labelStylePos == null)
            {
                labelStylePos = new GUIStyle(UnityEngine.GUI.skin.label);
                labelStylePos.fontSize = 12;
                labelStylePos.normal.textColor = Color.white;
            }

            if (this.visible)
            {
                string posX = LocalPlayer.GameObject.transform.position.x.ToString();
                string posY = LocalPlayer.GameObject.transform.position.y.ToString();
                string posZ = LocalPlayer.GameObject.transform.position.z.ToString();
                UnityEngine.GUI.Label(new Rect(10, 620, 300f, 20f), "("+posX + ", " + posY + ", " + posZ + ")", labelStylePos);

                UnityEngine.GUI.skin = ModAPI.GUI.Skin;
                Matrix4x4 matrix = UnityEngine.GUI.matrix;
                Matrix4x4 matrix2 = UnityEngine.GUI.matrix;
                if (this.labelStyle == null)
                {
                    this.labelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
                    this.labelStyle.fontSize = 12;
                }
                UnityEngine.GUI.Box(new Rect(10f, 10f, 700f, 600f), "", UnityEngine.GUI.skin.window);
                this.Tab = UnityEngine.GUI.Toolbar(new Rect(10f, 10f, 700f, 30f), this.Tab, new GUIContent[]
                {
                    new GUIContent("Cheats"),
                    new GUIContent("Environ."),
                    new GUIContent("Player"),
                    new GUIContent("Animals"),
                    new GUIContent("Mutants"),
                    new GUIContent("Spawn"),
                    new GUIContent("Inventory"),
                    new GUIContent("Coop"),
                    new GUIContent("Dev"),
                    new GUIContent("Game")
                }, UnityEngine.GUI.skin.GetStyle("Tabs"));
                float num = 50f;
                if (this.Tab == 0) // Cheats
                {
                    
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "God mode:", this.labelStyle);
                    UCheatmenu.GodMode = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.GodMode, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Flymode:", this.labelStyle);
                    UCheatmenu.FlyMode = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.FlyMode, "");
                    num += 30f; this.scroller += 30;
                    if (UCheatmenu.FlyMode)
                    {
                        UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "No clip:", this.labelStyle);
                        UCheatmenu.NoClip = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.NoClip, "");
                        num += 30f; this.scroller += 30;
                    }
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Instant Tree:", this.labelStyle);
                    UCheatmenu.InstantTree = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.InstantTree, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Instant Building Destroy:", this.labelStyle);
                    UCheatmenu.DestroyBuildings = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.DestroyBuildings, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Instant Kill:", this.labelStyle);
                    UCheatmenu.InstaKill = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.InstaKill, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Use Rebreather:", this.labelStyle);
                    UCheatmenu.Rebreather = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.Rebreather, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Unlimited Fuel:", this.labelStyle);
                    UCheatmenu.UnlimitedFuel = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.UnlimitedFuel, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Infinite Fire:", this.labelStyle);
                    UCheatmenu.InfFire = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.InfFire, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Fast Flintlock/Bow:", this.labelStyle);
                    UCheatmenu.FastFlint = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.FastFlint, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Sleep :", this.labelStyle);
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 150f, 20f), "Sleep"))
                    {
                        this.visible = false;
                        LocalPlayer.FpCharacter.UnLockView();
                        UCheatmenu.SleepTimer = true;
                    }
                    if (UnityEngine.GUI.Button(new Rect(340f, num, 150f, 20f), "Sleep & Save"))
                    {
                        this.visible = false;
                        LocalPlayer.FpCharacter.UnLockView();
                        UCheatmenu.SleepTimer = true;
                        LocalPlayer.Stats.Invoke("JustSave",3.5f);
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Free cam:", this.labelStyle);
                    UCheatmenu.FreeCam = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.FreeCam, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Freeze time:", this.labelStyle);
                    UCheatmenu.FreezeTime = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.FreezeTime, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Action radius ("+ Math.Round(UCheatmenu.ARadius).ToString() + "m):", this.labelStyle);
                    UnityEngine.GUI.Label(new Rect(190f, num, 100f, 20f), "Global", this.labelStyle);
                    num += 30f; this.scroller += 30;
                    UCheatmenu.ARadius = UnityEngine.GUI.HorizontalSlider(new Rect(20f, num + 3f, 150f, 30f), UCheatmenu.ARadius, 10f, 200f);
                    UCheatmenu.ARadiusGlobal = UnityEngine.GUI.Toggle(new Rect(190f, num, 20f, 30f), UCheatmenu.ARadiusGlobal, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Buildings:", labelStyle);
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 150f, 20f), "Cancel Ghosts"))
                    {
                        this._cancelallghosts();
                    }
                    if (UnityEngine.GUI.Button(new Rect(340f, num, 150f, 20f), "Repair"))
                    {
                        InstaBuild.repairAll();
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Log Hack", labelStyle);
                    if(UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), LocalPlayer.Inventory.Logs._infiniteLogHack, "") != LocalPlayer.Inventory.Logs._infiniteLogHack)
                    {
                        LocalPlayer.Inventory.Logs._infiniteLogHack = !LocalPlayer.Inventory.Logs._infiniteLogHack;
                        this._loghack();
                    }
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Item Hack", labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.ItemHack, "") != UCheatmenu.ItemHack)
                    {
                        UCheatmenu.ItemHack = !UCheatmenu.ItemHack;
                        this._itemhack();
                    }
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Build Hack", labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.BuildHack, "") != UCheatmenu.BuildHack)
                    {
                        UCheatmenu.BuildHack = !UCheatmenu.BuildHack;
                        this._buildhack();
                    }
                    num += 30f; this.scroller += 30;
                    
                    UnityEngine.GUI.Label(new Rect(20f, num, 410f, 20f), "Sphere (use scrollwheel to change radius)", this.labelStyle);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Cut trees:", this.labelStyle);
                    UCheatmenu.SphereTreeCut = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.SphereTreeCut, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Cut tree stumps:", this.labelStyle);
                    UCheatmenu.SphereTreeStumps = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.SphereTreeStumps, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Cut bushes:", this.labelStyle);
                    UCheatmenu.SphereBushes = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.SphereBushes, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Break crates:", this.labelStyle);
                    UCheatmenu.SphereCrates = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.SphereCrates, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Open suitcases:", this.labelStyle);
                    UCheatmenu.SphereSuitcases = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.SphereSuitcases, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Light Fires:", this.labelStyle);
                    UCheatmenu.SphereFires = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.SphereFires, "");
                    num += 30f; this.scroller += 30;


                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;
                    
                }
                if (this.Tab == 1) // Environment
                {
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Speed of time:", this.labelStyle);
                    TheForestAtmosphere.Instance.RotationSpeed = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), TheForestAtmosphere.Instance.RotationSpeed, 0.1f, 10f);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(280f, num, 100f, 20f), "Reset"))
                    {
                        TheForestAtmosphere.Instance.RotationSpeed = 0.13f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Time:", this.labelStyle);
                    TheForestAtmosphere.Instance.TimeOfDay = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), TheForestAtmosphere.Instance.TimeOfDay, 1f, 360f);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Night light:", this.labelStyle);
                    UCheatmenu.NightLight = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), UCheatmenu.NightLight, 1f, 10f);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Cave light:", this.labelStyle);
                    UCheatmenu.CaveLight = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), UCheatmenu.CaveLight, 0f, 1f);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Fog:", this.labelStyle);
                    UCheatmenu.Fog = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.Fog, "");
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "FreezeWeather:", this.labelStyle);
                    UCheatmenu.FreezeWeather = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.FreezeWeather, "");
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Clear Weather"))
                    {
                        UCheatmenu.ForceWeather = 0;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), "Cloudy"))
                    {
                        UCheatmenu.ForceWeather = 4;
                    }
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Light rain"))
                    {
                        UCheatmenu.ForceWeather = 1;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), "Light Snow"))
                    {
                        UCheatmenu.ForceWeather = 5;
                    }
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Medium rain"))
                    {
                        UCheatmenu.ForceWeather = 2;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), "Medium Snow"))
                    {
                        UCheatmenu.ForceWeather = 6;
                    }
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Heavy rain"))
                    {
                        UCheatmenu.ForceWeather = 3;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), "Heavy Snow"))
                    {
                        UCheatmenu.ForceWeather = 7;
                    }
                    num += 30f;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Garden:", this.labelStyle);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Plant"))
                    {
                        this._plantallgardens();
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), "Grow"))
                    {
                        this._growalldirtpiles();
                    }
                    num += 30f;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Trees (x|x%):", this.labelStyle);
                    setCutTrees = UnityEngine.GUI.TextField(new Rect(170f, num, 210f, 30f), setCutTrees, UnityEngine.GUI.skin.textField);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 95f, 20f), "Cut"))
                    {
                        this._cutdowntrees(setCutTrees);
                    }
                    if (UnityEngine.GUI.Button(new Rect(285f, num, 95f, 20f), "Regrow"))
                    {
                        this._cutdowntrees("-"+setCutTrees);
                    }
                    num += 30f;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Grass:", this.labelStyle);
                    setCGrowGrass = UnityEngine.GUI.TextField(new Rect(170f, num, 210f, 30f), setCGrowGrass, UnityEngine.GUI.skin.textField);
                    num += 30f;
                    /*if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), "Grow (radius)"))
                    {
                        this._growgrass(setCGrowGrass);
                    }
                    num += 30f;*/
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), "Cut (radius)"))
                    {
                        this._cutgrass(setCGrowGrass);
                    }
                    num += 30f;
                }
                if (this.Tab == 2) // Player
                {
                    UnityEngine.GUI.Label(new Rect(370f, num, 150f, 20f), "Fix", this.labelStyle);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Health:", this.labelStyle);
                    if (!UCheatmenu.FixHealth)
                    {
                        LocalPlayer.Stats.Health = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Health, 0f, 100f);
                    }
                    else
                    {
                        UCheatmenu.FixedHealth = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedHealth, 0f, 100f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Health * 10f) / 10f));
                    UCheatmenu.FixHealth = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixHealth, "");
                    if (UCheatmenu.FixHealth)
                    {
                        if (UCheatmenu.FixedHealth == -1f)
                        {
                            UCheatmenu.FixedHealth = LocalPlayer.Stats.Health;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedHealth = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Battery charge:", this.labelStyle);
                    if (!UCheatmenu.FixBatteryCharge)
                    {
                        LocalPlayer.Stats.BatteryCharge = (float)((int)UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.BatteryCharge, 0f, 100f));
                    }
                    else
                    {
                        UCheatmenu.FixedBatteryCharge = (float)((int)UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedBatteryCharge, 0f, 100f));
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.BatteryCharge * 10f) / 10f));
                    UCheatmenu.FixBatteryCharge = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixBatteryCharge, "");
                    if (UCheatmenu.FixBatteryCharge)
                    {
                        if (UCheatmenu.FixedBatteryCharge == -1f)
                        {
                            UCheatmenu.FixedBatteryCharge = LocalPlayer.Stats.BatteryCharge;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedBatteryCharge = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Fullness:", this.labelStyle);
                    if (!UCheatmenu.FixFullness)
                    {
                        LocalPlayer.Stats.Fullness = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Fullness, 0f, 1f);
                    }
                    else
                    {
                        UCheatmenu.FixedFullness = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedFullness, 0f, 1f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Fullness * 10f) / 10f));
                    UCheatmenu.FixFullness = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixFullness, "");
                    if (UCheatmenu.FixFullness)
                    {
                        if (UCheatmenu.FixedFullness == -1f)
                        {
                            UCheatmenu.FixedFullness = LocalPlayer.Stats.Fullness;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedFullness = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Stamina:", this.labelStyle);
                    if (!UCheatmenu.FixStamina)
                    {
                        LocalPlayer.Stats.Stamina = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Stamina, 0f, 100f);
                    }
                    else
                    {
                        UCheatmenu.FixedStamina = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedStamina, 0f, 100f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Stamina * 10f) / 10f));
                    UCheatmenu.FixStamina = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixStamina, "");
                    if (UCheatmenu.FixStamina)
                    {
                        if (UCheatmenu.FixedStamina == -1f)
                        {
                            UCheatmenu.FixedStamina = LocalPlayer.Stats.Stamina;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedStamina = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Energy:", this.labelStyle);
                    if (!UCheatmenu.FixEnergy)
                    {
                        LocalPlayer.Stats.Energy = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Energy, 0f, 100f);
                    }
                    else
                    {
                        UCheatmenu.FixedEnergy = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedEnergy, 0f, 100f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Energy * 10f) / 10f));
                    UCheatmenu.FixEnergy = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixEnergy, "");
                    if (UCheatmenu.FixEnergy)
                    {
                        if (UCheatmenu.FixedEnergy == -1f)
                        {
                            UCheatmenu.FixedEnergy = LocalPlayer.Stats.Energy;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedEnergy = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Thirst:", this.labelStyle);
                    if (!UCheatmenu.FixThirst)
                    {
                        LocalPlayer.Stats.Thirst = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Thirst, 0f, 1f);
                    }
                    else
                    {
                        UCheatmenu.FixedThirst = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedThirst, 0f, 1f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Thirst * 10f) / 10f));
                    UCheatmenu.FixThirst = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixThirst, "");
                    if (UCheatmenu.FixThirst)
                    {
                        if (UCheatmenu.FixedThirst == -1f)
                        {
                            UCheatmenu.FixedThirst = LocalPlayer.Stats.Thirst;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedThirst = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Starvation:", this.labelStyle);
                    if (!UCheatmenu.FixStarvation)
                    {
                        LocalPlayer.Stats.Starvation = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Starvation, 0f, 1f);
                    }
                    else
                    {
                        UCheatmenu.FixedStarvation = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedStarvation, 0f, 1f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Starvation * 10f) / 10f));
                    UCheatmenu.FixStarvation = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixStarvation, "");
                    if (UCheatmenu.FixStarvation)
                    {
                        if (UCheatmenu.FixedStarvation == -1f)
                        {
                            UCheatmenu.FixedStarvation = LocalPlayer.Stats.Starvation;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedStarvation = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Body Temp:", this.labelStyle);
                    if (!UCheatmenu.FixBodyTemp)
                    {
                        LocalPlayer.Stats.BodyTemp = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.BodyTemp, 10f, 60f);
                    }
                    else
                    {
                        UCheatmenu.FixedBodyTemp = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedBodyTemp, 10f, 60f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.BodyTemp * 10f) / 10f));
                    UCheatmenu.FixBodyTemp = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixBodyTemp, "");
                    if (UCheatmenu.FixBodyTemp)
                    {
                        if (UCheatmenu.FixedBodyTemp == -1f)
                        {
                            UCheatmenu.FixedBodyTemp = LocalPlayer.Stats.BodyTemp;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedBodyTemp = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Speed:", this.labelStyle);
                    UCheatmenu.SpeedMultiplier = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), UCheatmenu.SpeedMultiplier, 1f, 10f);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Jump power:", this.labelStyle);
                    UCheatmenu.JumpMultiplier = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), UCheatmenu.JumpMultiplier, 1f, 10f);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Invisible:", this.labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.InvisibleToggle, "") != UCheatmenu.InvisibleToggle)
                    {
                        UCheatmenu.InvisibleToggle = !UCheatmenu.InvisibleToggle;
                        this._invisible();
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Player:", this.labelStyle);
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 150f, 20f), "Kill me")) { this._killlocalplayer(); }
                    if (UnityEngine.GUI.Button(new Rect(340f, num, 150f, 20f), "Revive me")) { this._revivelocalplayer(); }
                    num += 30f;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Set (number)", labelStyle);
                    setNumVar = UnityEngine.GUI.TextField(new Rect(170f, num, 210f, 30f), setNumVar, UnityEngine.GUI.skin.textField);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), "Player Variation"))
                    {
                        this._setPlayerVariation(setNumVar);
                    }
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), "Player Body"))
                    {
                        this._setPlayerBody(setNumVar);
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Player Variation Extra:", labelStyle);
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 150f, 20f), "None"))
                    {
                        this._setVariationExtra("None");
                    }
                    if (UnityEngine.GUI.Button(new Rect(340f, num, 150f, 20f), "Jacket"))
                    {
                        this._setVariationExtra("Jacket");
                    }
                    num += 30f;
                }
                if (this.Tab == 3) // Animals
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "Animals:", this.labelStyle);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 170f, 20f), "Kill all animals"))
                    {
                        this._killallanimals();
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 170f, 20f), "Kill closest animal"))
                    {
                        this._killclosestanimal();
                    }
                    num += 30f; this.scroller += 30;


                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "Toggle Animals:", this.labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.AnimalToggle, "") != UCheatmenu.AnimalToggle)
                    {
                        UCheatmenu.AnimalToggle = !UCheatmenu.AnimalToggle;
                        this._animals();
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "Toggle Birds:", this.labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.BirdToggle, "") != UCheatmenu.BirdToggle)
                    {
                        UCheatmenu.BirdToggle = !UCheatmenu.BirdToggle;
                        this._birds();
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "Spawn Animals:", this.labelStyle);
                    num += 30f; this.scroller += 30;
                    foreach (string animal in AnimalNames)
                    {
                        string name = animal;
                        switch (name)
                        {
                            case "boarGo": name = "Boar"; break;
                            case "crocodileGo": name = "Crocodile"; break;
                            case "deerGo": name = "Deer"; break;
                            case "lizardGo": name = "Lizard"; break;
                            case "rabbitGo": name = "Rabbit"; break;
                            case "raccoonGo": name = "Raccoon"; break;
                            case "squirrelGo": name = "Squirrel"; break;
                            case "tortoiseGo": name = "Tortoise"; break;
                        }
                        if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), name))
                        {
                            
                            GameObject.Instantiate(AnimalPrefabs[animal], LocalPlayer.MainCam.transform.position + LocalPlayer.MainCam.transform.forward * 2f, Quaternion.identity);
                        }
                        num += 30f; this.scroller += 30;
                        
                    }
                    

                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;

                }
                if (this.Tab == 4) // Spawn Mutants
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Enemies:", this.labelStyle);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 170f, 20f), "Kill all enemies"))
                    {
                        this._killallenemies();
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 170f, 20f), "Kill closest enemy"))
                    {
                        this._killclosestenemy();
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 190f, 20f), "Knock down closest enemy"))
                    {
                        this._knockDownclosestenemy();
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 170f, 20f), "Kill Endboss"))
                    {
                        this._killEndBoss();
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "Toggle Enemies:", this.labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.MutantToggle, "") != UCheatmenu.MutantToggle)
                    {
                        UCheatmenu.MutantToggle = !UCheatmenu.MutantToggle;
                        this._enemies();
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Spawn Mutants", labelStyle);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Attributes:", labelStyle);
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Skinned:", this.labelStyle);
                    UCheatmenu.Mutant_Skinned = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.Mutant_Skinned, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Leader:", this.labelStyle);
                    UCheatmenu.Mutant_Leader = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.Mutant_Leader, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Painted:", this.labelStyle);
                    UCheatmenu.Mutant_Painted = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.Mutant_Painted, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Pale:", this.labelStyle);
                    UCheatmenu.Mutant_Pale = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.Mutant_Pale, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Old:", this.labelStyle);
                    UCheatmenu.Mutant_Old = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.Mutant_Old, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Dynamite:", this.labelStyle);
                    UCheatmenu.Mutant_Dynamite = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.Mutant_Dynamite, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Boss baby:", this.labelStyle);
                    UCheatmenu.Mutant_BBaby = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.Mutant_BBaby, "");
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Male")){ this._spawnmutant("male");}
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Male Skinny")) { this._spawnmutant("male_skinny"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Male Skinned")) { this._spawnmutant("skinnedMale"); }
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Female")) { this._spawnmutant("female"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Female Skinny")) { this._spawnmutant("female_skinny"); }
                    num += 30f; this.scroller += 30;
                    
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Pale")) { this._spawnmutant("pale"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Pale Skinny")) { this._spawnmutant("pale_skinny"); }
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Fireman")) { this._spawnmutant("fireman"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Vags")) { this._spawnmutant("vags"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Armsy")) { this._spawnmutant("armsy"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Baby")) { this._spawnmutant("baby"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Fat")) { this._spawnmutant("fat"); }
                    num += 30f; this.scroller += 30;
                    

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Spawn Mutant Families", labelStyle);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Regular Family")) { this._spawnRegularFamily(null); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Painted Family")) { this._spawnPaintedFamily(null); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Skinned Family")) { this._spawnSkinnedFamily(null); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Skinny Family")) { this._spawnSkinnyFamily(null); }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;
                }
                if (this.Tab == 5) // Spawn Items
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 340f, 540f), this.scrollPosition, new Rect(0f, 0f, 320f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "Spawn Items", labelStyle);
                    num += 30f; this.scroller += 30;

                    iItem = UnityEngine.GUI.TextField(new Rect(20f, num, 170f, 30f), iItem, UnityEngine.GUI.skin.textField);
                    num += 40f; this.scroller += 40;

                    foreach (int current in LocalPlayer.Inventory.InventoryItemViewsCache.Keys)
                    {
                        try
                        {
                            string curName = ItemDatabase.ItemById(current)._name;
                            if (iItem == "" || curName.ToLower().Contains(iItem.ToLower()))
                            {
                                UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), curName, labelStyle);
                                if (UnityEngine.GUI.Button(new Rect(190f, num, 130f, 20f), "Spawn"))
                                {
                                    LocalPlayer.Inventory.FakeDrop(current, null);
                                }
                                num += 30f; this.scroller += 30;
                            }

                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex.ToString());
                        }
                    }


                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;

                    /* Spawn Props */
                    this.scrollPosition2 = UnityEngine.GUI.BeginScrollView(new Rect(350f, 50f, 350f, 540f), this.scrollPosition2, new Rect(0f, 0f, 320f, this.scroller2));
                    this.scroller2 = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "Spawn Props", labelStyle);
                    num += 30f; this.scroller2 += 30;

                    pItem = UnityEngine.GUI.TextField(new Rect(20f, num, 170f, 30f), pItem, UnityEngine.GUI.skin.textField);
                    num += 40f; this.scroller2 += 40;
                    if (BoltNetwork.isRunning)
                    {
                        foreach (var prop in BoltPrefabsDict.OrderBy(key => key.Key))
                        {
                            try
                            {
                                if (prop.Key != "" && (pItem == "" || prop.Key.ToLower().Contains(pItem.ToLower())))
                                {
                                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "[MP]"+prop.Key.ToString(), labelStyle);
                                    if (UnityEngine.GUI.Button(new Rect(200f, num, 130f, 20f), "Spawn"))
                                    {
                                        BoltNetwork.Instantiate(prop.Value, LocalPlayer.MainCam.transform.position + LocalPlayer.MainCam.transform.forward * 2f + LocalPlayer.MainCam.transform.up * -3f, Quaternion.identity);
                                    }
                                    num += 30f; this.scroller2 += 30;
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Write(ex.ToString());
                            }
                        }
                    }
                    foreach (string prop in PropNames)
                    {
                        try
                        {
                            if (pItem == "" || prop.ToLower().Contains(pItem.ToLower()))
                            {
                                UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), prop, labelStyle);
                                if (UnityEngine.GUI.Button(new Rect(200f, num, 130f, 20f), "Spawn"))
                                {
                                    GameObject.Instantiate(PropPrefabs[prop], LocalPlayer.MainCam.transform.position + LocalPlayer.MainCam.transform.forward * 2f + LocalPlayer.MainCam.transform.up * -3f, Quaternion.identity);
                                }
                                num += 30f; this.scroller2 += 30;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex.ToString());
                        }
                    }
                    
                    


                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix2;
                }
                if (this.Tab == 6) // Inventory
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "Add Item (name or id)", labelStyle);
                    tItem = UnityEngine.GUI.TextField(new Rect(190f, num, 210f, 30f), tItem, UnityEngine.GUI.skin.textField);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(190f, num, 95f, 20f), "Add 1"))
                    {
                        this._additem(tItem, 1);
                    }
                    if (UnityEngine.GUI.Button(new Rect(305f, num, 95f, 20f), "Add Max"))
                    {
                        this._additem(tItem, 1000000);
                    }
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 170f, 20f), "Add All"))
                    {
                        this._addAllItems();
                    }
                    num += 30f; this.scroller += 30;


                    foreach (Item current in from i in ItemDatabase.Items
                                             orderby i._name
                                             select i)
                    {
                        if (tItem == "" || current._name.ToLower().Contains(tItem.ToLower()) || current._id.ToString().Contains(tItem))
                        {
                            UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), current._name + " [" + current._id + "]", labelStyle);
                            if (UnityEngine.GUI.Button(new Rect(190f, num, 100f, 20f), "Add 1"))
                            {
                                this._additem(current._id.ToString(), 1);
                            }
                            if (UnityEngine.GUI.Button(new Rect(310f, num, 100f, 20f), "Add Max"))
                            {
                                this._additem(current._id.ToString(), 100000);
                            }
                            num += 30f; this.scroller += 30;
                        }
                    }


                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;
                }
                if (this.Tab == 7) // Coop
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "This tab is in beta!", this.labelStyle);
                    num += 30f; this.scroller += 30;

                    if (PlayerManager.Players.Count == 0)
                    {
                        UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), "No players found", labelStyle);
                        num += 30f; this.scroller += 30;
                    }
                    else
                    {
                        foreach (var player in PlayerManager.Players)
                        {
                            UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), player.Name, labelStyle);

                            if (UnityEngine.GUI.Button(new Rect(210f, num, 100f, 20f), "Teleport"))
                            {
                                LocalPlayer.Transform.position = player.Position;
                            }

                            if (UnityEngine.GUI.Button(new Rect(330f, num, 100f, 20f), "Steam"))
                            {
                                SteamFriends.ActivateGameOverlayToUser("steamid", new CSteamID(player.SteamId));
                            }
                            num += 30f; this.scroller += 30;

                        }
                    }


                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;
                }
                if (this.Tab == 8) // Dev
                {
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Teleport:", labelStyle);
                    Teleport = UnityEngine.GUI.TextField(new Rect(170f, num, 210f, 30f), Teleport, UnityEngine.GUI.skin.textField);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), "Go"))
                    {
                        LocalPlayer.GameObject.transform.localPosition = Vector3FromString(Teleport);
                    }
                    num += 30f;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Where am i?"))
                    {
                        float x = LocalPlayer.GameObject.transform.position.x;
                        float y = LocalPlayer.GameObject.transform.position.z;
                        if (LocalPlayer.IsInCaves)
                        {
                            System.Diagnostics.Process.Start("https://theforestmap.com/caves/#" + x + "," + y);
                        }
                        else
                        {
                            System.Diagnostics.Process.Start("https://theforestmap.com/#" + x + "," + y);
                        }
                        
                    }
                    num += 30f;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Go in cave"))
                    {
                        this.GotoCave(true);
                    }
                    if (UnityEngine.GUI.Button(new Rect(190f, num, 150f, 20f), "Go out cave"))
                    {
                        this.GotoCave(false);
                    }
                    num += 30f;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Set current day ("+ Clock.Day.ToString() + "):", labelStyle);
                    setCurDay = UnityEngine.GUI.TextField(new Rect(170f, num, 210f, 30f), setCurDay, UnityEngine.GUI.skin.textField);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), "Set"))
                    {
                        this._setCurrentDay(setCurDay);
                    }
                    num += 30f;

                }
                if (this.Tab == 9) // Game
                {
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Save Game"))
                    {
                        this.visible = false;
                        LocalPlayer.Stats.JustSave();
                    }
                    num += 30f;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Veganmode", labelStyle);
                    Cheats.NoEnemies = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), Cheats.NoEnemies, "");
                    num += 30f;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Survival features", labelStyle);
                    Cheats.NoSurvival = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), Cheats.NoSurvival, "");
                    num += 30f;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Game Mode", labelStyle);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Standard"))
                    {
                        this._setGameMode("standard");
                    }
                    if (UnityEngine.GUI.Button(new Rect(190f, num, 150f, 20f), "Creative"))
                    {
                        this._setGameMode("creative");
                    }
                    if (UnityEngine.GUI.Button(new Rect(360f, num, 150f, 20f), "Mod"))
                    {
                        this._setGameMode("mod");
                    }
                    num += 30f;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Difficulty Mode", labelStyle);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Peaceful"))
                    {
                        this._setGameMode("peaceful");
                    }
                    if (UnityEngine.GUI.Button(new Rect(190f, num, 150f, 20f), "Normal"))
                    {
                        this._setGameMode("normal");
                    }
                    if (UnityEngine.GUI.Button(new Rect(360f, num, 150f, 20f), "Hard"))
                    {
                        this._setGameMode("hard");
                    }
                    num += 30f;


                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Ultimate Cheatmenu", labelStyle);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Save"))
                    {
                        saveSettings();
                    }
                    if (UnityEngine.GUI.Button(new Rect(190f, num, 150f, 20f), "Load"))
                    {
                        loadSettings();
                    }
                    if (UnityEngine.GUI.Button(new Rect(360f, num, 150f, 20f), "Reset"))
                    {
                        resetSettings();
                    }
                    num += 30f;



                }
            }
        }

        private void Start()
        {
            initSettings();

            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Mesh sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
            GameObject gameObject2 = new GameObject();
            gameObject2.name = "Inverted Sphere";
            MeshFilter meshFilter = gameObject2.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
            Vector3[] vertices = sharedMesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = vertices[i];
            }
            meshFilter.sharedMesh.vertices = vertices;
            int[] triangles = sharedMesh.triangles;
            for (int j = 0; j < triangles.Length; j += 3)
            {
                int num = triangles[j];
                triangles[j] = triangles[j + 2];
                triangles[j + 2] = num;
            }
            meshFilter.sharedMesh.triangles = triangles;
            Vector3[] normals = sharedMesh.normals;
            for (int k = 0; k < normals.Length; k++)
            {
                normals[k] = -normals[k];
            }
            meshFilter.sharedMesh.normals = normals;
            meshFilter.sharedMesh.uv = sharedMesh.uv;
            meshFilter.sharedMesh.uv2 = sharedMesh.uv2;
            meshFilter.sharedMesh.RecalculateBounds();
            UnityEngine.Object.DestroyImmediate(gameObject);
            this.Sphere = gameObject2;
            this.Sphere.AddComponent<MeshRenderer>();
            this.Sphere.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Legacy Shaders/Transparent/Diffuse"));
            this.Sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1f, 0f, 0f, 0.9f));
            this.Sphere.GetComponent<MeshRenderer>().enabled = false;
            this.Sphere.GetComponent<Collider>().enabled = false;

        }

        private void Update()
        {

            if (UCheatmenu.FreezeTime && !this.LastFreezeTime)
            {
                Time.timeScale = 0f;
                this.LastFreezeTime = true;
            }
            if (!UCheatmenu.FreezeTime && this.LastFreezeTime)
            {
                Time.timeScale = 1f;
                this.LastFreezeTime = false;
            }
            if (UCheatmenu.FreeCam && !this.LastFreeCam)
            {
                LocalPlayer.CamFollowHead.enabled = false;
                LocalPlayer.CamRotator.enabled = false;
                LocalPlayer.MainRotator.enabled = false;
                LocalPlayer.FpCharacter.enabled = false;
                this.LastFreeCam = true;
            }
            if (!UCheatmenu.FreeCam && this.LastFreeCam)
            {
                LocalPlayer.CamFollowHead.enabled = true;
                LocalPlayer.CamRotator.enabled = true;
                LocalPlayer.MainRotator.enabled = true;
                LocalPlayer.FpCharacter.enabled = true;
                this.LastFreeCam = false;
            }
            if (UCheatmenu.FreeCam)
            {
                bool arg_143_0 = TheForest.Utils.Input.GetButton("Crouch");
                bool arg_F3_0 = TheForest.Utils.Input.GetButton("Run");
                bool button = TheForest.Utils.Input.GetButton("Jump");
                float num = 0.1f;
                if (arg_F3_0)
                {
                    num = 2f;
                }
                Vector3 b = Camera.main.transform.rotation * (new Vector3(TheForest.Utils.Input.GetAxis("Horizontal"), 0f, TheForest.Utils.Input.GetAxis("Vertical")) * num);
                if (button)
                {
                    b.y += num;
                }
                if (arg_143_0)
                {
                    b.y -= num;
                }
                Camera.main.transform.position += b;
                float y = Camera.main.transform.localEulerAngles.y + TheForest.Utils.Input.GetAxis("Mouse X") * 15f;
                this.rotationY += TheForest.Utils.Input.GetAxis("Mouse Y") * 15f;
                this.rotationY = Mathf.Clamp(this.rotationY, -80f, 80f);
                Camera.main.transform.localEulerAngles = new Vector3(-this.rotationY, y, 0f);
            }
            if (ModAPI.Input.GetButton("MassTree") /*&& UCheatmenu.TreeCutEnabled*/)
            {
                if (!ChatBox.IsChatOpen)
                {
                    if (UnityEngine.Input.mouseScrollDelta != Vector2.zero)
                    {
                        this.massTreeRadius = Mathf.Clamp(this.massTreeRadius + UnityEngine.Input.mouseScrollDelta.y, 20f, 200f);
                    }
                    this.Sphere.GetComponent<MeshRenderer>().enabled = true;
                    this.Sphere.transform.position = LocalPlayer.Transform.position;
                    this.Sphere.transform.localScale = new Vector3(this.massTreeRadius * 2f, this.massTreeRadius * 2f, this.massTreeRadius * 2f);
                    this.DestroyTree = true;
                }
            }
            else
            {
                if (this.DestroyTree)
                {
                    
                    RaycastHit[] array = Physics.SphereCastAll(this.Sphere.transform.position, this.massTreeRadius, new Vector3(1f, 0f, 0f));
                    for (int i = 0; i < array.Length; i++)
                    {
                        RaycastHit raycastHit = array[i];

                        /* SphereTreeCut */
                        if (UCheatmenu.SphereTreeCut && raycastHit.collider.GetComponent<TreeHealth>() != null)
                        {
                            raycastHit.collider.gameObject.SendMessage("Explosion", 100f);
                        }
                        /* SphereTreeStumps */
                        else if (UCheatmenu.SphereTreeStumps && raycastHit.collider.GetComponent<ExplodeTreeStump>() != null)
                        {
                            raycastHit.collider.gameObject.SendMessage("LocalizedHit", new LocalizedHitData(raycastHit.collider.gameObject.transform.position, 1000f));
                        }
                        /* SphereBushes */
                        else if (UCheatmenu.SphereBushes && 
                            (raycastHit.collider.GetComponent<CutSappling>() != null || 
                            raycastHit.collider.GetComponent<CutBush>() != null ||
                            raycastHit.collider.GetComponent<CutBush2>() != null ||
                            raycastHit.collider.GetComponent<CutPlant>() != null ||
                            raycastHit.collider.GetComponent<CutStalactite>() != null ||
                            raycastHit.collider.GetComponent<CutTreeSmall>() != null
                            ))
                        {
                            raycastHit.collider.gameObject.SendMessage("CutDown");
                        }
                        /* SphereCrates */
                        else if (UCheatmenu.SphereCrates && (
                            raycastHit.collider.GetComponent<BreakWoodSimple>() != null ||
                            raycastHit.collider.GetComponent<BreakCrate>() != null
                            ))
                        {
                            raycastHit.collider.gameObject.SendMessage("Hit", 10000);
                        }
                        /* SphereSuitcases */
                        else if (UCheatmenu.SphereFires && raycastHit.collider.GetComponent<SuitCase>() != null)
                        {
                            raycastHit.collider.gameObject.SendMessage("Open");
                            raycastHit.collider.gameObject.SendMessage("Open_Perform");
                        }
                        /* LightFires */
                        else if (UCheatmenu.SphereFires && raycastHit.collider.GetComponent<FireStand>() != null)
                        {
                            if (BoltNetwork.isRunning)
                            {
                                FireLightEvent fireLightEvent = FireLightEvent.Create(GlobalTargets.OnlyServer);
                                fireLightEvent.Target = raycastHit.collider.GetComponent<FireStand>().entity;
                                fireLightEvent.Send();
                            }
                            else
                            {
                                raycastHit.collider.gameObject.SendMessage("LightFire");
                            }
                        }
                        else if (UCheatmenu.SphereFires && raycastHit.collider.GetComponent<Fire>() != null)
                        {
                            if (BoltNetwork.isRunning)
                            {
                                FireLightEvent fireLightEvent = FireLightEvent.Create(GlobalTargets.OnlyServer);
                                fireLightEvent.Target = raycastHit.collider.GetComponent<Fire>().entity;
                                fireLightEvent.Send();
                            }
                            else
                            {
                                raycastHit.collider.gameObject.SendMessage("LightFire");
                            }
                        }
                        else if (UCheatmenu.SphereFires && raycastHit.collider.GetComponent<Fire2>() != null)
                        {
                            if (BoltNetwork.isRunning)
                            {
                                FireLightEvent fireLightEvent = FireLightEvent.Create(GlobalTargets.OnlyServer);
                                fireLightEvent.Target = raycastHit.collider.GetComponent<Fire2>().entity;
                                fireLightEvent.Send();
                            }
                            else
                            {
                                raycastHit.collider.gameObject.SendMessage("Action_LightFire");
                            }
                        }
                        else if (UCheatmenu.SphereFires && raycastHit.collider.GetComponent<enableEffigy>() != null)
                        {
                            if (BoltNetwork.isRunning)
                            {
                                LightEffigy lightEffigy = LightEffigy.Create(GlobalTargets.OnlyServer);
                                lightEffigy.Effigy = raycastHit.collider.GetComponent<Fire2>().entity;
                                lightEffigy.Send();
                            }
                            else
                            {
                                raycastHit.collider.gameObject.SendMessage("lightEffigyReal");
                            }
                        }
                    }



                    this.DestroyTree = false;
                }
                this.Sphere.GetComponent<MeshRenderer>().enabled = false;
            }
            if (UCheatmenu.InstBuild)
            {
                //InstaBuild.exec();
            }
            if (UCheatmenu.FixBodyTemp)
            {
                LocalPlayer.Stats.BodyTemp = UCheatmenu.FixedBodyTemp;
            }
            if (UCheatmenu.FixBatteryCharge)
            {
                LocalPlayer.Stats.BatteryCharge = UCheatmenu.FixedBatteryCharge;
            }
            if (UCheatmenu.FixEnergy)
            {
                LocalPlayer.Stats.Energy = UCheatmenu.FixedEnergy;
            }
            if (UCheatmenu.FixHealth)
            {
                LocalPlayer.Stats.Health = UCheatmenu.FixedHealth;
            }
            if (UCheatmenu.FixStamina)
            {
                LocalPlayer.Stats.Stamina = UCheatmenu.FixedStamina;
            }
            if (UCheatmenu.FixFullness)
            {
                LocalPlayer.Stats.Fullness = UCheatmenu.FixedFullness;
            }
            if (UCheatmenu.FixStarvation)
            {
                LocalPlayer.Stats.Starvation = UCheatmenu.FixedStarvation;
            }
            if (UCheatmenu.FixThirst)
            {
                LocalPlayer.Stats.Thirst = UCheatmenu.FixedThirst;
            }
            if (ModAPI.Input.GetButtonDown("OpenMenu"))
            {
                if (this.firsttime)
                {
                    loadSettings();
                    this.firsttime = false;
                }

                if (this.visible)
                {
                    LocalPlayer.FpCharacter.UnLockView();
                }
                else
                {
                    LocalPlayer.FpCharacter.LockView(true);
                }
                this.visible = !this.visible;
            }
            if (ModAPI.Input.GetButtonDown("FreezeTime"))
            {
                UCheatmenu.FreezeTime = !UCheatmenu.FreezeTime;
            }
            if (ModAPI.Input.GetButtonDown("FreeCam"))
            {
                UCheatmenu.FreeCam = !UCheatmenu.FreeCam;
            }
            if (ModAPI.Input.GetButtonDown("Fly"))
            {
                UCheatmenu.FlyMode = !UCheatmenu.FlyMode;
            }
            if (ModAPI.Input.GetButtonDown("Noclip"))
            {
                UCheatmenu.NoClip = !UCheatmenu.NoClip;
            }
            if (ModAPI.Input.GetButtonDown("InstaBuild"))
            {
                if (!ChatBox.IsChatOpen)
                {
                    UCheatmenu.InstBuild = true;
                    InstaBuild.exec();
                    
                }
                
            }
            if (ModAPI.Input.GetButtonDown("InstaRepair"))
            {
                if (!ChatBox.IsChatOpen)
                {
                    InstaBuild.repairAll();
                }
            }

            

        }

        [ExecuteOnGameStart]
        public static void FindAnimals()
        {
            try {
                AnimalNames.Clear();
                AnimalPrefabs.Clear();

                AnimalSpawnZone[] animalSpawns = GameObject.FindObjectsOfType<AnimalSpawnZone>();
                foreach (AnimalSpawnZone aZone in animalSpawns)
                {
                    foreach (AnimalSpawnConfig config in aZone.Spawns)
                    {
                        string name = config.Prefab.name;
                        if (!AnimalNames.Contains(name))
                        {
                            AnimalNames.Add(name);
                            AnimalPrefabs.Add(name, config.Prefab);
                        }
                    }
                }
                AnimalNames.Sort();

            }
            catch (System.Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }

        [ExecuteOnGameStart]
        public static void FindProps()
        {
            try
            {
                PropNames.Clear();
                PropPrefabs.Clear();

                GreebleZone[] zones = (GreebleZone[])UnityEngine.Resources.FindObjectsOfTypeAll(typeof(GreebleZone));
                foreach (GreebleZone zone in zones)
                {
                    foreach (GreebleDefinition definition in zone.GreebleDefinitions)
                    {
                        string name = definition.Prefab.name;
                        if (!PropNames.Contains(name))
                        {
                            PropNames.Add(name);
                            PropPrefabs.Add(name, definition.Prefab);
                        }
                    }
                }

                PropNames.Sort();

            }
            catch (System.Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }

        [ExecuteOnGameStart]
        public static void FindBolts()
        {
            try
            {
                BoltNames.Clear();
                BoltPrefabsDict.Clear();

                Type type = typeof(BoltPrefabs);
                foreach (var p in type.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic))
                {
                    string name = p.Name;
                    if (!BoltNames.Contains(name))
                    {
                        BoltNames.Add(name);
                        BoltPrefabsDict.Add(name, (PrefabId)p.GetValue(null));
                    }
                }

                BoltNames.Sort();

            }
            catch (System.Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }

        private void _spawnmutant(string type)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<GameObject>("instantMutantSpawner"), LocalPlayer.Transform.position + new Vector3(0f, 1f, 2f), Quaternion.identity) as GameObject;
            switch (type)
            {
                case "male":
                    gameObject.GetComponent<spawnMutants>().amount_male = 1;
                    break;
                case "female":
                    gameObject.GetComponent<spawnMutants>().amount_female = 1;
                    break;
                case "female_skinny":
                    gameObject.GetComponent<spawnMutants>().amount_female_skinny = 1;
                    break;
                case "male_skinny":
                    gameObject.GetComponent<spawnMutants>().amount_male_skinny = 1;
                    break;
                case "pale":
                    gameObject.GetComponent<spawnMutants>().amount_pale = 1;
                    break;
                case "pale_skinny":
                    gameObject.GetComponent<spawnMutants>().amount_skinny_pale = 1;
                    break;
                case "fireman":
                    gameObject.GetComponent<spawnMutants>().amount_fireman = 1;
                    break;
                case "vags":
                    gameObject.GetComponent<spawnMutants>().amount_vags = 1;
                    break;
                case "armsy":
                    gameObject.GetComponent<spawnMutants>().amount_armsy = 1;
                    break;
                case "baby":
                    gameObject.GetComponent<spawnMutants>().amount_baby = 1;
                    break;
                case "fat":
                    gameObject.GetComponent<spawnMutants>().amount_fat = 1;
                    break;
                case "skinnedMale":
                    gameObject.GetComponent<spawnMutants>().amount_pale = 1;
                    gameObject.GetComponent<spawnMutants>().skinnedTribe = true;
                    gameObject.GetComponent<spawnMutants>().pale = true;
                    break;
            }
            if (Mutant_Skinned) gameObject.GetComponent<spawnMutants>().skinnedTribe = true;
            if (Mutant_Leader) gameObject.GetComponent<spawnMutants>().leader = true;
            if (Mutant_Painted) gameObject.GetComponent<spawnMutants>().paintedTribe = true;
            if (Mutant_Pale) gameObject.GetComponent<spawnMutants>().pale = true;
            if (Mutant_Old) gameObject.GetComponent<spawnMutants>().oldMutant = true;
            if (Mutant_BBaby) gameObject.GetComponent<spawnMutants>().bossBabySpawn = true;
            if (Mutant_Dynamite) gameObject.GetComponent<spawnMutants>().useDynamiteMan = true;
        }

        private void _spawnRegularFamily(Transform target)
        {
            if (BoltNetwork.isClient)
            {
                return;
            }
            if (target == null)
            {
                target = LocalPlayer.Transform;
            }
            GameObject gameObject = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<GameObject>("instantMutantSpawner"), target.position + new Vector3(0f, 1f, 2f), Quaternion.identity) as GameObject;
            spawnMutants component = gameObject.GetComponent<spawnMutants>();
            component.amount_armsy = 0;
            component.amount_baby = 0;
            component.amount_female = 1;
            component.amount_female_skinny = 0;
            component.amount_fireman = 0;
            component.amount_male = 4;
            component.amount_pale = 0;
            component.amount_male_skinny = 0;
            component.amount_vags = 0;
            component.amount_fat = 0;
            component.amount_armsy = 0;
            component.leader = true;
            component.pale = false;
        }

        private void _spawnPaintedFamily(Transform target)
        {
            if (BoltNetwork.isClient)
            {
                return;
            }
            if (target == null)
            {
                target = LocalPlayer.Transform;
            }
            GameObject gameObject = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<GameObject>("instantMutantSpawner"), target.position + new Vector3(0f, 1f, 2f), Quaternion.identity) as GameObject;
            spawnMutants component = gameObject.GetComponent<spawnMutants>();
            component.amount_armsy = 0;
            component.amount_baby = 0;
            component.amount_female = 1;
            component.amount_female_skinny = 0;
            component.amount_fireman = 0;
            component.amount_male = 4;
            component.amount_pale = 0;
            component.amount_male_skinny = 0;
            component.amount_vags = 0;
            component.amount_fat = 0;
            component.amount_armsy = 0;
            component.leader = true;
            component.paintedTribe = true;
            component.pale = false;
        }

        private void _spawnSkinnedFamily(Transform target)
        {
            if (BoltNetwork.isClient)
            {
                return;
            }
            if (target == null)
            {
                target = LocalPlayer.Transform;
            }
            GameObject gameObject = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<GameObject>("instantMutantSpawner"), target.position + new Vector3(0f, 1f, 2f), Quaternion.identity) as GameObject;
            spawnMutants component = gameObject.GetComponent<spawnMutants>();
            component.amount_armsy = 0;
            component.amount_baby = 0;
            component.amount_female = 0;
            component.amount_female_skinny = 0;
            component.amount_fireman = 0;
            component.amount_male = 0;
            component.amount_pale = 2;
            component.amount_skinny_pale = 3;
            component.amount_male_skinny = 0;
            component.amount_vags = 0;
            component.amount_fat = 0;
            component.amount_armsy = 0;
            component.leader = true;
            component.paintedTribe = false;
            component.pale = true;
            component.skinnedTribe = true;
        }

        private void _spawnSkinnyFamily(Transform target)
        {
            if (BoltNetwork.isClient)
            {
                return;
            }
            if (target == null)
            {
                target = LocalPlayer.Transform;
            }
            GameObject gameObject = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<GameObject>("instantMutantSpawner"), target.position + new Vector3(0f, 1f, 2f), Quaternion.identity) as GameObject;
            spawnMutants component = gameObject.GetComponent<spawnMutants>();
            component.amount_armsy = 0;
            component.amount_baby = 0;
            component.amount_female = 0;
            component.amount_female_skinny = 2;
            component.amount_fireman = 0;
            component.amount_male = 0;
            component.amount_pale = 0;
            component.amount_male_skinny = 3;
            component.amount_vags = 0;
            component.amount_fat = 0;
            component.amount_armsy = 0;
            component.leader = true;
            component.pale = false;
        }

        private void _additem(string nameOrId, int amount)
        {
            if (!string.IsNullOrEmpty(nameOrId))
            {
                Item item;
                int itemId;
                if (int.TryParse(nameOrId, out itemId))
                {
                    item = ItemDatabase.Items.FirstOrDefault((Item i) => i._id == itemId);
                }
                else
                {
                    string lowerName = nameOrId.ToLower();
                    item = ItemDatabase.Items.FirstOrDefault((Item i) => i._name.ToLower() == lowerName);
                }
                if (item != null)
                {
                    LocalPlayer.Inventory.AddItem(item._id, amount, false, false, null);
                }
                else
                {
                }
            }
        }

        private void _addAllItems()
        {
            Item[] items = ItemDatabase.Items;
            for (int i = 0; i < items.Length; i++)
            {
                Item item = items[i];
                try
                {
                    if (item._maxAmount >= 0 && !item.MatchType(Item.Types.Story) && LocalPlayer.Inventory.InventoryItemViewsCache.ContainsKey(item._id))
                    {
                        LocalPlayer.Inventory.AddItem(item._id, 100000, true, false, null);
                    }
                }
                catch (Exception e)
                {
                }
            }
        }

        private void _veganmode(string onoff)
        {
            if (onoff == "on")
            {
                Cheats.NoEnemies = true;
            }
            else if (onoff == "off")
            {
                Cheats.NoEnemies = false;
            }
            else
            {
            }
        }

        private void _loghack()
        {
            if (LocalPlayer.Inventory.Logs._infiniteLogHack)
            {
                //LocalPlayer.Inventory.Logs._infiniteLogHack = true;
                int id = ItemDatabase.ItemByName("Log")._id;
                if (!LocalPlayer.Inventory.Owns(id, true))
                {
                    LocalPlayer.Inventory.AddItem(id, 1, false, false, null);
                }
            }
            else
            {
                //LocalPlayer.Inventory.Logs._infiniteLogHack = false;
            }
        }

        private void _itemhack()
        {
            if (UCheatmenu.ItemHack)
            {
                LocalPlayer.Inventory.ItemFilter = new TheForest.Items.Inventory.InventoryItemFilter_Unlimited();
            }
            else
            {
                LocalPlayer.Inventory.ItemFilter = null;
            }
        }

        private void _cancelallghosts()
        {
            if (UCheatmenu.ARadiusGlobal)
            {
                Craft_Structure[] array = UnityEngine.Object.FindObjectsOfType<Craft_Structure>();
                if (array != null && array.Length > 0)
                {
                    Craft_Structure[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        Craft_Structure craft_Structure = array2[i];
                        craft_Structure.CancelBlueprint();
                    }
                }
            }
            else
            {
                RaycastHit[] array = Physics.SphereCastAll(TheForest.Utils.LocalPlayer.GameObject.transform.position, UCheatmenu.ARadius, new Vector3(1f, 0f, 0f));
                try
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        RaycastHit raycastHit = array[i];
                        if (raycastHit.collider.GetComponent<Craft_Structure>() != null)
                        {
                            raycastHit.collider.gameObject.SendMessage("CancelBlueprint");
                            raycastHit.collider.gameObject.SendMessage("CancelBlueprintSafe");
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        private void _buildhack()
        {
            if (UCheatmenu.BuildHack)
            {
                Cheats.Creative = true;
            }
            else
            {
                Cheats.Creative = false;
            }
        }

        private void _survival(string onoff)
        {
            if (onoff == "on")
            {
                Cheats.NoSurvival = false;
            }
            else if (onoff == "off")
            {
                Cheats.NoSurvival = true;
            }
            else
            {
            }
        }

        private void _killlocalplayer()
        {
            if (LocalPlayer.Stats)
            {
                LocalPlayer.Stats.Hit(1000, true, PlayerStats.DamageType.Physical);
            }
            else
            {
            }
        }

        private void _revivelocalplayer()
        {
            if (LocalPlayer.Stats)
            {
                if (LocalPlayer.Stats.Dead)
                {
                    Item item = ItemDatabase.ItemByName("meds");
                    TheForest.Items.Utils.ItemUtils.ApplyEffectsToStats(item._usedStatEffect, true, 1);
                    if (item._usedSFX != Item.SFXCommands.None)
                    {
                        LocalPlayer.Inventory.SendMessage(item._usedSFX.ToString());
                    }
                    LocalPlayer.Stats.HealedMp();
                }
                else
                {
                }
            }
            else
            {
            }
        }

        private void _setGameMode(string arg)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                string text = arg.ToLower();
                switch (text)
                {
                    case "standard":
                        GameSetup.SetGameType(TheForest.Commons.Enums.GameTypes.Standard);
                        goto IL_CA;
                    case "mod":
                        GameSetup.SetGameType(TheForest.Commons.Enums.GameTypes.Mod);
                        goto IL_CA;
                    case "creative":
                        GameSetup.SetGameType(TheForest.Commons.Enums.GameTypes.Creative);
                        goto IL_CA;
                }
            IL_CA:;
            }
            else
            {
            }
        }

        private void _setDifficultyMode(string arg)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                string text = arg.ToLower();
                switch (text)
                {
                    case "peaceful":
                        GameSetup.SetDifficulty(TheForest.Commons.Enums.DifficultyModes.Peaceful);
                        goto IL_CA;
                    case "normal":
                        GameSetup.SetDifficulty(TheForest.Commons.Enums.DifficultyModes.Normal);
                        goto IL_CA;
                    case "hard":
                        GameSetup.SetDifficulty(TheForest.Commons.Enums.DifficultyModes.Hard);
                        goto IL_CA;
                }
            IL_CA:;
            }
            else
            {
            }
        }

        private void _setPlayerVariation(string s)
        {
            int num;
            if (int.TryParse(s, out num))
            {
                CoopPlayerVariations component = LocalPlayer.GameObject.GetComponent<CoopPlayerVariations>();
                try
                {
                    component.SetVariation(LocalPlayer.Stats.PlayerVariation, LocalPlayer.Stats.PlayerVariationBody, LocalPlayer.Stats.PlayerVariationHair, LocalPlayer.Stats.PlayerVariationExtras, LocalPlayer.Stats.PlayerClothingVariation);
                    component.UpdateSkinVariation(LocalPlayer.Stats.IsBloody, LocalPlayer.Stats.IsMuddy, LocalPlayer.Stats.IsRed, LocalPlayer.Stats.IsCold);
                    LocalPlayer.Stats.PlayerVariation = num;
                    if (BoltNetwork.isRunning)
                    {
                        IPlayerState state = LocalPlayer.Entity.GetState<IPlayerState>();
                        state.PlayerVariation = LocalPlayer.Stats.PlayerVariation;
                        state.PlayerVariationBody = LocalPlayer.Stats.PlayerVariationBody;
                    }
                }
                catch
                {
                }
            }
            else
            {
            }
        }

        private void _setPlayerBody(string s)
        {
            int num;
            if (int.TryParse(s, out num))
            {
                CoopPlayerVariations component = LocalPlayer.GameObject.GetComponent<CoopPlayerVariations>();
                try
                {
                    component.SetVariation(LocalPlayer.Stats.PlayerVariation, num, LocalPlayer.Stats.PlayerVariationHair, LocalPlayer.Stats.PlayerVariationExtras, LocalPlayer.Stats.PlayerClothingVariation);
                    component.UpdateSkinVariation(LocalPlayer.Stats.IsBloody, LocalPlayer.Stats.IsMuddy, LocalPlayer.Stats.IsRed, LocalPlayer.Stats.IsCold);
                    LocalPlayer.Stats.PlayerVariationBody = num;
                    if (BoltNetwork.isRunning)
                    {
                        IPlayerState state = LocalPlayer.Entity.GetState<IPlayerState>();
                        state.PlayerVariation = LocalPlayer.Stats.PlayerVariation;
                        state.PlayerVariationBody = LocalPlayer.Stats.PlayerVariationBody;
                    }
                }
                catch
                {
                }
            }
            else
            {
            }
        }

        private void _setVariationExtra(string s)
        {
            try
            {
                PlayerCloting playerCloting = (PlayerCloting)((int)Enum.Parse(typeof(PlayerCloting), s));
                CoopPlayerVariations component = LocalPlayer.GameObject.GetComponent<CoopPlayerVariations>();
                try
                {
                    component.SetVariation(LocalPlayer.Stats.PlayerVariation, LocalPlayer.Stats.PlayerVariationBody, LocalPlayer.Stats.PlayerVariationHair, playerCloting, LocalPlayer.Stats.PlayerClothingVariation);
                    component.UpdateSkinVariation(LocalPlayer.Stats.IsBloody, LocalPlayer.Stats.IsMuddy, LocalPlayer.Stats.IsRed, LocalPlayer.Stats.IsCold);
                    LocalPlayer.Stats.PlayerVariationExtras = playerCloting;
                    if (BoltNetwork.isRunning)
                    {
                        IPlayerState state = LocalPlayer.Entity.GetState<IPlayerState>();
                        state.PlayerVariation = LocalPlayer.Stats.PlayerVariation;
                        state.PlayerVariationBody = LocalPlayer.Stats.PlayerVariationBody;
                        state.PlayerClothing = (int)LocalPlayer.Stats.PlayerVariationExtras;
                        state.PlayerClothingVariation = LocalPlayer.Stats.PlayerClothingVariation;
                    }
                }
                catch
                {
                }
            }
            catch
            {
            }
        }

        private void GotoCave(bool inCave)
        {
            if (inCave && !LocalPlayer.IsInCaves)
            {
                LocalPlayer.GameObject.SendMessage("InACave");
            }
            else if (!inCave && LocalPlayer.IsInCaves)
            {
                LocalPlayer.GameObject.SendMessage("NotInACave");
            }
        }

        private void _killclosestenemy()
        {
            List<GameObject> list = new List<GameObject>(Scene.MutantControler.activeCannibals);
            foreach (GameObject current in Scene.MutantControler.activeInstantSpawnedCannibals)
            {
                if (!list.Contains(current))
                {
                    list.Add(current);
                }
            }
            list.RemoveAll((GameObject o) => o == null);
            list.RemoveAll((GameObject o) => o != o.activeSelf);
            if (list.Count > 0)
            {
                list.Sort((GameObject c1, GameObject c2) => Vector3.Distance(LocalPlayer.Transform.position, c1.transform.position).CompareTo(Vector3.Distance(LocalPlayer.Transform.position, c2.transform.position)));
                list[0].SendMessage("killThisEnemy", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
            }
        }

        private void _killallenemies()
        {
            if (BoltNetwork.isClient)
            {
                return;
            }
            List<GameObject> list = new List<GameObject>(Scene.MutantControler.activeCannibals);
            foreach (GameObject current in Scene.MutantControler.activeInstantSpawnedCannibals)
            {
                if (!list.Contains(current))
                {
                    list.Add(current);
                }
            }
            list.RemoveAll((GameObject o) => o == null);
            list.RemoveAll((GameObject o) => o != o.activeSelf);
            if (list.Count > 0)
            {
                for(var i = 0; i < list.Count; i++)
                    list[i].SendMessage("killThisEnemy", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
            }
        }

        private void _knockDownclosestenemy()
        {
            List<GameObject> list = new List<GameObject>(Scene.MutantControler.activeCannibals);
            foreach (GameObject current in Scene.MutantControler.activeInstantSpawnedCannibals)
            {
                if (!list.Contains(current))
                {
                    list.Add(current);
                }
            }
            list.RemoveAll((GameObject o) => o == null);
            list.RemoveAll((GameObject o) => o != o.activeSelf);
            if (list.Count > 0)
            {
                list.Sort((GameObject c1, GameObject c2) => Vector3.Distance(LocalPlayer.Transform.position, c1.transform.position).CompareTo(Vector3.Distance(LocalPlayer.Transform.position, c2.transform.position)));
                list[0].SendMessage("knockDownThisEnemy", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
            }
        }

        private void _killclosestanimal()
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < PoolManager.Pools["creatures"].Count; i++)
            {
                GameObject gameObject = PoolManager.Pools["creatures"][i].gameObject;
                if (gameObject.activeSelf)
                {
                    list.Add(gameObject);
                }
            }
            list.RemoveAll((GameObject o) => o == null);
            if (list.Count > 0)
            {
                list.Sort((GameObject c1, GameObject c2) => Vector3.Distance(LocalPlayer.Transform.position, c1.transform.position).CompareTo(Vector3.Distance(LocalPlayer.Transform.position, c2.transform.position)));
                list[0].SendMessage("HitReal", 1000, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
            }
        }

        private void _killallanimals()
        {
            animalHealth[] array = UnityEngine.Object.FindObjectsOfType<animalHealth>();
            animalHealth[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                animalHealth animalHealth = array2[i];
                if (animalHealth.gameObject.activeInHierarchy)
                {
                    animalHealth.SendMessage("Die");
                }
            }
        }

        private void _killEndBoss()
        {
            GameObject gameObject = GameObject.Find("girl_base");
            if (gameObject)
            {
                gameObject.transform.parent.SendMessage("killThisEnemy", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
            }
        }

        private void _enemies()
        {
            if (UCheatmenu.MutantToggle)
            {
                Scene.MutantControler.enabled = true;
                Scene.MutantControler.startSetupFamilies();
            }
            else
            {
                Scene.MutantControler.StartCoroutine("removeAllEnemies");
                base.Invoke("disableMutantController", 0.5f);
            }

        }

        private void _invisible()
        {
            if (UCheatmenu.InvisibleToggle)
            {
                LocalPlayer.GameObject.layer = 31;
            }
            else
            {
                LocalPlayer.GameObject.layer = 18;
            }
        }

        private void _animals()
        {
            if (!this.animalController)
            {
                this.animalController = GameObject.Find("CoopAnimalSpawner");
            }
            if (UCheatmenu.AnimalToggle)
            {
                
                this.animalController.SetActive(true);
            }
            else
            {
                this.animalController.SetActive(false);
                PoolManager.Pools["creatures"].DespawnAll();
            }
        }

        private void _birds()
        {
            GameObject gameObject = GameObject.Find("_livingBirdsController");
            if (gameObject)
            {
                if (UCheatmenu.BirdToggle)
                {
                    gameObject.GetComponent<lb_BirdController>().enabled = true;
                }
                else
                {
                    gameObject.SendMessage("despawnAll");
                    gameObject.GetComponent<lb_BirdController>().enabled = false;
                }
            }
        }

        private void _setCurrentDay(string num)
        {
            int num2 = int.Parse(num);
            if (BoltNetwork.isClient)
            {
                return;
            }
            if (num2 > 0)
            {
                Clock.Day = num2;
            }
        }

        private void _plantallgardens()
        {
            Garden[] array = UnityEngine.Object.FindObjectsOfType<Garden>();
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i].Test = true;
                    for (int j = 0; j < array[i].GrowSpots.Length; j++)
                    {
                        array[i].PlantSeed();
                    }
                    array[i].Test = false;
                }
            }
            else
            {
            }
        } 

        private void _growalldirtpiles()
        {
            TheForest.Buildings.World.GardenDirtPile[] array = UnityEngine.Object.FindObjectsOfType<TheForest.Buildings.World.GardenDirtPile>();
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i].Growth();
                }
            }
            else
            {
            }
        }

        private void _cutdowntrees(string arg)
        {
            if (!BoltNetwork.isClient)
            {
                CoopTreeId[] array2 = UnityEngine.Object.FindObjectsOfType<CoopTreeId>();
                List<CoopTreeId> list3 = (from t in array2
                                          where t.lod && !t.lod.enabled && t.lod.CurrentView == null
                                          select t).ToList<CoopTreeId>();
                int count = list3.Count;
                int num2;
                if (arg.EndsWith("%"))
                {
                    arg = arg.TrimEnd(new char[]
                    {
                        '%'
                    });
                    int num;
                    if (!int.TryParse(arg, out num))
                    {
                        return;
                    }
                    num2 = (int)((float)num / 100f * (float)array2.Length);
                }
                else
                {
                    int num3;
                    if (!int.TryParse(arg, out num3))
                    {
                        return;
                    }
                    num2 = num3;
                }
                TreeLodGrid treeLodGrid = UnityEngine.Object.FindObjectOfType<TreeLodGrid>();
                if (num2 > 0)
                {
                    List<CoopTreeId> list4 = (from t in array2
                                              where t.lod != null && t.lod.enabled
                                              select t).ToList<CoopTreeId>();
                    int count2 = list4.Count;
                    int num4 = num2 - Mathf.Max(num2 - (array2.Length - count), 0);
                    float num5 = Mathf.Max((float)count2 / (float)num4, 1f);
                    int num6 = 0;
                    for (float num7 = 0f; num7 < (float)count2; num7 += num5)
                    {
                        int index = (int)num7;
                        if (list4[index])
                        {
                            if (BoltNetwork.isRunning)
                            {
                                list4[index].SendMessage("OnDestroyCallback");
                                list4[index].lod.DontSpawn = true;
                                list4[index].entity.Freeze(false);
                            }
                            foreach (Transform transform in list4[index].transform)
                            {
                                LOD_Stump component = transform.GetComponent<LOD_Stump>();
                                if (component)
                                {
                                    component.DespawnCurrent();
                                    component.CurrentView = null;
                                }
                                UnityEngine.Object.Destroy(transform.gameObject);
                            }
                            list4[index].lod.DespawnCurrent();
                            list4[index].lod.enabled = false;
                            list4[index].lod.CurrentView = null;
                            list4[index].lod.SpawnStumpLod();
                            if (treeLodGrid)
                            {
                                treeLodGrid.RegisterCutDownTree(list4[index].transform.position);
                            }
                            num6++;
                        }
                    }
                    if (num6 != 0 && BoltNetwork.isRunning)
                    {
                        CoopTreeGrid.SweepGrid();
                    }
                }
                else if (num2 < 0)
                {
                    num2 = Mathf.Abs(num2);
                    int num8 = num2 - Mathf.Max(num2 - count, 0);
                    float num9 = Mathf.Max((float)count / (float)num8, 1f);
                    int num10 = 0;
                    for (float num11 = 0f; num11 < (float)count; num11 += num9)
                    {
                        int index2 = (int)num11;
                        if (BoltNetwork.isRunning)
                        {
                            CoopTreeId coopTreeId = list3[index2];
                            if (coopTreeId)
                            {
                                coopTreeId.RegrowTree();
                            }
                            list3[index2].lod.DontSpawn = false;
                        }
                        list3[index2].lod.enabled = true;
                        list3[index2].lod.RefreshLODs();
                        if (treeLodGrid)
                        {
                            treeLodGrid.RegisterTreeRegrowth(list3[index2].transform.position);
                        }
                        foreach (Transform transform2 in list3[index2].transform)
                        {
                            LOD_Stump component2 = transform2.GetComponent<LOD_Stump>();
                            if (component2)
                            {
                                component2.DespawnCurrent();
                                component2.CurrentView = null;
                            }
                            UnityEngine.Object.Destroy(transform2.gameObject);
                        }
                        num10++;
                    }
                    if (num10 != 0 && BoltNetwork.isRunning)
                    {
                        CoopTreeGrid.SweepGrid();
                    }

                }
            }
            else
            {
            }
        }

        private void _cutgrass(string radiusArg)
        {
            if (string.IsNullOrEmpty(radiusArg))
            {
                radiusArg = "1";
            }
            int num = int.Parse(radiusArg);
            NeoGrassCutter.Cut(LocalPlayer.Transform.position, (float)num, true);
        }

        private void _growgrass(string radiusArg)
        {
            if (string.IsNullOrEmpty(radiusArg))
            {
                radiusArg = "1";
            }
            int num = int.Parse(radiusArg);
           // GrassCutterOv.GrowEdit(LocalPlayer.Transform.position, (float)num);
        }


        private void writeIni(string path)
        {
            INIHelper iniw = new INIHelper(path);
            iniw.Write("UCM", "GodMode",            UCheatmenu.GodMode.ToString());
            iniw.Write("UCM", "UnlimitedFuel",      UCheatmenu.UnlimitedFuel.ToString());
            iniw.Write("UCM", "Fog",                UCheatmenu.Fog.ToString());
            iniw.Write("UCM", "SpeedMultiplier",    UCheatmenu.SpeedMultiplier.ToString());
            iniw.Write("UCM", "JumpMultiplier",     UCheatmenu.JumpMultiplier.ToString());
            iniw.Write("UCM", "FlyMode",            UCheatmenu.FlyMode.ToString());
            iniw.Write("UCM", "NoClip",             UCheatmenu.NoClip.ToString());
            iniw.Write("UCM", "TimeSpeed",          UCheatmenu.TimeSpeed.ToString());
            iniw.Write("UCM", "InstantTree",        UCheatmenu.InstantTree.ToString());
            iniw.Write("UCM", "InstantBuild",       UCheatmenu.InstantBuild.ToString());
            iniw.Write("UCM", "CaveLight",          UCheatmenu.CaveLight.ToString());
            iniw.Write("UCM", "NightLight",         UCheatmenu.NightLight.ToString());
            iniw.Write("UCM", "ForceWeather",       UCheatmenu.ForceWeather.ToString());
            iniw.Write("UCM", "FreezeWeather",      UCheatmenu.FreezeWeather.ToString());
            iniw.Write("UCM", "AutoBuild",          UCheatmenu.AutoBuild.ToString());
            iniw.Write("UCM", "InstaKill",          UCheatmenu.InstaKill.ToString());
            iniw.Write("UCM", "Rebreather",         UCheatmenu.Rebreather.ToString());
            iniw.Write("UCM", "FixHealth",          UCheatmenu.FixHealth.ToString());
            iniw.Write("UCM", "FixBatteryCharge",   UCheatmenu.FixBatteryCharge.ToString());
            iniw.Write("UCM", "FixFullness",        UCheatmenu.FixFullness.ToString());
            iniw.Write("UCM", "FixStamina",         UCheatmenu.FixStamina.ToString());
            iniw.Write("UCM", "FixEnergy",          UCheatmenu.FixEnergy.ToString());
            iniw.Write("UCM", "FixThirst",          UCheatmenu.FixThirst.ToString());
            iniw.Write("UCM", "FixStarvation",      UCheatmenu.FixStarvation.ToString());
            iniw.Write("UCM", "FixBodyTemp",        UCheatmenu.FixBodyTemp.ToString());
            iniw.Write("UCM", "FixedHealth",        UCheatmenu.FixedHealth.ToString());
            iniw.Write("UCM", "FixedBatteryCharge", UCheatmenu.FixedBatteryCharge.ToString());
            iniw.Write("UCM", "FixedFullness",      UCheatmenu.FixedFullness.ToString());
            iniw.Write("UCM", "FixedStamina",       UCheatmenu.FixedStamina.ToString());
            iniw.Write("UCM", "FixedEnergy",        UCheatmenu.FixedEnergy.ToString());
            iniw.Write("UCM", "FixedThirst",        UCheatmenu.FixedThirst.ToString());
            iniw.Write("UCM", "FixedStarvation",    UCheatmenu.FixedStarvation.ToString());
            iniw.Write("UCM", "FixedBodyTemp",      UCheatmenu.FixedBodyTemp.ToString());
            iniw.Write("UCM", "DestroyBuildings",   UCheatmenu.DestroyBuildings.ToString());
            iniw.Write("UCM", "ARadius",            UCheatmenu.ARadius.ToString());
            iniw.Write("UCM", "ARadiusGlobal",      UCheatmenu.ARadiusGlobal.ToString());
            iniw.Write("UCM", "DestroyTree",        this.DestroyTree.ToString());
            iniw.Write("UCM", "Teleport",           this.Teleport.ToString());
            iniw.Write("UCM", "tItem",              this.tItem.ToString());
            iniw.Write("UCM", "setNumVar",          this.setNumVar.ToString());
            iniw.Write("UCM", "setCurDay",          this.setCurDay.ToString());
            iniw.Write("UCM", "setCutTrees",        this.setCutTrees.ToString());
            iniw.Write("UCM", "setCGrowGrass",      this.setCGrowGrass.ToString());
            iniw.Write("UCM", "RotationSpeed",      TheForestAtmosphere.Instance.RotationSpeed.ToString());
            iniw.Write("UCM", "SphereTreeCut",      UCheatmenu.SphereTreeCut.ToString());
            iniw.Write("UCM", "SphereTreeStumps",   UCheatmenu.SphereTreeStumps.ToString());
            iniw.Write("UCM", "SphereBushes",       UCheatmenu.SphereBushes.ToString());
            iniw.Write("UCM", "SphereCrates",       UCheatmenu.SphereCrates.ToString());
            iniw.Write("UCM", "SphereSuitcases",    UCheatmenu.SphereSuitcases.ToString());
            iniw.Write("UCM", "SphereFires",        UCheatmenu.SphereFires.ToString());
            iniw.Write("UCM", "InfFire",            UCheatmenu.InfFire.ToString());
            iniw.Write("UCM", "FastFlint",          UCheatmenu.FastFlint.ToString());
        }
        private void readIni(string path)
        {
            INIHelper inir = new INIHelper(path);
            UCheatmenu.GodMode = Convert.ToBoolean(inir.Read("UCM", "GodMode"));
            UCheatmenu.UnlimitedFuel = Convert.ToBoolean(inir.Read("UCM", "UnlimitedFuel"));
            UCheatmenu.Fog = Convert.ToBoolean(inir.Read("UCM", "Fog"));
            UCheatmenu.SpeedMultiplier = Convert.ToSingle(inir.Read("UCM", "SpeedMultiplier"));
            UCheatmenu.JumpMultiplier = Convert.ToSingle(inir.Read("UCM", "JumpMultiplier"));
            UCheatmenu.FlyMode = Convert.ToBoolean(inir.Read("UCM", "FlyMode"));
            UCheatmenu.NoClip = Convert.ToBoolean(inir.Read("UCM", "NoClip"));
            UCheatmenu.TimeSpeed = Convert.ToSingle(inir.Read("UCM", "TimeSpeed"));
            UCheatmenu.InstantTree = Convert.ToBoolean(inir.Read("UCM", "InstantTree"));
            UCheatmenu.InstantBuild = Convert.ToBoolean(inir.Read("UCM", "InstantBuild"));
            UCheatmenu.CaveLight = Convert.ToSingle(inir.Read("UCM", "CaveLight"));
            UCheatmenu.ForceWeather = Convert.ToInt32(inir.Read("UCM", "ForceWeather"));
            UCheatmenu.FreezeWeather = Convert.ToBoolean(inir.Read("UCM", "FreezeWeather"));
            UCheatmenu.AutoBuild = Convert.ToBoolean(inir.Read("UCM", "AutoBuild"));
            UCheatmenu.InstaKill = Convert.ToBoolean(inir.Read("UCM", "InstaKill"));
            UCheatmenu.Rebreather = Convert.ToBoolean(inir.Read("UCM", "Rebreather"));
            UCheatmenu.FixHealth = Convert.ToBoolean(inir.Read("UCM", "FixHealth"));
            UCheatmenu.FixBatteryCharge = Convert.ToBoolean(inir.Read("UCM", "FixBatteryCharge"));
            UCheatmenu.FixFullness = Convert.ToBoolean(inir.Read("UCM", "FixFullness"));
            UCheatmenu.FixStamina = Convert.ToBoolean(inir.Read("UCM", "FixStamina"));
            UCheatmenu.FixEnergy = Convert.ToBoolean(inir.Read("UCM", "FixEnergy"));
            UCheatmenu.FixThirst = Convert.ToBoolean(inir.Read("UCM", "FixThirst"));
            UCheatmenu.FixStarvation = Convert.ToBoolean(inir.Read("UCM", "FixStarvation"));
            UCheatmenu.FixBodyTemp = Convert.ToBoolean(inir.Read("UCM", "FixBodyTemp"));
            UCheatmenu.FixedHealth = Convert.ToSingle(inir.Read("UCM", "FixedHealth"));
            UCheatmenu.FixedBatteryCharge = Convert.ToSingle(inir.Read("UCM", "FixedBatteryCharge"));
            UCheatmenu.FixedFullness = Convert.ToSingle(inir.Read("UCM", "FixedFullness"));
            UCheatmenu.FixedStamina = Convert.ToSingle(inir.Read("UCM", "FixedStamina"));
            UCheatmenu.FixedEnergy = Convert.ToSingle(inir.Read("UCM", "FixedEnergy"));
            UCheatmenu.FixedThirst = Convert.ToSingle(inir.Read("UCM", "FixedThirst"));
            UCheatmenu.FixedStarvation = Convert.ToSingle(inir.Read("UCM", "FixedStarvation"));
            UCheatmenu.FixedBodyTemp = Convert.ToSingle(inir.Read("UCM", "FixedBodyTemp"));
            try{ UCheatmenu.DestroyBuildings = Convert.ToBoolean(inir.Read("UCM", "DestroyBuildings")); }
            catch{ UCheatmenu.DestroyBuildings = false; }
            try{ UCheatmenu.ARadius = Convert.ToSingle(inir.Read("UCM", "ARadius")); }
            catch{ UCheatmenu.ARadius = 10f; }
            try { UCheatmenu.ARadiusGlobal = Convert.ToBoolean(inir.Read("UCM", "ARadiusGlobal")); }
            catch { UCheatmenu.ARadiusGlobal = false; }
            try { UCheatmenu.NightLight = Convert.ToSingle(inir.Read("UCM", "NightLight")); }
            catch { UCheatmenu.NightLight = 1f; }
            try { UCheatmenu.SphereTreeCut = Convert.ToBoolean(inir.Read("UCM", "SphereTreeCut")); }
            catch{ UCheatmenu.SphereTreeCut = false; }
            try { UCheatmenu.SphereTreeStumps = Convert.ToBoolean(inir.Read("UCM", "SphereTreeStumps")); }
            catch { UCheatmenu.SphereTreeStumps = false; }
            try { UCheatmenu.SphereBushes = Convert.ToBoolean(inir.Read("UCM", "SphereBushes")); }
            catch { UCheatmenu.SphereBushes = false; }
            try { UCheatmenu.SphereCrates = Convert.ToBoolean(inir.Read("UCM", "SphereCrates")); }
            catch { UCheatmenu.SphereCrates = false; }
            try { UCheatmenu.SphereSuitcases = Convert.ToBoolean(inir.Read("UCM", "SphereSuitcases")); }
            catch { UCheatmenu.SphereSuitcases = false; }
            try { UCheatmenu.SphereFires = Convert.ToBoolean(inir.Read("UCM", "SphereFires")); }
            catch { UCheatmenu.SphereFires = false; }
            try { UCheatmenu.InfFire = Convert.ToBoolean(inir.Read("UCM", "InfFire")); }
            catch { UCheatmenu.InfFire = false; }
            try { UCheatmenu.FastFlint = Convert.ToBoolean(inir.Read("UCM", "FastFlint")); }
            catch { UCheatmenu.FastFlint = false; }


            this.DestroyTree = Convert.ToBoolean(inir.Read("UCM", "DestroyTree"));
            this.Teleport = inir.Read("UCM", "Teleport");
            this.tItem = inir.Read("UCM", "tItem");
            this.setNumVar = inir.Read("UCM", "setNumVar");
            this.setCurDay = inir.Read("UCM", "setCurDay");
            this.setCutTrees = inir.Read("UCM", "setCutTrees");
            this.setCGrowGrass = inir.Read("UCM", "setCGrowGrass");
            TheForestAtmosphere.Instance.RotationSpeed = Convert.ToSingle(inir.Read("UCM", "RotationSpeed"));
        }

        private void saveSettings()
        {
            writeIni("Mods/UCM.settings");
        }

        private void initSettings()
        {
            //if (!File.Exists("Mods/UCM.default.settings"))
            //{
                writeIni("Mods/UCM.default.settings");
            //}
        }
        private void loadSettings()
        {
            if (File.Exists("Mods/UCM.settings"))
            {
                readIni("Mods/UCM.settings");
            }
        }

        private void resetSettings()
        {
            readIni("Mods/UCM.default.settings");
        }


    }
}
