extern alias ACS; /* Assembly-CSharp: Aliase: global,ACS */

using ModAPI;
using ModAPI.Attributes;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
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
using TheForest.Items.Special;
using TheForest.Buildings.Creation;
using TheForest.Buildings.World;
using TheForest.World;
using TheForest.UI.Multiplayer;
using TheForest.Networking;
using Bolt;
using Steamworks;
using UnityEngine;
using PathologicalGames;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using TheForest.Items.Craft;
using TheForest.Items.Inventory;
using TheForest.Player.Clothing;
using Object = UnityEngine.Object;

namespace UltimateCheatmenu
{
    public class UCheatmenu : MonoBehaviour
    {
        public static bool visible;
        protected bool firsttime = true;
        private static bool firsttimeLanguageDownload = true;

        public string[] LanguageLocaleOptions = new string[] { "English", "German", "Polish", "Dutch", "Portuguese", "Spanish", "S. Chinese", "Korean", "Japanese" };
        public static string[] LanguageCodesOptions = new string[] { "en", "de", "pl", "nl", "pt", "es", "zh-CN", "ko", "ja" };

        public static bool GodMode = false;
        public static bool UnlimitedFuel = false;
        public static bool UnlimitedHairspray = false;
        public static bool InfFire = false;
        public static bool InstLighter = false;
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
        public static bool SphereTraps = false;
        public static bool SphereCrane = false;
        public static bool SphereHolders = false;
        public static int SphereHoldersType = 0;
        public static int LanguageLocale = 0;
        public static int CrosshairType = 0;

        public string[] SphereHolderOptions = new string[] { "Log", "Rock", "Stick" };
        public string[] CrosshairOptions = new string[] { "Cross"};

        public static bool SpawnLookAt = false;
        public static bool EnableGarden = false;
        public static bool AutoBuild = false;
        public static bool SleepTimer = false;
        public static float ARadius = 10f; 
        public static bool ARadiusGlobal = false;
        protected float a = 2f;

        protected GUIStyle labelStyle;

        protected static bool Already = false;
        protected int Tab;
        public static bool InstaKill = false;
        protected static bool FixHealth = false;
        protected static bool FixBatteryCharge = false;
        protected static bool FixFullness = false;
        protected static bool FixStamina = false;
        protected static bool FixEnergy = false;
        protected static bool FixThirst = false;
        protected static bool FixStarvation = false;
        protected static bool FixBodyTemp = false;
        protected static bool FixSanity = false;
        protected static bool FixWeight = false;
        protected static bool FixStrength = false;
        protected static float FixedHealth = -1f;
        protected static float FixedBatteryCharge = -1f;
        protected static float FixedFullness = -1f;
        protected static float FixedStamina = -1f;
        protected static float FixedEnergy = -1f;
        protected static float FixedThirst = -1f;
        protected static float FixedStarvation = -1f;
        protected static float FixedBodyTemp = -1f;
        protected static float FixedSanity = -1f;
        protected static float FixedWeight = -1f;
        protected static float FixedStrength = -1f;
        public static float ExplosionRadius = 15f;

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
        public string playclothinglist = "";
        public string setCGrowGrass = "";

        public Vector2 scrollPosition = Vector2.zero;
        public Vector2 scrollPosition2 = Vector2.zero;

        private GameObject animalController;

        private float scroller;
        private float scroller2;

        public Color CurrentFogColor;

        public static bool DestroyBuildings = false;
        public static bool ItemHack = false;
        public static bool InfiniteSpawnedLogs = false;
        public static bool BuildHack = false;
        public static bool InvisibleToggle = false;
        public static bool AnimalToggle = true;
        public static bool BirdToggle = true;
        public static bool MutantToggle = true;
        public static bool VeganmodeToggle = false;
        public static bool SurvivalToggle = false;
        public static bool NoUnderwaterBlur = false;

        public static bool EnableEffigies = false;
        public static bool EnableEndgame = false;

        public static bool TimeToggle = false;

        public static Dictionary<string, GameObject> AnimalPrefabs = new Dictionary<string, GameObject>();
        public static List<string> AnimalNames = new List<string>();

        public static Dictionary<string, GameObject> PropPrefabs = new Dictionary<string, GameObject>();
        public static List<string> PropNames = new List<string>();

        public static Dictionary<string, PrefabId> BoltPrefabsPre = new Dictionary<string, PrefabId>();
        public static Dictionary<string, PrefabId> BoltPrefabsDict = new Dictionary<string, PrefabId>();
        public static List<string> BoltNames = new List<string>();

        public static Dictionary<string, PrefabId> PrefabDict = new Dictionary<string, PrefabId>();
        public static List<string> PrefabNames = new List<string>();

        public static List<GameObject> AllObjects = new List<GameObject>();
        public static List<Prefabs> AllResources = new List<Prefabs>();
        public static bool toggleobj = false;
        public static bool togglepref = false;
        public static bool togglekinetic = false;
        public static bool initobjects = false;

        protected static bool Mutant_Skinned = false;
        protected static bool Mutant_Leader = false;
        protected static bool Mutant_Painted = false;
        protected static bool Mutant_Pale = false;
        protected static bool Mutant_Old = false;
        protected static bool Mutant_BBaby = false;
        protected static bool Mutant_Dynamite = false;

        private Transform pooled;

        public static bool instgrow = false;
        public static float instgrowspeed = 1;
        public static int seedtype = -1;

        public static string lastObject = "";
        public static GameObject lastObjectGObject = null;
        public static string lastObjectType = "";
        public static PrefabId lastObjectPrefab;

        public static bool TorchToggle = false;
        public static int TorchR = 1;
        public static int TorchG = 1;
        public static int TorchB = 1;
        public static float TorchI = 1.0f;

        public static bool CrosshairToggle = false;
        public static int CrosshairR = 0;
        public static int CrosshairG = 255;
        public static int CrosshairB = 0;
        public static float CrosshairI = 0.7f;
        public static float CrosshairSize = 10;

        public static float WaterLevel = 41.5f;

        public static bool UnlimitedUpgrade = false;

        public static bool BuildingCollision = true;
        public static bool ItemConsume = false;

        public static int gstats;

        private GUIStyle crossStyle = null;

        public static string[] list = { "Drop_Down_Menu" };
        private int wepBonus;

        public static Localization ln = new Localization();


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
            if (BoltNetwork.isRunning && TheForest.Utils.Scene.SceneTracker != null && TheForest.Utils.Scene.SceneTracker.allPlayerEntities != null)
            {
                // Refresh players
                PlayerManager.Players.Clear();
                PlayerManager.Players.AddRange(TheForest.Utils.Scene.SceneTracker.allPlayerEntities
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

            if (UCheatmenu.CrosshairToggle)
            {
                if (LocalPlayer.Inventory._inventoryGO.tag == "closed" && !LocalPlayer.Animator.GetBool("bookHeld"))
                {

                    if (crossStyle == null)
                    {
                        Texture2D t = new Texture2D(1, 1);
                        t.SetPixel(0, 0, new Color(CrosshairR, CrosshairG, CrosshairB, CrosshairI));
                        t.Apply();

                        crossStyle = new GUIStyle();
                        crossStyle.normal.background = t;
                    }

                    switch (CrosshairType)
                    {
                        case 0:
                        default:
                            int space = 3;
                            // top
                            UnityEngine.GUI.Box(new Rect(
                                    (Screen.width / 2) - 1,
                                    (Screen.height / 2) - CrosshairSize - space,
                                    2,
                                    CrosshairSize)
                                , "", crossStyle);
                            // bottom
                            UnityEngine.GUI.Box(new Rect(
                                    (Screen.width / 2) - 1,
                                    (Screen.height / 2) + space,
                                    2,
                                    CrosshairSize)
                                , "", crossStyle);
                            // left
                            UnityEngine.GUI.Box(new Rect(
                                    (Screen.width / 2) - CrosshairSize - space,
                                    (Screen.height / 2) - 1,
                                    CrosshairSize,
                                    2)
                                , "", crossStyle);
                            // right
                            UnityEngine.GUI.Box(new Rect(
                                    (Screen.width / 2) + space,
                                    (Screen.height / 2) - 1,
                                    CrosshairSize,
                                    2)
                                , "", crossStyle);
                            break;
                    }
                }
            }

            if (UCheatmenu.visible)
            {
                string posX = LocalPlayer.GameObject.transform.position.x.ToString();
                string posY = LocalPlayer.GameObject.transform.position.y.ToString();
                string posZ = LocalPlayer.GameObject.transform.position.z.ToString();
                UnityEngine.GUI.Label(new Rect(10, 620, 300f, 20f), "("+posX + ", " + posY + ", " + posZ + ")", labelStylePos);

                UnityEngine.GUI.skin = ModAPI.Gui.Skin;
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
                    new GUIContent(ln.GetText("Tab.Cheats")),
                    new GUIContent(ln.GetText("Tab.Environment")),
                    new GUIContent(ln.GetText("Tab.Player")),
                    new GUIContent(ln.GetText("Tab.Animals")),
                    new GUIContent(ln.GetText("Tab.Mutants")),
                    new GUIContent(ln.GetText("Tab.Spawn")),
                    new GUIContent(ln.GetText("Tab.Inventory")),
                    new GUIContent(ln.GetText("Tab.Coop")),
                    new GUIContent(ln.GetText("Tab.Dev")),
                    new GUIContent(ln.GetText("Tab.Game"))
                }, UnityEngine.GUI.skin.GetStyle("Tabs"));
                float num = 50f;
                if (this.Tab == 0) // Cheats
                {
                    
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("GodMode"), this.labelStyle);
                    UCheatmenu.GodMode = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.GodMode, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("Flymode"), ln.GetText("Default")+": F6"), this.labelStyle);
                    UCheatmenu.FlyMode = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.FlyMode, "");
                    num += 30f; this.scroller += 30;
                    if (UCheatmenu.FlyMode)
                    {
                        UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("Noclip"), ln.GetText("Default")+": F7"), this.labelStyle);
                        UCheatmenu.NoClip = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.NoClip, "");
                        num += 30f; this.scroller += 30;
                    }
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("InstantTree"), ln.GetText("InstantTreeDesc")), this.labelStyle);
                    UCheatmenu.InstantTree = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.InstantTree, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("InstantBuildingDestroy"), ln.GetText("InstantBuildingDestroyDesc")), this.labelStyle);
                    UCheatmenu.DestroyBuildings = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.DestroyBuildings, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("InstantKill"), ln.GetText("InstantKillDesc")), this.labelStyle);
                    UCheatmenu.InstaKill = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.InstaKill, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("UnlimitedFuel"), ln.GetText("UnlimitedFuelDesc")), this.labelStyle);
                    UCheatmenu.UnlimitedFuel = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.UnlimitedFuel, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("UnlimitedHairspray"), ln.GetText("UnlimitedHairsprayDesc")), this.labelStyle);
                    UCheatmenu.UnlimitedHairspray = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.UnlimitedHairspray, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("NoUnderwaterBlur"), ln.GetText("NoUnderwaterBlurDesc")), this.labelStyle);
                    UCheatmenu.NoUnderwaterBlur = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.NoUnderwaterBlur, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("InfiniteFire"), ln.GetText("InfiniteFireDesc")), this.labelStyle);
                    UCheatmenu.InfFire = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.InfFire, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("AutoLighter"), ln.GetText("AutoLighterDesc")), this.labelStyle);
                    UCheatmenu.InstLighter = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.InstLighter, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("FastFlintlockBow"), ln.GetText("FastFlintlockBow")), this.labelStyle);
                    UCheatmenu.FastFlint = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.FastFlint, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Sleep"), this.labelStyle);
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 150f, 20f), ln.GetText("Sleep")))
                    {
                        UCheatmenu.visible = false;
                        LocalPlayer.FpCharacter.UnLockView();
                        UCheatmenu.SleepTimer = true;
                    }
                    if (UnityEngine.GUI.Button(new Rect(340f, num, 150f, 20f), ln.GetText("SleepSave")))
                    {
                        UCheatmenu.visible = false;
                        LocalPlayer.FpCharacter.UnLockView();
                        UCheatmenu.SleepTimer = true;
                        LocalPlayer.Stats.Invoke("JustSave",3.5f);
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("FreeCam"), ln.GetText("Default")+": F3"), this.labelStyle);
                    UCheatmenu.FreeCam = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.FreeCam, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("FreezeTime"), ln.GetText("Default") + ": F4"), this.labelStyle);
                    UCheatmenu.FreezeTime = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.FreezeTime, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), new GUIContent(ln.GetText("ExplosionRadius")+" (" + System.Math.Round(UCheatmenu.ExplosionRadius).ToString() + "m)", ln.GetText("ExplosionRadiusDesc")), this.labelStyle);
                    num += 30f; this.scroller += 30;
                    UCheatmenu.ExplosionRadius = UnityEngine.GUI.HorizontalSlider(new Rect(20f, num + 3f, 150f, 30f), UCheatmenu.ExplosionRadius, 0f, 200f);
                    if (UnityEngine.GUI.Button(new Rect(190f, num, 150f, 20f), ln.GetText("Reset")))
                    {
                        UCheatmenu.ExplosionRadius = 15f;
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), new GUIContent(ln.GetText("ActionRadius")+" (" + System.Math.Round(UCheatmenu.ARadius).ToString() + "m)", ln.GetText("ActionRadiusDesc")), this.labelStyle);
                    UnityEngine.GUI.Label(new Rect(190f, num, 100f, 20f), new GUIContent(ln.GetText("Global"), ln.GetText("GlobalDesc")), this.labelStyle);
                    num += 30f; this.scroller += 30;
                    UCheatmenu.ARadius = UnityEngine.GUI.HorizontalSlider(new Rect(20f, num + 3f, 150f, 30f), UCheatmenu.ARadius, 10f, 200f);
                    UCheatmenu.ARadiusGlobal = UnityEngine.GUI.Toggle(new Rect(190f, num, 20f, 30f), UCheatmenu.ARadiusGlobal, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Buildings"), labelStyle);
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 150f, 20f), ln.GetText("CancelGhosts")))
                    {
                        this._cancelallghosts();
                    }
                    if (UnityEngine.GUI.Button(new Rect(340f, num, 150f, 20f), ln.GetText("Repair")))
                    {
                        InstaBuild.repairAll();
                    }
                    UnityEngine.GUI.Label(new Rect(500f, num, 70f, 20f), new GUIContent(ln.GetText("Collision"), ln.GetText("CollisionDesc")), this.labelStyle);
                    UCheatmenu.BuildingCollision = UnityEngine.GUI.Toggle(new Rect(580f, num, 20f, 30f), UCheatmenu.BuildingCollision, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("EnableEffigies"), ln.GetText("EnableEffigiesDesc")), labelStyle);
                    UCheatmenu.EnableEffigies = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.EnableEffigies, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("EnableEndgame"), ln.GetText("EnableEndgameDesc")), labelStyle);
                    UCheatmenu.EnableEndgame = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.EnableEndgame, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("LogHack"), ln.GetText("LogHackDesc")), labelStyle);
                    if(UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), LocalPlayer.Inventory.Logs._infiniteLogHack, "") != LocalPlayer.Inventory.Logs._infiniteLogHack)
                    {
                        LocalPlayer.Inventory.Logs._infiniteLogHack = !LocalPlayer.Inventory.Logs._infiniteLogHack;
                        this._loghack();
                    }
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("InfiniteSpawnedLogs"), ln.GetText("InfiniteSpawnedLogsDesc")), labelStyle);
                    UCheatmenu.InfiniteSpawnedLogs = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.InfiniteSpawnedLogs, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("ItemHack"), ln.GetText("ItemHackDesc")), labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.ItemHack, "") != UCheatmenu.ItemHack)
                    {
                        UCheatmenu.ItemHack = !UCheatmenu.ItemHack;
                        this._itemhack();
                    }
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("NoItemConsume"), ln.GetText("NoItemConsumeDesc")), labelStyle);
                    UCheatmenu.ItemConsume = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.ItemConsume, "");
                    num += 30f; this.scroller += 30;
                    /*
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent("Unlimited Craft Upgrade", "Unlimited amount of items can be used for item upgrades"), labelStyle);
                    UCheatmenu.UnlimitedUpgrade = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.UnlimitedUpgrade, "");
                    num += 30f; this.scroller += 30;
                    */              
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("BuildHack"), ln.GetText("BuildHackDesc")), labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.BuildHack, "") != UCheatmenu.BuildHack)
                    {
                        UCheatmenu.BuildHack = !UCheatmenu.BuildHack;
                        this._buildhack();
                    }
                    num += 30f; this.scroller += 30;
                    
                    UnityEngine.GUI.Label(new Rect(20f, num, 410f, 20f), new GUIContent(ln.GetText("Sphere"), ln.GetText("SphereDesc")), this.labelStyle);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("CutTrees"), this.labelStyle);
                    UCheatmenu.SphereTreeCut = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SphereTreeCut, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("CutTreeStumps"), this.labelStyle);
                    UCheatmenu.SphereTreeStumps = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SphereTreeStumps, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("CutBushes"), ln.GetText("CutBushesDesc")), this.labelStyle);
                    UCheatmenu.SphereBushes = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SphereBushes, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("BreakCrates"), this.labelStyle);
                    UCheatmenu.SphereCrates = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SphereCrates, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("OpenSuitcases"), this.labelStyle);
                    UCheatmenu.SphereSuitcases = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SphereSuitcases, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("LightFires"), this.labelStyle);
                    UCheatmenu.SphereFires = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SphereFires, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("ResetTraps"), this.labelStyle);
                    UCheatmenu.SphereTraps = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SphereTraps, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("CallCrane"), ln.GetText("CallCraneDesc")), this.labelStyle);
                    UCheatmenu.SphereCrane = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SphereCrane, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 150f, 20f), new GUIContent(ln.GetText("FillHolder"), ln.GetText("FillHolderDesc")), this.labelStyle);
                    UCheatmenu.SphereHolders = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SphereHolders, "");
                    //num += 30f; this.scroller += 30;
                    UCheatmenu.SphereHoldersType = UnityEngine.GUI.SelectionGrid(new Rect(200f, num, 300f, 30f), UCheatmenu.SphereHoldersType, SphereHolderOptions, SphereHolderOptions.Length, UnityEngine.GUI.skin.toggle);
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;
                    
                }
                if (this.Tab == 1) // Environment
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("SpeedOfTime")+" (" + TheForestAtmosphere.GameTimeScale + ")", this.labelStyle);
                    TheForestAtmosphere.GameTimeScale = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), TheForestAtmosphere.GameTimeScale, 0.1f, 50f);
                    num += 30f; this.scroller += 30;
                    UCheatmenu.TimeToggle = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.TimeToggle, "");
                    UnityEngine.GUI.Label(new Rect(50f, num, 200f, 20f), ln.GetText("PauseTime"), this.labelStyle);
                    if (UnityEngine.GUI.Button(new Rect(280f, num, 100f, 20f), ln.GetText("Reset")))
                    {
                        TheForestAtmosphere.GameTimeScale = 1f;
                    }
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Time"), this.labelStyle);
                    TheForestAtmosphere.Instance.TimeOfDay = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), TheForestAtmosphere.Instance.TimeOfDay, 1f, 360f);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("NightLight"), this.labelStyle);
                    UCheatmenu.NightLight = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), UCheatmenu.NightLight, 1f, 10f);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("CaveLight"), this.labelStyle);
                    UCheatmenu.CaveLight = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), UCheatmenu.CaveLight, 0f, 1f);
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("Torch"), ln.GetText("TorchDesc")), this.labelStyle);
                    UCheatmenu.TorchToggle = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.TorchToggle, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 50f, 20f), "R", this.labelStyle);
                    UnityEngine.GUI.Label(new Rect(80f, num, 50f, 20f), "G", this.labelStyle);
                    UnityEngine.GUI.Label(new Rect(140f, num, 50f, 20f), "B", this.labelStyle);
                    UnityEngine.GUI.Label(new Rect(200f, num, 100f, 20f), ln.GetText("Intensity")+" (" + UCheatmenu.TorchI.ToString("0.0") + ")", this.labelStyle);
                    num += 30f; this.scroller += 30;
                    UCheatmenu.TorchR = Convert.ToInt32(UnityEngine.GUI.TextField(new Rect(20f, num, 50f, 30f), UCheatmenu.TorchR.ToString(), UnityEngine.GUI.skin.textField));
                    UCheatmenu.TorchG = Convert.ToInt32(UnityEngine.GUI.TextField(new Rect(80f, num, 50f, 30f), UCheatmenu.TorchG.ToString(), UnityEngine.GUI.skin.textField));
                    UCheatmenu.TorchB = Convert.ToInt32(UnityEngine.GUI.TextField(new Rect(140f, num, 50f, 30f), UCheatmenu.TorchB.ToString(), UnityEngine.GUI.skin.textField));
                    UCheatmenu.TorchI = UnityEngine.GUI.HorizontalSlider(new Rect(200f, num + 3f, 150f, 30f), UCheatmenu.TorchI, 0f, 2f);
                    num += 40f; this.scroller += 40;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("Fog"), this.labelStyle);
                    UCheatmenu.Fog = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.Fog, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("FreezeWeather"), this.labelStyle);
                    UCheatmenu.FreezeWeather = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.FreezeWeather, "");
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), ln.GetText("ClearWeather")))
                    {
                        UCheatmenu.ForceWeather = 0;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), ln.GetText("Cloudy")))
                    {
                        UCheatmenu.ForceWeather = 4;
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), ln.GetText("LightRain")))
                    {
                        UCheatmenu.ForceWeather = 1;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), ln.GetText("LightSnow")))
                    {
                        UCheatmenu.ForceWeather = 5;
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), ln.GetText("MediumRain")))
                    {
                        UCheatmenu.ForceWeather = 2;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), ln.GetText("MediumSnow")))
                    {
                        UCheatmenu.ForceWeather = 6;
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), ln.GetText("HeavyRain")))
                    {
                        UCheatmenu.ForceWeather = 3;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), ln.GetText("HeavySnow")))
                    {
                        UCheatmenu.ForceWeather = 7;
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), new GUIContent(ln.GetText("Garden"),""), this.labelStyle);
                    //UCheatmenu.EnableGarden = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), UCheatmenu.EnableGarden, "");
                    num += 30f; this.scroller += 30;

                    if (UCheatmenu.EnableGarden)
                    {
                    }
                    /*
                    Garden[] gardens = GameObject.FindObjectsOfType<Garden>();
                    foreach (Garden garden in gardens)
                    {
                        int i = 0;
                        foreach (Garden.SeedTypes seedid in garden._seeds)
                        {
                            string seedname;
                            switch (seedid._itemId)
                            {
                                case 103: seedname = "Aloe"; break;
                                case 206: seedname = "Blueberry"; break;
                                case 205: seedname = "Coneflower"; break;
                                default: seedname = seedid._itemId.ToString(); break;
                            }
                            if (UnityEngine.GUI.Button(new Rect(20f + (i * 120f), num, 100f, 20f), seedname))
                            {
                                this._plantallgardens(i);
                            }
                            i++;
                        }
                        break;
                    }
                    */
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 100f, 20f), ln.GetText("Aloe")))
                    {
                        this._plantallgardens(0);
                    }
                    if (UnityEngine.GUI.Button(new Rect(140f, num, 100f, 20f), ln.GetText("Coneflower")))
                    {
                        this._plantallgardens(1);
                    }
                    if (UnityEngine.GUI.Button(new Rect(260f, num, 100f, 20f), ln.GetText("Blueberry")))
                    {
                        this._plantallgardens(2);
                    }

                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 100f, 20f), ln.GetText("Grow")+" 1"))
                    {
                        this._growalldirtpiles(1);
                    }
                    if (UnityEngine.GUI.Button(new Rect(140f, num, 100f, 20f), ln.GetText("Grow")+" 2"))
                    {
                        this._growalldirtpiles(2);
                    }
                    if (UnityEngine.GUI.Button(new Rect(260f, num, 100f, 20f), ln.GetText("Grow")+" 3"))
                    {
                        this._growalldirtpiles(3);
                    }
                    num += 30f; this.scroller += 30;
                    

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), new GUIContent(ln.GetText("Trees")+" (x|x%)", ln.GetText("TreesDesc")), this.labelStyle);
                    setCutTrees = UnityEngine.GUI.TextField(new Rect(170f, num, 210f, 30f), setCutTrees, UnityEngine.GUI.skin.textField);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 95f, 20f), ln.GetText("Cut")))
                    {
                        this._cutdowntrees(setCutTrees);
                    }
                    if (UnityEngine.GUI.Button(new Rect(285f, num, 95f, 20f), ln.GetText("Regrow")))
                    {
                        this._cutdowntrees("-"+setCutTrees);
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), new GUIContent(ln.GetText("Grass"), ln.GetText("GrassDesc")), this.labelStyle);
                    setCGrowGrass = UnityEngine.GUI.TextField(new Rect(170f, num, 210f, 30f), setCGrowGrass, UnityEngine.GUI.skin.textField);
                    num += 30f; this.scroller += 30;
                    /*if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), "Grow (radius)"))
                    {
                        this._growgrass(setCGrowGrass);
                    }
                    num += 30f;*/
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), ln.GetText("Cut")+" (radius)"))
                    {
                        this._cutgrass(setCGrowGrass);
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;
                }
                if (this.Tab == 2) // Player
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(370f, num, 150f, 20f), "Fix", this.labelStyle);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Health"), this.labelStyle);
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
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("BatteryCharge"), this.labelStyle);
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
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Fullness"), this.labelStyle);
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
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Stamina"), this.labelStyle);
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
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Energy"), this.labelStyle);
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
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Thirst"), this.labelStyle);
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
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Starvation"), this.labelStyle);
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
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("BodyTemp"), this.labelStyle);
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
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Sanity"), this.labelStyle);
                    if (!UCheatmenu.FixSanity)
                    {
                        LocalPlayer.Stats.Sanity.CurrentSanity = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Sanity.CurrentSanity, 0f, 100f);
                    }
                    else
                    {
                        UCheatmenu.FixedSanity = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedSanity, 0f, 100f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Sanity.CurrentSanity * 10f) / 10f));
                    UCheatmenu.FixSanity = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixSanity, "");
                    if (UCheatmenu.FixSanity)
                    {
                        if (UCheatmenu.FixedSanity == -1f)
                        {
                            UCheatmenu.FixedSanity = LocalPlayer.Stats.Sanity.CurrentSanity;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedSanity = -1f;
                    }
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Weight"), this.labelStyle);
                    if (!UCheatmenu.FixWeight)
                    {
                        LocalPlayer.Stats.PhysicalStrength.CurrentWeight = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.PhysicalStrength.CurrentWeight, LocalPlayer.Stats.PhysicalStrength.MinWeight, LocalPlayer.Stats.PhysicalStrength.MaxWeight);
                    }
                    else
                    {
                        UCheatmenu.FixedWeight = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedWeight, LocalPlayer.Stats.PhysicalStrength.MinWeight, LocalPlayer.Stats.PhysicalStrength.MaxWeight);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.PhysicalStrength.CurrentWeight * 10f) / 10f));
                    UCheatmenu.FixWeight = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixWeight, "");
                    if (UCheatmenu.FixWeight)
                    {
                        if (UCheatmenu.FixedWeight == -1f)
                        {
                            UCheatmenu.FixedWeight = LocalPlayer.Stats.PhysicalStrength.CurrentWeight;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedWeight = -1f;
                    }
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Strength"), this.labelStyle);
                    if (!UCheatmenu.FixStrength)
                    {
                        LocalPlayer.Stats.PhysicalStrength.CurrentStrength = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.PhysicalStrength.CurrentStrength, LocalPlayer.Stats.PhysicalStrength.MinStrength, LocalPlayer.Stats.PhysicalStrength.MaxStrength);
                    }
                    else
                    {
                        UCheatmenu.FixedStrength = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), UCheatmenu.FixedStrength, LocalPlayer.Stats.PhysicalStrength.MinStrength, LocalPlayer.Stats.PhysicalStrength.MaxStrength);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.PhysicalStrength.CurrentStrength * 10f) / 10f));
                    UCheatmenu.FixStrength = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), UCheatmenu.FixStrength, "");
                    if (UCheatmenu.FixStrength)
                    {
                        if (UCheatmenu.FixedStrength == -1f)
                        {
                            UCheatmenu.FixedStrength = LocalPlayer.Stats.PhysicalStrength.CurrentStrength;
                        }
                    }
                    else
                    {
                        UCheatmenu.FixedStrength = -1f;
                    }
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Speed"), this.labelStyle);
                    UCheatmenu.SpeedMultiplier = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), UCheatmenu.SpeedMultiplier, 1f, 10f);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("JumpPower"), this.labelStyle);
                    UCheatmenu.JumpMultiplier = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), UCheatmenu.JumpMultiplier, 1f, 10f);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 150f, 20f), new GUIContent(ln.GetText("Invisible"), ln.GetText("InvisibleDesc")), this.labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.InvisibleToggle, "") != UCheatmenu.InvisibleToggle)
                    {
                        UCheatmenu.InvisibleToggle = !UCheatmenu.InvisibleToggle;
                        this._invisible();
                    }
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Tab.Player"), this.labelStyle);
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 150f, 20f), ln.GetText("KillMe"))) { this._killlocalplayer(); }
                    if (UnityEngine.GUI.Button(new Rect(340f, num, 150f, 20f), ln.GetText("ReviveMe"))) { this._revivelocalplayer(); }
                    num += 30f; this.scroller += 30;
 
                    /* clothes */
                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), new GUIContent(ln.GetText("PlayerClothing"), ln.GetText("PlayerClothingDesc")), labelStyle);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("PlayerClothingDesc2"), labelStyle);
                    num += 30f; this.scroller += 30;

                    playclothinglist = UnityEngine.GUI.TextField(new Rect(20f, num, 210f, 30f), playclothinglist, UnityEngine.GUI.skin.textField);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 210f, 20f), ln.GetText("Set")))
                    {
                        this._addClothingById(playclothinglist);
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;
                }
                if (this.Tab == 3) // Animals
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("Tab.Animals"), this.labelStyle);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("KillAllAnimals")))
                    {
                        this._killallanimals();
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("KillClosestAnimal")))
                    {
                        this._killclosestanimal();
                    }
                    num += 30f; this.scroller += 30;


                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("ToggleAnimals"), this.labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.AnimalToggle, "") != UCheatmenu.AnimalToggle)
                    {
                        UCheatmenu.AnimalToggle = !UCheatmenu.AnimalToggle;
                        this._animals();
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("ToggleBirds"), this.labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.BirdToggle, "") != UCheatmenu.BirdToggle)
                    {
                        UCheatmenu.BirdToggle = !UCheatmenu.BirdToggle;
                        this._birds();
                    }
                    num += 30f; this.scroller += 30;

                    if (!BoltNetwork.isRunning)
                    {
                        UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("SpawnAnimals"),
                            this.labelStyle);
                        num += 30f;
                        this.scroller += 30;
                        foreach (string animal in AnimalNames)
                        {
                            string name = animal;
                            switch (name)
                            {
                                case "boarGo":
                                    name = ln.GetText("Boar");
                                    break;
                                case "crocodileGo":
                                    name = ln.GetText("Crocodile");
                                    break;
                                case "deerGo":
                                    name = ln.GetText("Deer");
                                    break;
                                case "lizardGo":
                                    name = ln.GetText("Lizard");
                                    break;
                                case "rabbitGo":
                                    name = ln.GetText("Rabbit");
                                    break;
                                case "raccoonGo":
                                    name = ln.GetText("Raccoon");
                                    break;
                                case "squirrelGo":
                                    name = ln.GetText("Squirrel");
                                    break;
                                case "tortoiseGo":
                                    name = ln.GetText("Tortoise");
                                    break;
                            }
                            if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), name))
                            {
                                UCheatmenu.lastObjectType = "animal";
                                UCheatmenu.lastObject = animal;

                                GameObject.Instantiate(AnimalPrefabs[animal],
                                    LocalPlayer.MainCam.transform.position + LocalPlayer.MainCam.transform.forward * 2f,
                                    Quaternion.identity);
                            }
                            num += 30f;
                            this.scroller += 30;

                        }
                    }


                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;

                }
                if (this.Tab == 4) // Spawn Mutants
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 200f, 20f), ln.GetText("Enemies"), this.labelStyle);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("KillAllEnemies")))
                    {
                        this._killallenemies();
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("KillClosestEnemy")))
                    {
                        this._killclosestenemy();
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("KnockDownClosestEnemy")))
                    {
                        this._knockDownclosestenemy();
                    }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("KillEndboss")))
                    {
                        this._killEndBoss();
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("Toggle") + " " + ln.GetText("Enemies"), this.labelStyle);
                    if (UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.MutantToggle, "") != UCheatmenu.MutantToggle)
                    {
                        UCheatmenu.MutantToggle = !UCheatmenu.MutantToggle;
                        this._enemies();
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("SpawnMutants"), labelStyle);
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("Attributes"), labelStyle);
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("Skinned"), this.labelStyle);
                    UCheatmenu.Mutant_Skinned = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.Mutant_Skinned, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("Leader"), this.labelStyle);
                    UCheatmenu.Mutant_Leader = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.Mutant_Leader, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("Painted"), this.labelStyle);
                    UCheatmenu.Mutant_Painted = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.Mutant_Painted, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("Pale"), this.labelStyle);
                    UCheatmenu.Mutant_Pale = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.Mutant_Pale, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("Old"), this.labelStyle);
                    UCheatmenu.Mutant_Old = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.Mutant_Old, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("Dynamite"), this.labelStyle);
                    UCheatmenu.Mutant_Dynamite = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.Mutant_Dynamite, "");
                    num += 30f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("BossBaby"), this.labelStyle);
                    UCheatmenu.Mutant_BBaby = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.Mutant_BBaby, "");
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("Male"))){ this._spawnmutant("male");}
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("MaleSkinny"))) { this._spawnmutant("male_skinny"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("MaleSkinned"))) { this._spawnmutant("skinnedMale"); }
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("Female"))) { this._spawnmutant("female"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("FemaleSkinny"))) { this._spawnmutant("female_skinny"); }
                    num += 30f; this.scroller += 30;
                    
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("Pale"))) { this._spawnmutant("pale"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("PaleSkinny"))) { this._spawnmutant("pale_skinny"); }
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("Fireman"))) { this._spawnmutant("fireman"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("Vags"))) { this._spawnmutant("vags"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("Armsy"))) { this._spawnmutant("armsy"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("Baby"))) { this._spawnmutant("baby"); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("Fat"))) { this._spawnmutant("fat"); }
                    num += 30f; this.scroller += 30;
                    

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("SpawnMutantFamilies"), labelStyle);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("RegularFamily"))) { this._spawnRegularFamily(null); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("PaintedFamily"))) { this._spawnPaintedFamily(null); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("SkinnedFamily"))) { this._spawnSkinnedFamily(null); }
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("SkinnyFamily"))) { this._spawnSkinnyFamily(null); }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;
                }
                if (this.Tab == 5) // Spawn Items
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 340f, 540f), this.scrollPosition, new Rect(0f, 0f, 320f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 200f, 20f), ln.GetText("SpawnItems"), labelStyle);
                    num += 30f; this.scroller += 30;

                    iItem = UnityEngine.GUI.TextField(new Rect(20f, num, 170f, 30f), iItem, UnityEngine.GUI.skin.textField);
                    num += 40f; this.scroller += 40;

                    Dictionary<string, int> invitems = new Dictionary<string, int>();
                    foreach (int current in LocalPlayer.Inventory.InventoryItemViewsCache.Keys)
                    {
                        if (!invitems.ContainsKey(ItemDatabase.ItemById(current)._name))
                        {
                            invitems.Add(ItemDatabase.ItemById(current)._name, current);
                        }
                    }
                    invitems = invitems.OrderBy(o => o.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
                    foreach (var current in invitems)
                    {
                        try
                        {
                            string curName = current.Key;
                            if (iItem == "" || curName.ToLower().Contains(iItem.ToLower()))
                            {
                                UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), new GUIContent(curName, curName), labelStyle);
                                if (UnityEngine.GUI.Button(new Rect(190f, num, 130f, 20f), "Spawn"))
                                {
                                    UCheatmenu.lastObjectType = "spawnitem";
                                    UCheatmenu.lastObject = current.Value.ToString();
                                    LocalPlayer.Inventory.FakeDrop(current.Value, null);
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

                    UnityEngine.GUI.Label(new Rect(20f, num, 200f, 20f), ln.GetText("SpawnProps"), labelStyle);
                    num += 30f; this.scroller2 += 30;

                    pItem = UnityEngine.GUI.TextField(new Rect(20f, num, 170f, 30f), pItem, UnityEngine.GUI.skin.textField);
                    if (UnityEngine.GUI.Button(new Rect(200f, num, 130f, 20f), "Refresh"))
                    {
                        initobjects = false;
                        FindBolts();
                        FindProps();
                        FindObjects();
                    }
                    num += 40f; this.scroller2 += 40;

                    UnityEngine.GUI.Label(new Rect(50f, num, 200f, 20f), ln.GetText("SpawnLookingAt"), labelStyle);
                    UCheatmenu.SpawnLookAt = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.SpawnLookAt, "");
                    num += 30f; this.scroller2 += 30;
                    UnityEngine.GUI.Label(new Rect(50f, num, 200f, 20f), ln.GetText("ShowObjects"), labelStyle);
                    toggleobj = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), toggleobj, "");
                    num += 30f; this.scroller2 += 30;
                    /*
                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), "Show Picked", labelStyle);
                    togglepref = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), togglepref, "");
                    num += 30f; this.scroller2 += 30;
                    */

                    if (BoltNetwork.isRunning)
                    {
                        foreach (var prop in BoltPrefabsDict.OrderBy(key => key.Key))
                        {
                            try
                            {
                                if (prop.Key != "" && (pItem == "" || prop.Key.ToLower().Contains(pItem.ToLower())))
                                {
                                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), new GUIContent("[MP]" +prop.Key.ToString(), prop.Key.ToString()), labelStyle);
                                    if (UnityEngine.GUI.Button(new Rect(200f, num, 130f, 20f), "Spawn"))
                                    {
                                        Vector3 spawnposition;
                                        if (UCheatmenu.SpawnLookAt)
                                        {
                                            spawnposition = getRayPoint();
                                        }
                                        else
                                        {
                                            spawnposition = LocalPlayer.MainCam.transform.position + LocalPlayer.MainCam.transform.forward * 2f + LocalPlayer.MainCam.transform.up * -3f;
                                        }
                                        UCheatmenu.lastObjectType = "spawnmp";
                                        UCheatmenu.lastObjectPrefab = prop.Value;
                                        BoltNetwork.Instantiate(prop.Value, spawnposition, Quaternion.identity);
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
                                UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), new GUIContent(prop, prop), labelStyle);
                                if (UnityEngine.GUI.Button(new Rect(200f, num, 130f, 20f), "Spawn"))
                                {
                                    Vector3 spawnposition;
                                    if (UCheatmenu.SpawnLookAt)
                                    {
                                        spawnposition = getRayPoint();
                                    }
                                    else
                                    {
                                        spawnposition = LocalPlayer.MainCam.transform.position + LocalPlayer.MainCam.transform.forward * 2f + LocalPlayer.MainCam.transform.up * -3f;
                                    }
                                    UCheatmenu.lastObjectType = "spawnprop";
                                    UCheatmenu.lastObject = prop;
                                    UnityEngine.GameObject obj = (UnityEngine.GameObject)GameObject.Instantiate(PropPrefabs[prop], spawnposition, Quaternion.identity);
                                }
                                num += 30f; this.scroller2 += 30;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex.ToString());
                        }
                    }


                    if (toggleobj)
                    {
                        if (AllObjects.Count > 0)
                        {
                            foreach (var res in AllObjects)
                            {
                                try
                                {
                                    if (pItem == "" || res.name.ToLower().Contains(pItem.ToLower()))
                                    {
                                        UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f),
                                            new GUIContent(res.name, res.name), labelStyle);
                                        if (UnityEngine.GUI.Button(new Rect(200f, num, 130f, 20f), "Spawn"))
                                        {
                                            Vector3 spawnposition;
                                            if (UCheatmenu.SpawnLookAt)
                                            {
                                                spawnposition = getRayPoint();
                                            }
                                            else
                                            {
                                                spawnposition =
                                                    LocalPlayer.MainCam.transform.position +
                                                    LocalPlayer.MainCam.transform.forward * 2f +
                                                    LocalPlayer.MainCam.transform.up * -3f;
                                            }
                                            UCheatmenu.lastObjectType = "spawnobject";
                                            UCheatmenu.lastObjectGObject = res;
                                            UnityEngine.GameObject obj = (UnityEngine.GameObject) GameObject.Instantiate(res, spawnposition, Quaternion.identity);
                                        }
                                        num += 30f;
                                        this.scroller2 += 30;
                                    }
                                }
                                catch (Exception e)
                                {

                                }
                            }
                        }
                    }

                    /*
                    if (togglepref)
                    {
                        try
                        {
                            UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), new GUIContent("Timmy", "Timmy"), labelStyle);
                            if (UnityEngine.GUI.Button(new Rect(200f, num, 130f, 20f), "Spawn"))
                            {
                                Vector3 spawnposition;
                                if (UCheatmenu.SpawnLookAt)
                                {
                                    spawnposition = getRayPoint();
                                }
                                else
                                {
                                    spawnposition =
                                        LocalPlayer.MainCam.transform.position +
                                        LocalPlayer.MainCam.transform.forward * 2f +
                                        LocalPlayer.MainCam.transform.up * -3f;
                                }

                                GameObject gameObject = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<GameObject>("activateTimmyPickup"), spawnposition, Quaternion.identity) as GameObject;
                                activateTimmyPickup component = gameObject.GetComponent<activateTimmyPickup>();
                                
                            }
                            num += 30f;
                            this.scroller2 += 30;

                            UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), new GUIContent("Timmy 2", "Timmy 2"), labelStyle);
                            if (UnityEngine.GUI.Button(new Rect(200f, num, 130f, 20f), "Spawn"))
                            {
                                Vector3 spawnposition;
                                if (UCheatmenu.SpawnLookAt)
                                {
                                    spawnposition = getRayPoint();
                                }
                                else
                                {
                                    spawnposition =
                                        LocalPlayer.MainCam.transform.position +
                                        LocalPlayer.MainCam.transform.forward * 2f +
                                        LocalPlayer.MainCam.transform.up * -3f;
                                }

                                GameObject gameObject = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<GameObject>("activateTimmyPickup"), spawnposition, Quaternion.identity) as GameObject;
                                activateTimmyPickup component = gameObject.GetComponent<activateTimmyPickup>();
                                component.enabled = true;
                                component.gameObject.SetActive(true);

                                UnityEngine.GameObject obj = (UnityEngine.GameObject)GameObject.Instantiate(component.timmyGo, spawnposition,Quaternion.identity);
                            }
                            num += 30f;
                            this.scroller2 += 30;

                        }
                        catch (Exception e)
                        {
                        }
                    }
                    */


                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix2;
                }
                if (this.Tab == 6) // Inventory
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    int scroller_2 = 25;

                    num = 10;

                    // TheForest.Items.Craft.WeaponStatUpgrade
                    // ApplyWeaponStatsUpgrades
                    list = new[] {"None"};
                    list = list.Concat(Enum.GetNames(typeof(WeaponStatUpgrade.Types))).ToArray();

                    UnityEngine.GUI.Label(new Rect(430f, num, 170f, 20f), ln.GetText("Modifier"), labelStyle);
                    num += 30f; this.scroller += 30;
                    scroller_2 += 30;
                    wepBonus = UnityEngine.GUI.SelectionGrid(new Rect(430f, num, 300f, list.Length*25), wepBonus, list, 1, UnityEngine.GUI.skin.toggle);

                    scroller_2 += list.Length * 25;
                    num = 10f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), ln.GetText("AddItem")+" (name or id)", labelStyle);
                    tItem = UnityEngine.GUI.TextField(new Rect(190f, num, 210f, 30f), tItem, UnityEngine.GUI.skin.textField);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(190f, num, 95f, 20f), ln.GetText("Add")+" +1"))
                    {
                        this._additem(tItem, 1);
                    }
                    if (UnityEngine.GUI.Button(new Rect(305f, num, 95f, 20f), ln.GetText("Add") + " ∞"))
                    {
                        this._additem(tItem, 1000000);
                    }
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 170f, 20f), ln.GetText("Add") + " ∞"))
                    {
                        this._addAllItems();
                    }
                    if (UnityEngine.GUI.Button(new Rect(200f, num, 170f, 20f), ln.GetText("Remove") + " ∞"))
                    {
                        this._removeAllItems();
                    }
                    num += 30f; this.scroller += 30;


                    foreach (Item current in from i in ItemDatabase.Items
                                             orderby i._name
                                             select i)
                    {
                        if (tItem == "" || current._name.ToLower().Contains(tItem.ToLower()) || current._id.ToString().Contains(tItem))
                        {
                            UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), new GUIContent(current._name + " [" + current._id + "]", current._name + " [" + current._id + "]"), labelStyle);
                            if (UnityEngine.GUI.Button(new Rect(190f, num, 40f, 20f), "+1"))
                            {
                                this._additem(current._id.ToString(), 1);
                            }
                            if (UnityEngine.GUI.Button(new Rect(240f, num, 40f, 20f), "+∞"))
                            {
                                this._additem(current._id.ToString(), 100000);
                            }
                            if (UnityEngine.GUI.Button(new Rect(300f, num, 40f, 20f), "-1"))
                            {
                                this._removeitem(current._id.ToString());
                            }
                            if (UnityEngine.GUI.Button(new Rect(350f, num, 40f, 20f), "-∞"))
                            {
                                this._removeitem(current._id.ToString(), true);
                            }
                            num += 30f; this.scroller += 30;
                        }
                    }

                    if (scroller_2 > this.scroller) this.scroller = scroller_2 + 30;

                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;
                }
                if (this.Tab == 7) // Coop
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    if (PlayerManager.Players.Count == 0)
                    {
                        UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), ln.GetText("NoPlayersFound"), labelStyle);
                        num += 30f; this.scroller += 30;
                    }
                    else
                    {
                        foreach (var player in PlayerManager.Players)
                        {
                            UnityEngine.GUI.Label(new Rect(20f, num, 170f, 20f), player.Name, labelStyle);

                            if (UnityEngine.GUI.Button(new Rect(210f, num, 80f, 20f), ln.GetText("Teleport")))
                            {
                                LocalPlayer.Transform.position = player.Position;
                            }

                            if (UnityEngine.GUI.Button(new Rect(410f, num, 80f, 20f), ln.GetText("Revive")))
                            {
                                GameObject deadTriggerObject = player.DeadTriggerObject;
                                if (deadTriggerObject != null && deadTriggerObject.activeSelf)
                                {
                                    RespawnDeadTrigger component = deadTriggerObject.GetComponent<RespawnDeadTrigger>();
                                    PlayerHealed phealed = PlayerHealed.Create(GlobalTargets.Others);
                                    phealed.HealingItemId = component._healItemId;
                                    phealed.HealTarget = player.Entity;
                                    phealed.Send();
                                    component.SendMessage("SetActive", false);
                                }
                            }

                            if (UnityEngine.GUI.Button(new Rect(310f, num, 80f, 20f), ln.GetText("Profile")))
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
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("Teleport"), labelStyle);
                    Teleport = UnityEngine.GUI.TextField(new Rect(170f, num, 210f, 30f), Teleport, UnityEngine.GUI.skin.textField);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), "Go"))
                    {
                        LocalPlayer.GameObject.transform.localPosition = Vector3FromString(Teleport);
                    }
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), new GUIContent(ln.GetText("WhereAmI"), ln.GetText("WhereAmIDesc"))))
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
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("GoInCave")))
                    {
                        this.GotoCave(true);
                    }
                    if (UnityEngine.GUI.Button(new Rect(240f, num, 150f, 20f), ln.GetText("GoOutCave")))
                    {
                        this.GotoCave(false);
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), ln.GetText("SetCurrentDay")+" (" + Clock.Day.ToString() + "):", labelStyle);
                    setCurDay = UnityEngine.GUI.TextField(new Rect(170f, num, 210f, 30f), setCurDay, UnityEngine.GUI.skin.textField);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(170f, num, 210f, 20f), ln.GetText("Set")))
                    {
                        this._setCurrentDay(setCurDay);
                        TheForest.Utils.LocalPlayer.Stats.DaySurvived = Convert.ToSingle(setCurDay);
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), "SceneTracker:", labelStyle);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), "WentLight()"))
                    {
                        TheForest.Utils.Scene.SceneTracker.WentLight();
                    }
                    if (UnityEngine.GUI.Button(new Rect(240f, num, 200f, 20f), "WentDark()"))
                    {
                        TheForest.Utils.Scene.SceneTracker.WentDark();
                    }
                    num += 30f; this.scroller += 30;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 300f, 20f), ln.GetText("EnableDeveloperConsole")))
                    {
                        this.toggleConsole();
                    }
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), "Water level: "+ GameObject.FindWithTag("OceanHeight").transform.position.y.ToString()+ "m", this.labelStyle);
                    num += 30f; this.scroller += 30;
                    
                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;

                }
                if (this.Tab == 9) // Game
                {
                    this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 690f, 540f), this.scrollPosition, new Rect(0f, 0f, 670f, this.scroller));
                    this.scroller = 25;
                    num = 10;

                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), ln.GetText("SaveGame")))
                    {
                        UCheatmenu.visible = false;
                        LocalPlayer.Stats.JustSave();
                    }
                    num += 50f; this.scroller += 50;


                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), "Ultimate Cheatmenu", labelStyle);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), ln.GetText("Save")))
                    {
                        saveSettings();
                    }
                    if (UnityEngine.GUI.Button(new Rect(190f, num, 150f, 20f), ln.GetText("Load")))
                    {
                        loadSettings();
                    }
                    if (UnityEngine.GUI.Button(new Rect(360f, num, 150f, 20f), ln.GetText("Reset")))
                    {
                        resetSettings();
                    }
                    num += 40f; this.scroller += 40;

                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Language", labelStyle);
                    num += 30f; this.scroller += 30;
                    int lnlength = 5;
                    UCheatmenu.LanguageLocale = UnityEngine.GUI.SelectionGrid(new Rect(20f, num, 500f, 60f), UCheatmenu.LanguageLocale, LanguageLocaleOptions, lnlength, UnityEngine.GUI.skin.toggle);
                    num += 70f; this.scroller += 70;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 200f, 20f), "Set Language"))
                    {
                        initl18n();
                    }
                    num += 40f; this.scroller += 40;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("Veganmode"), labelStyle);
                    Cheats.NoEnemies = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), Cheats.NoEnemies, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), ln.GetText("NoSurvivalFeatures"), labelStyle);
                    Cheats.NoSurvival = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), Cheats.NoSurvival, "");
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("GameMode")+" (" + GameSetup.Game.ToString() + ")", labelStyle);
                    num += 30f; this.scroller += 30;
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
                    num += 30f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("DifficultyMode")+" (" + GameSetup.Difficulty.ToString() + ")", labelStyle);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), "Peaceful"))
                    {
                        this._setDifficultyMode("peaceful");
                        TheForest.Utils.Settings.GameSettings.Survival.Refresh();
                        TheForest.Utils.Settings.GameSettings.Animals.Refresh();
                        TheForest.Utils.Settings.GameSettings.Ai.Refresh();
                    }
                    if (UnityEngine.GUI.Button(new Rect(190f, num, 150f, 20f), "Normal"))
                    {
                        this._setDifficultyMode("normal");
                        TheForest.Utils.Settings.GameSettings.Survival.Refresh();
                        TheForest.Utils.Settings.GameSettings.Animals.Refresh();
                        TheForest.Utils.Settings.GameSettings.Ai.Refresh();
                    }
                    if (UnityEngine.GUI.Button(new Rect(360f, num, 150f, 20f), "Hard"))
                    {
                        this._setDifficultyMode("hard");
                        TheForest.Utils.Settings.GameSettings.Survival.Refresh();
                        TheForest.Utils.Settings.GameSettings.Animals.Refresh();
                        TheForest.Utils.Settings.GameSettings.Ai.Refresh();
                    }
                    num += 30f; this.scroller += 30;



                    UnityEngine.GUI.Label(new Rect(50f, num, 500f, 20f), new GUIContent(ln.GetText("Crosshair"), ln.GetText("CrosshairDesc")), this.labelStyle);
                    UCheatmenu.CrosshairToggle = UnityEngine.GUI.Toggle(new Rect(20f, num, 20f, 30f), UCheatmenu.CrosshairToggle, "");
                    num += 30f; this.scroller += 30;
                    /*
                    UCheatmenu.CrosshairType = UnityEngine.GUI.SelectionGrid(new Rect(20f, num, 300f, 30f), UCheatmenu.CrosshairType, CrosshairOptions, CrosshairOptions.Length, UnityEngine.GUI.skin.toggle);
                    num += 30f; this.scroller += 30;
                    */
                    UnityEngine.GUI.Label(new Rect(20f, num, 50f, 20f), "R", this.labelStyle);
                    UnityEngine.GUI.Label(new Rect(80f, num, 50f, 20f), "G", this.labelStyle);
                    UnityEngine.GUI.Label(new Rect(140f, num, 50f, 20f), "B", this.labelStyle);
                    UnityEngine.GUI.Label(new Rect(200f, num, 100f, 20f), ln.GetText("Opacity")+" (" + UCheatmenu.CrosshairI.ToString("0.0") + ")", this.labelStyle);
                    UnityEngine.GUI.Label(new Rect(360f, num, 100f, 20f), ln.GetText("Size")+" (" + UCheatmenu.CrosshairSize.ToString("0") + ")", this.labelStyle);
                    num += 30f; this.scroller += 30;
                    UCheatmenu.CrosshairR = Convert.ToInt32(UnityEngine.GUI.TextField(new Rect(20f, num, 50f, 30f), UCheatmenu.CrosshairR.ToString(), UnityEngine.GUI.skin.textField));
                    UCheatmenu.CrosshairG = Convert.ToInt32(UnityEngine.GUI.TextField(new Rect(80f, num, 50f, 30f), UCheatmenu.CrosshairG.ToString(), UnityEngine.GUI.skin.textField));
                    UCheatmenu.CrosshairB = Convert.ToInt32(UnityEngine.GUI.TextField(new Rect(140f, num, 50f, 30f), UCheatmenu.CrosshairB.ToString(), UnityEngine.GUI.skin.textField));
                    UCheatmenu.CrosshairI = UnityEngine.GUI.HorizontalSlider(new Rect(200f, num + 3f, 150f, 30f), UCheatmenu.CrosshairI, 0f, 1f);
                    UCheatmenu.CrosshairSize = UnityEngine.GUI.HorizontalSlider(new Rect(360f, num + 3f, 150f, 30f), UCheatmenu.CrosshairSize, 0f, 100f);
                    num += 30f; this.scroller += 30;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 150f, 20f), ln.GetText("Apply")))
                    {
                        crossStyle = null;
                    }
                    num += 50f; this.scroller += 30;

                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("MoreUCMFunctions"), this.labelStyle);
                    num += 20f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("InfiniteGardenSize"), this.labelStyle);
                    num += 20f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("InfiniteBuildingHeight"), this.labelStyle);
                    num += 20f; this.scroller += 30;
                    UnityEngine.GUI.Label(new Rect(20f, num, 500f, 20f), ln.GetText("Scrollwheel"), this.labelStyle);
                    num += 20f; this.scroller += 30;

                    UnityEngine.GUI.EndScrollView();
                    UnityEngine.GUI.matrix = matrix;

                }
                /* Tooltips */
                if (UnityEngine.GUI.tooltip != "")
                {
                    UnityEngine.GUI.skin.label.wordWrap = true;
                    float tsize = UnityEngine.GUI.skin.label.CalcHeight(new GUIContent(UnityEngine.GUI.tooltip), 680f);
                    UnityEngine.GUI.Box(new Rect(10f, 610f, 700f, (30f + tsize + 10f)), "", UnityEngine.GUI.skin.window);
                    UnityEngine.GUI.Label(new Rect(20f, 615f, 680f, 20f), ln.GetText("Tooltip"));
                    UnityEngine.GUI.Label(new Rect(20f, 640f, 680f, tsize), UnityEngine.GUI.tooltip);
                    UnityEngine.GUI.skin.label.wordWrap = false;
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
            if (UCheatmenu.FreeCam && !UCheatmenu.visible)
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
                            raycastHit.collider.GetComponent<CutTreeSmall>() != null ||
                            raycastHit.collider.GetComponent<BushDamage>() != null
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
                        else if (UCheatmenu.SphereSuitcases && raycastHit.collider.GetComponent<SuitCase>() != null)
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
                                lightEffigy.Effigy = raycastHit.collider.GetComponentInParent<BoltEntity>();
                                lightEffigy.Send();
                            }
                            else
                            {
                                raycastHit.collider.gameObject.SendMessage("lightEffigyReal");
                            }
                        }
                        /* SphereTraps */
                        else if (UCheatmenu.SphereTraps && raycastHit.collider.GetComponent<ResetTraps>() != null)
                        {
                            if (BoltNetwork.isRunning)
                            {
                                ResetTrap resetTrap = ResetTrap.Create(GlobalTargets.OnlyServer);
                                resetTrap.TargetTrap = raycastHit.collider.GetComponentInParent<ResetTraps>().entity;
                                resetTrap.Send();
                            }
                            else
                            {
                                raycastHit.collider.GetComponentInParent<ResetTraps>().RestoreSafe();
                            }
                        }
                        /* SphereCrane */
                        else if (UCheatmenu.SphereCrane && raycastHit.collider.GetComponent<CraneTrigger>() != null)
                        {
                            float direction;
                            float playerheight = (LocalPlayer.GameObject.transform.position.y);
                            float craneheight = raycastHit.collider.GetComponent<CraneTrigger>().transform.position.y;
                            direction = playerheight - craneheight;

                            for (int h = 0; h <= 250; h++)
                            {
                                craneheight = raycastHit.collider.GetComponent<CraneTrigger>().transform.position.y;
                                if (BoltNetwork.isRunning)
                                {
                                    raycastHit.collider.GetComponent<CraneTrigger>().SendMessage("TryMove", direction);
                                    raycastHit.collider.GetComponent<CraneTrigger>().SendMessage("TryMoveMp", direction);
                                }
                                else
                                {
                                    raycastHit.collider.GetComponent<CraneTrigger>().SendMessage("TryMove", direction);
                                }
                                if (Convert.ToInt32(playerheight) == Convert.ToInt32(craneheight)) break;
                            }

                        }
                        /* SphereHolders */
                        else if (UCheatmenu.SphereHolders && raycastHit.collider.GetComponent<LogHolder>() != null)
                        {
                            raycastHit.collider.gameObject.SendMessage("ResetLogs");
                            for (int y = 0; y < 7; y++)
                            {
                                if (BoltNetwork.isRunning)
                                {
                                    ItemHolderAddItem itemHolderAddItem = ItemHolderAddItem.Create(GlobalTargets.OnlyServer);
                                    itemHolderAddItem.Target = raycastHit.collider.GetComponent<LogHolder>().entity;
                                    itemHolderAddItem.Send();
                                }
                                else
                                {

                                    raycastHit.collider.GetComponent<LogHolder>().SendMessage("addLogs");
                                    raycastHit.collider.GetComponent<LogHolder>().LogRender[y].SetActive(true);
                                    raycastHit.collider.gameObject.SendMessage("RefreshMassAndDrag");
                                }
                            }
                        }
                        /*stick holders / rock holders / log holders / arrow baskets / bone baskets / log sleds / tree sap collectors / water collector
                         UCheatmenu.SphereHoldersType
                         */
                        else if (UCheatmenu.SphereHolders && raycastHit.collider.GetComponent<MultiHolder>() != null)
                        {
                            int content = raycastHit.collider.GetComponent<MultiHolder>()._contentActual;

                            /* reset content */
                            raycastHit.collider.GetComponent<MultiHolder>()._contentActual = 0;
                            for (int y = 0; y < raycastHit.collider.GetComponent<MultiHolder>().LogRender.Length; y++)
                            {
                                if (BoltNetwork.isRunning)
                                {
                                    raycastHit.collider.GetComponent<MultiHolder>()._contentTypeActual = MultiHolder.ContentTypes.Log;
                                    ItemHolderTakeItem itemHolderTakeItem = ItemHolderTakeItem.Create(GlobalTargets.OnlyServer);
                                    itemHolderTakeItem.ContentType = (int)raycastHit.collider.GetComponent<MultiHolder>()._contentTypeActual;
                                    itemHolderTakeItem.Target = raycastHit.collider.GetComponent<MultiHolder>().entity;
                                    itemHolderTakeItem.Player = null;
                                    itemHolderTakeItem.Send();
                                }
                                raycastHit.collider.GetComponent<MultiHolder>().LogRender[y].SetActive(false);
                            }
                            for (int y = 0; y < raycastHit.collider.GetComponent<MultiHolder>().RockRender.Length; y++)
                            {
                                if (BoltNetwork.isRunning)
                                {
                                    raycastHit.collider.GetComponent<MultiHolder>()._contentTypeActual = MultiHolder.ContentTypes.Rock;
                                    ItemHolderTakeItem itemHolderTakeItem = ItemHolderTakeItem.Create(GlobalTargets.OnlyServer);
                                    itemHolderTakeItem.ContentType = (int)raycastHit.collider.GetComponent<MultiHolder>()._contentTypeActual;
                                    itemHolderTakeItem.Target = raycastHit.collider.GetComponent<MultiHolder>().entity;
                                    itemHolderTakeItem.Player = null;
                                    itemHolderTakeItem.Send();
                                }
                                raycastHit.collider.GetComponent<MultiHolder>().RockRender[y].SetActive(false);
                            }
                            for (int y = 0; y < raycastHit.collider.GetComponent<MultiHolder>().StickRender.Length; y++)
                            {
                                if (BoltNetwork.isRunning)
                                {
                                    raycastHit.collider.GetComponent<MultiHolder>()._contentTypeActual = MultiHolder.ContentTypes.Stick;
                                    ItemHolderTakeItem itemHolderTakeItem = ItemHolderTakeItem.Create(GlobalTargets.OnlyServer);
                                    itemHolderTakeItem.ContentType = (int)raycastHit.collider.GetComponent<MultiHolder>()._contentTypeActual;
                                    itemHolderTakeItem.Target = raycastHit.collider.GetComponent<MultiHolder>().entity;
                                    itemHolderTakeItem.Player = null;
                                    itemHolderTakeItem.Send();
                                }
                                raycastHit.collider.GetComponent<MultiHolder>().StickRender[y].SetActive(false);
                            }

                            content = 0;

                            if (UCheatmenu.SphereHoldersType == 0) // LOG
                            {
                                raycastHit.collider.GetComponent<MultiHolder>()._contentTypeActual = MultiHolder.ContentTypes.Log;
                                for (int y = content; y < raycastHit.collider.GetComponent<MultiHolder>().LogRender.Length; y++)
                                {
                                    if (BoltNetwork.isRunning)
                                    {
                                        ItemHolderAddItem itemHolderAddItem = ItemHolderAddItem.Create(GlobalTargets.OnlyServer);
                                        itemHolderAddItem.ContentType = 1;
                                        itemHolderAddItem.Target = raycastHit.collider.GetComponent<MultiHolder>().entity;
                                        itemHolderAddItem.Send();
                                    }
                                    else
                                    {
                                        raycastHit.collider.GetComponent<MultiHolder>()._contentActual++;
                                        raycastHit.collider.GetComponent<MultiHolder>().LogRender[y].SetActive(true);
                                        raycastHit.collider.gameObject.SendMessage("RefreshMassAndDrag");
                                    }
                                }
                            }
                            else if (UCheatmenu.SphereHoldersType == 1) // ROCK
                            {
                                raycastHit.collider.GetComponent<MultiHolder>()._contentTypeActual = MultiHolder.ContentTypes.Rock;
                                for (int y = content; y < raycastHit.collider.GetComponent<MultiHolder>().RockRender.Length; y++)
                                {
                                    if (BoltNetwork.isRunning)
                                    {
                                        ItemHolderAddItem itemHolderAddItem = ItemHolderAddItem.Create(GlobalTargets.OnlyServer);
                                        itemHolderAddItem.ContentType = 3;
                                        itemHolderAddItem.Target = raycastHit.collider.GetComponent<MultiHolder>().entity;
                                        itemHolderAddItem.Send();
                                    }
                                    else
                                    {
                                        raycastHit.collider.GetComponent<MultiHolder>()._contentActual++;
                                        raycastHit.collider.GetComponent<MultiHolder>().RockRender[y].SetActive(true);
                                        raycastHit.collider.gameObject.SendMessage("RefreshMassAndDrag");
                                    }
                                }
                            }
                            else if (UCheatmenu.SphereHoldersType == 2) // STICK
                            {
                                raycastHit.collider.GetComponent<MultiHolder>()._contentTypeActual = MultiHolder.ContentTypes.Stick;
                                for (int y = content; y < raycastHit.collider.GetComponent<MultiHolder>().StickRender.Length; y++)
                                {
                                    if (BoltNetwork.isRunning)
                                    {
                                        ItemHolderAddItem itemHolderAddItem = ItemHolderAddItem.Create(GlobalTargets.OnlyServer);
                                        itemHolderAddItem.ContentType = 4;
                                        itemHolderAddItem.Target = raycastHit.collider.GetComponent<MultiHolder>().entity;
                                        itemHolderAddItem.Send();
                                    }
                                    else
                                    {
                                        raycastHit.collider.GetComponent<MultiHolder>()._contentActual++;
                                        raycastHit.collider.GetComponent<MultiHolder>().StickRender[y].SetActive(true);
                                        raycastHit.collider.gameObject.SendMessage("RefreshMassAndDrag");
                                    }
                                }
                            }
                        }
                        else if (UCheatmenu.SphereHolders && raycastHit.collider.GetComponent<ItemHolder>() != null)
                        {
                            for (int y = raycastHit.collider.GetComponent<ItemHolder>().Items; y < raycastHit.collider.GetComponent<ItemHolder>().ItemsRender.Length; y++)
                            {
                                if (BoltNetwork.isRunning)
                                {
                                    ItemHolderAddItem itemHolderAddItem = ItemHolderAddItem.Create(GlobalTargets.OnlyServer);
                                    itemHolderAddItem.Target = raycastHit.collider.GetComponent<ItemHolder>().entity;
                                    itemHolderAddItem.Send();
                                }
                                else
                                {
                                    raycastHit.collider.GetComponent<ItemHolder>().Items++;
                                    raycastHit.collider.GetComponent<ItemHolder>().ItemsRender[raycastHit.collider.GetComponent<ItemHolder>().Items - 1].SetActive(true);
                                }
                            }
                        }
                        else if (UCheatmenu.SphereHolders && raycastHit.collider.GetComponent<RainCollector>() != null)
                        {
                            raycastHit.collider.GetComponent<RainCollector>()._source.AddWater(10);
                        }
                        else if (UCheatmenu.SphereHolders && raycastHit.collider.GetComponent<TreesapSource>() != null)
                        {
                            raycastHit.collider.GetComponent<TreesapSource>().AddTreesap(30f);
                        }
                        else if (UCheatmenu.SphereHolders && raycastHit.collider.GetComponent<ACS::RabbitCage>() != null)
                        {
                            raycastHit.collider.GetComponent<ACS::RabbitCage>().SendMessage("AddRabbit");
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
            if (UCheatmenu.FixSanity)
            {
                LocalPlayer.Stats.Sanity.CurrentSanity = UCheatmenu.FixedSanity;
            }
            if (UCheatmenu.FixStrength)
            {
                LocalPlayer.Stats.PhysicalStrength.CurrentStrength = UCheatmenu.FixedStrength;
            }
            if (UCheatmenu.FixWeight)
            {
                LocalPlayer.Stats.PhysicalStrength.CurrentWeight = UCheatmenu.FixedWeight;
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
            if (UCheatmenu.EnableEffigies)
            {
                LocalPlayer.SavedData.ReachedLowSanityThreshold.SetValue(true);
            }
            else
            {
                if (LocalPlayer.Stats.Sanity.CurrentSanity >= 90)
                {
                    LocalPlayer.SavedData.ReachedLowSanityThreshold.SetValue(false);
                }
            }
            if (UCheatmenu.EnableEndgame)
            {
                LocalPlayer.SavedData.ExitedEndgame.SetValue(true);
            }
            if (ModAPI.Input.GetButtonDown("OpenMenu"))
            {
                if (this.firsttime)
                {
                    loadSettings();

                    initl18n();

                    this.firsttime = false;
                }

                if (UCheatmenu.visible)
                {
                    LocalPlayer.FpCharacter.UnLockView();
                }
                else
                {
                    LocalPlayer.FpCharacter.LockView(true);
                }
                UCheatmenu.visible = !UCheatmenu.visible;
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

            if (ModAPI.Input.GetButtonDown("Respawn") || ModAPI.Input.GetButton("RespawnInfinite"))
            {
                if (!ChatBox.IsChatOpen)
                {
                    Vector3 spawnposition;
                    if (UCheatmenu.SpawnLookAt)
                    {
                        spawnposition = getRayPoint();
                    }
                    else
                    {
                        spawnposition = LocalPlayer.MainCam.transform.position + LocalPlayer.MainCam.transform.forward * 2f + LocalPlayer.MainCam.transform.up * -3f;
                    }

                    if (UCheatmenu.lastObjectType == "animal")
                    {
                        GameObject.Instantiate(AnimalPrefabs[UCheatmenu.lastObject], LocalPlayer.MainCam.transform.position + LocalPlayer.MainCam.transform.forward * 2f, Quaternion.identity);
                    }
                    else if (UCheatmenu.lastObjectType == "mutant")
                    {
                        _spawnmutant(UCheatmenu.lastObject);
                    }
                    else if (UCheatmenu.lastObjectType == "item")
                    {
                        _additem(UCheatmenu.lastObject, 1);
                    }
                    else if (UCheatmenu.lastObjectType == "spawnprop")
                    {
                        GameObject.Instantiate(PropPrefabs[UCheatmenu.lastObject], spawnposition, Quaternion.identity);
                    }
                    else if (UCheatmenu.lastObjectType == "spawnmp")
                    {
                        BoltNetwork.Instantiate(UCheatmenu.lastObjectPrefab, spawnposition, Quaternion.identity);
                    }
                    else if (UCheatmenu.lastObjectType == "spawnitem")
                    {
                        LocalPlayer.Inventory.FakeDrop(Int32.Parse(UCheatmenu.lastObject), null);
                    }
                    else if (UCheatmenu.lastObjectType == "spawnobject")
                    {
                        GameObject.Instantiate(UCheatmenu.lastObjectGObject, spawnposition, Quaternion.identity);
                    }
                }
            }

        }

        private Vector3 getRayPoint()
        {
            Camera camera = LocalPlayer.MainCam;
            Ray ray = camera.ScreenPointToRay(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0f));
            ray.origin += camera.transform.forward * 1f;
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 1000f))
            {
                return raycastHit.point;
            }
            return LocalPlayer.MainCam.transform.position + LocalPlayer.MainCam.transform.forward * 2f + LocalPlayer.MainCam.transform.up * -3f;
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
            
            PropNames.Clear();
            PropPrefabs.Clear();


            GreebleZone[] zones = (GreebleZone[]) UnityEngine.Resources.FindObjectsOfTypeAll(typeof(GreebleZone));
            foreach (GreebleZone zone in zones)
            {
                if (zone != null)
                {
                    foreach (GreebleDefinition definition in zone.GreebleDefinitions)
                    {
                        if (definition != null)
                        {
                            if (definition.Prefab != null)
                            {
                                string name = definition.Prefab.name;
                                if (!PropNames.Contains(name) && !PropPrefabs.ContainsKey(name))
                                {
                                    PropNames.Add(name);
                                    PropPrefabs.Add(name, definition.Prefab);
                                }
                            }
                        }
                    }
                }
            }

            PropNames.Sort();
        }

        [ExecuteOnGameStart]
        public static void FindObjects()
        {
            if (!initobjects)
            {
                GameObject[] go = (GameObject[]) UnityEngine.Resources.FindObjectsOfTypeAll(typeof(GameObject));
                List<GameObject> obj = go.ToList();

                if (obj.Count > 0)
                {
                    AllObjects = obj
                        .Where(m => m != null)
                        .GroupBy(p => p.name)
                        .Select(g => g.FirstOrDefault())
                        .OrderBy(o => o.name)
                        .ToList();
                }
            }
            initobjects = true;

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

        private void toggleConsole()
        {
            global::Cheats.DebugConsole = !global::Cheats.DebugConsole;
            DebugConsole exists = UnityEngine.Object.FindObjectOfType<DebugConsole>();
            if (global::Cheats.DebugConsole)
            {
                if (!exists)
                {
                    GameObject gameObject = new GameObject("DebugConsole");
                    gameObject.AddComponent<DebugConsole>();
                    UnityEngine.Object.DontDestroyOnLoad(gameObject);
                }
            }
            else if (exists)
            {
                //UnityEngine.Object.Destroy(exists);
            }
        }

        private void _spawnmutant(string type)
        {
            UCheatmenu.lastObjectType = "mutant";
            UCheatmenu.lastObject = type;
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
                UCheatmenu.lastObjectType = "item";
                UCheatmenu.lastObject = nameOrId;
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
                    if (list[wepBonus] == "None")
                    {
                        LocalPlayer.Inventory.AddItem(item._id, amount, false, false, null);
                    }
                    else
                    {
                        try
                        {
                            ItemProperties iprop = new ItemProperties();
                            WeaponStatUpgrade.Types wtype =
                                (WeaponStatUpgrade.Types) System.Enum.Parse(typeof(WeaponStatUpgrade.Types),
                                    list[wepBonus]);
                            iprop.ActiveBonus = wtype;
                            iprop.ActiveBonusValue = 100;

                            LocalPlayer.Inventory.AddItem(item._id, amount, false, false, iprop);
                        }
                        catch(Exception e)
                        {
                            LocalPlayer.Inventory.AddItem(item._id, amount, false, false, null);
                        }
                    }
                    

                    //LocalPlayer.Inventory.AddItem(item._id, amount, false, false, null);
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
                    if (item._maxAmount >= 0 && LocalPlayer.Inventory.InventoryItemViewsCache.ContainsKey(item._id))
                    {
                        if (list[wepBonus] == "None")
                        {
                            LocalPlayer.Inventory.AddItem(item._id, 100000, false, false, null);
                        }
                        else
                        {
                            try
                            {
                                ItemProperties iprop = new ItemProperties();
                                WeaponStatUpgrade.Types wtype =
                                    (WeaponStatUpgrade.Types)System.Enum.Parse(typeof(WeaponStatUpgrade.Types),
                                        list[wepBonus]);
                                iprop.ActiveBonus = wtype;
                                iprop.ActiveBonusValue = 100;

                                LocalPlayer.Inventory.AddItem(item._id, 100000, false, false, iprop);
                            }
                            catch (Exception e)
                            {
                                LocalPlayer.Inventory.AddItem(item._id, 100000, false, false, null);
                            }
                        }
                        //LocalPlayer.Inventory.AddItem(item._id, 100000, true, false, null);
                    }
                }
                catch (Exception e)
                {
                }
            }
        }

        private void _removeitem(string nameOrId, bool all = false)
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
                    LocalPlayer.Inventory.RemoveItem(item._id, all?LocalPlayer.Inventory.AmountOfNF(item._id, false):1, true, true);
                }
                else
                {
                }
            }
            else
            {
            }
        }


        private void _removeAllItems()
        {
            Item[] items = ItemDatabase.Items;
            for (int i = 0; i < items.Length; i++)
            {
                Item item = items[i];
                try
                {
                    if (item._maxAmount >= 0 && LocalPlayer.Inventory.InventoryItemViewsCache.ContainsKey(item._id))
                    {
                        LocalPlayer.Inventory.RemoveItem(item._id, LocalPlayer.Inventory.AmountOfNF(item._id, false), true, true);
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

        private void _addClothingById(string param)
        {
            string[] array = param.Split(new char[]
            {
                ' '
            });
            List<int> list = new List<int>();
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string s = array2[i];
                int id;
                if (int.TryParse(s, out id))
                {
                    TheForest.Player.Clothing.ClothingItem addingCloth = TheForest.Player.Clothing.ClothingItemDatabase.ClothingItemById(id);
                    TheForest.Player.Clothing.ClothingItemDatabase.CombineClothingItemWithOutfit(list, addingCloth);
                }
                else
                {
                }
            }
            if (list.Count > 0 && TheForest.Utils.LocalPlayer.Clothing.AddClothingOutfit(list, true))
            {
                TheForest.Utils.LocalPlayer.Clothing.RefreshVisibleClothing();
                //TheForest.Utils.LocalPlayer.Stats.CheckArmsStart();
                //Debug.Log("$> Added outfit successfully");
            }
            else
            {
                //Debug.Log("$> Add outfit failed");
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
            List<GameObject> list = new List<GameObject>(TheForest.Utils.Scene.MutantControler.activeCannibals);
            foreach (GameObject current in TheForest.Utils.Scene.MutantControler.activeInstantSpawnedCannibals)
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
            List<GameObject> list = new List<GameObject>(TheForest.Utils.Scene.MutantControler.activeCannibals);
            foreach (GameObject current in TheForest.Utils.Scene.MutantControler.activeInstantSpawnedCannibals)
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
            List<GameObject> list = new List<GameObject>(TheForest.Utils.Scene.MutantControler.activeCannibals);
            foreach (GameObject current in TheForest.Utils.Scene.MutantControler.activeInstantSpawnedCannibals)
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

            lb_Bird[] arrayB = UnityEngine.Object.FindObjectsOfType<lb_Bird>();
            lb_Bird[] arrayB2 = arrayB;
            for (int i = 0; i < arrayB2.Length; i++)
            {
                lb_Bird birdHealth = arrayB2[i];
                if (birdHealth.gameObject.activeInHierarchy)
                {
                    birdHealth.SendMessage("die");
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
                TheForest.Utils.Scene.MutantControler.enabled = true;
                TheForest.Utils.Scene.MutantControler.startSetupFamilies();
            }
            else
            {
                TheForest.Utils.Scene.MutantControler.StartCoroutine("removeAllEnemies");
                this.Invoke("disableMutantController", 0.5f);
            }

        }

        private void disableMutantController()
        {
            TheForest.Utils.Scene.MutantControler.enabled = false;
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

        private void _plantallgardens(int seed = -1)
        {
            UCheatmenu.seedtype = seed;
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

        private void _growalldirtpiles(float speed = 1)
        {
            TheForest.Buildings.World.GardenDirtPile[] array = UnityEngine.Object.FindObjectsOfType<TheForest.Buildings.World.GardenDirtPile>();
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    UCheatmenu.instgrow = true;
                    UCheatmenu.instgrowspeed = speed;
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


        [ExecuteOnGameStart]
        public static void LoadLanguages()
        {
            if (firsttimeLanguageDownload)
            {
                firsttimeLanguageDownload = false;
                foreach (string code in LanguageCodesOptions)
                {
                    ln.DownloadLanguage(code);
                }
            }
        }

        private void initl18n()
        {
            // https://poeditor.com/
            ln.SetLocale(LanguageCodesOptions[LanguageLocale]);
        }

        private void writeIni(string path)
        {
            INIHelper iniw = new INIHelper(path);
            iniw.Write("UCM", "GodMode",            UCheatmenu.GodMode.ToString());
            iniw.Write("UCM", "UnlimitedFuel",      UCheatmenu.UnlimitedFuel.ToString());
            iniw.Write("UCM", "UnlimitedHairspray", UCheatmenu.UnlimitedHairspray.ToString());
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
            iniw.Write("UCM", "FixHealth",          UCheatmenu.FixHealth.ToString());
            iniw.Write("UCM", "FixBatteryCharge",   UCheatmenu.FixBatteryCharge.ToString());
            iniw.Write("UCM", "FixFullness",        UCheatmenu.FixFullness.ToString());
            iniw.Write("UCM", "FixStamina",         UCheatmenu.FixStamina.ToString());
            iniw.Write("UCM", "FixEnergy",          UCheatmenu.FixEnergy.ToString());
            iniw.Write("UCM", "FixThirst",          UCheatmenu.FixThirst.ToString());
            iniw.Write("UCM", "FixStarvation",      UCheatmenu.FixStarvation.ToString());
            iniw.Write("UCM", "FixBodyTemp",        UCheatmenu.FixBodyTemp.ToString());
            iniw.Write("UCM", "FixSanity",          UCheatmenu.FixSanity.ToString());
            iniw.Write("UCM", "FixWeight",          UCheatmenu.FixWeight.ToString());
            iniw.Write("UCM", "FixStrength",        UCheatmenu.FixStrength.ToString());
            iniw.Write("UCM", "FixedHealth",        UCheatmenu.FixedHealth.ToString());
            iniw.Write("UCM", "FixedBatteryCharge", UCheatmenu.FixedBatteryCharge.ToString());
            iniw.Write("UCM", "FixedFullness",      UCheatmenu.FixedFullness.ToString());
            iniw.Write("UCM", "FixedStamina",       UCheatmenu.FixedStamina.ToString());
            iniw.Write("UCM", "FixedEnergy",        UCheatmenu.FixedEnergy.ToString());
            iniw.Write("UCM", "FixedThirst",        UCheatmenu.FixedThirst.ToString());
            iniw.Write("UCM", "FixedStarvation",    UCheatmenu.FixedStarvation.ToString());
            iniw.Write("UCM", "FixedBodyTemp",      UCheatmenu.FixedBodyTemp.ToString());
            iniw.Write("UCM", "FixedSanity",        UCheatmenu.FixedSanity.ToString());
            iniw.Write("UCM", "FixedWeight",        UCheatmenu.FixedWeight.ToString());
            iniw.Write("UCM", "FixedStrength",      UCheatmenu.FixedStrength.ToString());
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
            iniw.Write("UCM", "SphereTraps",        UCheatmenu.SphereTraps.ToString());
            iniw.Write("UCM", "SphereCrane",        UCheatmenu.SphereCrane.ToString());
            iniw.Write("UCM", "SphereHolders",      UCheatmenu.SphereHolders.ToString());
            iniw.Write("UCM", "SphereHoldersType",  UCheatmenu.SphereHoldersType.ToString());
            iniw.Write("UCM", "InfFire",            UCheatmenu.InfFire.ToString());
            iniw.Write("UCM", "InstLighter",        UCheatmenu.InstLighter.ToString());
            iniw.Write("UCM", "FastFlint",          UCheatmenu.FastFlint.ToString());
            iniw.Write("UCM", "SpawnLookAt",        UCheatmenu.SpawnLookAt.ToString());
            iniw.Write("UCM", "ExplosionRadius",    UCheatmenu.ExplosionRadius.ToString());
            iniw.Write("UCM", "TorchToggle",        UCheatmenu.TorchToggle.ToString());
            iniw.Write("UCM", "TorchR",             UCheatmenu.TorchR.ToString());
            iniw.Write("UCM", "TorchG",             UCheatmenu.TorchG.ToString());
            iniw.Write("UCM", "TorchB",             UCheatmenu.TorchB.ToString());
            iniw.Write("UCM", "TorchI",             UCheatmenu.TorchI.ToString());
            iniw.Write("UCM", "BuildingCollision",  UCheatmenu.BuildingCollision.ToString());
            iniw.Write("UCM", "ItemConsume",        UCheatmenu.ItemConsume.ToString());
            iniw.Write("UCM", "NoUnderwaterBlur",   UCheatmenu.NoUnderwaterBlur.ToString());
            iniw.Write("UCM", "CrosshairToggle",    UCheatmenu.CrosshairToggle.ToString());
            iniw.Write("UCM", "CrosshairType",      UCheatmenu.CrosshairType.ToString());
            iniw.Write("UCM", "CrosshairSize",      UCheatmenu.CrosshairSize.ToString());
            iniw.Write("UCM", "CrosshairR",         UCheatmenu.CrosshairR.ToString());
            iniw.Write("UCM", "CrosshairG",         UCheatmenu.CrosshairG.ToString());
            iniw.Write("UCM", "CrosshairB",         UCheatmenu.CrosshairB.ToString());
            iniw.Write("UCM", "CrosshairI",         UCheatmenu.CrosshairI.ToString());
            iniw.Write("UCM", "TimeToggle",         UCheatmenu.TimeToggle.ToString());
            iniw.Write("UCM", "EnableEffigies",     UCheatmenu.EnableEffigies.ToString());
            iniw.Write("UCM", "EnableEndgame",      UCheatmenu.EnableEffigies.ToString());
            iniw.Write("UCM", "LanguageLocale",     UCheatmenu.LanguageLocale.ToString());
            iniw.Write("UCM", "InfiniteSpawnedLogs",UCheatmenu.InfiniteSpawnedLogs.ToString());
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
            try { UCheatmenu.FixSanity = Convert.ToBoolean(inir.Read("UCM", "FixSanity")); }
            catch { UCheatmenu.FixSanity = false; }
            try { UCheatmenu.FixWeight = Convert.ToBoolean(inir.Read("UCM", "FixWeight")); }
            catch { UCheatmenu.FixWeight = false; }
            try { UCheatmenu.FixStrength = Convert.ToBoolean(inir.Read("UCM", "FixStrength")); }
            catch { UCheatmenu.FixStrength = false; }
            try { UCheatmenu.FixedSanity = Convert.ToSingle(inir.Read("UCM", "FixedSanity")); }
            catch { UCheatmenu.FixedSanity = LocalPlayer.Stats.Sanity.CurrentSanity; }
            try { UCheatmenu.FixedWeight = Convert.ToSingle(inir.Read("UCM", "FixedWeight")); }
            catch { UCheatmenu.FixedWeight = LocalPlayer.Stats.PhysicalStrength.CurrentWeight; }
            try { UCheatmenu.FixedStrength = Convert.ToSingle(inir.Read("UCM", "FixedStrength")); }
            catch { UCheatmenu.FixedStrength = LocalPlayer.Stats.PhysicalStrength.CurrentStrength; }
            try { UCheatmenu.DestroyBuildings = Convert.ToBoolean(inir.Read("UCM", "DestroyBuildings")); }
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
            try { UCheatmenu.SphereTraps = Convert.ToBoolean(inir.Read("UCM", "SphereTraps")); }
            catch { UCheatmenu.SphereTraps = false; }
            try { UCheatmenu.SphereCrane = Convert.ToBoolean(inir.Read("UCM", "SphereCrane")); }
            catch { UCheatmenu.SphereCrane = false; }
            try { UCheatmenu.SphereHolders = Convert.ToBoolean(inir.Read("UCM", "SphereHolders")); }
            catch { UCheatmenu.SphereHolders = false; }
            try { UCheatmenu.SphereHoldersType = Convert.ToInt32(inir.Read("UCM", "SphereHoldersType")); }
            catch { UCheatmenu.SphereHoldersType = 0; }
            try { UCheatmenu.InfFire = Convert.ToBoolean(inir.Read("UCM", "InfFire")); }
            catch { UCheatmenu.InfFire = false; }
            try { UCheatmenu.InstLighter = Convert.ToBoolean(inir.Read("UCM", "InstLighter")); }
            catch { UCheatmenu.InstLighter = false; }
            try { UCheatmenu.FastFlint = Convert.ToBoolean(inir.Read("UCM", "FastFlint")); }
            catch { UCheatmenu.FastFlint = false; }
            try { UCheatmenu.SpawnLookAt = Convert.ToBoolean(inir.Read("UCM", "SpawnLookAt")); }
            catch { UCheatmenu.SpawnLookAt = false; }
            this.DestroyTree = Convert.ToBoolean(inir.Read("UCM", "DestroyTree"));
            this.Teleport = inir.Read("UCM", "Teleport");
            this.tItem = inir.Read("UCM", "tItem");
            this.setNumVar = inir.Read("UCM", "setNumVar");
            this.setCurDay = inir.Read("UCM", "setCurDay");
            this.setCutTrees = inir.Read("UCM", "setCutTrees");
            this.setCGrowGrass = inir.Read("UCM", "setCGrowGrass");
            TheForestAtmosphere.Instance.RotationSpeed = Convert.ToSingle(inir.Read("UCM", "RotationSpeed"));
            try { UCheatmenu.ExplosionRadius = Convert.ToSingle(inir.Read("UCM", "ExplosionRadius")); }
            catch { UCheatmenu.ExplosionRadius = 15f; }
            try { UCheatmenu.TorchToggle = Convert.ToBoolean(inir.Read("UCM", "TorchToggle")); }
            catch { UCheatmenu.TorchToggle = false; }
            try { UCheatmenu.TorchR = Convert.ToInt32(inir.Read("UCM", "TorchR")); }
            catch { UCheatmenu.TorchR = 1; }
            try { UCheatmenu.TorchG = Convert.ToInt32(inir.Read("UCM", "TorchG")); }
            catch { UCheatmenu.TorchG = 1; }
            try { UCheatmenu.TorchB = Convert.ToInt32(inir.Read("UCM", "TorchB")); }
            catch { UCheatmenu.TorchB = 1; }
            try { UCheatmenu.TorchI = Convert.ToSingle(inir.Read("UCM", "TorchI")); }
            catch { UCheatmenu.TorchI = 1.0f; }
            try { UCheatmenu.BuildingCollision = Convert.ToBoolean(inir.Read("UCM", "BuildingCollision")); }
            catch { UCheatmenu.BuildingCollision = true; }
            try { UCheatmenu.ItemConsume = Convert.ToBoolean(inir.Read("UCM", "ItemConsume")); }
            catch { UCheatmenu.ItemConsume = false; }
            try { UCheatmenu.NoUnderwaterBlur = Convert.ToBoolean(inir.Read("UCM", "NoUnderwaterBlur")); }
            catch { UCheatmenu.NoUnderwaterBlur = false; }
            try { UCheatmenu.CrosshairToggle = Convert.ToBoolean(inir.Read("UCM", "CrosshairToggle")); }
            catch { UCheatmenu.CrosshairToggle = false; }
            try { UCheatmenu.CrosshairType = Convert.ToInt32(inir.Read("UCM", "CrosshairType")); }
            catch { UCheatmenu.CrosshairType = 0; }
            try { UCheatmenu.CrosshairSize = Convert.ToSingle(inir.Read("UCM", "CrosshairSize")); }
            catch { UCheatmenu.CrosshairSize = 10; }
            try { UCheatmenu.CrosshairR = Convert.ToInt32(inir.Read("UCM", "CrosshairR")); }
            catch { UCheatmenu.CrosshairR = 0; }
            try { UCheatmenu.CrosshairG = Convert.ToInt32(inir.Read("UCM", "CrosshairG")); }
            catch { UCheatmenu.CrosshairG = 255; }
            try { UCheatmenu.CrosshairB = Convert.ToInt32(inir.Read("UCM", "CrosshairB")); }
            catch { UCheatmenu.CrosshairB = 0; }
            try { UCheatmenu.CrosshairI = Convert.ToSingle(inir.Read("UCM", "CrosshairI")); }
            catch { UCheatmenu.CrosshairI = 0.7f; }
            try { UCheatmenu.TimeToggle = Convert.ToBoolean(inir.Read("UCM", "TimeToggle")); }
            catch { UCheatmenu.TimeToggle = false; }
            try { UCheatmenu.UnlimitedHairspray = Convert.ToBoolean(inir.Read("UCM", "UnlimitedHairspray")); }
            catch { UCheatmenu.UnlimitedHairspray = false; }
            try { UCheatmenu.EnableEffigies = Convert.ToBoolean(inir.Read("UCM", "EnableEffigies")); }
            catch { UCheatmenu.EnableEffigies = false; }
            try { UCheatmenu.EnableEndgame = Convert.ToBoolean(inir.Read("UCM", "EnableEndgame")); }
            catch { UCheatmenu.EnableEndgame = false; }
            try { UCheatmenu.LanguageLocale = Convert.ToInt32(inir.Read("UCM", "LanguageLocale")); }
            catch { UCheatmenu.LanguageLocale = 0; }
            try { UCheatmenu.InfiniteSpawnedLogs = Convert.ToBoolean(inir.Read("UCM", "InfiniteSpawnedLogs")); }
            catch { UCheatmenu.InfiniteSpawnedLogs = false; }
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
