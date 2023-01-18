using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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
    public class PlayerManager : IOnPlayerConnectedLate, IOnAssemblyLoaded
    {
        List<ActiveColony> activeColonies;

        public void OnAssemblyLoaded(string path)
        {
            activeColonies = new List<ActiveColony>();
        }

        public void OnPlayerConnectedLate(Players.Player player)
        {
            for (int i = 0; i < player.ColonyGroups.Count; i++)
            {
                bool foundActiveColony = false;
                for (int i2 = 0; i2 < activeColonies.Count; i2++)
                {
                    if (player.ColonyGroups[i].ColonyGroupID.ToString() == activeColonies[i].colonyID)
                    {
                        activeColonies[i].onlinePlayers.Add(player.ID.SteamID.ToString());
                        foundActiveColony = true;
                        Log.Write("Adding player to existing colony list");
                        break;
                    }
                }

                if (!foundActiveColony)
                {
                    Log.Write("Creating new entry");
                    activeColonies.Add(new ActiveColony(player.ColonyGroups[i].ColonyGroupID.ToString(), player.ID.SteamID.ToString()));
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
