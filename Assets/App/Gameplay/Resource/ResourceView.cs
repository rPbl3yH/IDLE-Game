using UnityEngine;

namespace App.Gameplay.Resource
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private ResourceModel _resourceModel;

        private void OnEnable()
        {
            _resourceModel.Amount.OnChanged += OnChanged;
        }

        private void OnDisable()
        {
            _resourceModel.Amount.OnChanged -= OnChanged;
        }

        private void OnChanged(int value)
        {
            _view.SetActive(value != 0);
        }
    }
}