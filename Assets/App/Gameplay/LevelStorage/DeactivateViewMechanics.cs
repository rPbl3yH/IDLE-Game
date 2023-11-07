using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class DeactivateViewMechanics
    {
        private AtomicEvent _deactivated;
        private GameObject _view;

        public DeactivateViewMechanics(AtomicEvent deactivated, GameObject view)
        {
            _deactivated = deactivated;
            _view = view;
        }

        public void OnEnable()
        {
            _deactivated.AddListener(OnDeactivated);
        }

        private void OnDeactivated()
        {
            _view.SetActive(false);
        }

        public void OnDisable()
        {
            _deactivated.RemoveListener(OnDeactivated);
        }
    }
}