using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class DeathResourceMechanics
    {
        private readonly GameObject _view;

        private readonly AtomicVariable<int> _amount;

        public DeathResourceMechanics(GameObject view, AtomicVariable<int> amount)
        {
            _view = view;
            _amount = amount;
        }

        public void OnEnable()
        {
            _amount.OnChanged += OnChanged;
        }

        public void OnDisable()
        {
            _amount.OnChanged -= OnChanged;
        }
        
        private void OnChanged(int value)
        {
            _view.SetActive(value != 0);
        }
    }
}