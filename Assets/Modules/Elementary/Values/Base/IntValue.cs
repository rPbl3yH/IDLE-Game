using System;
using UnityEngine;

namespace Modules.Elementary.Values.Base
{
    [Serializable]
    public sealed class IntValue : IValue<int>
    {
        public int Current
        {
            get { return this.value; }
        }

        [SerializeField]
        private int value;

        public IntValue(int value)
        {
            this.value = value;
        }
    }
}