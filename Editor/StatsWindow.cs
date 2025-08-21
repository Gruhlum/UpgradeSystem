using System;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.UpgradeSystem;
using RedGame.Framework.EditorTools;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.UpgradeSystem.Editor
{
    public class StatsWindow : EditorWindow
    {
        [SerializeField] private StatCollectionDataBaseCollection playerCollection = default;
        [SerializeField] private StatCollectionDataBaseCollection enemyCollection = default;


        private SimpleEditorTableView<StatUpgradeItem> _tableView;
        private StatUpgradeItem[] statUpgradeItems;

        [MenuItem("Tools/UpgradeSystem/Stats Editor")]
        private static void ShowWindow()
        {
            StatsWindow window = GetWindow<StatsWindow>();
            window.titleContent = new GUIContent("Stats");
            window.Show();
        }
        private SimpleEditorTableView<StatUpgradeItem> CreateTable(IUpgradeableStatCollection upgradeableStatCollection)
        {
            if (upgradeableStatCollection == null)
            {
                return null;
            }
            var stats = upgradeableStatCollection.GetStats();
            //SetStats(upgradeableStatCollection.GetStats());
            int totalTickets = upgradeableStatCollection.GetTotalTickets();

            SimpleEditorTableView<StatUpgradeItem> tableView = new SimpleEditorTableView<StatUpgradeItem>();

            tableView.AddColumn("Stat", 210, (rect, item) =>
            {
                item.stat.StatType = (StatType)EditorGUI.ObjectField(rect, item.stat.StatType, typeof(StatType), false);
            });

            tableView.AddColumn("Value", 60, (rect, item) =>
            {
                Stat stat = stats.Find(x => x.StatType == item.stat.StatType);
                if (stat != null)
                {
                    stat.FlatValue = EditorGUI.IntField(rect, stat.FlatValue);
                }
            }).SetMaxWidth(60);

            tableView.AddColumn("Increase", 60, (rect, item) =>
            {
                if (item.upgradeItem != null && item.upgradeItem.upgradeEffect is NormalUpgradeEffect normalUpgrade)
                {
                    normalUpgrade.increase = EditorGUI.IntField(rect, normalUpgrade.increase);
                }

            }).SetMaxWidth(60).SetTooltip("Increase");
            tableView.AddColumn("Tickets", 60, (rect, item) =>
            {
                if (item.upgradeItem != null && item.upgradeItem.upgradeEffect != null)
                {
                    item.upgradeItem.upgradeEffect.tickets = EditorGUI.IntField(rect, item.upgradeItem.upgradeEffect.tickets);
                }

            }).SetMaxWidth(60).SetTooltip("Tickets");

            tableView.AddColumn("Rarity", 150, (rect, item) =>
            {
                if (item.upgradeItem != null && item.upgradeItem.upgradeEffect != null)
                {
                    item.upgradeItem.upgradeEffect.rarity =
                    (Rarity)EditorGUI.ObjectField(rect, item.upgradeItem.upgradeEffect.rarity, typeof(Rarity), false);
                }

            });

            tableView.AddColumn("Chance", 60, (rect, item) =>
            {
                if (item.upgradeItem != null && item.upgradeItem.upgradeEffect != null)
                {
                    EditorGUI.LabelField(rect, (item.upgradeItem.upgradeEffect.tickets / (float)totalTickets).ToString("00.0%"));
                }
            }).SetMaxWidth(60).SetTooltip("Increase");

            return tableView;
        }
        //private void SetStats(List<Stat> stats)
        //{
        //    List<Stat> statsToRemove = new List<Stat>();
        //    for (int i = stats.Count - 1; i >= 0; i--)
        //    {
        //        if (stats[i].StatType != null && stats[i].StatType.Ignore)
        //        {
        //            stats.RemoveAt(i);
        //        }
        //    }
        //}

        private void OnEnable()
        {
            if (playerCollection != null && playerCollection.TotalItems > 0)
            {
                SelectCollection(playerCollection.First());
            }
        }
        private void OnDestroy()
        {
            DirtyAll();
        }

        private void DirtyAll()
        {
            if (playerCollection != null)
            {
                foreach (StatCollectionDataBase item in playerCollection)
                {
                    EditorUtility.SetDirty(item);
                }
            }
            if (enemyCollection != null)
            {
                foreach (StatCollectionDataBase item in enemyCollection)
                {
                    EditorUtility.SetDirty(item);
                }
            }
        }

        private void OnGUI()
        {
            if (playerCollection == null || enemyCollection == null)
            {
                return;
            }

            TitleGUI();

            if (_tableView != null && statUpgradeItems != null)
            {
                _tableView.DrawTableGUI(statUpgradeItems);
            }
        }

        private StatUpgradeItem[] CreateStatUpgradeItems(StatCollectionDataBase item)
        {
            List<StatUpgradeItem> results = new List<StatUpgradeItem>();

            var collection = item.GetStatCollection();
            StatUpgradeData upgradeList = null;
            List<Stat> stats = collection.GetStats();

            if (collection is IUpgradeableStatCollection upgradeStatCollection)
            {
                upgradeList = upgradeStatCollection.StatUpgradeData;
            }

            foreach (var stat in stats)
            {
                var statUpgradeItem = new StatUpgradeItem();
                statUpgradeItem.stat = stat;
                if (upgradeList != null)
                {
                    statUpgradeItem.upgradeItem = upgradeList.upgradeItems.Find(x => x.statType == stat.StatType);
                }
                results.Add(statUpgradeItem);
            }

            return results.ToArray();
        }

        private void CreateTableView(StatCollectionDataBase item)
        {
            if (item.GetStatCollection() is not IUpgradeableStatCollection upgradeStatCollection)
            {
                Debug.LogError("Not inheriting from " + nameof(IUpgradeableStatCollection));
                return;
            }
            _tableView = CreateTable(upgradeStatCollection);
            DirtyAll();
        }

        private void TitleGUI()
        {
            EditorGUILayout.BeginHorizontal();

            foreach (StatCollectionDataBase item in playerCollection)
            {
                CreateSelectionButton(item);
            }

            EditorGUILayout.EndHorizontal();
            //EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            foreach (StatCollectionDataBase item in enemyCollection)
            {
                CreateSelectionButton(item);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void CreateSelectionButton(StatCollectionDataBase item)
        {
            if (GUILayout.Button(item.name))
            {
                SelectCollection(item);
            }
        }

        private void SelectCollection(StatCollectionDataBase item)
        {
            CreateTableView(item);
            statUpgradeItems = CreateStatUpgradeItems(item);
        }
    }
}