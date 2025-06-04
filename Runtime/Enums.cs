using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public enum StatClamp { None, Value, Stat }
    public enum ClampType { Min, Max }
    public enum UpgradeType { None, Normal, Flat, RarityIncrease, Percent }
    public enum EquationSymbol { Equal, Greater, Less }
    public enum ConditionType { All, Any }
    public enum FormattingType { None, Percent, Custom }
    public enum StatUpgradeType { Single, Multi, OverTime }
}