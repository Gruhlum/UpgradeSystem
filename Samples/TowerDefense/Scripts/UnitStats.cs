using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.Decks;
using UnityEngine;

namespace HexTecGames.UpgradeSystem.TowerDefense
{
    [System.Serializable]
    public class UnitStats
    {
        public Stat Damage;
        public Stat CritChance;
        public Stat CritMultiplier;
        public Stat AttackSpeed;
        public Stat MaxHP;
        //public ClampedStat CurrentHP;

        private List<Stat> allStats = new List<Stat>();

        private BoolDeck critDeck;


        public UnitStats CreateCopy()
        {
            UnitStats clone = new UnitStats();
            clone.Damage = Damage.CreateCopy();
            clone.CritChance = CritChance.CreateCopy();
            clone.CritMultiplier = CritMultiplier.CreateCopy();
            clone.AttackSpeed = AttackSpeed.CreateCopy();
            clone.MaxHP = MaxHP.CreateCopy();
            //clone.CurrentHP = CurrentHP.CreateCopy() as ClampedStat;
            return clone;
        }
        

        private void CritChance_OnValueChanged(Stat stat, int value)
        {
            CreateCritDeck();
        }

        private void CreateCritDeck()
        {
            critDeck = new BoolDeck(CritChance.Value, 100 - CritChance.Value);
        }
        private void AddStat(Stat stat)
        {
            allStats.Add(stat);
        }

        public List<Upgrade> GetAllValidUpgrades()
        {
            List<Upgrade> validUpgrades = new List<Upgrade>();

            //foreach (var stat in allStats)
            //{
            //    validUpgrades.Add(stat.GetUpgrade());
            //}

            return validUpgrades;
        }
        public AttackData GetAttackData()
        {
            int totalDamage = Damage.Value;
            //bool isCrit = critDeck.RevealNextCard();
            //if (isCrit)
            //{
            //    totalDamage += (totalDamage * CritMultiplier.Value / 100);
            //}

            //return new AttackData(totalDamage, isCrit);
            return null;
        }


    }
}