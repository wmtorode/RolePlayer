using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RolePlayer.Data;
using Harmony;

namespace RolePlayer
{
    public static class Main
    {
        internal static Logger modLog;

        public static void Init(string modDir, string settingsJSON)
        {
            var harmony = HarmonyInstance.Create("ca.jwolf.RolePlayer");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            modLog = new Logger(modDir, "RolePlayer", true);

        }
    }
}
