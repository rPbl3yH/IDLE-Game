using Atomic.Elements;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class DeactivateViewMechanics
    {
        private readonly AtomicEvent _deactivated;
        private readonly GameObject _view;

        public DeactivateViewMechanics(AtomicEvent deactivated, GameObject view)
        {
            _deactivated = deactivated;
            _view = view;
        }

        public void OnEnable()
        {
            _deactivated.Subscribe(OnDeactivated);
        }

        private void OnDeactivated()
        {
            _view.SetActive(false);
        }

        public void OnDisable()
        {
            _deactivated.Unsubscribe(OnDeactivated);
        }
    }
}