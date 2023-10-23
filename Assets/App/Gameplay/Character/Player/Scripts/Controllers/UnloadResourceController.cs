using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay
{
    public class UnloadResourceController : MonoBehaviour
    {
        [SerializeField] private LevelStorageService _levelStorageService;
        [SerializeField] private CharacterModel _characterModel;
        
        private void Update()
        {
            var barn = _levelStorageService.GetStorage();
            var distance = Vector3.Distance(_characterModel.Root.position, barn.UnloadingPoint.position);

            if (distance <= _characterModel.UnloadingDistance.Value)
            {
                _characterModel.CanUnloadResources.Value = true;
            }
            else
            {
                _characterModel.CanUnloadResources.Value = false;
            }
        }
    }
}