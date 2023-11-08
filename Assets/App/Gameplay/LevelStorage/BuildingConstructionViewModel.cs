using App.Gameplay.Resource.View;
using Modules.Atomic.Values;
using UnityEngine;
using VContainer;

namespace App.Gameplay.LevelStorage
{
    public class BuildingConstructionViewModel : MonoBehaviour
    {
        [SerializeField] 
        private BuildingConstructionModel _model;

        public GameObject View;

        public AtomicVariable<float> ActivateShowTime = new(0.5f);

        private ActivateModelViewMechanics _activateModelViewMechanics;
        private DeactivateViewMechanics _deactivateViewMechanics;

        [Inject]
        private void Construct()
        {
            _activateModelViewMechanics = new ActivateModelViewMechanics(View, _model.IsEnable, ActivateShowTime);
            _deactivateViewMechanics = new DeactivateViewMechanics(_model.Deactivated, View);

            _activateModelViewMechanics.OnEnable();
            _deactivateViewMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _activateModelViewMechanics.OnDisable();            
            _deactivateViewMechanics.OnDisable();
        }
    }
}