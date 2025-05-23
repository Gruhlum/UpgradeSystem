using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames
{
    [CreateAssetMenu(menuName = "HexTecGames/MutaBlob/MultiUpgradeData")]
    public class MultiStatUpgradeData : ScriptableObject, ITicket
    {
        public List<StatType> StatTypes
        {
            get
            {
                return this.statTypes;
            }
            private set
            {
                this.statTypes = value;
            }
        }
        [SerializeField] private List<StatType> statTypes = default;

        public int Tickets
        {
            get
            {
                return tickets;
            }
        }
        [SerializeField] private int tickets = default;

        public bool HasStatType(StatType type)
        {
            return StatTypes.Contains(type);
        }
    }
}