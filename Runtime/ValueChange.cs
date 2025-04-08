using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class ValueChange
    {
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                if (this.value == value)
                {
                    return;
                }
                this.value = value;
                OnValueChanged?.Invoke(this, value);
            }
        }
        private int value;


        public event Action<ValueChange, int> OnValueChanged;
        public event Action<ValueChange> OnRemoved;

        public ValueChange(int value)
        {
            this.value = value;
        }

        public void Remove()
        {
            OnRemoved?.Invoke(this);
        }
    }
}