using BepInEx;

using HarmonyLib;

using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Steamworks;

namespace AAV
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class AAV : BaseUnityPlugin
    {
        public const string PluginGuid = "mayo.is.an.instrument.aav";
        public const string PluginName = "AAV";
        public const string PluginVersion = "1.0.2";
        public static float HPercent;
        public static float SPercent;

        Harmony _harmony;


        public void Awake()
        {
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), harmonyInstanceId: PluginGuid);
        }
        public void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }
    }
}