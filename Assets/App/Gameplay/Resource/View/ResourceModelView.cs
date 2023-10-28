using App.Gameplay.Resource.Model;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ResourceModelView : MonoBehaviour
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private ResourceModel _resourceModel;

        private DeathResourceMechanics _deathResourceMechanics;
        private ShakeViewMechanics _shakeViewMechanics;

        private void Awake()
        {
            _shakeViewMechanics = new ShakeViewMechanics(_view.transform, _resourceModel.Gathered);
            _deathResourceMechanics = new DeathResourceMechanics(_view, _resourceModel.Amount);
        }

        private void OnEnable()
        {
            _shakeViewMechanics.OnEnable();
            _deathResourceMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _shakeViewMechanics.OnDisable();
            _deathResourceMechanics.OnDisable();
        }
    }
}