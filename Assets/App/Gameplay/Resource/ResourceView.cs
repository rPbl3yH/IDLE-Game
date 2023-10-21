using Atomic;
using DG.Tweening;
using UnityEngine;

namespace App.Gameplay.Resource
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private ResourceModel _resourceModel;

        private ShakeViewMechanics _shakeViewMechanics;

        private void Awake()
        {
            _shakeViewMechanics = new ShakeViewMechanics(_view.transform, _resourceModel.Gathered);
        }

        private void OnEnable()
        {
            _shakeViewMechanics.OnEnable();
            _resourceModel.Amount.OnChanged += OnChanged;
        }

        private void OnDisable()
        {
            _shakeViewMechanics.OnDisable();
            _resourceModel.Amount.OnChanged -= OnChanged;
        }

        private void OnChanged(int value)
        {
            _view.SetActive(value != 0);
        }
    }

    public class ShakeViewMechanics
    {
        private readonly Transform _view;
        private readonly AtomicEvent<int> _gathered;

        public ShakeViewMechanics(Transform view, AtomicEvent<int> gathered)
        {
            _view = view;
            _gathered = gathered;
        }

        public void OnEnable()
        {
            _gathered.AddListener(OnGathered);
        }

        public void OnDisable()
        {
            _gathered.AddListener(OnGathered);
        }
        
        private void OnGathered(int value)
        {
            _view.DOShakeScale(0.2f, 0.5f, 1);
        }
    }
}