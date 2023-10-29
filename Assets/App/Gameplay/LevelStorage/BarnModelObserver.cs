using App.Gameplay.Player;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BarnModelObserver : MonoBehaviour
    {
        [SerializeField] private BarnModel _barnModel;
        [SerializeField] private LoadResourceViewController _viewController;

        [SerializeField] private PlayerModel _playerModel;

        private void OnEnable()
        {
            _viewController.ResourceSelected += OnResourceSelected;
            _playerModel.IsShowLoadResources.OnChanged += OnChangedShowLoadResources;
        }

        private void OnChangedShowLoadResources(bool value)
        {
            if (!value)
            {
                _viewController.Hide();
                return;
            }
            
            if (_barnModel.ResourceStorage.GetAllResources().Count > 0)
            {
                _viewController.Show();
            }
            else
            {
                print("No resources");
            }
        }

        private void OnResourceSelected(ResourceType resourceType)
        {
            _viewController.Hide();
            _playerModel.LoadResourceSelected?.Invoke(resourceType);
        }
    }
}