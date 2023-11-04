using System;
using System.Collections.Generic;
using App.Gameplay.LevelStorage;
using App.Gameplay.Player;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.Building
{
    [Serializable]
    public class BuildingData
    {
        public LevelStorage.Building Building;
        public ResourceStorageConfig BuildConfig;
    }
    
    public class LevelBuildingSpawner : SerializedMonoBehaviour
    {
        [SerializeField] 
        private BuildingModel _buildingModelPrefab;

        [OdinSerialize]
        private Dictionary<Transform, BuildingData> _buildings = new();

        [Inject] 
        private IObjectResolver _objectResolver;

        [Inject] 
        private ResourceStorageModelService _resourceStorageModelService;
        
        private void Start()
        {
            foreach (var pair in _buildings)
            {
                _buildingModelPrefab.Building = pair.Value.Building;
                var buildingModel = _objectResolver.Instantiate(_buildingModelPrefab, pair.Key);
                buildingModel.ResourceStorage.StorageConfig = pair.Value.BuildConfig;
                _resourceStorageModelService.AddStorage(buildingModel);
            }
        }
    }
}
