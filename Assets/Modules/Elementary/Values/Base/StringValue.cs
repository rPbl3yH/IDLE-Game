using System;
using UnityEngine;

namespace Modules.Elementary.Values.Base
{
    [Serializable]
    public sealed class StringValue : IValue<string>
    {
        public string Current
        {
            get { return this.value; }
        }

        [SerializeField]
        private string value;

        public StringValue(string value)
        {
            this.value = value;
        }
    }
}