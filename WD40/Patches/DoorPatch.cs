using System;
using System.Collections.Generic;
using HarmonyLib;
using static WD40.PluginConfig;

using UnityEngine;


namespace WD40.Patches {

    [HarmonyPatch(typeof(Door))]
    internal class DoorPatch {

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Door.Interact))]
        static void InteractPrefix(Humanoid character, bool hold, bool alt, Door __instance) {

            if (!IsModEnabled.Value || !character || !__instance || !__instance.m_nview) {  return; }
            __instance.m_nview.ClaimOwnership();
        }
    }
}