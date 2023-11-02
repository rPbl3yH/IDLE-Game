using System.Collections.Generic;
using App.Gameplay.Building;
using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildMechanics
    {
        private readonly ResourceStorage _resourceStorage;
        private readonly Transform _spawnPoint;
        private readonly IAtomicAction<Building> _built;
        private readonly Building _building;
        private readonly BuildingSpawner _buildingSpawner;
        
        public BuildMechanics(
            ResourceStorage resourceStorage, 
            IAtomicAction<Building> built,
            BuildingSpawner spawner,
            Building building, 
            Transform spawnPoint)
        {
            _buildingSpawner = spawner;
            _resourceStorage = resourceStorage;
            _built = built;
            _building = building;
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
            var building = _buildingSpawner.Spawn(_building, _spawnPoint);
            _built?.Invoke(building);
        }
    }
}