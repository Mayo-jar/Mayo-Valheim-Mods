using BepInEx;

using HarmonyLib;

using UnityEngine;
using static WD40.PluginConfig;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace WD40
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class WD40 : BaseUnityPlugin
    {
        public const string PluginGuid = "mayo.is.an.instrument.WD40";
        public const string PluginName = "WD40";
        public const string PluginVersion = "1.0.0";

        Harmony _harmony;


        public void Awake()
        {
            BindConfig(Config);

            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), harmonyInstanceId: PluginGuid);
        }
        public void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }
    }
}