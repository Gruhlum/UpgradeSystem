using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class Efficiency
    {
        public float Total
        {
            get
            {
                return total;
            }
            private set
            {
                total = value;
            }
        }
        private float total;

        private SortedList<int, List<EfficiencyValue>> efficiencyValues = new SortedList<int, List<EfficiencyValue>>();

        public Efficiency()
        {
        }

        public void Add(int order, EfficiencyValue efficiencyValue)
        {
            efficiencyValues.Add(order, efficiencyValue);
            CalculateTotal();
        }
        public void Add(int order, Stat stat, MathMode mode)
        {
            if (stat.StatType == null)
            {
                Add(order, string.Empty, mode, stat.Value / 100f);
            }
            else Add(order, stat.StatType.name, mode, stat.Value / 100f);
        }
        public void Add(int order, string type, MathMode mode, float value)
        {
            Add(order, new EfficiencyValue(type, mode, value));
        }
        public void Remove(int order, EfficiencyValue efficiencyValue)
        {
            if (efficiencyValues.TryGetValue(order, out List<EfficiencyValue> result))
            {
                result.Remove(efficiencyValue);
                CalculateTotal();
            }
        }

        private void CalculateTotal()
        {
            Total = 0;
            foreach (var dictValue in efficiencyValues)
            {
                foreach (var efficiencyValue in dictValue.Value)
                {
                    Total = efficiencyValue.Apply(Total);
                }
            }
        }

        //private void CalculateTotal()
        //{
        //    efficiencyValues = efficiencyValues.OrderBy(x => x.order).ToList();

        //    float totalValue = 1;
        //    float currentValue = 0;

        //    int currentOrder = efficiencyValues[0].order;

        //    foreach (var efficiencyValue in efficiencyValues)
        //    {
        //        if (efficiencyValue.order == currentOrder)
        //        {
        //            currentValue += efficiencyValue.value;
        //        }
        //        else
        //        {
        //            totalValue *= currentValue;
        //            currentValue = 0;
        //            currentOrder = efficiencyValue.order;
        //        }
        //    }
        //    totalValue *= currentValue;
        //    Total = totalValue;
        //}

        public TableText GetTable()
        {
            List<MultiText> multiTexts = new List<MultiText>();

            foreach (var efficiencyValue in efficiencyValues)
            {
                foreach (var item in efficiencyValue.Value)
                {
                    if (item.mode == MathMode.Add && item.value == 0)
                    {
                        continue;
                    }
                    if (item.mode == MathMode.Multiply && item.value == 1)
                    {
                        continue;
                    }
                    multiTexts.Add(new MultiText(item.type, item.value.ToString("#.%")));
                }
            }
            multiTexts.Add(new MultiText("----", string.Empty));
            //multiTexts.Add(new MultiText("----", string.Empty));
            multiTexts.Add(new MultiText("Total", Total.ToString("#.%")));
            return new TableText(multiTexts);
        }
        public override string ToString()
        {
            return $"{Total} Efficiency";
        }

    }
}