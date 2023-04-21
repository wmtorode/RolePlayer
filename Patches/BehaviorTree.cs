using RolePlayer;
using BattleTech;

namespace RolePlayer.Patches
{
        [HarmonyPatch(typeof(BehaviorTree), "GetBehaviorVariableValue")]
        public static class BehaviorTree_GetBehaviorVariableValue_Patch
        {
            public static void Prefix(ref bool __runOriginal, BehaviorTree __instance, BehaviorVariableName name, ref BehaviorVariableValue __result)
            {
                
                if (!__runOriginal)
                {
                    return;
                }
                
                var value = BehaviorVariableManager.Instance.getBehaviourVariable(__instance.unit, name);
                if (value == null)
                {
                    return;
                }

                __result = value;
                __runOriginal = false;
            }
        }
}
