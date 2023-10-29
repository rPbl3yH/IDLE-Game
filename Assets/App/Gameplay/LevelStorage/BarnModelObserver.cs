using App.Gameplay.Player;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BarnModelObserver : MonoBehaviour
    {
        [SerializeField] private BarnModel _barnModel;
        [SerializeField] private LoadResourceView _view;

        [SerializeField] private PlayerModel _playerModel;

        private void OnEnable()
        {
            _view.ResourceSelected += OnResourceSelected;
            _playerModel.IsShowLoadResources.OnChanged += OnChangedShowLoadResources;
        }

        private void OnChangedShowLoadResources(bool value)
        {
            _view.gameObject.SetActive(value);
        }

        private void OnResourceSelected(ResourceType resourceType)
        {
            _playerModel.LoadResourceSelected?.Invoke(resourceType);
        }
    }
}