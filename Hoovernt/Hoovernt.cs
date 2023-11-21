using BepInEx;

using HarmonyLib;

using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Reflection;



namespace Hoovernt
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Hoovernt : BaseUnityPlugin
    {
        public const string PluginGuid = "mayo.is.an.instrument.hoovernt";
        public const string PluginName = "Hoovern't";
        public const string PluginVersion = "1.0.0";
        public static bool AutoPickup;

        Harmony _harmony;
        

        public void Awake()
        {
            //BindConfig(Config);

            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), harmonyInstanceId: PluginGuid);
        }
        public void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }
    }
}