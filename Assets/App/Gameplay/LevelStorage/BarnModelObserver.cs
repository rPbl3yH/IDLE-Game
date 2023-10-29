using App.Gameplay.Player;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BarnModelObserver : MonoBehaviour
    {
        [SerializeField] private BarnModel _barnModel;
        [SerializeField] private PlayerModel _playerModel; 
            
        private void OnEnable()
        {
            _barnModel.ResourceSelected.AddListener(OnResourceSelected);
        }

        private void OnResourceSelected(ResourceType resourceType)
        {
            _playerModel.LoadResourceSelected?.Invoke(resourceType);
        }
    }
}