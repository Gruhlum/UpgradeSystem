using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;

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

        private List<EfficiencyValue> efficiencyValues = new List<EfficiencyValue>();

        public Efficiency()
        {
        }

        public void Add(EfficiencyValue efficiencyValue)
        {
            if (efficiencyValues.Any(x => x.type == efficiencyValue.type))
            {
                EfficiencyValue result = efficiencyValues.Find(x => x.type == efficiencyValue.type);
                result.value += efficiencyValue.value;
            }
            else efficiencyValues.Add(efficiencyValue);

            CalculateTotal();
        }
        public void Add(Stat stat, int order)
        {
            Add(stat.StatType, stat.Value / 100f, order);
        }
        public void Add(TagType type, float value, int order)
        {
            Add(new EfficiencyValue(type, value, order));
        }
        public void Remove(EfficiencyValue efficiencyValue)
        {
            efficiencyValues.Remove(efficiencyValue);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            Total = 1;
            foreach (EfficiencyValue item in efficiencyValues)
            {
                Total *= item.value;
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

            foreach (EfficiencyValue efficiencyValue in efficiencyValues)
            {
                if (efficiencyValue.value == 0)
                {
                    continue;
                }
                multiTexts.Add(new MultiText(efficiencyValue.type.name, efficiencyValue.value.ToString("0#.%")));
            }
            multiTexts.Add(new MultiText("----", string.Empty));
            multiTexts.Add(new MultiText("Total", Total.ToString("0#.%")));
            return new TableText(multiTexts);
        }
    }
}