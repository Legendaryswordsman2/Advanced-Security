using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Security
{
    public class ColonyPlayerList
    {
        public string colonyID;

        public List<string> onlinePlayers = new List<string>();

        public ColonyPlayerList(string _colonyID, string playerID = "")
        {
            colonyID = _colonyID;

            if(playerID != "")
                onlinePlayers.Add(playerID);
        }
    }
}
