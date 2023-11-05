using App.Gameplay.Resource.Model;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ResourceModelView : MonoBehaviour
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private ResourceModel _resourceModel;

        private ActivateViewResourceMechanics _activateViewResourceMechanics;
        private ShakeViewMechanics _shakeViewMechanics;

        private void Awake()
        {
            _shakeViewMechanics = new ShakeViewMechanics(_view.transform, _resourceModel.Gathered);
            _activateViewResourceMechanics = new ActivateViewResourceMechanics(_view, _resourceModel.IsEnable);
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