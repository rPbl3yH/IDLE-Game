    using App.Gameplay.LevelStorage;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.Building
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private BarnService _barnService;
        
        public BuildingModel Spawn(BuildingModel buildingModelModel, Transform point)
        {
            var spawnedModel = _objectResolver.Instantiate(buildingModelModel, point.position, point.rotation, _parent);
            
            if (spawnedModel is ResourceStorageModel resourceStorageModel)
            {
                _barnService.RegisterBarn(resourceStorageModel);
            }

            return spawnedModel;
        }
    }
}