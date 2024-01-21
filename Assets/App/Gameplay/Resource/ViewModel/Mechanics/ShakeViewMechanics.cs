﻿using DG.Tweening;
using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ShakeViewMechanics
    {
        private readonly Transform _view;
        private readonly AtomicEvent<int> _gathered;

        public ShakeViewMechanics(Transform view, AtomicEvent<int> gathered)
        {
            _view = view;
            _gathered = gathered;
        }

        public void OnEnable()
        {
            _gathered.AddListener(OnGathered);
        }

        public void OnDisable()
        {
            _gathered.AddListener(OnGathered);
        }
        
        private void OnGathered(int value)
        {
            _view.DOShakeScale(0.2f, 0.3f, 1).SetLink(_view.gameObject);
        }
    }
}