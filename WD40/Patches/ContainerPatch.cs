using System;
using System.Collections.Generic;
using HarmonyLib;
using static WD40.PluginConfig;

using UnityEngine;
using System.Xml.Linq;
using System.Security.Cryptography;


namespace WD40.Patches
{

    [HarmonyPatch(typeof(Container))]
    internal class ContainerPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Container.RPC_RequestOpen))]
        static void RPC_RequestOpenPrefix(long uid, long playerID, Container __instance)
        {

            if (!IsModEnabled.Value || !__instance || !__instance.m_nview || ((__instance.IsInUse() || (__instance.m_wagon && __instance.m_wagon.InUse())) && uid != ZNet.GetUID())) { return; }
            __instance.m_nview.ClaimOwnership();
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Container.RPC_RequestTakeAll))]
        static void RPC_RequestTakeAllPrefix(long uid, long playerID, Container __instance)
        {

            if (!IsModEnabled.Value || !__instance || !__instance.m_nview || ((__instance.IsInUse() || (__instance.m_wagon && __instance.m_wagon.InUse())) && uid != ZNet.GetUID())) { return; }
            __instance.m_nview.ClaimOwnership();
        }
    }
}
