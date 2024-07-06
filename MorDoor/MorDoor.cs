using BepInEx;

using HarmonyLib;

using UnityEngine;
using static MorDoor.PluginConfig;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace MorDoor
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class MorDoor : BaseUnityPlugin
    {
        public const string PluginGuid = "mayo.is.an.instrument.MorDoor";
        public const string PluginName = "MorDoor";
        public const string PluginVersion = "2.0.0";
        public static List<Piece> Doors = new List<Piece>();
        public static List<Piece> Pieces = new List<Piece>();
        public static bool PlayerInitiated = true;

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

        public static void GetDoors(Humanoid character) {

            Piece.GetAllPiecesInRadius(character.transform.position, 10f, Pieces);
            foreach(Piece piece in Pieces) {

                if (piece.m_nview.gameObject.TryGetComponent(out Door door)) {

                    Doors.Add(piece);
                }
            }
            Pieces = new List<Piece>();
        }
        
        public static bool DoubleDoorCheck(Door point, Door next) {

            if (Vector3.Distance(point.transform.position, next.transform.position) <= 5f   
                && point.transform.position != next.transform.position         
                && point.transform.position.y == next.transform.position.y  
                && Math.Abs(Math.Abs(Vector3.SignedAngle(point.transform.position - next.transform.position, point.transform.forward, Vector3.up)) - 90) <= 0.1f
                && Math.Abs(Quaternion.Angle(point.transform.rotation, next.transform.rotation) - 180f) <= 0.5f
                && point.m_nview.GetZDO().GetInt(ZDOVars.s_state, 0) + next.m_nview.GetZDO().GetInt(ZDOVars.s_state, 0) == 0f) {

                return true;
            }
            return false;
        }
    }
}