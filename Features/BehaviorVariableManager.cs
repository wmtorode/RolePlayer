using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using RolePlayer;
using RolePlayer.Data;
using BattleTech;

namespace RolePlayer.Features
{
    public class BehaviorVariableManager

    {
        private static BehaviorVariableManager instance;
        private Dictionary<string, BehaviorVariableScope> scopesByRole;
        private Dictionary<string, List<string>> actorRoleCache;

        public static BehaviorVariableManager Instance
        {
            get
            {
                if (instance == null) instance = new BehaviorVariableManager();
                return instance;
            }
        }


        public void initialize()
        {
            loadBehaviourScopes();
        }

        public void clearCache()
        {
            actorRoleCache.Clear();
        }

        public BehaviorVariableScope getScope(string role)
        {
            if (scopesByRole.ContainsKey(role))
            {
                return scopesByRole[role];
            }
            return (BehaviorVariableScope) null;
        }

        public void loadBehaviourScopes()
        {
            scopesByRole = new Dictionary<string, BehaviorVariableScope>();
            foreach(BehaviorRoleDef behaviourDef in Main.settings.behaviours)
            {
                string filePath = $"{Main.modDir}/{Main.settings.behaviourDirectory}/{behaviourDef.behaviorFile}.json";
                if (File.Exists(filePath))
                {
                    string jData = File.ReadAllText(filePath);

                    BehaviorVariableScope behaviorVariableScope = getScope(behaviourDef.roleTag);
                    if (behaviorVariableScope == null)
                    {
                        scopesByRole[behaviourDef.roleTag] = new BehaviorVariableScope();
                    }
                    behaviorVariableScope.FromJSON(jData);
                    foreach (AIMood mood in behaviourDef.moods)
                    {
                        if (mood != AIMood.Undefined)
                        {
                            behaviorVariableScope.ScopesByMood[mood] = new BehaviorVariableScope();
                            behaviorVariableScope.ScopesByMood[mood].FromJSON(jData);
                        }
                    }
                }
                else
                {
                    Main.modLog.LogError($"Missing Behaviour file: {behaviourDef.behaviorFile}");
                }
            }
        }

        private List<string> getActorTags(AbstractActor actor)
        {
            if (actorRoleCache.ContainsKey(actor.uid))
            {
                return actorRoleCache[actor.uid];
            }
            Main.modLog.DebugMessage($"Cache Miss: {actor.uid}");
            actorRoleCache[actor.uid] = actor.GetTags().ToArray().ToList();

            Mech mech = actor as Mech;
            if(mech != null)
            {
                actorRoleCache[actor.uid] = actorRoleCache[actor.uid].Concat(mech.MechDef.MechTags.ToArray().ToList()).ToList();
            }
            return actorRoleCache[actor.uid];
        }

        public BehaviorVariableValue getBehaviourVariable(AbstractActor actor)
        {
            List<string> tags = getActorTags(actor);
            return (BehaviorVariableValue) null;
        }

    }


}
