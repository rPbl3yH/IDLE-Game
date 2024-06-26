using System;
using System.Collections.Generic;
using Modules.Elementary.Actions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Variables.MonoBehaviours
{
    [AddComponentMenu("Elementary/Variables/Variable «Int»")]
    public sealed class MonoIntVariable : MonoBehaviour, IVariable<int>
    {
        public event Action<int> OnValueChanged;

        public int Current
        {
            get { return this.value; }
            set { this.SetValue(value); }
        }

        private readonly List<IAction<int>> listeners = new();

        [OnValueChanged("SetValue")]
        [SerializeField]
        private int value;

        [SerializeField]
        private UnityEvent<int> onValueChanged;

        public void SetValue(int value)
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.Do(value);
            }

            this.value = value;
            this.onValueChanged?.Invoke(value);
            this.OnValueChanged?.Invoke(value);
        }
        
        public void Plus(int range)
        {
            var newValue = this.value + range;
            this.SetValue(newValue);
        }

        public void Minus(int range)
        {
            var newValue = this.value - range;
            this.SetValue(newValue);
        }

        public void Multiply(int multiplier)
        {
            var newValue = this.value * multiplier;
            this.SetValue(newValue);
        }

        public void Divide(int divider)
        {
            var newValue = this.value / divider;
            this.SetValue(newValue);
        }

        public void Increment()
        {
            var newValue = this.value + 1;
            this.SetValue(newValue);
        }

        public void Decrement()
        {
            var newValue = this.value - 1;
            this.SetValue(newValue);
        }

        public void AddListener(IAction<int> listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction<int> listener)
        {
            this.listeners.Remove(listener);
        }
    }
}