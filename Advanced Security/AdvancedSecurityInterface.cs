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

namespace Advanced_Security
{
    [ModLoader.ModManager]
    public class AdvancedSecurityInterface: IOnConstructColonySettingsUI, IOnPlayerChangedNetworkUIStorage, IOnAssemblyLoaded
    {
        bool toggleTesting;

        public void OnAssemblyLoaded(string path)
        {
            //WorldDB worldDataBase = ServerManager.SaveManager.WorldDataBase;

            //if (worldDataBase == null) return;

            //if (worldDataBase.TryGetWorldKeyValue(player.ColonyGroups[i].ColonyGroupID.ToString(), out JToken jDifficulty) && jDifficulty != null)
            //{
            //    string colonyDifficulty = JsonConvert.DeserializeObject<string>(jDifficulty.ToString());

            //    player.ColonyGroups[i].DifficultySetting.Key = colonyDifficulty;
            //    Log.Write("Colony '" + player.ColonyGroups[i].Name + "' (Owned by: " + player.Name + ") is now active, setting difficulty to index " + colonyDifficulty);
            //}
        }

        [ModLoader.ModCallback("thing", 100)]
        public void OnConstructColonySettingsUI(Players.Player player, JObject localStorage, List<IItem> items)
        {
            if (player.ActiveColony == null) return;

            List<(IItem, int)> list = new List<(IItem, int)>(2);
            list.Add((new Label(new LabelData("Set diffiuclty to none on leave"), 30), 247));
            list.Add((new ToggleNoLabel("GMS.SetDifficultyNoneOnLeave"), 30));

            items.Add(new HorizontalRow(list));

            //toggleTesting = true;

            localStorage.SetAs("GMS.SetDifficultyNoneOnLeave", (JToken)toggleTesting);
            //localStorage.GetAsOrDefault("GMS.SetDifficultyNoneOnLeave", false);

            //localStorage.SetAs("pipliz.colonyname", (JToken)list);

            //WorldDB worldDataBase = ServerManager.SaveManager.WorldDataBase;

            //player.ActiveColony.JobFinder.

            //if (worldDataBase == null) return;

            //string json = JsonConvert.SerializeObject(colonyDifficulty);

            //worldDataBase.SetWorldKeyValue(player.ColonyGroups[i].ColonyGroupID.ToString(), json);
        }

        //[ModLoader.ModCallback("thing", 100)]
        //public void OnConstructInventoryManageColonyUI(Players.Player player, NetworkMenu menu, (Table left, Table right) tables)
        //{
        //    if (player.ActiveColony == null) return;

        //    //NetworkMenuManager

        //    ToggleNoLabel autoSetDifficulty = new ToggleNoLabel("GMS.SetDifficultyNoneOnLeave");

        //    //autoSetDifficulty.Equals(true);
        //    //autoSetDifficulty.

        //    List<(IItem, int)> list = new List<(IItem, int)>(2);
        //    list.Add((new Label(new LabelData("Set diffiuclty to none on leave"), 30), 247));
        //    list.Add((autoSetDifficulty, 30));

        //    list[1].Item1.Equals(true);

        //    tables.right.Rows.Add(new HorizontalRow(list));

            

        //    //disableMobSpawning = new ButtonCallback("GMS.disableZombieSpawn.button", new LabelData("Disable Zombie Spawn On Leave", Color.green));
        //}

        public void OnPlayerChangedNetworkUIStorage((Players.Player player, JObject context, string menuname) tuple)
        {
            Log.Write(tuple.context.GetAsOrDefault("GMS.SetDifficultyNoneOnLeave", false).ToString());
            toggleTesting = tuple.context.GetAsOrDefault("GMS.SetDifficultyNoneOnLeave", false);
        }
    }
}
