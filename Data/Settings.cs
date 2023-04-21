using System.Collections.Generic;

namespace RolePlayer.Data
{
    public class Settings
    {
        public string behaviourDirectory = "Behaviours";
        public bool debug = false;
        public bool allowMultiMatch = false;

        public List<BehaviorRoleDef> behaviours = new List<BehaviorRoleDef>();
    }
}
