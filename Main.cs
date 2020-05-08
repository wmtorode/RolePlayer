using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RolePlayer.Data;
using Harmony;
using Newtonsoft.Json;
using System.IO;

namespace RolePlayer
{
    public static class Main
    {
        internal static Logger modLog;
        internal static Settings settings;
        internal static string modDir;

        public static void Init(string modDirectory, string settingsJSON)
        {
         
            modDir = modDirectory;
            modLog = new Logger(modDir, "RolePlayer", true);

            try
            {
                using(StreamReader reader = new StreamReader($"{modDir}/settings.json"))
                {
                    string jdata = reader.ReadToEnd();
                    settings = JsonConvert.DeserializeObject<Settings>(jdata);
                }
                BehaviorVariableManager.Instance.initialize();

            }

            catch (Exception ex)
            {
                modLog.LogException(ex);
            }

            var harmony = HarmonyInstance.Create("ca.jwolf.RolePlayer");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

        }
    }
}
