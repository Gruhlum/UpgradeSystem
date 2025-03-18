using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.Decks;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class UnitStats
    {
        public Stat Damage;
        public Stat CritChance;
        public Stat CritMultiplier;
        public Stat AttackSpeed;
        public Stat MaxHP;
        public ClampedStat CurrentHP;

        private List<Stat> allStats = new List<Stat>();

        private BoolDeck critDeck;

        public void Setup(UnitStatsData data)
        {
            AddStat(Damage);
            AddStat(CritChance);
            AddStat(CritMultiplier);
            AddStat(AttackSpeed);
            AddStat(MaxHP);

            CurrentHP = new ClampedStat(CurrentHP.StatType);
            AddStat(CurrentHP);

            ApplyData(Damage, data.Damage);
            ApplyData(CritChance, data.CritChance);
            ApplyData(CritMultiplier, data.CritMulti);
            ApplyData(AttackSpeed, data.AttackSpeed);
            ApplyData(MaxHP, data.MaxHP);

            CritChance.OnValueChanged += CritChance_OnValueChanged;
            CreateCritDeck();
            CurrentHP.SetMaxValue(MaxHP);
            CurrentHP.Value = MaxHP.Value;
        }

        private void CritChance_OnValueChanged(Stat stat, int value)
        {
            CreateCritDeck();
        }

        private void CreateCritDeck()
        {
            critDeck = new BoolDeck(CritChance.Value, 100 - CritChance.Value);
        }

        private void ApplyData(Stat stat, StatData data)
        {
            stat.ApplyData(allStats, data);
        }
        private void AddStat(Stat stat)
        {
            allStats.Add(stat);
        }

        public List<Upgrade> GetAllValidUpgrades()
        {
            List<Upgrade> validUpgrades = new List<Upgrade>();

            foreach (var stat in allStats)
            {
                validUpgrades.Add(stat.GetUpgrade());
            }

            return validUpgrades;
        }
        public AttackData GetAttackData()
        {
            int totalDamage = Damage.Value;
            bool isCrit = critDeck.RevealNextCard();
            if (isCrit)
            {
                totalDamage += (totalDamage * CritMultiplier.Value / 100);
            }

            return new AttackData(totalDamage, isCrit);
        }
    }
}