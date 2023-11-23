using System;
using HarmonyLib;

using UnityEngine;
using static Hoovernt.PluginConfig;
using static Hoovernt.Hoovernt;

namespace Hoovernt.Patches
{

    [HarmonyPatch(typeof(Player))]
    internal class PlayerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.OnDeath))]
        static void OnDeathPrefix(ref bool ___m_enableAutoPickup)
        {
            if (!IsModEnabled.Value) {  return; }
            AutoPickup = ___m_enableAutoPickup;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.Awake))]
        static void AwakePostFix(ref bool ___m_enableAutoPickup)
        {
            if (!IsModEnabled.Value) { return; }
            ___m_enableAutoPickup = AutoPickup;
        }
    }
}
