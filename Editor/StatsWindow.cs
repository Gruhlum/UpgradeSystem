using System;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.UpgradeSystem;
using RedGame.Framework.EditorTools;
using UnityEditor;
using UnityEngine;

namespace HexTecGames
{
    public class StatsWindow : EditorWindow
    {
        [SerializeField] private StatCollectionDataBaseCollection playerCollection = default;
        [SerializeField] private StatCollectionDataBaseCollection enemyCollection = default;


        private SimpleEditorTableView<Stat> _tableView;
        private Stat[] stats = Array.Empty<Stat>();


        [MenuItem("Stats/Show")]
        private static void ShowWindow()
        {
            StatsWindow window = GetWindow<StatsWindow>();
            window.titleContent = new GUIContent("Stats");
            window.Show();
        }
        private SimpleEditorTableView<Stat> CreateTable(List<Stat> stats)
        {
            SetStats(stats);

            int totalTickets = this.stats.Sum(x => x.UpgradeInfo.Tickets);

            SimpleEditorTableView<Stat> tableView = new SimpleEditorTableView<Stat>();

            tableView.AddColumn("Stat", 210, (rect, item) =>
            {
                item.StatType = (StatType)EditorGUI.ObjectField(rect, item.StatType, typeof(StatType));
            });

            tableView.AddColumn("Value", 60, (rect, item) =>
            {
                item.FlatValue = EditorGUI.IntField(rect, item.FlatValue);
            }).SetMaxWidth(60);

            tableView.AddColumn("Increase", 60, (rect, item) =>
            {
                if (item.UpgradeInfo.UpgradeType != UpgradeType.None && item.UpgradeInfo.UpgradeType != UpgradeType.Percent)
                {
                    item.UpgradeInfo.Increase = EditorGUI.IntField(rect, item.UpgradeInfo.Increase);
                }

            }).SetMaxWidth(60).SetTooltip("Increase");
            tableView.AddColumn("Tickets", 60, (rect, item) =>
            {
                if (item.UpgradeInfo.UpgradeType != UpgradeType.None && item.UpgradeInfo.UpgradeType != UpgradeType.Percent)
                {
                    item.UpgradeInfo.Tickets = EditorGUI.IntField(rect, item.UpgradeInfo.Tickets);
                }

            }).SetMaxWidth(60).SetTooltip("Tickets");

            tableView.AddColumn("Rarity", 150, (rect, item) =>
            {
                if (item.UpgradeInfo.UpgradeType != UpgradeType.Percent && item.UpgradeInfo.UpgradeType != UpgradeType.None)
                {
                    item.UpgradeInfo.Rarity = (Rarity)EditorGUI.ObjectField(rect, item.UpgradeInfo.Rarity, typeof(Rarity));
                }
            });

            tableView.AddColumn("Chance", 60, (rect, item) =>
            {
                if (item.UpgradeInfo.UpgradeType != UpgradeType.Percent && item.UpgradeInfo.UpgradeType != UpgradeType.None)
                {
                    EditorGUI.LabelField(rect, (item.UpgradeInfo.Tickets / (float)totalTickets).ToString("00.0%"));
                }
            }).SetMaxWidth(60).SetTooltip("Increase");

            return tableView;
        }
        private void SetStats(List<Stat> stats)
        {
            List<Stat> statsToRemove = new List<Stat>();
            for (int i = stats.Count - 1; i >= 0; i--)
            {
                if (stats[i].StatType != null && stats[i].StatType.Ignore)
                {
                    stats.RemoveAt(i);
                }
            }
            this.stats = stats.ToArray();
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
            if (_tableView == null)
            {
                if (stats == null)
                {
                    _tableView = CreateTable(playerCollection.First().GetStats());
                }
                else _tableView = CreateTable(stats.ToList());
            }
            _tableView.DrawTableGUI(stats);
        }
        private void TitleGUI()
        {
            EditorGUILayout.BeginHorizontal();

            foreach (StatCollectionDataBase item in playerCollection)
            {
                if (GUILayout.Button(item.name))
                {
                    _tableView = CreateTable(item.GetStats());
                    DirtyAll();
                }
            }

            EditorGUILayout.EndHorizontal();
            //EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            foreach (StatCollectionDataBase item in enemyCollection)
            {
                if (GUILayout.Button(item.name))
                {
                    _tableView = CreateTable(item.GetStats());
                    DirtyAll();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}