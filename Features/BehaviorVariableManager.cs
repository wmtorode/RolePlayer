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

namespace RolePlayer
{
    public class BehaviorVariableManager

    {
        private static BehaviorVariableManager instance;
        private Dictionary<string, BehaviorVariableScope> scopesByRole;
        private Dictionary<string, string> actorRoleCache;

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
            actorRoleCache = new Dictionary<string, string>();
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
            foreach (BehaviorRoleDef behaviourDef in Main.settings.behaviours)
            {
                string filePath = $"{Main.modDir}/{Main.settings.behaviourDirectory}/{behaviourDef.behaviourFile}.json";
                if (File.Exists(filePath))
                {
                    string jData = File.ReadAllText(filePath);

                    BehaviorVariableScope behaviorVariableScope = getScope(behaviourDef.roleTag);
                    if (behaviorVariableScope == null)
                    {
                        behaviorVariableScope = new BehaviorVariableScope();
                        behaviorVariableScope.FromJSON(jData);
                        scopesByRole[behaviourDef.roleTag] = behaviorVariableScope;
                        Main.modLog.LogMessage($"Loaded {behaviourDef.behaviourFile} for tag: {behaviourDef.roleTag}");
                    }
                    
                    foreach (AIMood mood in behaviourDef.moods)
                    {
                        if (mood != AIMood.Undefined)
                        {
                            behaviorVariableScope.ScopesByMood[mood] = new BehaviorVariableScope();
                            behaviorVariableScope.ScopesByMood[mood].FromJSON(jData);
                            Main.modLog.LogMessage($"Applied {behaviourDef.behaviourFile} for tag: {behaviourDef.roleTag}, with Mood {mood.ToString()}");
                        }
                    }
                }
                else
                {
                    Main.modLog.LogError($"Missing Behaviour file: {behaviourDef.behaviourFile}");
                }
            }
        }

        private string getActorTag(AbstractActor actor)
        {
            if (actorRoleCache.ContainsKey(actor.uid))
            {
                return actorRoleCache[actor.uid];
            }
            Main.modLog.DebugMessage($"Cache Miss: {actor.uid}");
            List<string> tags = actor.GetTags().ToArray().ToList();

            Mech mech = actor as Mech;
            if(mech != null)
            {
                tags = tags.Concat(mech.MechDef.MechTags.ToArray().ToList()).ToList();
            }
            foreach (string tag in tags)
            {
                if (scopesByRole.ContainsKey(tag))
                {
                    actorRoleCache[actor.uid] = tag;
                    return tag;
                }
            }
            actorRoleCache[actor.uid] = (string) null;
            return (string) null;
        }

        public BehaviorVariableValue getBehaviourVariable(AbstractActor actor, BehaviorVariableName name)
        {
            string tag = getActorTag(actor);
            if (tag != null)
            {

                if (scopesByRole.ContainsKey(tag))
                {
                    BehaviorVariableScope roleScope = scopesByRole[tag];
                    BehaviorVariableValue roleValue = roleScope.GetVariableWithMood(name, actor.BehaviorTree.mood);
                    if (roleValue != null)
                    {
                        if (Main.settings.debug)
                        {
                            Main.modLog.DebugMessage($"Hit for Var: {name.ToString()}");
                        }
                        return roleValue;
                    }
                }
                if (Main.settings.debug)
                {
                    Main.modLog.DebugMessage($"Hit for Var: {name.ToString()}");
                }
            }
            else
            {
                if (Main.settings.debug)
                {
                    Main.modLog.DebugMessage("actor has no defined role tag");
                }
            }
            return (BehaviorVariableValue) null;
        }

    }


}
