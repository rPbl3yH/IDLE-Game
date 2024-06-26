using System;
using System.Collections.Generic;
using Modules.Elementary.Actions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Variables.MonoBehaviours
{
    [AddComponentMenu("Elementary/Variables/Variable «Bool»")]
    public sealed class MonoBoolVariable : MonoBehaviour, IVariable<bool>
    {
        public event Action<bool> OnValueChanged;

        public bool Current
        {
            get { return this.value; }
            set { this.SetValue(value); }
        }

        private readonly List<IAction<bool>> listeners = new();

        [OnValueChanged("SetValue")]
        [SerializeField]
        private bool value;

        [SerializeField]
        private UnityEvent<bool> onValueChanged;

        public void SetValue(bool value)
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

        public void SetTrue()
        {
            this.SetValue(true);
        }

        public void SetFalse()
        {
            this.SetValue(false);
        }

        public void AddListener(IAction<bool> listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction<bool> listener)
        {
            this.listeners.Remove(listener);
        }
    }
}