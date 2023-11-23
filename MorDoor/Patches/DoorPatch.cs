using System;
using System.Collections.Generic;
using HarmonyLib;
using static MorDoor.PluginConfig;

using UnityEngine;

using static MorDoor.MorDoor;
using static MeleeWeaponTrail;
using static System.Net.Mime.MediaTypeNames;

namespace MorDoor.Patches {

    [HarmonyPatch(typeof(Door))]
    internal class DoorPatch {

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Door.Interact))]
        static void InteractPrefix(Humanoid character, bool hold, bool alt, Door __instance) {

            if (!IsModEnabled.Value || !PlayerInitiated) {  return; }

            GetDoors(character);
            PlayerInitiated = false;
            foreach (Piece piece in Doors) {

                if (piece.m_nview.gameObject.TryGetComponent(out Door door) && DoubleDoorCheck(__instance,door)){
                    
                    door.Interact(character, hold, alt);                  
                }
            }
            Doors.Clear();
            PlayerInitiated = true;
        }
    }
}