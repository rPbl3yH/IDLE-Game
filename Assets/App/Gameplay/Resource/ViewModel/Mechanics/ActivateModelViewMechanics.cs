﻿using Atomic.Elements;
using DG.Tweening;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ActivateModelViewMechanics
    {
        private readonly GameObject _view;
        private readonly IAtomicObservable<bool> _isEnabled;
        private readonly IAtomicValue<float> _activateShowTime;

        private Tween _tween;

        public ActivateModelViewMechanics(GameObject view, IAtomicObservable<bool> isEnabled, IAtomicValue<float> activateShowTime)
        {
            _view = view;
            _isEnabled = isEnabled;
            _activateShowTime = activateShowTime;
        }

        public void OnEnable()
        {
            _isEnabled.Subscribe(OnChanged);
        }

        public void OnDisable()
        {
            _isEnabled.Unsubscribe(OnChanged);
        }
        
        private void OnChanged(bool value)
        {
            if (value)
            {
                _view.SetActive(true);
                
                _tween?.Kill();
                _tween = _view.transform
                    .DOScale(Vector3.one, _activateShowTime.Value)
                    .From(0f)
                    .SetEase(Ease.OutBack)
                    .SetLink(_view.gameObject);
            }
            else
            {
                _tween?.Kill();
                _tween = _view.transform
                    .DOScale(Vector3.zero, _activateShowTime.Value)
                    .SetEase(Ease.InBack)
                    .SetLink(_view.gameObject)
                    .OnComplete(() => _view.SetActive(false));
            }
        }
    }
}