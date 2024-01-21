using App.Gameplay.LevelStorage;
using App.Gameplay.Resource.Model;
using Atomic.Elements;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ResourceViewModel : MonoBehaviour
    {
        public GameObject View;
        [SerializeField] private ParticleSystem _gatheringVfx;
        [SerializeField] private ResourceModel _resourceModel;

        public AtomicVariable<float> ActivateShowTime;

        private ActivateModelViewMechanics _activateModelViewMechanics;
        private ShakeViewMechanics _shakeViewMechanics;
        private ResourceVFXMechanics _resourceVFXMechanics;
        private DeactivateViewMechanics _deactivateViewMechanics;

        private void Awake()
        {
            _shakeViewMechanics = new ShakeViewMechanics(View.transform, _resourceModel.Gathered);
            _activateModelViewMechanics = new ActivateModelViewMechanics(View, _resourceModel.IsEnable, ActivateShowTime);
            _resourceVFXMechanics = new ResourceVFXMechanics(_gatheringVfx, _resourceModel.Gathered);
            _deactivateViewMechanics = new DeactivateViewMechanics(_resourceModel.Deactivated, View);
        }

        private void OnEnable()
        {
            _shakeViewMechanics.OnEnable();
            _activateModelViewMechanics.OnEnable();
            _resourceVFXMechanics.OnEnable();
            _deactivateViewMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _shakeViewMechanics.OnDisable();
            _activateModelViewMechanics.OnDisable();
            _resourceVFXMechanics.OnDisable();
            _deactivateViewMechanics.OnDisable();
        }
    }
}