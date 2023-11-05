using DG.Tweening;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ActivateModelViewMechanics
    {
        private readonly GameObject _view;
        private readonly IAtomicVariable<bool> _isEnabled;
        private readonly IAtomicValue<float> _activateShowTime;

        public ActivateModelViewMechanics(GameObject view, IAtomicVariable<bool> isEnabled, IAtomicValue<float> activateShowTime)
        {
            _view = view;
            _isEnabled = isEnabled;
            _activateShowTime = activateShowTime;
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
            if (value)
            {
                _view.SetActive(true);
                _view.transform
                    .DOScale(Vector3.one, _activateShowTime.Value)
                    .From(0f)
                    .SetEase(Ease.OutBack);
            }
            else
            {
                _view.transform
                    .DOScale(Vector3.zero, _activateShowTime.Value)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => _view.SetActive(false));
            }
        }
    }
}