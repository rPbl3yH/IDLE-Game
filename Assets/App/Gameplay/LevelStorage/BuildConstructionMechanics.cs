using System.Collections.Generic;
using App.Gameplay.Building;
using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildConstructionMechanics
    {
        private readonly ResourceStorage _resourceStorage;
        private readonly Transform _spawnPoint;
        private readonly IAtomicAction<BuildingModel> _built;
        private readonly BuildingModel _buildingModel;
        private readonly BuildingSpawner _buildingSpawner;
        
        public BuildConstructionMechanics(
            ResourceStorage resourceStorage, 
            IAtomicAction<BuildingModel> built,
            BuildingSpawner spawner,
            BuildingModel buildingModel, 
            Transform spawnPoint)
        {
            _buildingSpawner = spawner;
            _resourceStorage = resourceStorage;
            _built = built;
            _buildingModel = buildingModel;
            _spawnPoint = spawnPoint;
        }

        public void OnEnable()
        {
            _resourceStorage.ResourcesChanged += OnResourcesChanged;   
        }

        public void OnDisable()
        {
            _resourceStorage.ResourcesChanged -= OnResourcesChanged;
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> value)
        {
            if (_resourceStorage.IsFull())
            {
                Build();
            }
        }

        private void Build()
        {
            var building = _buildingSpawner.Spawn(_buildingModel, _spawnPoint);
            _built?.Invoke(building);
        }
    }
}