using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
