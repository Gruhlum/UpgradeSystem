using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public interface ICloneable<T>
    {
        public T CreateCopy();
    }
}