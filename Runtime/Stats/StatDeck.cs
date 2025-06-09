using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.Decks;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatDeck : BoolDeck
    {
        private Stat stat;

        public StatDeck(Stat stat) : base(stat.Value)
        {
            this.stat = stat;
            stat.OnValueChanged += Stat_OnValueChanged;
        }

        private void Stat_OnValueChanged(Stat stat, int value)
        {
            ChangeOdds(stat.Value);
        }
    }
}