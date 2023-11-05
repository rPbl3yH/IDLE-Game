using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ActivateViewResourceMechanics
    {
        private readonly GameObject _view;
        private readonly IAtomicVariable<bool> _isEnabled;

        public ActivateViewResourceMechanics(GameObject view, IAtomicVariable<bool> isEnabled)
        {
            _view = view;
            _isEnabled = isEnabled;
        }

        public void OnEnable()
        {
            _isEnabled.OnChanged += OnChanged;
        }

        public void OnDisable()
        {
            _isEnabled.OnChanged -= OnChanged;
        }
        
        private void OnChanged(bool value)
        {
            _view.SetActive(value);
        }
    }
}