using System;
using BepInEx.Configuration;



namespace MorDoor {
    public class PluginConfig {
        public static ConfigEntry<bool> IsModEnabled { get; private set; }
        public static void BindConfig(ConfigFile config) {

            PluginConfig.IsModEnabled = config.Bind<bool>("_Global", "isModEnabled", true, "Globally enable or disable this mod.");
        }
    }
}