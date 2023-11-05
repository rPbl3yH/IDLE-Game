using System.Collections.Generic;
using App.Gameplay.LevelStorage;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.Building
{
    public class BuildingConstructionSpawner : SerializedMonoBehaviour, IInitializable
    {
        [SerializeField] 
        private BuildingConstructionModel _buildingConstructionModelPrefab;

        [OdinSerialize]
        private Dictionary<Transform, BuildingData> _buildings = new();

        [Inject] 
        private IObjectResolver _objectResolver;

        [Inject] 
        private ResourceStorageModelService _resourceStorageModelService;

        [Inject] 
        private BuildingConstructionService _buildingConstructionService;

        public void Initialize()
        {
            foreach (var pair in _buildings)
            {
                _buildingConstructionModelPrefab.BuildingModel = pair.Value.BuildingModel;
                var buildingModel = _objectResolver.Instantiate(_buildingConstructionModelPrefab, pair.Key);
                buildingModel.ResourceStorage.StorageConfig = pair.Value.BuildConfig;
                _resourceStorageModelService.AddStorage(buildingModel);
                _buildingConstructionService.AddService(buildingModel);
            }
        }
    }
}
