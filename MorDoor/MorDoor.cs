using BepInEx;

using HarmonyLib;

using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Reflection;



namespace MorDoor
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class MorDoor : BaseUnityPlugin
    {
        public const string PluginGuid = "mayo.is.an.instrument.MorDoor";
        public const string PluginName = "MorDoor";
        public const string PluginVersion = "1.0.0";
        public static List<Piece> Doors = new List<Piece>();
        public static List<Piece> Pieces = new List<Piece>();
        public static bool PlayerInitiated = true;

        Harmony _harmony;


        public void Awake()
        {
            //BindConfig(Config);

            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), harmonyInstanceId: PluginGuid);
        }
        public void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }

        public static void GetDoors(Humanoid character) {

            Piece.GetAllPiecesInRadius(character.transform.position, 10f, Pieces);
            foreach(Piece piece in Pieces) {

                if (piece.m_name == "$piece_wooddoor"
                    || piece.m_name == "$piece_woodgate"
                    || piece.m_name == "$piece_irongate"
                    || piece.m_name == "$piece_woodwindowshutter"
                    || piece.m_name == "$piece_darkwoodgate") {

                    Doors.Add(piece);
                }
            }
            Pieces = new List<Piece>();
        }
        
    }
}