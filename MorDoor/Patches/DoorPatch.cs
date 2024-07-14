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

            if (!IsModEnabled.Value || !PlayerInitiated || !character || !__instance || !__instance.m_nview) {  return; }

            Doors = new List<Piece>();
            Piece.GetAllPiecesInRadius(character.transform.position, 10f, Doors);
            __instance.m_nview.ClaimOwnership();
            PlayerInitiated = false;
            foreach (Piece piece in Doors) {

                if(!piece || !piece.m_nview || !piece.m_nview.gameObject) {
                    PlayerInitiated = true;
                    return; }
                if (piece.m_nview.gameObject.TryGetComponent(out Door door) && DoubleDoorCheck(__instance,door)){
                    
                    door.m_nview.ClaimOwnership();
                    door.Interact(character, hold, alt);                  
                }
            }
            Doors.Clear();
            PlayerInitiated = true;
        }
    }
}