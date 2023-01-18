using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using ModLoaderInterfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pipliz;
using Saving;

namespace Advanced_Security
{
    public class Class1 : IOnPlayerConnectedLate, IOnPlayerDisconnected
    {
        public void OnPlayerConnectedLate(Players.Player player)
        {
            WorldDB worldDataBase = ServerManager.SaveManager.WorldDataBase;

            if (worldDataBase == null) return;


            if (worldDataBase.TryGetWorldKeyValue(player.ID.SteamID.ToString(), out JToken jDifficulty) && jDifficulty != null)
            {
                string[] playerColonyDiffiuclties = JsonConvert.DeserializeObject<string[]>(jDifficulty.ToString());

                for (int i = 0; i < playerColonyDiffiuclties.Length; i++)
                {
                    player.ColonyGroups[i].DifficultySetting.Key = playerColonyDiffiuclties[i];
                    Log.Write("Player joining, setting the difficulty of colony '" + player.ColonyGroups[i].Name + "' (Owned by: " + player.Name + ") to index " + playerColonyDiffiuclties[i]);
                    //Log.Write("Player: '" + player.Name + "' has joined, setting difficulty for their colony (" + player.ColonyGroups[i].Name + ") to " + playerColonyDiffiuclties[i]);
                }
            }
        }

        public void OnPlayerDisconnected(Players.Player player)
        {
            WorldDB worldDataBase = ServerManager.SaveManager.WorldDataBase;

            if (worldDataBase == null && player.ColonyGroups.Count == 0) return;


            string[] colonyDifficulties = new string[player.ColonyGroups.Count];

            for (int i = 0; i < player.ColonyGroups.Count; i++)
            {
                colonyDifficulties[i] = player.ColonyGroups[i].DifficultySetting.Key;
            }

            string json = JsonConvert.SerializeObject(colonyDifficulties);

            worldDataBase.SetWorldKeyValue(player.ID.SteamID.ToString(), json);

            //Log.Write("Player: " + player.Name + " has left, changing their colony difficulty");
            for (int i = 0; i < player.ColonyGroups.Count; i++)
            {
                player.ColonyGroups[i].DifficultySetting.Key = "0";
                Log.Write("Player leaving, setting the difficulty of colony '"+ player.ColonyGroups[i].Name + "' (Owned by: " + player.Name + ") to none");
            }
        }
    }
}
