using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public abstract class StatCollectionDataBase : ScriptableObject
    {
        public abstract List<Stat> GetStats();
    }
}