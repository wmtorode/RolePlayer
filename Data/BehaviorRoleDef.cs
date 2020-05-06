using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleTech;
using Newtonsoft.Json;

namespace RolePlayer.Data
{
    public class BehaviorRoleDef
    {
        public string roleTag;
        public string behaviorFile;
        public List<AIMood> moods;
    }
}
