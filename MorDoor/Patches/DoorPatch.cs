using System;
using System.Collections.Generic;
using HarmonyLib;

using UnityEngine;

using static MorDoor.MorDoor;

namespace MorDoor.Patches {

    [HarmonyPatch(typeof(Door))]
    internal class DoorPatch {

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Door.Interact))]
        static void InteractPostfix(Humanoid character, bool hold, bool alt, Door __instance) {

            if (!PlayerInitiated) {

                return;
            }
            GetDoors(character);
            PlayerInitiated = false;
            foreach (Piece piece in Doors) {

                Vector3 origin = __instance.transform.position;
                Vector3 target = piece.transform.position;
                if (Vector3.Distance(origin, target) <= 3f && origin != target && origin.y == target.y
                    && Math.Abs(Math.Abs(Vector3.SignedAngle(origin - target, __instance.transform.forward, Vector3.up))-90) <= 0.1f
                    && piece.m_nview.gameObject.TryGetComponent(out Door door)
                    && Math.Abs(Quaternion.Angle(__instance.transform.rotation, door.transform.rotation) - 180f) <= 0.5f) {

                    door.Interact(character, hold, alt);
                }
            }
            Doors = new List<Piece>();
            PlayerInitiated = true;
        }
    }
}