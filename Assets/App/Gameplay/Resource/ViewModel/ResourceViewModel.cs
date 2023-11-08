using App.Gameplay.Resource.Model;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ResourceViewModel : MonoBehaviour
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private ParticleSystem _gatheringVfx;
        [SerializeField] private ResourceModel _resourceModel;

        public AtomicVariable<float> ActivateShowTime;

        private ActivateModelViewMechanics _activateModelViewMechanics;
        private ShakeViewMechanics _shakeViewMechanics;
        private ResourceVFXMechanics _resourceVFXMechanics;

        private void Awake()
        {
            _shakeViewMechanics = new ShakeViewMechanics(_view.transform, _resourceModel.Gathered);
            _activateModelViewMechanics = new ActivateModelViewMechanics(_view, _resourceModel.IsEnable, ActivateShowTime);
            _resourceVFXMechanics = new ResourceVFXMechanics(_gatheringVfx, _resourceModel.Gathered);
        }

        private void OnEnable()
        {
            _shakeViewMechanics.OnEnable();
            _activateModelViewMechanics.OnEnable();
            _resourceVFXMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _shakeViewMechanics.OnDisable();
            _activateModelViewMechanics.OnDisable();
            _resourceVFXMechanics.OnDisable();
        }
    }
}