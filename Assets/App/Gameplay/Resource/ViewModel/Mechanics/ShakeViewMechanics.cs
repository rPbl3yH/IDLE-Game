using Atomic.Elements;
using DG.Tweening;
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
            _gathered.Subscribe(OnGathered);
        }

        public void OnDisable()
        {
            _gathered.Subscribe(OnGathered);
        }
        
        private void OnGathered(int value)
        {
            _view.DOShakeScale(0.2f, 0.3f, 1).SetLink(_view.gameObject);
        }
    }
}