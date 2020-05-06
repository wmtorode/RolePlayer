using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayer;
using BattleTech;

namespace RolePlayer.Features
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

        public string behviorsDirectory { get; set; }


    }


}
