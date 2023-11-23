using System;
using BepInEx.Configuration;



namespace Hoovernt {
    public class PluginConfig {
        public static ConfigEntry<bool> IsModEnabled { get; private set; }
        public static ConfigEntry<bool> DefaultPickupSetting { get; private set; }
        public static void BindConfig(ConfigFile config) {

            PluginConfig.IsModEnabled = config.Bind<bool>("_Global", "isModEnabled", true, "Globally enable or disable this mod.");
            PluginConfig.DefaultPickupSetting = config.Bind<bool>("Player", "Default Autopickup Setting", true, "Autopickup setting that is applied when you log in.");
        }
    }
}