using System;
using System.Runtime.CompilerServices;
using HarmonyLib;

using UnityEngine;

using static AAV.AAV;

namespace AAV.Patches
{

    [HarmonyPatch(typeof(Player))]
    internal class PlayerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.CanConsumeItem))]
        static void CanConsumeItemPostfix(Player __instance, ref bool __result, ItemDrop.ItemData item)
        {
            HPercent = __instance.GetHealthPercentage();
            SPercent = __instance.GetStaminaPercentage();
            string name = item.m_shared.m_name;
            if ((name == "$item_mead_hp_major" || name == "$item_mead_hp_medium" || name == "$item_mead_hp_minor") && HPercent >= 0.9)
            {
                ZLog.Log($"Health Percentage is {HPercent * 100} so {name} should not be consumed.");
                __instance.Message(MessageHud.MessageType.Center, "You've had enough", 0, null);
                __result = false;
            }
            if ((name == "$item_mead_stamina_medium" || name == "$item_mead_stamina_minor") && SPercent >= 0.9)
            {
                ZLog.Log($"Stamina Percentage is {SPercent * 100} so {name} should not be consumed.");
                __instance.Message(MessageHud.MessageType.Center, "You've had enough", 0, null);
                __result = false;
            }
        }
    }
}