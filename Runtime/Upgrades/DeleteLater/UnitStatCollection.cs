using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames
{
    [System.Serializable]
    public class UnitStatCollection : CloneableStatCollection<UnitStatCollection>
    {
        public Stat Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }
        [SerializeField] private Stat damage;

        public Stat Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }
        [SerializeField] private Stat range;



        protected override UnitStatCollection InstantiateCopy()
        {
            return new UnitStatCollection();
        }

        protected override void AddStatsToList(List<Stat> stats)
        {
            stats.Add(Damage);
            stats.Add(Range);
        }
    }
}