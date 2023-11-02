using App.Gameplay.Player;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BarnModelObserver : MonoBehaviour
    {
        [SerializeField] private BarnModel _barnModel;
        [SerializeField] private LoadResourceViewController _viewController;

        private PlayerModel _playerModel;

        public void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
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
            
            if (_barnModel.ResourceStorage.Resources.Count > 0)
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