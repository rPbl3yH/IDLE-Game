    using App.Gameplay.LevelStorage;
    using DG.Tweening;
    using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.Building
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private float _spawnDuration = 0.3f;
        
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private BarnService _barnService;
        
        public BuildingModel Spawn(BuildingModel buildingModelModel, Transform point)
        {
            var spawnedModel = _objectResolver.Instantiate(buildingModelModel, point.position, point.rotation, _parent);
            spawnedModel.transform.DOScale(Vector3.one, _spawnDuration).From(0f).SetEase(Ease.OutBack).SetLink(spawnedModel.gameObject);
            
            if (spawnedModel is ResourceStorageModel resourceStorageModel)
            {
                _barnService.RegisterBarn(resourceStorageModel);
            }

            return spawnedModel;
        }
    }
}