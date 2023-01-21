using BlockEntities.Implementations;
using Jobs;
using ModLoaderInterfaces;
using NetworkUI;
using NetworkUI.Items;
using Newtonsoft.Json.Linq;
using Pipliz;
using StarterPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Saving;
using Newtonsoft.Json;
using static Players;
//using Steamworks;
using System.Xml.Schema;
using colonyserver.Assets.UIGeneration;
using Shared.Networking;
using System.Xml;

namespace Advanced_Security
{
    [ModLoader.ModManager]
    public class AdvancedSecurityInterface : IOnConstructColonySettingsUI, IOnPlayerChangedNetworkUIStorage, IOnLoadingColonyGroup, IOnCreatedColonyGroup, IOnQuit, IOnPlayerConnectedEarly, IOnAssemblyLoaded//, IOnRecalculateThreatLevel
    {
        bool initialized = false;

        public static AdvancedSecurityInterface Instance;

        public List<ColonyGroupExtraData> colonyGroups { get; private set; } = new List<ColonyGroupExtraData>();

        [ModLoader.ModCallback("OnAssemblyLoaded_Interface", 10)]
        public void OnAssemblyLoaded(string path)
        {
            Instance = this;

            //NetworkMenuManager
            //ServerManager


            //AlarmbellTracker alarmbellTracker= new AlarmbellTracker();

            //alarmbellTracker.
        }

        [ModLoader.ModCallback("OnConstructColonySettingsUI", 100)]
        public void OnConstructColonySettingsUI(Players.Player player, JObject localStorage, List<IItem> items)
        {
            if (player.ActiveColony == null) return;

            List<(IItem, int)> list = new List<(IItem, int)>(2);
            list.Add((new Label(new LabelData("Auto set difficulty on join/leave"), 30), 247));
            list.Add((new ToggleNoLabel("GMS.SetDifficultyNoneOnLeave"), 30));

            items.Add(new HorizontalRow(list));
            ColonyGroupExtraData colonyGroupExtraData = colonyGroups.Where(colonyGroup => colonyGroup.colonyGroupID == player.ActiveColonyGroup.ColonyGroupID.ToString()).ToList()[0];

            localStorage.SetAs("GMS.SetDifficultyNoneOnLeave", (JToken)colonyGroupExtraData.autoSetDifficulty);
        }

        public void OnCreatedColonyGroup(ColonyGroup colony)
        {
            colonyGroups.Add(new ColonyGroupExtraData(colony.ColonyGroupID.ToString()));
        }

        [ModLoader.ModCallback("OnLoadingColonyGroup", 100)]
        public void OnLoadingColonyGroup(ColonyGroup colony, JObject json)
        {
            //Log.Write("Loading Colony | " + colony.ColonyGroupID);
            colonyGroups.Add(new ColonyGroupExtraData(colony.ColonyGroupID.ToString()));
        }

        public void OnPlayerChangedNetworkUIStorage((Players.Player player, JObject context, string menuname) tuple)
        {

            //Log.Write(tuple.context.GetAsOrDefault("GMS.SetDifficultyNoneOnLeave", false).ToString());
            ColonyGroupExtraData colonyGroupExtraData = colonyGroups.Where(colonyGroup => colonyGroup.colonyGroupID == tuple.player.ActiveColonyGroup.ColonyGroupID.ToString()).ToList()[0];
            colonyGroupExtraData.autoSetDifficulty = tuple.context.GetAsOrDefault("GMS.SetDifficultyNoneOnLeave", false);

            //WorldDB worldDataBase = ServerManager.SaveManager.WorldDataBase;

            //if (worldDataBase == null) return;

            //string json = JsonConvert.SerializeObject(autoSetDifficulty);

            //worldDataBase.SetWorldKeyValue("GMS.autoSetDifficulty." + tuple.player.ActiveColonyGroup.ColonyGroupID, json);
        }

        [ModLoader.ModCallback("OnPlayerConnectedEarly", -100)]
        public void OnPlayerConnectedEarly(Player player) // Closest thing I could get to a start method
        {
            if (initialized) return;

            WorldDB worldDataBase = ServerManager.SaveManager.WorldDataBase;

            if (worldDataBase == null) return;

            if (worldDataBase.TryGetWorldKeyValue("GMS.ColonyGroupsExtraData", out JToken jcolonyGroups) && jcolonyGroups != null)
                colonyGroups = JsonConvert.DeserializeObject<List<ColonyGroupExtraData>>(jcolonyGroups.ToString());

            initialized = true;
        }

        public void OnQuit()
        {
            WorldDB worldDataBase = ServerManager.SaveManager.WorldDataBase;

            if (worldDataBase == null) return;

            string json = JsonConvert.SerializeObject(colonyGroups);

            worldDataBase.SetWorldKeyValue("GMS.ColonyGroupsExtraData", json);
        }

        public void OnRecalculateThreatLevel(ColonyGroup colony)
        {
            //colony.SendThreatLevelsToActiveOwners();
        }
    }
}
