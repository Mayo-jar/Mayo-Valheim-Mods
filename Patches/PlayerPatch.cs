using System;
using HarmonyLib;

using UnityEngine;

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
            AutoPickup = ___m_enableAutoPickup;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.Awake))]
        static void AwakePostFix(ref bool ___m_enableAutoPickup)
        {
            ___m_enableAutoPickup = AutoPickup;
        }
    }
}
