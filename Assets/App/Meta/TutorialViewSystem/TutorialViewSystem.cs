using UnityEngine;
using VContainer;

namespace App.Meta
{
    public class TutorialViewSystem
    {
        [Inject] 
        private PlayerPointerController _playerPointerController;

        [Inject] 
        private ObjectPointerController _objectPointerController;

        [Inject] 
        private HintTextObserver _hintTextObserver;

        public void Show(Transform targetPoint, string text)
        {
            _playerPointerController.SetTarget(targetPoint);
            _objectPointerController.SetTarget(targetPoint);
            _hintTextObserver.Show(text);
        }

        public void Hide()
        {
            _playerPointerController.SetTarget(null);
            _objectPointerController.SetTarget(null);
            _hintTextObserver.Hide();
        }
    }
}