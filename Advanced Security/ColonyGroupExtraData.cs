using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Security
{
    public class ColonyGroupExtraData
    {
        public string colonyGroupID;
        public bool autoSetDifficulty = true;

        public ColonyGroupExtraData(string colonyGroupID)
        {
            this.colonyGroupID = colonyGroupID;
        }
    }
}
