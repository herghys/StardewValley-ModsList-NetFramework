using StardewValleyModList.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StardewValleyModList.Helper
{
    public static class Globals
    {
        public static string StardewDirectory = "";
        public static string StardewModsDirectory = "";

        public static List<ModsDataModel> MyMods { get; set; } = new List<ModsDataModel>();
        public static List<ModsDataModel> OtherPlayerMods { get; set; } = new List<ModsDataModel>();
    }

    public static class AppContants
    {
        public const string ModsFolder = "Mods";
    }
}
