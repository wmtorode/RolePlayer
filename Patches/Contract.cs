using RolePlayer;
using BattleTech;

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
