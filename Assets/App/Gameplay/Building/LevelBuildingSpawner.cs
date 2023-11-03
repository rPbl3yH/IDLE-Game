using System.Collections.Generic;
using App.Gameplay.LevelStorage;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.Building
{
    public class LevelBuildingSpawner : SerializedMonoBehaviour
    {
        [SerializeField] 
        private BuildingModel _buildingModelPrefab;

        [OdinSerialize]
        private Dictionary<Transform, LevelStorage.Building> _buildings = new();

        [Inject] 
        private IObjectResolver _objectResolver;

        [Inject] 
        private ResourceStorageModelService _resourceStorageModelService;
        
        private void Start()
        {
            foreach (var pair in _buildings)
            {
                _buildingModelPrefab.Building = pair.Value;
                var buildingModel = _objectResolver.Instantiate(_buildingModelPrefab, pair.Key);
                _resourceStorageModelService.AddStorage(buildingModel);
            }
        }
    }
}
