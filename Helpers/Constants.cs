using UnityEngine;

namespace squishOS.Helpers
{
    public class Constants
    {
        public const string DefaultShaderName = "Standard";
        public const string CustomMapsFolderName = "Mods";
        public const string PluginVersion = "1.0.0";
        public static ModInfo ModInfoError = new MapInfo
        {
            PackageInfo = new MapPackageInfo
            {
                Descriptor = new Descriptor
                {
                   Name = "[error]",
                }
            }
        };
    }
}