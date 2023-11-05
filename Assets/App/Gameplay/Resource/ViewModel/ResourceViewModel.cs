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

        private ActivateModelViewMechanics _activateModelViewMechanics;
        private ShakeViewMechanics _shakeViewMechanics;

        private void Awake()
        {
            _shakeViewMechanics = new ShakeViewMechanics(_view.transform, _resourceModel.Gathered);
            _activateModelViewMechanics = new ActivateModelViewMechanics(_view, _resourceModel.IsEnable, ActivateShowTime);
        }

        private void OnEnable()
        {
            _shakeViewMechanics.OnEnable();
            _activateModelViewMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _shakeViewMechanics.OnDisable();
            _activateModelViewMechanics.OnDisable();
        }
    }
}