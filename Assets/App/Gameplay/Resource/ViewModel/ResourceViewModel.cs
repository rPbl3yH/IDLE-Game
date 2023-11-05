using App.Gameplay.Resource.Model;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ResourceViewModel : MonoBehaviour
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private ResourceModel _resourceModel;

        public AtomicVariable<float> ActivateShowTime;

        private ActivateViewResourceMechanics _activateViewResourceMechanics;
        private ShakeViewMechanics _shakeViewMechanics;

        private void Awake()
        {
            _shakeViewMechanics = new ShakeViewMechanics(_view.transform, _resourceModel.Gathered);
            _activateViewResourceMechanics = new ActivateViewResourceMechanics(_view, _resourceModel.IsEnable, ActivateShowTime);
        }

        private void OnEnable()
        {
            _shakeViewMechanics.OnEnable();
            _activateViewResourceMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _shakeViewMechanics.OnDisable();
            _activateViewResourceMechanics.OnDisable();
        }
    }
}