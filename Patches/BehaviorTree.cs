using RolePlayer;
using Harmony;
using BattleTech;

namespace RolePlayer.Patches
{
        [HarmonyPatch(typeof(BehaviorTree), "GetBehaviorVariableValue")]
        public static class BehaviorTree_GetBehaviorVariableValue_Patch
        {
            public static bool Prefix(BehaviorTree __instance, BehaviorVariableName name, ref BehaviorVariableValue __result)
            {
                var value = BehaviorVariableManager.Instance.getBehaviourVariable(__instance.unit, name);
                if (value == null)
                {
                    return true;
                }

                __result = value;
                return false;
            }
        }
}
