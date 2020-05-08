using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayer;
using BattleTech;
using Harmony;

namespace RolePlayer.Patches
{
    [HarmonyPatch(typeof(Contract), "CompleteContract")]
    public static class Contract_CompleteContract_Patch
    {
        static void Postfix()
        {
            BehaviorVariableManager.Instance.clearCache();
        }
    }
}
