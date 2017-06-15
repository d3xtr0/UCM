using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bolt;
using Steamworks;
using TheForest.Networking;
using TheForest.Utils;
using UnityEngine;

namespace UltimateCheatmenu
{
    public class PlayerManager
    {
        public static List<Player> Players { get; } = new List<Player>();

        public PlayerManager(UCheatmenu instance)
        {
        }

        public Player GetPlayerBySteamId(ulong steamId)
        {
            return Players.FirstOrDefault(o => o.SteamId == steamId);
        }
    }

    public class Player
    {
        private static readonly Dictionary<string, ulong> CachedIds = new Dictionary<string, ulong>();

        public BoltEntity Entity { get; }
        public ulong SteamId =>
            CachedIds.ContainsKey(Name)
                ? CachedIds[Name]
                : (CachedIds[Name] = CoopLobby.Instance.AllMembers.FirstOrDefault(o => SteamFriends.GetFriendPersonaName(o) == Name).m_SteamID);

        public string Name => Entity.GetState<IPlayerState>().name;
        public BoltPlayerSetup PlayerSetup => Entity.GetComponent<BoltPlayerSetup>();
        public CoopPlayerRemoteSetup CoopPlayer => Entity.GetComponent<CoopPlayerRemoteSetup>();

        public GameObject DeadTriggerObject
        {
            get
            {
                var playerSetup = PlayerSetup;
                if (playerSetup != null)
                {
                    var fieldInfo = typeof(BoltPlayerSetup).GetField("RespawnDeadTrigger", BindingFlags.NonPublic | BindingFlags.Instance);
                    var gameObject = fieldInfo?.GetValue(playerSetup) as GameObject;
                    if (gameObject != null)
                    {
                        return gameObject;
                    }
                }
                return null;
            }
        }

        public Transform Transform => Entity.transform;
        public Vector3 Position => Transform.position;
        public NetworkId NetworkId => Entity.networkId;

        public Player(BoltEntity player)
        {
            Entity = player;
        }
    }
}