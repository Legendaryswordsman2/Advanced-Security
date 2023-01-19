using BlockEntities.Implementations;
using Jobs;
using ModLoaderInterfaces;
using NetworkUI;
using NetworkUI.Items;
using Pipliz;
using StarterPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Advanced_Security
{
    [ModLoader.ModManager]
    public class AdvancedSecurityInterface : IOnConstructInventoryManageColonyUI
    {
        [ModLoader.ModCallback("thing", 100)]
        public void OnConstructInventoryManageColonyUI(Players.Player player, NetworkMenu menu, (Table left, Table right) tables)
        {
            if (player.ActiveColony == null) return;

            //NetworkMenuManager
            ButtonCallback disableMobSpawning;

            //Log.Write(tables.right.Rows.Count.ToString());


            //menu.Width = 100;
            //tables.right.BackgroundColor = Color.red;
            //tables.left.BackgroundColor= Color.blue;
            //tables.right.Rows.Clear();
            //menu.TextColor = Color.red;
            //menu.Items.
            //menu.Height = 10000;

            //HorizontalRow horizontalRow = (HorizontalRow)menu.Items[0];

            //horizontalRow.InsetTop = 100;

            //horizontalRow.Items[1].Item2.

            //horizontalRow.Items.

            //horizontalRow.Items[0].Item1 = player.ActiveColony;

            //horizontalRow.Items.Remove(horizontalRow.Items[0]);

            //horizontalRow.Items.Remove(horizontalRow.Items[0]);

            //menu.Items.Remove(menu.Items[0]);

            Toggle toggleTest;

            toggleTest = new Toggle("Set diffiuclty to none on leave          ", "test_toggle");

            //Log.Write(toggleTest.ToggleSize.ToString());
            toggleTest.ToggleSize = 25;

            Label testLabel = new Label("Testy Test");
            ToggleNoLabel toggleTesties = new ToggleNoLabel("Testty");

            //tables.right.Rows.Add(testLabel);
            //tables.right.Rows.Add(toggleTesties);

            //tables.right.Rows.Insert(14, toggleTest);

            List<(IItem, int)> list = new List<(IItem, int)>(2);
            list.Add((new Label(new LabelData("Set diffiuclty to none on leave"), 30), 247));
            list.Add((new ToggleNoLabel("colonymanagement.recruitautochanged"), 30));

            tables.right.Rows.Add(new HorizontalRow(list));

            disableMobSpawning = new ButtonCallback("GMS.disableZombieSpawn.button", new LabelData("Disable Zombie Spawn On Leave", Color.green));

            //for (int i = 0; i < menu.Items.Count; i++)
            //{
            //    Log.Write(menu.Items[i].ToString());
            //}


            //Log.Write(menu.Items.Count.ToString());

            //menu.UIParent.parent.



            //HorizontalRow horizontalRow = (HorizontalRow)menu.Items[0];

            ////Log.Write(horizontalRow.Items.Count.ToString());
            //for (int i = 0; i < horizontalRow.Items.Count; i++)
            //{
            //    Log.Write(horizontalRow.Items[i].ToString());
            //}

            //Log.Write(horizontalRow.Items[1].Item1.ToString());
            //Log.Write(horizontalRow.Items[1].Item2.ToString());

            //horizontalRow.Items.Reverse();

            //CommandToolManager.GenerateTwoColumnCenteredRow(menu,horizontalRow);

            //horizontalRow.Items.Add(new (IItem, int))

            //Log.Write(tables.right.Rows[1].ToString() + "       rehjre5jt");


            //menu.Items.Add(toggleTest);

            //tables.right.Rows.Add(toggleTest);

            //tables.right.Rows.Reverse();

            //menu.Items.Reverse();

            //menu.Items.Sort();
            //menu.Items.Clear();
            //disableMobSpawning.Width = 20;
            //tables.right.BackgroundColor = Color.white;
            //tables.right.Rows.Add(toggleTest);
            //menu.Items.Add(toggleTest);
            //menu.Items.Insert(0, toggleTest);
            //tables.right.a
            //tables.right.Rows.Reverse();
            //tables.right.Rows.Add(disableMobSpawning);
            //tables.right.Rows.Sort();

            //tables.right.Height = 0;
        }
    }
}
