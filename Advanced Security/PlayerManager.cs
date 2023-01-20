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
    public class PlayerManager : IOnPlayerConnectedLate, IOnPlayerDisconnected, IOnAssemblyLoaded
    {
        AdvancedSecurityInterface asInterface;

        [ModLoader.ModCallback("OnAssemblyLoaded", 100)]
        public void OnAssemblyLoaded(string path)
        {
            asInterface = AdvancedSecurityInterface.Instance;
        }

        public void OnPlayerConnectedLate(Players.Player player)
        {
            WorldDB worldDataBase = ServerManager.SaveManager.WorldDataBase;

            if (worldDataBase == null) return;

            for (int i = 0; i < player.ColonyGroups.Count; i++)
            {
                ColonyGroupExtraData colonyGroupExtraData = asInterface.colonyGroups.Where(colonyGroup => colonyGroup.colonyGroupID == player.ColonyGroups[i].ColonyGroupID.ToString()).ToList()[0];
                if (colonyGroupExtraData.autoSetDifficulty)
                {
                    bool anotherPlayerAlreadyConnectedInSameColony = false;
                    for (int i2 = 0; i2 < player.ColonyGroups[i].Owners.Count; i2++)
                    {
                        if (player.ColonyGroups[i].Owners[i2].ConnectionState == Players.EConnectionState.Connected && player.ColonyGroups[i].Owners[i2].ID.ID.ID != player.ID.ID.ID)
                        {
                            // Another player is still online in the same colony, so the diffiuclty remains unchanged
                            Log.Write("Another player joined who is a part of colony '" + player.ColonyGroups[i].Name + "' however someone else is already online in that colony so the difficulty will not be changed");
                            anotherPlayerAlreadyConnectedInSameColony = true;
                        }
                    }

                    if (!anotherPlayerAlreadyConnectedInSameColony)
                    {
                        if (worldDataBase.TryGetWorldKeyValue(player.ColonyGroups[i].ColonyGroupID.ToString(), out JToken jDifficulty) && jDifficulty != null)
                        {
                            string colonyDifficulty = JsonConvert.DeserializeObject<string>(jDifficulty.ToString());

                            player.ColonyGroups[i].DifficultySetting.Key = colonyDifficulty;
                            Log.Write("Colony '" + player.ColonyGroups[i].Name + "' (Owned by: " + player.Name + ") is now active, setting difficulty to index " + colonyDifficulty);
                        }
                    }
                }
            }
        }

        public void OnPlayerDisconnected(Players.Player player)
        {
            WorldDB worldDataBase = ServerManager.SaveManager.WorldDataBase;

            if (worldDataBase == null && player.ColonyGroups.Count == 0) return;

            // Check if another player is currently connected in the same colony, if they are then skip that colony
            for (int i = 0; i < player.ColonyGroups.Count; i++)
            {
                ColonyGroupExtraData colonyGroupExtraData = asInterface.colonyGroups.Where(colonyGroup => colonyGroup.colonyGroupID == player.ColonyGroups[i].ColonyGroupID.ToString()).ToList()[0];
                if (colonyGroupExtraData.autoSetDifficulty)
                {
                    bool colonyActive = false;
                    for (int i2 = 0; i2 < player.ColonyGroups[i].Owners.Count; i2++)
                    {
                        if (player.ColonyGroups[i].Owners[i2].ConnectionState == Players.EConnectionState.Connected)
                        {
                            // Another player is still online in the same colony, so the diffiuclty remains unchanged
                            Log.Write("A player who is a part of '" + player.ColonyGroups[i].Name + "' has left the game however there is stilll at least one more player online in that colony so the difficulty will not be changed");
                            colonyActive = true;
                        }
                    }

                    if (!colonyActive)
                    {
                        string colonyDifficulty = player.ColonyGroups[i].DifficultySetting.Key;

                        string json = JsonConvert.SerializeObject(colonyDifficulty);

                        worldDataBase.SetWorldKeyValue(player.ColonyGroups[i].ColonyGroupID.ToString(), json);

                        // Set colony difficulty to none
                        player.ColonyGroups[i].DifficultySetting.Key = "0";
                        Log.Write("Colony '" + player.ColonyGroups[i].Name + "' (Owned by: " + player.Name + ") no longer has any online players, setting difficulty to none");
                    }
                }
            }
        }
    }
}
