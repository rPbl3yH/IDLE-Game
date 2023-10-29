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
            if (!value)
            {
                _view.gameObject.SetActive(false);
                return;
            }
            
            if (_barnModel.ResourceStorage.TryGetResource(_view.ResourceType, out var resource))
            {
                _view.gameObject.SetActive(true);
            }
            else
            {
                print("No resources");
            }
        }

        private void OnResourceSelected(ResourceType resourceType)
        {
            _playerModel.LoadResourceSelected?.Invoke(resourceType);
        }
    }
}