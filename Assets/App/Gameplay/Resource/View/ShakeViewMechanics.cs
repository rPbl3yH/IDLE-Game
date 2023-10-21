using Atomic;
using DG.Tweening;
using UnityEngine;

namespace App.Gameplay.Resource
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
            _view.DOShakeScale(0.2f, 0.5f, 1);
        }
    }
}