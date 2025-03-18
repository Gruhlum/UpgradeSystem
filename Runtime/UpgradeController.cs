using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public class UpgradeController : MonoBehaviour
    {
        [SerializeField] private UpgradeDisplayController upgradeDisplayController = default;
        [SerializeField] private Unit unit = default;

        [SerializeField] private float secondsPerUpgrade = 10;

        private float upgradeTimer;
        public PermissionGroup AllowTimer
        {
            get
            {
                return allowTimer;
            }
            private set
            {
                allowTimer = value;
            }
        }
        private PermissionGroup allowTimer = new PermissionGroup();


        private void Awake()
        {
            upgradeDisplayController.OnDisplayClicked += UpgradeDisplayController_OnDisplayClicked;
        }
        private void FixedUpdate()
        {
            if (!AllowTimer.Allowed)
            {
                return;
            }

            upgradeTimer += Time.deltaTime;
            if (upgradeTimer >= secondsPerUpgrade)
            {
                upgradeTimer = 0;
                ShowUpgrades();
            }
        }

        private void UpgradeDisplayController_OnDisplayClicked(UpgradeDisplay display)
        {
            display.Item.Apply();
            unit.Enable.SetPermissionState(this, true);
            AllowTimer.SetPermissionState(this, true);
            upgradeDisplayController.ClearItems();
        }

        private void ShowUpgrades()
        {
            unit.Enable.SetPermissionState(this, false);
            List<Upgrade> upgrades = GetUpgrades(3);
            upgradeDisplayController.SetItems(upgrades);
            AllowTimer.SetPermissionState(this, false);
        }

        private List<Upgrade> GetUpgrades(int amount)
        {
           return unit.GetAllValidUpgrades().RandomUnique(amount);
        }
    }
}